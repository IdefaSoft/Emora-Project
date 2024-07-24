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
            // ... (website dictionary remains the same)
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
