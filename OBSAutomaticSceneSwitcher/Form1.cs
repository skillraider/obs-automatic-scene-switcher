using AsyncAwaitBestPractices;
using OBSWebsocketDotNet;
using Microsoft.EntityFrameworkCore;
using OBSWebsocketDotNet.Types;

namespace OBSAutomaticSceneSwitcher;

public partial class Form1 : Form
{
    private OBSWebsocket obs;
    private readonly WindowsService _ws;
    private readonly DatabaseContext _dbContext;

    public Form1()
    {
        InitializeComponent();

        _ws = new();
        _dbContext = new();
        _dbContext.Database.EnsureCreated();

        obs = new();
        obs.Connected += Obs_Connected;
        obs.SceneCreated += Obs_SceneCreated;
        obs.SceneRemoved += Obs_SceneRemoved;

        Load += Form1_Load;
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        LoadWindowList();
        LoadMapList();
        WatchWindows();
    }

    private void WatchWindows()
    {
        Task.Run(() =>
        {
            BeginInvoke(async () =>
            {
                while (true)
                {
                    if (obs.IsConnected)
                    {
                        List<WindowToScene> windowToScenes = await _dbContext.WindowToScenes.ToListAsync();
                        string window = _ws.GetCurrentWindow();
                        WindowToScene? windowToScene = windowToScenes.FirstOrDefault(x => window.Contains(x.WindowSearch));
                        if (windowToScene is not null)
                        {
                            if (obs.GetCurrentProgramScene() != windowToScene.SceneName)
                            {
                                obs.SetCurrentProgramScene(windowToScene.SceneName);
                            }
                        }
                    }
                    await Task.Delay(1000);
                }
            });
        }).SafeFireAndForget();
    }

    private void Obs_SceneRemoved(object? sender, OBSWebsocketDotNet.Types.Events.SceneRemovedEventArgs e)
    {
        BeginInvoke(UpdateScenes);
    }

    private void Obs_SceneCreated(object? sender, OBSWebsocketDotNet.Types.Events.SceneCreatedEventArgs e)
    {
        BeginInvoke(UpdateScenes);
    }

    private void Obs_Connected(object? sender, EventArgs e)
    {
        BeginInvoke(UpdateScenes);
    }

    private void LoadWindowList()
    {
        Dictionary<IntPtr, string> windowsDict = _ws.GetWindows();
        comboBox2.DataSource = windowsDict.Values.OrderBy(x => x).ToList();
    }

    private void LoadMapList()
    {
        List<WindowToScene> windowScenes = _dbContext.WindowToScenes.ToList();
        dataGridView1.AutoGenerateColumns = false;
        dataGridView1.Columns.Clear();
        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
        {
            DataPropertyName = "Id",
            HeaderText = "Id",
            Visible = false
        });
        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
        {
            HeaderText = "Window Search",
            DataPropertyName = "WindowSearch",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
        {
            HeaderText = "Scene",
            DataPropertyName = "SceneName",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
        dataGridView1.DataSource = windowScenes;
    }

    private void UpdateScenes()
    {
        if (obs.IsConnected)
        {
            GetSceneListInfo scenes = obs.GetSceneList();
            comboBox1.DataSource = scenes.Scenes;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        BeginInvoke(() =>
        {
            try
            {
                obs.ConnectAsync(textBox1.Text, textBox2.Text);
                comboBox1.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        });
    }

    private void button2_Click(object sender, EventArgs e)
    {
        LoadWindowList();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        SceneBasicInfo? a = (SceneBasicInfo)comboBox1.SelectedItem;
        string? b = comboBox2.SelectedItem?.ToString();
        if (a is not null && b is not null)
        {
            _dbContext.WindowToScenes.Add(new()
            {
                WindowSearch = b,
                SceneName = a.Name
            });
            _dbContext.SaveChanges();
            LoadMapList();
        }
    }

    private void button4_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
        {
            WindowToScene row = (WindowToScene)selectedRow.DataBoundItem;
            _dbContext.WindowToScenes.Remove(row);
        }

        _dbContext.SaveChanges();
        LoadMapList();
    }
}