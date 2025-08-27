namespace TwitchPlaysController;

partial class MainForm
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
        btnStart = new Button();
        btnStop = new Button();
        gboControls = new GroupBox();
        lblPresetSelect = new Label();
        cboKeyPreset = new ComboBox();
        tboConsole = new TextBox();
        btnPause = new Button();
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        settingsToolStripMenuItem = new ToolStripMenuItem();
        exitToolStripMenuItem = new ToolStripMenuItem();
        viewToolStripMenuItem = new ToolStripMenuItem();
        keybindPresetManagerToolStripMenuItem = new ToolStripMenuItem();
        gboControls.SuspendLayout();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // btnStart
        // 
        btnStart.Location = new Point(18, 28);
        btnStart.Margin = new Padding(3, 2, 3, 2);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(82, 22);
        btnStart.TabIndex = 0;
        btnStart.Text = "Start";
        btnStart.UseVisualStyleBackColor = true;
        btnStart.Click += btnStart_Click;
        // 
        // btnStop
        // 
        btnStop.Enabled = false;
        btnStop.Location = new Point(325, 28);
        btnStop.Margin = new Padding(3, 2, 3, 2);
        btnStop.Name = "btnStop";
        btnStop.Size = new Size(82, 22);
        btnStop.TabIndex = 1;
        btnStop.Text = "Stop Listening";
        btnStop.UseVisualStyleBackColor = true;
        btnStop.Click += btnStop_Click;
        // 
        // gboControls
        // 
        gboControls.Controls.Add(lblPresetSelect);
        gboControls.Controls.Add(cboKeyPreset);
        gboControls.Controls.Add(tboConsole);
        gboControls.Controls.Add(btnPause);
        gboControls.Controls.Add(btnStart);
        gboControls.Controls.Add(btnStop);
        gboControls.Location = new Point(12, 38);
        gboControls.Margin = new Padding(3, 2, 3, 2);
        gboControls.Name = "gboControls";
        gboControls.Padding = new Padding(3, 2, 3, 2);
        gboControls.Size = new Size(427, 401);
        gboControls.TabIndex = 2;
        gboControls.TabStop = false;
        gboControls.Text = "Controls";
        // 
        // lblPresetSelect
        // 
        lblPresetSelect.AutoSize = true;
        lblPresetSelect.Location = new Point(286, 344);
        lblPresetSelect.Name = "lblPresetSelect";
        lblPresetSelect.Size = new Size(75, 15);
        lblPresetSelect.TabIndex = 5;
        lblPresetSelect.Text = "Active Preset";
        // 
        // cboKeyPreset
        // 
        cboKeyPreset.FormattingEnabled = true;
        cboKeyPreset.Location = new Point(286, 362);
        cboKeyPreset.Name = "cboKeyPreset";
        cboKeyPreset.Size = new Size(121, 23);
        cboKeyPreset.TabIndex = 4;
        cboKeyPreset.SelectedIndexChanged += cboKeyPreset_SelectedIndexChanged;
        // 
        // tboConsole
        // 
        tboConsole.Location = new Point(18, 63);
        tboConsole.Margin = new Padding(3, 2, 3, 2);
        tboConsole.Multiline = true;
        tboConsole.Name = "tboConsole";
        tboConsole.Size = new Size(389, 236);
        tboConsole.TabIndex = 3;
        // 
        // btnPause
        // 
        btnPause.Enabled = false;
        btnPause.Location = new Point(166, 28);
        btnPause.Margin = new Padding(3, 2, 3, 2);
        btnPause.Name = "btnPause";
        btnPause.Size = new Size(82, 22);
        btnPause.TabIndex = 2;
        btnPause.Text = "Pause";
        btnPause.UseVisualStyleBackColor = true;
        btnPause.Click += btnPause_Click;
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(849, 24);
        menuStrip1.TabIndex = 3;
        menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, exitToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(37, 20);
        fileToolStripMenuItem.Text = "File";
        // 
        // settingsToolStripMenuItem
        // 
        settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
        settingsToolStripMenuItem.Size = new Size(116, 22);
        settingsToolStripMenuItem.Text = "Settings";
        settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
        // 
        // exitToolStripMenuItem
        // 
        exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        exitToolStripMenuItem.Size = new Size(116, 22);
        exitToolStripMenuItem.Text = "Exit";
        exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
        // 
        // viewToolStripMenuItem
        // 
        viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { keybindPresetManagerToolStripMenuItem });
        viewToolStripMenuItem.Name = "viewToolStripMenuItem";
        viewToolStripMenuItem.Size = new Size(44, 20);
        viewToolStripMenuItem.Text = "View";
        // 
        // keybindPresetManagerToolStripMenuItem
        // 
        keybindPresetManagerToolStripMenuItem.Name = "keybindPresetManagerToolStripMenuItem";
        keybindPresetManagerToolStripMenuItem.Size = new Size(202, 22);
        keybindPresetManagerToolStripMenuItem.Text = "Keybind Preset Manager";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(849, 527);
        Controls.Add(gboControls);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        Margin = new Padding(3, 2, 3, 2);
        Name = "MainForm";
        Text = "TwitchPlays";
        gboControls.ResumeLayout(false);
        gboControls.PerformLayout();
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button btnStart;
    private Button btnStop;
    private GroupBox gboControls;
    private Button btnPause;
    private TextBox tboConsole;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem settingsToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem viewToolStripMenuItem;
    private ToolStripMenuItem keybindPresetManagerToolStripMenuItem;
    private Label lblPresetSelect;
    private ComboBox cboKeyPreset;
}
