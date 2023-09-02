using AsyncAwaitBestPractices;
using OBSWebsocketDotNet;
using Microsoft.EntityFrameworkCore;
using OBSWebsocketDotNet.Types;

namespace OBSAutomaticSceneSwitcher;

public partial class Form1 : Form
{
    private readonly DatabaseContext _dbContext;
    private readonly OBSWebsocket obs;
    private readonly WindowsService _ws;

    public Form1()
    {
        InitializeComponent();

        _dbContext = new();
        _dbContext.Database.EnsureCreated();

        obs = new();
        obs.Connected += Obs_Connected;
        obs.SceneCreated += Obs_SceneCreated;
        obs.SceneRemoved += Obs_SceneRemoved;

        _ws = new();

        Load += Form1_Load;
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        ConnectionSettings? connectionSettings = _dbContext.ConnectionSettings.FirstOrDefault();
        if (connectionSettings is not null)
        {
            addressTextBox.Text = connectionSettings.IpPort;
            passwordTextBox.Text = connectionSettings.Password;
        }

        mapTypeComboBox.DataSource = Enum.GetNames(typeof(MapType)).ToList();

        LoadWindowList();
        LoadMapList();
        WatchWindows();
    }

    private void Obs_Connected(object? sender, EventArgs e)
    {
        BeginInvoke(UpdateScenes);
        SaveConnectionSettings();
    }

    private void Obs_SceneCreated(object? sender, OBSWebsocketDotNet.Types.Events.SceneCreatedEventArgs e)
    {
        BeginInvoke(UpdateScenes);
    }

    private void Obs_SceneRemoved(object? sender, OBSWebsocketDotNet.Types.Events.SceneRemovedEventArgs e)
    {
        BeginInvoke(UpdateScenes);
    }

    private void SaveConnectionSettings()
    {
        ConnectionSettings? connectionSettings = _dbContext.ConnectionSettings.FirstOrDefault();
        if (connectionSettings is null)
        {
            connectionSettings = new();
        }

        connectionSettings.IpPort = addressTextBox.Text;
        connectionSettings.Password = passwordTextBox.Text;
        _dbContext.ConnectionSettings.Update(connectionSettings);
        _dbContext.SaveChanges();
    }

    private void LoadWindowList()
    {
        Dictionary<IntPtr, string> windowsDict = _ws.GetWindows();
        windowsComboBox.DataSource = windowsDict.Values.OrderBy(x => x).ToList();
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
        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
        {
            HeaderText = "Map Type",
            DataPropertyName = "MapType",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
        dataGridView1.DataSource = windowScenes;
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
                        WindowToScene? windowToScene = windowToScenes.FirstOrDefault(x => window.Contains(x.WindowSearch, StringComparison.CurrentCultureIgnoreCase));
                        if (windowToScene is not null)
                        {
                            if (windowToScene.MapType == MapType.Scene)
                            {
                                if (obs.GetCurrentProgramScene() != windowToScene.SceneName)
                                {
                                    obs.SetCurrentProgramScene(windowToScene.SceneName);
                                }
                            }
                            else
                            {
                                string currentSceneName = obs.GetCurrentProgramScene();
                                List<SceneItemDetails> sceneItems = obs.GetSceneItemList(currentSceneName);
                                SceneItemDetails? source = sceneItems.FirstOrDefault(x => x.SourceName == windowToScene.SceneName);
                                if (source is not null)
                                {
                                    foreach (SceneItemDetails item in sceneItems)
                                    {
                                        if (item.ItemId == source.ItemId)
                                        {
                                            bool isEnabled = obs.GetSceneItemEnabled(currentSceneName, item.ItemId);
                                            if (!isEnabled)
                                            {
                                                obs.SetSceneItemEnabled(currentSceneName, source.ItemId, true);
                                            }
                                        }
                                        else
                                        {
                                            bool isEnabled = obs.GetSceneItemEnabled(currentSceneName, item.ItemId);
                                            if (isEnabled)
                                            {
                                                obs.SetSceneItemEnabled(currentSceneName, item.ItemId, false);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    await Task.Delay(1000);
                }
            });
        }).SafeFireAndForget();
    }

    private void LoadSources(string sceneName)
    {
        GetSceneListInfo scenes = obs.GetSceneList();
        foreach (var scene in scenes.Scenes)
        {
            if (scene.Name == sceneName)
            {
                List<SceneItemDetails> sceneItems = obs.GetSceneItemList(scene.Name);
                sourcesComboBox.DataSource = sceneItems;
            }
        }
    }

    private void UpdateScenes()
    {
        if (obs.IsConnected)
        {
            GetSceneListInfo scenes = obs.GetSceneList();
            scenesComboBox.DataSource = scenes.Scenes;
        }
    }

    private void connectButton_Click(object sender, EventArgs e)
    {
        BeginInvoke(() =>
        {
            try
            {
                obs.ConnectAsync(addressTextBox.Text, passwordTextBox.Text);
                scenesComboBox.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        });
    }

    private void reloadWindowsButton_Click(object sender, EventArgs e)
    {
        LoadWindowList();
    }

    private void saveMapButton_Click(object sender, EventArgs e)
    {
        string windowSearch;
        if (freeFormCheckBox.Checked)
        {
            windowSearch = freeFormTextBox.Text;
        }
        else
        {
            windowSearch = windowsComboBox.SelectedItem?.ToString() ?? "";
        }

        MapType mapType = (MapType)Enum.Parse(typeof(MapType), mapTypeComboBox.SelectedItem.ToString());

        string sceneOrSourceName;
        if (mapType == MapType.Scene)
        {
            SceneBasicInfo sceneBasicInfo = (SceneBasicInfo)scenesComboBox.SelectedItem;
            sceneOrSourceName = sceneBasicInfo.Name;
        }
        else
        {
            SceneItemDetails sceneItemDetails = (SceneItemDetails)sourcesComboBox.SelectedItem;
            sceneOrSourceName = sceneItemDetails.SourceName;
        }

        if (!string.IsNullOrEmpty(windowSearch) && !string.IsNullOrEmpty(sceneOrSourceName))
        {
            _dbContext.WindowToScenes.Add(new()
            {
                WindowSearch = windowSearch,
                MapType = mapType,
                SceneName = sceneOrSourceName
            });
            _dbContext.SaveChanges();
            LoadMapList();
        }
    }

    private void deleteMapButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
        {
            WindowToScene row = (WindowToScene)selectedRow.DataBoundItem;
            _dbContext.WindowToScenes.Remove(row);
        }

        _dbContext.SaveChanges();
        LoadMapList();
    }

    private void scenesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        SceneBasicInfo scene = (SceneBasicInfo)scenesComboBox.SelectedItem;
        BeginInvoke(() => LoadSources(scene.Name));
    }

    private void mapTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        MapType mapType = (MapType)Enum.Parse(typeof(MapType), mapTypeComboBox.SelectedItem.ToString());
        if (mapType == MapType.Scene)
        {
            sourcesComboBox.Enabled = false;
            scenesComboBox.SelectedIndexChanged -= scenesComboBox_SelectedIndexChanged;
        }
        else
        {
            sourcesComboBox.Enabled = true;
            scenesComboBox.SelectedIndexChanged += scenesComboBox_SelectedIndexChanged;
        }
    }

    private void freeFormCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (freeFormCheckBox.Checked)
        {
            windowsComboBox.Visible = false;
            windowsComboBox.Enabled = false;
            freeFormTextBox.Visible = true;
            freeFormTextBox.Enabled = true;
        }
        else
        {
            windowsComboBox.Visible = true;
            windowsComboBox.Enabled = true;
            freeFormTextBox.Visible = false;
            freeFormTextBox.Enabled = false;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        SceneBasicInfo scene = (SceneBasicInfo)scenesComboBox.SelectedItem;
        BeginInvoke(() => LoadSources(scene.Name));
        sourcesComboBox.Enabled = true;

    }
}