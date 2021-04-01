namespace MusicLibrary
{
    partial class MusicLibrary
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusicLibrary));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maintenanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bandMembersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.membersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.membertoolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.bandtoolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.bandmembertoolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.viewmembertoolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.viewbandtoolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.abouttoolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.maintenanceToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(969, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.fileToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Tag = "About";
            this.aboutToolStripMenuItem.Text = "About Us";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // maintenanceToolStripMenuItem
            // 
            this.maintenanceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.memberToolStripMenuItem,
            this.bandToolStripMenuItem,
            this.bandMembersToolStripMenuItem});
            this.maintenanceToolStripMenuItem.Name = "maintenanceToolStripMenuItem";
            this.maintenanceToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.maintenanceToolStripMenuItem.Text = "Maintenance";
            // 
            // memberToolStripMenuItem
            // 
            this.memberToolStripMenuItem.Name = "memberToolStripMenuItem";
            this.memberToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.memberToolStripMenuItem.Tag = "AddMember";
            this.memberToolStripMenuItem.Text = "Member";
            this.memberToolStripMenuItem.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // bandToolStripMenuItem
            // 
            this.bandToolStripMenuItem.Name = "bandToolStripMenuItem";
            this.bandToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bandToolStripMenuItem.Tag = "AddBand";
            this.bandToolStripMenuItem.Text = "Band";
            this.bandToolStripMenuItem.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // bandMembersToolStripMenuItem
            // 
            this.bandMembersToolStripMenuItem.Name = "bandMembersToolStripMenuItem";
            this.bandMembersToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bandMembersToolStripMenuItem.Tag = "ViewBandMember";
            this.bandMembersToolStripMenuItem.Text = "Band Members";
            this.bandMembersToolStripMenuItem.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.membersToolStripMenuItem,
            this.bandsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.viewToolStripMenuItem.Text = "Browse";
            // 
            // membersToolStripMenuItem
            // 
            this.membersToolStripMenuItem.Name = "membersToolStripMenuItem";
            this.membersToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.membersToolStripMenuItem.Tag = "ViewMember";
            this.membersToolStripMenuItem.Text = "Members";
            this.membersToolStripMenuItem.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // bandsToolStripMenuItem
            // 
            this.bandsToolStripMenuItem.Name = "bandsToolStripMenuItem";
            this.bandsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bandsToolStripMenuItem.Tag = "ViewBand";
            this.bandsToolStripMenuItem.Text = "Bands";
            this.bandsToolStripMenuItem.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.Black;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.membertoolStripButton1,
            this.bandtoolStripButton2,
            this.bandmembertoolStripButton3,
            this.toolStripSeparator3,
            this.viewmembertoolStripButton4,
            this.viewbandtoolStripButton5,
            this.toolStripSeparator4,
            this.abouttoolStripButton1});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(969, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // membertoolStripButton1
            // 
            this.membertoolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.membertoolStripButton1.Image = global::MusicLibrary.Properties.Resources.icons8_jelly_band_48;
            this.membertoolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.membertoolStripButton1.Name = "membertoolStripButton1";
            this.membertoolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.membertoolStripButton1.Tag = "AddMember";
            this.membertoolStripButton1.Text = "Add Member";
            this.membertoolStripButton1.Click += new System.EventHandler(this.ShowForm);
            // 
            // bandtoolStripButton2
            // 
            this.bandtoolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bandtoolStripButton2.Image = global::MusicLibrary.Properties.Resources.icons8_music_band_48;
            this.bandtoolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bandtoolStripButton2.Name = "bandtoolStripButton2";
            this.bandtoolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.bandtoolStripButton2.Tag = "AddBand";
            this.bandtoolStripButton2.Text = "Add Band";
            this.bandtoolStripButton2.Click += new System.EventHandler(this.ShowForm);
            // 
            // bandmembertoolStripButton3
            // 
            this.bandmembertoolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bandmembertoolStripButton3.Image = global::MusicLibrary.Properties.Resources.icons8_drum_set_48;
            this.bandmembertoolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bandmembertoolStripButton3.Name = "bandmembertoolStripButton3";
            this.bandmembertoolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.bandmembertoolStripButton3.Tag = "AddBandMember";
            this.bandmembertoolStripButton3.Text = "Add Band Member";
            this.bandmembertoolStripButton3.Click += new System.EventHandler(this.ShowForm);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // viewmembertoolStripButton4
            // 
            this.viewmembertoolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.viewmembertoolStripButton4.Image = global::MusicLibrary.Properties.Resources.icons8_guitarist_48;
            this.viewmembertoolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewmembertoolStripButton4.Name = "viewmembertoolStripButton4";
            this.viewmembertoolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.viewmembertoolStripButton4.Tag = "ViewMember";
            this.viewmembertoolStripButton4.Text = "View Members";
            this.viewmembertoolStripButton4.Click += new System.EventHandler(this.ShowForm);
            // 
            // viewbandtoolStripButton5
            // 
            this.viewbandtoolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.viewbandtoolStripButton5.Image = global::MusicLibrary.Properties.Resources.icons8_rock_music_48;
            this.viewbandtoolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewbandtoolStripButton5.Name = "viewbandtoolStripButton5";
            this.viewbandtoolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.viewbandtoolStripButton5.Tag = "ViewBand";
            this.viewbandtoolStripButton5.Text = "View Bands";
            this.viewbandtoolStripButton5.Click += new System.EventHandler(this.ShowForm);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.Black;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar1});
            this.statusStrip.Location = new System.Drawing.Point(0, 725);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(969, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // abouttoolStripButton1
            // 
            this.abouttoolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.abouttoolStripButton1.Image = global::MusicLibrary.Properties.Resources.icons8_about_48;
            this.abouttoolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.abouttoolStripButton1.Name = "abouttoolStripButton1";
            this.abouttoolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.abouttoolStripButton1.Tag = "About";
            this.abouttoolStripButton1.Text = "About Us";
            this.abouttoolStripButton1.Click += new System.EventHandler(this.ShowForm);
            // 
            // MusicLibrary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(969, 747);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MusicLibrary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MusicLibrary";
            this.Load += new System.EventHandler(this.MusicLibrary_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem maintenanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem membersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bandMembersToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton membertoolStripButton1;
        private System.Windows.Forms.ToolStripButton bandtoolStripButton2;
        private System.Windows.Forms.ToolStripButton bandmembertoolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton viewmembertoolStripButton4;
        private System.Windows.Forms.ToolStripButton viewbandtoolStripButton5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton abouttoolStripButton1;
    }
}



