namespace OBSAutomaticSceneSwitcher
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            connectButton = new Button();
            addressTextBox = new TextBox();
            passwordTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            reloadWindowsButton = new Button();
            saveMapButton = new Button();
            scenesComboBox = new ComboBox();
            windowsComboBox = new ComboBox();
            deleteMapButton = new Button();
            dataGridView1 = new DataGridView();
            sourcesComboBox = new ComboBox();
            mapTypeComboBox = new ComboBox();
            label5 = new Label();
            label6 = new Label();
            freeFormTextBox = new TextBox();
            freeFormCheckBox = new CheckBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // connectButton
            // 
            connectButton.Location = new Point(430, 30);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(140, 23);
            connectButton.TabIndex = 0;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // addressTextBox
            // 
            addressTextBox.Location = new Point(10, 30);
            addressTextBox.Name = "addressTextBox";
            addressTextBox.Size = new Size(200, 23);
            addressTextBox.TabIndex = 1;
            addressTextBox.Text = "ws://192.168.1.113:4455";
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(220, 30);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(200, 23);
            passwordTextBox.TabIndex = 2;
            passwordTextBox.Text = "jQ1TnGVeBVWdo8ku";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 10);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 3;
            label1.Text = "IP:Port";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(220, 10);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 4;
            label2.Text = "Password";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 188);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 6;
            label3.Text = "Scenes";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 60);
            label4.Name = "label4";
            label4.Size = new Size(56, 15);
            label4.TabIndex = 8;
            label4.Text = "Windows";
            // 
            // reloadWindowsButton
            // 
            reloadWindowsButton.Location = new Point(10, 109);
            reloadWindowsButton.Name = "reloadWindowsButton";
            reloadWindowsButton.Size = new Size(125, 23);
            reloadWindowsButton.TabIndex = 9;
            reloadWindowsButton.Text = "Reload Window List";
            reloadWindowsButton.UseVisualStyleBackColor = true;
            reloadWindowsButton.Click += reloadWindowsButton_Click;
            // 
            // saveMapButton
            // 
            saveMapButton.Location = new Point(314, 252);
            saveMapButton.Name = "saveMapButton";
            saveMapButton.Size = new Size(125, 23);
            saveMapButton.TabIndex = 11;
            saveMapButton.Text = "Save Map";
            saveMapButton.UseVisualStyleBackColor = true;
            saveMapButton.Click += saveMapButton_Click;
            // 
            // scenesComboBox
            // 
            scenesComboBox.DisplayMember = "Name";
            scenesComboBox.Enabled = false;
            scenesComboBox.FormattingEnabled = true;
            scenesComboBox.Location = new Point(10, 208);
            scenesComboBox.Name = "scenesComboBox";
            scenesComboBox.Size = new Size(200, 23);
            scenesComboBox.TabIndex = 12;
            scenesComboBox.ValueMember = "Name";
            // 
            // windowsComboBox
            // 
            windowsComboBox.DisplayMember = "Value";
            windowsComboBox.FormattingEnabled = true;
            windowsComboBox.Location = new Point(10, 80);
            windowsComboBox.Name = "windowsComboBox";
            windowsComboBox.Size = new Size(200, 23);
            windowsComboBox.TabIndex = 13;
            windowsComboBox.ValueMember = "Key";
            // 
            // deleteMapButton
            // 
            deleteMapButton.Location = new Point(445, 252);
            deleteMapButton.Name = "deleteMapButton";
            deleteMapButton.Size = new Size(125, 23);
            deleteMapButton.TabIndex = 14;
            deleteMapButton.Text = "Delete Map";
            deleteMapButton.UseVisualStyleBackColor = true;
            deleteMapButton.Click += deleteMapButton_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(10, 281);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(560, 382);
            dataGridView1.TabIndex = 15;
            // 
            // sourcesComboBox
            // 
            sourcesComboBox.DisplayMember = "SourceName";
            sourcesComboBox.Enabled = false;
            sourcesComboBox.FormattingEnabled = true;
            sourcesComboBox.Location = new Point(10, 252);
            sourcesComboBox.Name = "sourcesComboBox";
            sourcesComboBox.Size = new Size(200, 23);
            sourcesComboBox.TabIndex = 16;
            sourcesComboBox.ValueMember = "ItemId";
            // 
            // mapTypeComboBox
            // 
            mapTypeComboBox.FormattingEnabled = true;
            mapTypeComboBox.Location = new Point(10, 156);
            mapTypeComboBox.Name = "mapTypeComboBox";
            mapTypeComboBox.Size = new Size(200, 23);
            mapTypeComboBox.TabIndex = 17;
            mapTypeComboBox.SelectedIndexChanged += mapTypeComboBox_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 136);
            label5.Name = "label5";
            label5.Size = new Size(58, 15);
            label5.TabIndex = 18;
            label5.Text = "Map Type";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 234);
            label6.Name = "label6";
            label6.Size = new Size(48, 15);
            label6.TabIndex = 19;
            label6.Text = "Sources";
            // 
            // freeFormTextBox
            // 
            freeFormTextBox.Enabled = false;
            freeFormTextBox.Location = new Point(10, 80);
            freeFormTextBox.Name = "freeFormTextBox";
            freeFormTextBox.Size = new Size(200, 23);
            freeFormTextBox.TabIndex = 20;
            freeFormTextBox.Visible = false;
            // 
            // freeFormCheckBox
            // 
            freeFormCheckBox.AutoSize = true;
            freeFormCheckBox.Location = new Point(216, 82);
            freeFormCheckBox.Name = "freeFormCheckBox";
            freeFormCheckBox.Size = new Size(77, 19);
            freeFormCheckBox.TabIndex = 21;
            freeFormCheckBox.Text = "Free form";
            freeFormCheckBox.UseVisualStyleBackColor = true;
            freeFormCheckBox.CheckedChanged += freeFormCheckBox_CheckedChanged;
            // 
            // button1
            // 
            button1.Location = new Point(220, 208);
            button1.Name = "button1";
            button1.Size = new Size(125, 23);
            button1.TabIndex = 22;
            button1.Text = "Reload Scene List";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 680);
            Controls.Add(button1);
            Controls.Add(freeFormCheckBox);
            Controls.Add(freeFormTextBox);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(mapTypeComboBox);
            Controls.Add(sourcesComboBox);
            Controls.Add(dataGridView1);
            Controls.Add(deleteMapButton);
            Controls.Add(windowsComboBox);
            Controls.Add(scenesComboBox);
            Controls.Add(saveMapButton);
            Controls.Add(reloadWindowsButton);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(passwordTextBox);
            Controls.Add(addressTextBox);
            Controls.Add(connectButton);
            Name = "Form1";
            Text = "OBS Automatic Scene/Source Switcher";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button connectButton;
        private TextBox addressTextBox;
        private TextBox passwordTextBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button reloadWindowsButton;
        private Button saveMapButton;
        private ComboBox scenesComboBox;
        private ComboBox windowsComboBox;
        private Button deleteMapButton;
        private DataGridView dataGridView1;
        private ComboBox sourcesComboBox;
        private ComboBox mapTypeComboBox;
        private Label label5;
        private Label label6;
        private TextBox freeFormTextBox;
        private CheckBox freeFormCheckBox;
        private Button button1;
    }
}