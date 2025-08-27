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
        tboConsole = new TextBox();
        btnPause = new Button();
        gboControls.SuspendLayout();
        SuspendLayout();
        // 
        // btnStart
        // 
        btnStart.Location = new Point(21, 38);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(94, 29);
        btnStart.TabIndex = 0;
        btnStart.Text = "Start";
        btnStart.UseVisualStyleBackColor = true;
        btnStart.Click += btnStart_Click;
        // 
        // btnStop
        // 
        btnStop.Enabled = false;
        btnStop.Location = new Point(371, 38);
        btnStop.Name = "btnStop";
        btnStop.Size = new Size(94, 29);
        btnStop.TabIndex = 1;
        btnStop.Text = "Stop Listening";
        btnStop.UseVisualStyleBackColor = true;
        btnStop.Click += btnStop_Click;
        // 
        // gboControls
        // 
        gboControls.Controls.Add(tboConsole);
        gboControls.Controls.Add(btnPause);
        gboControls.Controls.Add(btnStart);
        gboControls.Controls.Add(btnStop);
        gboControls.Location = new Point(23, 23);
        gboControls.Name = "gboControls";
        gboControls.Size = new Size(488, 446);
        gboControls.TabIndex = 2;
        gboControls.TabStop = false;
        gboControls.Text = "Controls";
        // 
        // tboConsole
        // 
        tboConsole.Location = new Point(21, 110);
        tboConsole.Multiline = true;
        tboConsole.Name = "tboConsole";
        tboConsole.Size = new Size(444, 314);
        tboConsole.TabIndex = 3;
        // 
        // btnPause
        // 
        btnPause.Enabled = false;
        btnPause.Location = new Point(190, 38);
        btnPause.Name = "btnPause";
        btnPause.Size = new Size(94, 29);
        btnPause.TabIndex = 2;
        btnPause.Text = "Pause";
        btnPause.UseVisualStyleBackColor = true;
        btnPause.Click += btnPause_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(960, 746);
        Controls.Add(gboControls);
        Name = "MainForm";
        Text = "TwitchPlays";
        gboControls.ResumeLayout(false);
        gboControls.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Button btnStart;
    private Button btnStop;
    private GroupBox gboControls;
    private Button btnPause;
    private TextBox tboConsole;
}
