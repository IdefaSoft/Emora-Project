namespace Emora
{
    partial class EmoraForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmoraForm));
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.pnlUsername = new System.Windows.Forms.Panel();
            this.imgWebsiteIcons = new System.Windows.Forms.ImageList(this.components);
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblThreads = new System.Windows.Forms.Label();
            this.nudThreads = new System.Windows.Forms.NumericUpDown();
            this.tlpResults = new System.Windows.Forms.TableLayoutPanel();
            this.pnlResults = new System.Windows.Forms.Panel();
            this.pnlUsername.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreads)).BeginInit();
            this.pnlResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(230)))), ((int)(((byte)(154)))));
            this.btnSearch.Location = new System.Drawing.Point(517, -2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(65, 66);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "🔎";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.ForeColor = System.Drawing.SystemColors.Window;
            this.txtUsername.Location = new System.Drawing.Point(12, 14);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(506, 32);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Text = "Enter username here...";
            // 
            // pnlUsername
            // 
            this.pnlUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.pnlUsername.Controls.Add(this.txtUsername);
            this.pnlUsername.Location = new System.Drawing.Point(0, -2);
            this.pnlUsername.Name = "pnlUsername";
            this.pnlUsername.Size = new System.Drawing.Size(518, 66);
            this.pnlUsername.TabIndex = 2;
            // 
            // imgWebsiteIcons
            // 
            this.imgWebsiteIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgWebsiteIcons.ImageStream")));
            this.imgWebsiteIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgWebsiteIcons.Images.SetKeyName(0, "Imgur");
            this.imgWebsiteIcons.Images.SetKeyName(1, "About.me");
            this.imgWebsiteIcons.Images.SetKeyName(2, "DailyMotion");
            this.imgWebsiteIcons.Images.SetKeyName(3, "Chess");
            this.imgWebsiteIcons.Images.SetKeyName(4, "Docker Hub");
            this.imgWebsiteIcons.Images.SetKeyName(5, "Duolingo");
            this.imgWebsiteIcons.Images.SetKeyName(6, "Fiverr");
            this.imgWebsiteIcons.Images.SetKeyName(7, "Flickr");
            this.imgWebsiteIcons.Images.SetKeyName(8, "Genius (Artists)");
            this.imgWebsiteIcons.Images.SetKeyName(9, "Genius (Users)");
            this.imgWebsiteIcons.Images.SetKeyName(10, "Giphy");
            this.imgWebsiteIcons.Images.SetKeyName(11, "GitHub");
            this.imgWebsiteIcons.Images.SetKeyName(12, "Minecraft");
            this.imgWebsiteIcons.Images.SetKeyName(13, "npm");
            this.imgWebsiteIcons.Images.SetKeyName(14, "Pastebin");
            this.imgWebsiteIcons.Images.SetKeyName(15, "Patreon");
            this.imgWebsiteIcons.Images.SetKeyName(16, "PyPi");
            this.imgWebsiteIcons.Images.SetKeyName(17, "Reddit");
            this.imgWebsiteIcons.Images.SetKeyName(18, "RootMe");
            this.imgWebsiteIcons.Images.SetKeyName(19, "Scribd");
            this.imgWebsiteIcons.Images.SetKeyName(20, "Snapchat");
            this.imgWebsiteIcons.Images.SetKeyName(21, "SoundCloud");
            this.imgWebsiteIcons.Images.SetKeyName(22, "Spotify");
            this.imgWebsiteIcons.Images.SetKeyName(23, "Steam");
            this.imgWebsiteIcons.Images.SetKeyName(24, "Tenor");
            this.imgWebsiteIcons.Images.SetKeyName(25, "Vimeo");
            this.imgWebsiteIcons.Images.SetKeyName(26, "Replit");
            this.imgWebsiteIcons.Images.SetKeyName(27, "Roblox");
            this.imgWebsiteIcons.Images.SetKeyName(28, "Telegram");
            this.imgWebsiteIcons.Images.SetKeyName(29, "Wattpad");
            this.imgWebsiteIcons.Images.SetKeyName(30, "GeeksforGeeks");
            this.imgWebsiteIcons.Images.SetKeyName(31, "SourceForge");
            this.imgWebsiteIcons.Images.SetKeyName(32, "TryHackMe");
            this.imgWebsiteIcons.Images.SetKeyName(33, "Wikipedia");
            this.imgWebsiteIcons.Images.SetKeyName(34, "Medium");
            this.imgWebsiteIcons.Images.SetKeyName(35, "Instagram");
            this.imgWebsiteIcons.Images.SetKeyName(36, "AllMyLinks");
            this.imgWebsiteIcons.Images.SetKeyName(37, "Buy Me a Coffee");
            this.imgWebsiteIcons.Images.SetKeyName(38, "BuzzFeed");
            this.imgWebsiteIcons.Images.SetKeyName(39, "Cash App");
            this.imgWebsiteIcons.Images.SetKeyName(40, "Ebay");
            this.imgWebsiteIcons.Images.SetKeyName(41, "JsFiddle");
            this.imgWebsiteIcons.Images.SetKeyName(42, "Linktree");
            this.imgWebsiteIcons.Images.SetKeyName(43, "Pinterest");
            this.imgWebsiteIcons.Images.SetKeyName(44, "Rapid API");
            this.imgWebsiteIcons.Images.SetKeyName(45, "TradingView");
            this.imgWebsiteIcons.Images.SetKeyName(46, "Facebook");
            this.imgWebsiteIcons.Images.SetKeyName(47, "Goodreads");
            this.imgWebsiteIcons.Images.SetKeyName(48, "Ko-fi");
            this.imgWebsiteIcons.Images.SetKeyName(49, "Trello");
            this.imgWebsiteIcons.Images.SetKeyName(50, "Bandcamp");
            this.imgWebsiteIcons.Images.SetKeyName(51, "Bluesky");
            this.imgWebsiteIcons.Images.SetKeyName(52, "Codeberg");
            this.imgWebsiteIcons.Images.SetKeyName(53, "Codepen");
            this.imgWebsiteIcons.Images.SetKeyName(54, "Discogs");
            this.imgWebsiteIcons.Images.SetKeyName(55, "Freesound");
            this.imgWebsiteIcons.Images.SetKeyName(56, "Gitee");
            this.imgWebsiteIcons.Images.SetKeyName(57, "GitLab");
            this.imgWebsiteIcons.Images.SetKeyName(58, "Hackaday");
            this.imgWebsiteIcons.Images.SetKeyName(59, "HudsonRock");
            this.imgWebsiteIcons.Images.SetKeyName(60, "JEUXVIDEO");
            this.imgWebsiteIcons.Images.SetKeyName(61, "Kongregate");
            this.imgWebsiteIcons.Images.SetKeyName(62, "Lichess");
            this.imgWebsiteIcons.Images.SetKeyName(63, "MyAnimeList");
            this.imgWebsiteIcons.Images.SetKeyName(64, "Scratch");
            this.imgWebsiteIcons.Images.SetKeyName(65, "Threads");
            this.imgWebsiteIcons.Images.SetKeyName(66, "VirusTotal");
            this.imgWebsiteIcons.Images.SetKeyName(67, "Behance");
            this.imgWebsiteIcons.Images.SetKeyName(68, "CodeChef");
            this.imgWebsiteIcons.Images.SetKeyName(69, "DeviantArt");
            this.imgWebsiteIcons.Images.SetKeyName(70, "Dribbble");
            this.imgWebsiteIcons.Images.SetKeyName(71, "Etsy");
            this.imgWebsiteIcons.Images.SetKeyName(72, "Hack The Box");
            this.imgWebsiteIcons.Images.SetKeyName(73, "Hacker News");
            this.imgWebsiteIcons.Images.SetKeyName(74, "Hugging Face");
            this.imgWebsiteIcons.Images.SetKeyName(75, "itch.io");
            this.imgWebsiteIcons.Images.SetKeyName(76, "Kickstarter");
            this.imgWebsiteIcons.Images.SetKeyName(77, "Kik");
            this.imgWebsiteIcons.Images.SetKeyName(78, "Last.fm");
            this.imgWebsiteIcons.Images.SetKeyName(79, "LeetCode");
            this.imgWebsiteIcons.Images.SetKeyName(80, "LinkedIn");
            this.imgWebsiteIcons.Images.SetKeyName(81, "Myspace");
            this.imgWebsiteIcons.Images.SetKeyName(82, "OpenStreetMap");
            this.imgWebsiteIcons.Images.SetKeyName(83, "PayPal");
            this.imgWebsiteIcons.Images.SetKeyName(84, "Periscope");
            this.imgWebsiteIcons.Images.SetKeyName(85, "Product Hunt");
            this.imgWebsiteIcons.Images.SetKeyName(86, "Speedrun");
            this.imgWebsiteIcons.Images.SetKeyName(87, "Tellonym");
            this.imgWebsiteIcons.Images.SetKeyName(88, "ThemeForest");
            this.imgWebsiteIcons.Images.SetKeyName(89, "TikTok");
            this.imgWebsiteIcons.Images.SetKeyName(90, "Tumblr");
            this.imgWebsiteIcons.Images.SetKeyName(91, "Twitch");
            this.imgWebsiteIcons.Images.SetKeyName(92, "Venmo");
            this.imgWebsiteIcons.Images.SetKeyName(93, "VK");
            this.imgWebsiteIcons.Images.SetKeyName(94, "X");
            this.imgWebsiteIcons.Images.SetKeyName(95, "Gitea");
            this.imgWebsiteIcons.Images.SetKeyName(96, "Tinder");
            this.imgWebsiteIcons.Images.SetKeyName(97, "AlternativeTo");
            this.imgWebsiteIcons.Images.SetKeyName(98, "Archive of Our Own");
            this.imgWebsiteIcons.Images.SetKeyName(99, "ArtStation");
            this.imgWebsiteIcons.Images.SetKeyName(100, "Audiomack");
            this.imgWebsiteIcons.Images.SetKeyName(101, "Codewars");
            this.imgWebsiteIcons.Images.SetKeyName(102, "CodinGame");
            this.imgWebsiteIcons.Images.SetKeyName(103, "CurseForge");
            this.imgWebsiteIcons.Images.SetKeyName(104, "Douban");
            this.imgWebsiteIcons.Images.SetKeyName(105, "Gaia Online");
            this.imgWebsiteIcons.Images.SetKeyName(106, "HackerRank");
            this.imgWebsiteIcons.Images.SetKeyName(107, "HubPages");
            this.imgWebsiteIcons.Images.SetKeyName(108, "Instructables");
            this.imgWebsiteIcons.Images.SetKeyName(109, "Letterboxd");
            this.imgWebsiteIcons.Images.SetKeyName(110, "Mixcloud");
            this.imgWebsiteIcons.Images.SetKeyName(111, "ModDB");
            this.imgWebsiteIcons.Images.SetKeyName(112, "Newgrounds");
            this.imgWebsiteIcons.Images.SetKeyName(113, "Odnoklassniki");
            this.imgWebsiteIcons.Images.SetKeyName(114, "Pixelfed");
            this.imgWebsiteIcons.Images.SetKeyName(115, "Plurk");
            this.imgWebsiteIcons.Images.SetKeyName(116, "ReverbNation");
            this.imgWebsiteIcons.Images.SetKeyName(117, "Smule");
            this.imgWebsiteIcons.Images.SetKeyName(118, "Squarespace");
            this.imgWebsiteIcons.Images.SetKeyName(119, "Topcoder");
            this.imgWebsiteIcons.Images.SetKeyName(120, "Truth Social");
            this.imgWebsiteIcons.Images.SetKeyName(121, "Untappd");
            this.imgWebsiteIcons.Images.SetKeyName(122, "VSCO");
            this.imgWebsiteIcons.Images.SetKeyName(123, "Zomato");
            this.imgWebsiteIcons.Images.SetKeyName(124, "Weebly");
            this.imgWebsiteIcons.Images.SetKeyName(125, "Discord");
            this.imgWebsiteIcons.Images.SetKeyName(126, "Komi");
            this.imgWebsiteIcons.Images.SetKeyName(127, "500px");
            this.imgWebsiteIcons.Images.SetKeyName(128, "Fandom");
            this.imgWebsiteIcons.Images.SetKeyName(129, "Naver");
            this.imgWebsiteIcons.Images.SetKeyName(130, "XING");
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(6, 78);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(323, 21);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Click on the 🔎 button to start the search";
            // 
            // lblThreads
            // 
            this.lblThreads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblThreads.AutoSize = true;
            this.lblThreads.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThreads.Location = new System.Drawing.Point(435, 78);
            this.lblThreads.Name = "lblThreads";
            this.lblThreads.Size = new System.Drawing.Size(74, 21);
            this.lblThreads.TabIndex = 7;
            this.lblThreads.Text = "Threads:";
            // 
            // nudThreads
            // 
            this.nudThreads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudThreads.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.nudThreads.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nudThreads.ForeColor = System.Drawing.SystemColors.Window;
            this.nudThreads.Location = new System.Drawing.Point(528, 81);
            this.nudThreads.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudThreads.Name = "nudThreads";
            this.nudThreads.Size = new System.Drawing.Size(42, 19);
            this.nudThreads.TabIndex = 8;
            this.nudThreads.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // tlpResults
            // 
            this.tlpResults.AutoSize = true;
            this.tlpResults.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpResults.ColumnCount = 1;
            this.tlpResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpResults.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpResults.Location = new System.Drawing.Point(0, 0);
            this.tlpResults.Name = "tlpResults";
            this.tlpResults.RowCount = 1;
            this.tlpResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpResults.Size = new System.Drawing.Size(582, 0);
            this.tlpResults.TabIndex = 9;
            // 
            // pnlResults
            // 
            this.pnlResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlResults.AutoScroll = true;
            this.pnlResults.Controls.Add(this.tlpResults);
            this.pnlResults.Location = new System.Drawing.Point(0, 106);
            this.pnlResults.Name = "pnlResults";
            this.pnlResults.Size = new System.Drawing.Size(582, 477);
            this.pnlResults.TabIndex = 0;
            // 
            // EmoraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(582, 584);
            this.Controls.Add(this.pnlResults);
            this.Controls.Add(this.nudThreads);
            this.Controls.Add(this.lblThreads);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pnlUsername);
            this.Controls.Add(this.btnSearch);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "EmoraForm";
            this.Text = "Emora";
            this.pnlUsername.ResumeLayout(false);
            this.pnlUsername.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreads)).EndInit();
            this.pnlResults.ResumeLayout(false);
            this.pnlResults.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Panel pnlUsername;
        private System.Windows.Forms.ImageList imgWebsiteIcons;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblThreads;
        private System.Windows.Forms.NumericUpDown nudThreads;
        private System.Windows.Forms.TableLayoutPanel tlpResults;
        private System.Windows.Forms.Panel pnlResults;
    }
}

