using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emora
{
    public partial class Form1 : Form
    {
        private class WebsiteInfo
        {
            public string ErrorType { get; set; }
            public string ErrorMessage { get; set; }
            public string Url { get; set; }
        }

        private readonly Dictionary<string, WebsiteInfo> websites = new Dictionary<string, WebsiteInfo>
        {
            {"About.me", new WebsiteInfo {ErrorType = "status_code", Url = "https://about.me/{}"}},
            {"Chess", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.chess.com/member/{}"}},
            {"DailyMotion", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.dailymotion.com/{}"}},
            {"Docker Hub", new WebsiteInfo {ErrorType = "status_code", Url = "https://hub.docker.com/u/{}"}},
            {"Duolingo", new WebsiteInfo {ErrorType = "message", ErrorMessage = "Duolingo - Learn a language for free @duolingo", Url = "https://www.duolingo.com/profile/{}"}},
            {"Fiverr", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.fiverr.com/{}"}},
            {"Flickr", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.flickr.com/people/{}"}},
            {"GeeksforGeeks", new WebsiteInfo {ErrorType = "message", ErrorMessage = "Login GeeksforGeeks", Url = "https://auth.geeksforgeeks.org/user/{}"}},
            {"Genius (Artists)", new WebsiteInfo {ErrorType = "status_code", Url = "https://genius.com/artists/{}"}},
            {"Genius (Users)", new WebsiteInfo {ErrorType = "status_code", Url = "https://genius.com/{}"}},
            {"Giphy", new WebsiteInfo {ErrorType = "status_code", Url = "https://giphy.com/{}"}},
            {"GitHub", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.github.com/{}"}},
            {"Imgur", new WebsiteInfo {ErrorType = "status_code", Url = "https://api.imgur.com/account/v1/accounts/{}?client_id=546c25a59c58ad7"}},
            {"Minecraft", new WebsiteInfo {ErrorType = "status_code", Url = "https://api.mojang.com/users/profiles/minecraft/{}"}},
            {"npm", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.npmjs.com/~{}"}},
            {"Pastebin", new WebsiteInfo {ErrorType = "status_code", Url = "https://pastebin.com/u/{}"}},
            {"Patreon", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.patreon.com/{}"}},
            {"PyPi", new WebsiteInfo {ErrorType = "status_code", Url = "https://pypi.org/user/{}"}},
            {"Reddit", new WebsiteInfo {ErrorType = "message", ErrorMessage = "\"error\": 404)", Url = "https://www.reddit.com/user/{}/about.json"}},
            {"Replit", new WebsiteInfo {ErrorType = "status_code", Url = "https://replit.com/@{}"}},
            {"Roblox", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.roblox.com/user.aspx?username={}"}},
            {"RootMe", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.root-me.org/{}"}},
            {"Scribd", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.scribd.com/{}"}},
            {"Snapchat", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.snapchat.com/add/{}"}},
            {"SoundCloud", new WebsiteInfo {ErrorType = "status_code", Url = "https://soundcloud.com/{}"}},
            {"SourceForge", new WebsiteInfo {ErrorType = "status_code", Url = "https://sourceforge.net/u/{}"}},
            {"Spotify", new WebsiteInfo {ErrorType = "status_code", Url = "https://open.spotify.com/user/{}"}},
            {"Steam", new WebsiteInfo {ErrorType = "message", ErrorMessage = "Steam Community :: Error", Url = "https://steamcommunity.com/id/{}"}},
            {"Telegram", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<meta name=\"robots\" content=\"noindex, nofollow\">", Url = "https://t.me/{}"}},
            {"Tenor", new WebsiteInfo {ErrorType = "status_code", Url = "https://tenor.com/users/{}"}},
            {"TryHackMe", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title>TryHackMe</title>", Url = "https://tryhackme.com/p/{}"}},
            {"Vimeo", new WebsiteInfo {ErrorType = "status_code", Url = "https://vimeo.com/{}"}},
            {"Wattpad", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.wattpad.com/user/{}"}},
            {"Wikipedia", new WebsiteInfo {ErrorType = "message", ErrorMessage = "(centralauth-admin-nonexistent:", Url = "https://en.wikipedia.org/wiki/Special:CentralAuth/{}?uselang=qqx"}},
            {"AllMyLinks", new WebsiteInfo {ErrorType = "status_code", Url = "https://allmylinks.com/{}"}},
            {"Buy Me a Coffee", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.buymeacoffee.com/{}"}},
            {"BuzzFeed", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.buzzfeed.com/{}"}},
            {"Cash APP", new WebsiteInfo {ErrorType = "status_code", Url = "https://cash.app/${}"}},
            {"Ebay", new WebsiteInfo {ErrorType = "message", Url = "https://www.ebay.com/usr/{}"}},
            {"Instagram", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.picuki.com/profile/{}"}},
            {"JsFiddle", new WebsiteInfo {ErrorType = "status_code", Url = "https://jsfiddle.net/user/{}/"}},
            {"Linktree", new WebsiteInfo {ErrorType = "message", ErrorMessage = "\"statusCode\":404", Url = "https://linktr.ee/{}"}},
            {"Medium", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<span class=\"fs\">404</span>", Url = "https://{}.medium.com/about"}},
            {"Pinterest", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title></title>", Url = "https://pinterest.com/{}/"}},
            {"Rapid API", new WebsiteInfo {ErrorType = "status_code", Url = "https://rapidapi.com/user/{}"}},
            {"TradingView", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.tradingview.com/u/{}/"}},
        };

        private int checkedCount = 0;
        private int resultsCount = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            textBox1.GotFocus += (s, e) => { if (textBox1.Text == "Enter username here...") textBox1.Text = ""; };
            textBox1.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(textBox1.Text)) textBox1.Text = "Enter username here..."; };
            
            tableLayoutPanel1.HorizontalScroll.Maximum = 0;
            tableLayoutPanel1.AutoScroll = false;
            tableLayoutPanel1.VerticalScroll.Visible = false;
            tableLayoutPanel1.AutoScroll = true;
        }

        private async void btn_search_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(username) || username == "Enter username here...")
            {
                MessageBox.Show("Please enter a username to check.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SetSearchState(false, username);
            ResetSearchResults();

            var stopwatch = Stopwatch.StartNew();
            await PerformSearch(username);
            stopwatch.Stop();

            UpdateResultsDisplay(username, stopwatch.Elapsed.TotalSeconds);
            SetSearchState(true);
        }

        private void SetSearchState(bool enabled, string username = null)
        {
            btn_search.Enabled = enabled;
            textBox1.Enabled = enabled;
            if (!enabled && username != null)
            {
                label1.Text = $"Checking username {username}";
                Text = $"Emora | Checking username {username}";
            }
        }

        private void ResetSearchResults()
        {
            checkedCount = 0;
            resultsCount = 0;
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.Controls.Clear();
        }

        private async Task PerformSearch(string username)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(8);
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:124.0) Gecko/20100101 Firefox/124.0");

                var semaphore = new SemaphoreSlim((int)numericUpDown1.Value);
                var tasks = websites.Select(website => CheckWebsiteAsync(username, website.Key, website.Value, httpClient, semaphore));
                await Task.WhenAll(tasks);
            }
        }

        private async Task CheckWebsiteAsync(string username, string websiteName, WebsiteInfo websiteInfo, HttpClient httpClient, SemaphoreSlim semaphore)
        {
            await semaphore.WaitAsync();
            try
            {
                var url = websiteInfo.Url.Replace("{}", username);
                var response = await httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                bool isFound = websiteInfo.ErrorType == "status_code" 
                    ? response.IsSuccessStatusCode 
                    : !content.Contains(websiteInfo.ErrorMessage);

                if (isFound)
                {
                    AddResult(websiteName, url);
                }
            }
            catch (Exception) { }
            finally
            {
                Interlocked.Increment(ref checkedCount);
                UpdateProgressDisplay(username);
                semaphore.Release();
            }
        }

        private void UpdateProgressDisplay(string username)
        {
            Invoke(new Action(() =>
            {
                Text = $"Emora | Checking username {username} [{resultsCount} accounts found] [{checkedCount}/{websites.Count} websites checked]";
            }));
        }

        private void AddResult(string websiteName, string url)
        {
            Interlocked.Increment(ref resultsCount);
            Invoke(new Action(() =>
            {
                var panel = CreateResultPanel(websiteName, url);
                if (tableLayoutPanel1.Controls.Count > 0)
                {
                    tableLayoutPanel1.Controls.RemoveAt(tableLayoutPanel1.Controls.Count - 1);
                }
                tableLayoutPanel1.Controls.Add(panel);
                tableLayoutPanel1.Controls.Add(new Label());
                label1.Text = $"{resultsCount} accounts found";
            }));
        }

        private Panel CreateResultPanel(string websiteName, string url)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 80,
                BackColor = Color.FromArgb(66, 66, 66),
                BorderStyle = BorderStyle.None,
                TabIndex = tableLayoutPanel1.Controls.Count
            };

            var pictureBox = new PictureBox
            {
                Image = imageList1.Images[imageList1.Images.IndexOfKey(websiteName)],
                Size = new Size(64, 64),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(panel.Width / 8, (panel.Height - 64) / 2)
            };

            var label = new Label
            {
                Text = websiteName,
                AutoSize = true,
                Font = new Font(Font.FontFamily, 18),
                Location = new Point(pictureBox.Right + 10, (panel.Height - 18) / 3)
            };

            panel.Controls.AddRange(new Control[] { pictureBox, label });

            var toolTip = new ToolTip
            {
                AutoPopDelay = 5000,
                InitialDelay = 800,
                ReshowDelay = 500,
                ShowAlways = true
            };

            foreach (Control control in new Control[] { panel, pictureBox, label })
            {
                control.Click += (s, e) => Process.Start(url);
                control.Cursor = Cursors.Hand;
                toolTip.SetToolTip(control, control == panel ? "View User Profile" : "Visit Website");
            }

            return panel;
        }

        private void UpdateResultsDisplay(string username, double elapsedSeconds)
        {
            Text = $"Emora | {resultsCount} accounts found for {username}";
            label1.Text = $"{resultsCount} accounts found for {username} in {elapsedSeconds:F2} seconds";
        }
    }
}
