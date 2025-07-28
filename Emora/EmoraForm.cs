using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emora
{
    public partial class EmoraForm : Form
    {
        private int checkedWebsites = 0;
        private const int minColumnWidth = 360;
        private int results = 0;
        private class WebsiteInfo
        {
            public string ErrorType { get; set; }
            public string ErrorMessage { get; set; }
            public string SuccessMessage { get; set; }
            public string Url { get; set; }
            public string ProfileUrl { get; set; }
            public string Data { get; set; }
            public string Method { get; set; }
            public Dictionary<string, string> Headers { get; set; }
        }

        private readonly Dictionary<string, WebsiteInfo> websites = new Dictionary<string, WebsiteInfo>
        {
            {"500px", new WebsiteInfo {ErrorType = "message", ErrorMessage = "\"statusText\":\"Not Found\",", ProfileUrl = "https://500px.com/p/{}", Url = "https://api.500px.com/graphql", Data = "{\"operationName\":\"ProfileRendererQuery\",\"variables\":{\"username\":\"{USERNAME}\",\"avatarSizes\":[\"MEDIUM\",\"LARGE\"]},\"query\":\"query ProfileRendererQuery($username: String!, $avatarSizes: [UserAvatarResizeImageSize!]) {\\n  profile: userByUsername(username: $username) {\\n    id\\n    legacyId\\n    userType: type\\n    username\\n    firstName\\n    displayName\\n    registeredAt\\n    canonicalPath\\n    isFeaturedPhotographer\\n    isBlockedByMe\\n    winnerCount\\n    crownFlag\\n    latestWinAt\\n    originalAvatar: avatar {\\n      images {\\n        url\\n        id\\n      }\\n      id\\n    }\\n    avatar {\\n      ...ProfileAvatarRefetchContainer_avatar_2v4paw\\n      id\\n    }\\n    badges {\\n      badge\\n    }\\n    userProfile {\\n      username\\n      firstname\\n      lastname\\n      state\\n      country\\n      city\\n      about\\n      id\\n    }\\n    userSetting {\\n      firstnameVisible\\n      locationVisible\\n      id\\n    }\\n    socialMedia {\\n      website\\n      twitter\\n      instagram\\n      facebook\\n      id\\n    }\\n    socialMediaItems {\\n      name\\n      value\\n      visible\\n      id\\n    }\\n    coverPhotoUrl\\n    followedBy {\\n      totalCount\\n      isFollowedByMe\\n    }\\n    followingUsers {\\n      totalCount\\n    }\\n    membership {\\n      expiryDate\\n      membershipTier: tier\\n      photoUploadQuota\\n      refreshPhotoUploadQuotaAt\\n      paymentStatus\\n      id\\n    }\\n    profileTabs {\\n      tabs {\\n        name\\n        visible\\n        count\\n      }\\n    }\\n    ...EditCover_cover\\n    ...EditProfileCover_cover\\n    photoStats {\\n      likeCount\\n      viewCount\\n    }\\n    portfolio {\\n      id\\n      status\\n      userDisabled\\n    }\\n  }\\n}\\n\\nfragment EditCover_cover on User {\\n  coverPhotoUrl\\n}\\n\\nfragment EditProfileCover_cover on User {\\n  coverPhotoUrl\\n}\\n\\nfragment ProfileAvatarRefetchContainer_avatar_2v4paw on UserAvatar {\\n  id\\n  images(sizes: $avatarSizes) {\\n    size\\n    url\\n    id\\n  }\\n}\\n\"}", Headers = new Dictionary<string, string> {{ "Content-Type", "application/json" } }}},
            {"About.me", new WebsiteInfo {ErrorType = "status_code", Url = "https://about.me/{}"}},
            {"AllMyLinks", new WebsiteInfo {ErrorType = "status_code", Url = "https://allmylinks.com/{}"}},
            {"AlternativeTo", new WebsiteInfo {ErrorType = "status_code", Url = "https://alternativeto.net/user/{}/"}},
            {"Archive of Our Own", new WebsiteInfo {ErrorType = "status_code", Url = "https://archiveofourown.org/users/{}"}},
            {"ArtStation", new WebsiteInfo {ErrorType = "status_code", ProfileUrl = "https://www.artstation.com/{}", Url = "https://www.artstation.com/users/{}/quick.json"}},
            {"Audiomack", new WebsiteInfo {ErrorType = "status_code", Url = "https://audiomack.com/{}"}},
            {"Bandcamp", new WebsiteInfo {ErrorType = "status_code", Url = "https://bandcamp.com/{}"}},
            {"Behance", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.behance.net/{}"}},
            {"Bluesky", new WebsiteInfo {ErrorType = "status_code", ProfileUrl = "https://bsky.app/profile/{}.bsky.social", Url = "https://public.api.bsky.app/xrpc/app.bsky.actor.getProfile?actor={}.bsky.social"}},
            {"Buy Me a Coffee", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.buymeacoffee.com/{}"}},
            {"BuzzFeed", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.buzzfeed.com/{}"}},
            {"Cash App", new WebsiteInfo {ErrorType = "status_code", Url = "https://cash.app/${}"}},
            {"Chess", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.chess.com/member/{}"}},
            {"Codeberg", new WebsiteInfo {ErrorType = "status_code", Url = "https://codeberg.org/{}"}},
            {"CodeChef", new WebsiteInfo {ErrorType = "message", ErrorMessage = "CodeChef - Learn and Practice Coding with Problems", Url = "https://www.codechef.com/users/{}"}},
            {"Codepen", new WebsiteInfo {ErrorType = "status_code", Url = "https://codepen.io/{}"}},
            {"Codewars", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.codewars.com/users/{}"}},
            {"CodinGame", new WebsiteInfo {ErrorType = "status_code", ProfileUrl = "https://www.codingame.com/forum/u/{}", Url = "https://forum.codingame.com/u/{}/summary"}},
            {"DeviantArt", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.deviantart.com/{}"}},
            {"Discogs", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.discogs.com/user/{}"}},
            {"Discord", new WebsiteInfo {ErrorType = "message", SuccessMessage = "{\"taken\":true}", ProfileUrl = "https://discord.com", Url = "https://discord.com/api/v9/unique-username/username-attempt-unauthed", Data = "{\"username\":\"{USERNAME}\"}", Headers = new Dictionary<string, string> {{ "Content-Type", "application/json" } }}},
            {"Docker Hub", new WebsiteInfo {ErrorType = "status_code", Url = "https://hub.docker.com/u/{}"}},
            {"Douban", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.douban.com/people/{}/"}},
            {"Dribbble", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title>Sorry, the page you were looking for doesn't exist. (404)</title>", Url = "https://dribbble.com/{}"}},
            {"Duolingo", new WebsiteInfo {ErrorType = "message", ErrorMessage = "Duolingo - Learn a language for free @duolingo", Url = "https://www.duolingo.com/profile/{}"}},
            {"Ebay", new WebsiteInfo {ErrorType = "message", ErrorMessage = "Sorry, this user was not found.", SuccessMessage = "on eBay</title>", Url = "https://www.ebay.com/usr/{}"}},
            {"Facebook", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title id=\"pageTitle\">", SuccessMessage = "<title>", Url = "https://www.facebook.com/{}/", Headers = new Dictionary<string, string> {{ "Sec-Fetch-Mode", "navigate" }}}},
            {"Fandom", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.fandom.com/u/{}"}},
            {"Flickr", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.flickr.com/people/{}"}},
            {"Freesound", new WebsiteInfo {ErrorType = "status_code", Url = "https://freesound.org/people/{}/"}},
            {"Gaia Online", new WebsiteInfo {ErrorType = "message", ErrorMessage = "No user ID specified or user does not exist!</div>", Url = "https://www.gaiaonline.com/profiles/{}"}},
            {"GeeksforGeeks", new WebsiteInfo {ErrorType = "message", ErrorMessage = "Page Not Found - GeeksforGeeks", Url = "https://www.geeksforgeeks.org/user/{}/"}},
            {"Genius (Artists)", new WebsiteInfo {ErrorType = "status_code", Url = "https://genius.com/artists/{}"}},
            {"Genius (Users)", new WebsiteInfo {ErrorType = "status_code", Url = "https://genius.com/{}"}},
            {"Giphy", new WebsiteInfo {ErrorType = "message", ErrorMessage = "Explore <span class=\"", Url = "https://giphy.com/{}"}},
            {"Gitea", new WebsiteInfo {ErrorType = "status_code", Url = "https://gitea.com/{}"}},
            {"Gitee", new WebsiteInfo {ErrorType = "status_code", Url = "https://gitee.com/{}"}},
            {"GitHub", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.github.com/{}"}},
            {"GitLab", new WebsiteInfo {ErrorType = "status_code", Url = "https://gitlab.com/{}"}},
            {"GoodReads", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title>Page not found</title>", Url = "https://www.goodreads.com/{}"}},
            {"Hack The Box", new WebsiteInfo {ErrorType = "status_code", Url = "https://forum.hackthebox.com/u/{}"}},
            {"Hackaday", new WebsiteInfo {ErrorType = "status_code", Url = "https://hackaday.io/{}"}},
            {"Hacker News", new WebsiteInfo {ErrorType = "message", ErrorMessage = "No such user.", Url = "https://news.ycombinator.com/user?id={}"}},
            {"HackerRank", new WebsiteInfo {ErrorType = "status_code", ProfileUrl = "https://www.hackerrank.com/profile/{}", Url = "https://www.hackerrank.com/rest/contests/master/hackers/{}/profile"}},
            {"HubPages", new WebsiteInfo {ErrorType = "status_code", Url = "https://hubpages.com/@{}"}},
            {"HudsonRock", new WebsiteInfo {ErrorType = "message", ErrorMessage = "This username is not associated with a computer infected by an info-stealer.", Url = "https://cavalier.hudsonrock.com/api/json/v2/osint-tools/search-by-username?username={}"}},
            {"Hugging Face", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title>404 – Hugging Face</title>", Url = "https://huggingface.co/{}"}},
            {"Imgur", new WebsiteInfo {ErrorType = "status_code", Url = "https://api.imgur.com/account/v1/accounts/{}?client_id=546c25a59c58ad7"}},
            {"Instagram", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.picuki.com/profile/{}", ProfileUrl="https://instagram.com/{}"}},
            {"Instructables", new WebsiteInfo {ErrorType = "status_code", ProfileUrl = "https://www.instructables.com/member/{}/", Url = "https://www.instructables.com/json-api/showAuthorExists?screenName={}"}},
            {"itch.io", new WebsiteInfo {ErrorType = "status_code", Url = "https://itch.io/profile/{}"}},
            {"JEUXVIDEO", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.jeuxvideo.com/profil/{}?mode=page_perso"}},
            {"JsFiddle", new WebsiteInfo {ErrorType = "status_code", Url = "https://jsfiddle.net/user/{}/"}},
            {"Kickstarter", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.kickstarter.com/profile/{}"}},
            {"Kik", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<h1 class=\"display-name\"> </h1>", Url = "https://kik.me/{}"}},
            {"Ko-fi", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title>Explore Featured Creators on Ko-fi", SuccessMessage = "<title>Support ", Url = "https://ko-fi.com/{}"}},
            {"Komi", new WebsiteInfo {ErrorType = "message", ErrorMessage = ",\"code\":1000", SuccessMessage = ",\"accountStatus\":\"active\"", ProfileUrl = "https://{}.komi.io", Url = "https://api.komi.io/api/talent/usernames/{}"}},
            {"Kongregate", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.kongregate.com/accounts/{}"}},
            {"Last.fm", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.last.fm/user/{}"}},
            {"LeetCode", new WebsiteInfo {ErrorType = "status_code", Url = "https://leetcode.com/u/{}/"}},
            {"Letterboxd", new WebsiteInfo {ErrorType = "status_code", Url = "https://letterboxd.com/{}/"}},
            {"Lichess", new WebsiteInfo {ErrorType = "status_code", Url = "https://lichess.org/@/{}"}},
            {"LinkedIn", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.linkedin.com/in/{}"}},
            {"Linktree", new WebsiteInfo {ErrorType = "message", ErrorMessage = "\"statusCode\":404", Url = "https://linktr.ee/{}"}},
            {"Medium", new WebsiteInfo {ErrorType = "message", ErrorMessage = ">404</span>", Url = "https://{}.medium.com/about"}},
            {"Minecraft", new WebsiteInfo {ErrorType = "status_code", Url = "https://api.mojang.com/users/profiles/minecraft/{}"}},
            {"Mixcloud", new WebsiteInfo {ErrorType = "status_code", ProfileUrl = "https://www.mixcloud.com/{}/", Url = "https://api.mixcloud.com/{}/"}},
            {"ModDB", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.moddb.com/members/{}"}},
            {"MyAnimeList", new WebsiteInfo {ErrorType = "status_code", Url = "https://myanimelist.net/profile/{}"}},
            {"Myspace", new WebsiteInfo {ErrorType = "status_code", Url = "https://myspace.com/{}"}},
            {"Naver", new WebsiteInfo {ErrorType = "status_code", Url = "https://blog.naver.com/{}"}},
            {"Newgrounds", new WebsiteInfo {ErrorType = "status_code", Url = "https://{}.newgrounds.com/"}},
            {"npm", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.npmjs.com/~{}"}},
            {"Odnoklassniki", new WebsiteInfo {ErrorType = "status_code", Url = "https://ok.ru/{}"}},
            {"OpenStreetMap", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.openstreetmap.org/user/{}"}},
            {"Pastebin", new WebsiteInfo {ErrorType = "status_code", Url = "https://pastebin.com/u/{}"}},
            {"Patreon", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.patreon.com/{}"}},
            {"PayPal", new WebsiteInfo {ErrorType = "message", ErrorMessage = "pplogo384.png\" /><meta property=\"og:title\"", SuccessMessage = "\"userInfo\":", Url = "https://www.paypal.com/paypalme/{}"}},
            {"Periscope", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.periscope.tv/{}"}},
            {"Pinterest", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title></title>", Url = "https://www.pinterest.com/{}/"}},
            {"Pixelfed", new WebsiteInfo {ErrorType = "status_code", Url = "https://pixelfed.social/{}"}},
            {"Plurk", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title>User Not Found! - Plurk</title>", Url = "https://www.plurk.com/{}"}},
            {"Product Hunt", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.producthunt.com/@{}"}},
            {"PyPi", new WebsiteInfo {ErrorType = "status_code", ProfileUrl = "https://pypi.org/user/{}", Url = "https://pypi.org/_includes/administer-user-include/{}"}},
            {"Rate Your Music", new WebsiteInfo {ErrorType = "status_code", Url = "https://rateyourmusic.com/~{}"}},
            {"Reddit", new WebsiteInfo {ErrorType = "message", ErrorMessage = "\"error\": 404}", SuccessMessage = "is_employee:", ProfileUrl = "https://www.reddit.com/user/{}/", Url = "https://www.reddit.com/user/{}/about.json"}},
            {"Replit", new WebsiteInfo {ErrorType = "status_code", Url = "https://replit.com/@{}"}},
            {"ReverbNation", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title>Page Not Found</title>", Url = "https://www.reverbnation.com/{}"}},
            {"Roblox", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.roblox.com/user.aspx?username={}"}},
            {"RootMe", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.root-me.org/{}"}},
            {"Scratch", new WebsiteInfo {ErrorType = "status_code", Url ="https://scratch.mit.edu/users/{}/"}},
            {"Scribd", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.scribd.com/{}"}},
            {"Smule", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title>Smule | Page Not Found (404)</title>", Url = "https://www.smule.com/{}"}},
            {"Snapchat", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.snapchat.com/add/{}"}},
            {"SoundCloud", new WebsiteInfo {ErrorType = "status_code", Url = "https://soundcloud.com/{}"}},
            {"SourceForge", new WebsiteInfo {ErrorType = "status_code", Url = "https://sourceforge.net/u/{}"}},
            {"Speedrun", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title>Not found - Speedrun.com</title>", Url = "https://www.speedrun.com/users/{}"}},
            {"Spotify", new WebsiteInfo {ErrorType = "status_code", Url = "https://open.spotify.com/user/{}"}},
            {"Squarespace", new WebsiteInfo {ErrorType = "status_code", Url = "http://{}.squarespace.com/"}},
            {"Steam", new WebsiteInfo {ErrorType = "message", ErrorMessage = "Steam Community :: Error", Url = "https://steamcommunity.com/id/{}"}},
            {"Telegram", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<meta name=\"robots\" content=\"noindex, nofollow\">", Url = "https://t.me/{}"}},
            {"Tellonym", new WebsiteInfo {ErrorType = "status_code", Url = "https://tellonym.me/{}"}},
            {"Tenor", new WebsiteInfo {ErrorType = "status_code", Url = "https://tenor.com/users/{}"}},
            {"ThemeForest", new WebsiteInfo {ErrorType = "status_code", Url = "https://themeforest.net/user/{}"}},
            {"TryHackMe", new WebsiteInfo {ErrorType = "message", ErrorMessage = " doesn't exist\"}", ProfileUrl = "https://tryhackme.com/p/{}", Url = "https://tryhackme.com/api/v2/public-profile?username={}"}},
            {"TikTok", new WebsiteInfo {ErrorType = "status_code", ProfileUrl = "https://www.tiktok.com/@{}", Url = "https://www.tiktok.com/oembed?url=https://www.tiktok.com/@{}"}},
            {"Tinder", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title data-react-helmet=\"true\">Tinder | ", Url = "https://tinder.com/@{}"}},
            {"Threads", new WebsiteInfo {ErrorType = "message", ErrorMessage = "<title>Threads", Url = "https://www.threads.net/@{}", Headers = new Dictionary<string, string> {{ "Sec-Fetch-Mode", "navigate" }}}},
            {"Topcoder", new WebsiteInfo {ErrorType = "status_code", ProfileUrl = "https://profiles.topcoder.com/{}/", Url = "https://api.topcoder.com/v5/members/{}"}},
            {"TradingView", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.tradingview.com/u/{}/"}},
            {"Trello", new WebsiteInfo {ErrorType = "status_code", ProfileUrl = "https://trello.com/u/{}/activity", Url = "https://trello.com/1/Members/{}"}},
            {"Truth Social", new WebsiteInfo {ErrorType = "status_code", ProfileUrl = "https://truthsocial.com/@{}", Url = "https://truthsocial.com/api/v1/accounts/lookup?acct={}"}},
            {"Tumblr", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.tumblr.com/{}"}},
            {"Twitch", new WebsiteInfo {ErrorType = "status_code", ProfileUrl = "https://www.twitch.tv/{}", Url = "https://twitchtracker.com/{}"}},
            {"Untappd", new WebsiteInfo {ErrorType = "status_code", Url = "https://untappd.com/user/{}"}},
            {"Venmo", new WebsiteInfo {ErrorType = "status_code", Url = "https://account.venmo.com/u/{}"}},
            {"Vimeo", new WebsiteInfo {ErrorType = "status_code", Url = "https://vimeo.com/{}"}},
            {"VirusTotal", new WebsiteInfo {ErrorType = "status_code", ProfileUrl = "https://www.virustotal.com/gui/user/{}", Url = "https://www.virustotal.com/ui/users/{}/avatar"}},
            {"VK", new WebsiteInfo {ErrorType = "status_code", Url = "https://vk.com/{}"}},
            {"VSCO", new WebsiteInfo {ErrorType = "status_code", Url = "https://vsco.co/{}/gallery"}},
            {"Wattpad", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.wattpad.com/user/{}"}},
            {"Weebly", new WebsiteInfo {ErrorType = "status_code", Url = "https://{}.weebly.com/"}},
            {"Wikipedia", new WebsiteInfo {ErrorType = "message", ErrorMessage = "(centralauth-admin-nonexistent:", ProfileUrl = "https://en.wikipedia.org/wiki/User:{}", Url = "https://en.wikipedia.org/wiki/Special:CentralAuth/{}?uselang=qqx"}},
            {"X", new WebsiteInfo {ErrorType = "status_code", ProfileUrl = "https://x.com/{}", Url = "https://nitter.privacydev.net/{}"}},
            {"XING", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.xing.com/profile/{}"}},
            {"Zomato", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.zomato.com/{}/reviews"}},
            {"CurseForge", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.curseforge.com/members/{}/projects"}},
            {"Etsy", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.etsy.com/people/{}"}},
            {"Fiverr", new WebsiteInfo {ErrorType = "status_code", Url = "https://www.fiverr.com/{}"}},

            //{"DailyMotion", new WebsiteInfo {ErrorType = "message", ErrorMessage = "", Url = "", ProfileUrl = "https://www.dailymotion.com/{}", Data=""}}, // Currently doesn't work
            //{"Rapid API", new WebsiteInfo {ErrorType = "status_code", Url = "https://rapidapi.com/user/{}"}},  // Currently doesn't work
        };

        public EmoraForm()
        {
            InitializeComponent();
            InitializeEvents();
            nudThreads.Value = Environment.ProcessorCount;
        }

        private void InitializeEvents()
        {
            Resize += (s, e) => UpdateTableLayoutColumns();
            Load += (s, e) => UpdateTableLayoutColumns();
            txtUsername.GotFocus += (s, e) => { if (txtUsername.Text == "Enter username here...") txtUsername.Text = ""; };
            txtUsername.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(txtUsername.Text)) txtUsername.Text = "Enter username here..."; };
            txtUsername.TextChanged += FilterResultsByUsername;
            btnSearch.Click += BtnSearchClick;
        }

        private async void BtnSearchClick(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            if (string.IsNullOrEmpty(username) || username == "Enter username here...")
            {
                MessageBox.Show("Please enter a username to check.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SetSearchUIState(true, username);
            Stopwatch stopwatch = Stopwatch.StartNew();

            using (var httpClient = CreateConfiguredHttpClient())
            {
                var tasks = new List<Task>();
                var semaphore = new SemaphoreSlim((int)nudThreads.Value);

                foreach (var website in websites)
                {
                    await semaphore.WaitAsync();
                    var task = Task.Run(async () =>
                    {
                        try
                        {
                            await CheckWebsite(username, website.Key, website.Value, httpClient);
                        }
                        finally
                        {
                            checkedWebsites++;
                            Invoke(new Action(() =>
                            {
                                Text = $"Emora | Checking username {username} [{results} accounts found] [{checkedWebsites}/{websites.Count} websites checked]";
                            }));
                            semaphore.Release();
                        }
                    });
                    tasks.Add(task);
                }
                await Task.WhenAll(tasks);

                stopwatch.Stop();
                Text = $"Emora | {results} accounts found for {username}";
                lblStatus.Text = $"{results} accounts found for {username} in {stopwatch.ElapsedMilliseconds / 1000.0} seconds";
                SetSearchUIState(false, username);
            }
        }

        private void SetSearchUIState(bool isSearching, string username)
        {
            btnSearch.Enabled = !isSearching;
            txtUsername.Enabled = !isSearching;

            if (isSearching)
            {
                Text = $"Emora | Checking username {username}";
                lblStatus.Text = $"Checking username {username}";
                checkedWebsites = 0;
                results = 0;
                tlpResults.RowStyles.Clear();
                tlpResults.Controls.Clear();
            }
        }

        private HttpClient CreateConfiguredHttpClient()
        {
            var client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(8)
            };
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:141.0) Gecko/20100101 Firefox/141.0");
            return client;
        }

        private async Task CheckWebsite(string username, string websiteName, WebsiteInfo websiteInfo, HttpClient httpClient)
        {
            try
            {
                var url = websiteInfo.Url.Replace("{}", username);
                string method = !string.IsNullOrEmpty(websiteInfo.Method)
                    ? websiteInfo.Method
                    : (!string.IsNullOrEmpty(websiteInfo.Data) ? "POST" : "GET");
                string profileUrl = string.IsNullOrEmpty(websiteInfo.ProfileUrl)
                    ? url
                    : websiteInfo.ProfileUrl.Replace("{}", username);

                var request = CreateHttpRequest(method, url, username, websiteInfo);
                var response = await httpClient.SendAsync(request);

                await ProcessResponse(response, websiteInfo, websiteName, profileUrl);
            }
            catch {}
        }

        private HttpRequestMessage CreateHttpRequest(string method, string url, string username, WebsiteInfo websiteInfo)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), url);

            if (websiteInfo.Headers != null)
            {
                foreach (var header in websiteInfo.Headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            if (!string.IsNullOrEmpty(websiteInfo.Data))
            {
                request.Content = new StringContent(websiteInfo.Data.Replace("{USERNAME}", username), Encoding.UTF8, "application/json");
            }

            return request;
        }

        private async Task ProcessResponse(HttpResponseMessage response, WebsiteInfo websiteInfo, string websiteName, string profileUrl)
        {
            if (websiteInfo.ErrorType == "status_code")
            {
                if (response.IsSuccessStatusCode)
                {
                    AddResult(websiteName, profileUrl);
                }
            }
            else if (websiteInfo.ErrorType == "message")
            {
                string content = await GetResponseContent(response);
                bool isErrorMessageAbsent = string.IsNullOrEmpty(websiteInfo.ErrorMessage) || !content.Contains(websiteInfo.ErrorMessage);
                bool isSuccessMessagePresent = string.IsNullOrEmpty(websiteInfo.SuccessMessage) || content.Contains(websiteInfo.SuccessMessage);

                if (isErrorMessageAbsent && isSuccessMessagePresent)
                {
                    AddResult(websiteName, profileUrl);
                }
            }
        }

        private async Task<string> GetResponseContent(HttpResponseMessage response)
        {
            try
            {
                return await response.Content.ReadAsStringAsync();
            }
            catch (InvalidOperationException)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        private void AddResult(string websiteName, string url)
        {
            results++;
            var panel = CreateResultPanel(websiteName, url);

            Invoke(new Action(() =>
            {
                tlpResults.Controls.Add(panel);
                lblStatus.Text = $"{results} accounts found";

                UpdateTableLayoutColumns();
            }));
        }

        private Panel CreateResultPanel(string websiteName, string url)
        {
            var panel = new Panel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(66, 66, 66),
                BorderStyle = BorderStyle.None,
                Cursor = Cursors.Hand,
                Height = 80,
                TabIndex = tlpResults.Controls.Count,
                Tag = websiteName.ToLower()
            };

            PictureBox pictureBox = new PictureBox
            {
                Image = imgWebsiteIcons.Images[imgWebsiteIcons.Images.IndexOfKey(websiteName)],
                Size = new Size(64, 64),
                SizeMode = PictureBoxSizeMode.Zoom,
                Cursor = Cursors.Hand
            };
            pictureBox.Location = new Point((panel.Width - pictureBox.Width) / 8, (panel.Height - pictureBox.Height) / 2);
            panel.Controls.Add(pictureBox);

            var label = new Label
            {
                Text = websiteName,
                AutoSize = true,
                Font = new Font(Font.FontFamily, 18),
                Cursor = Cursors.Hand
            };
            label.Location = new Point(pictureBox.Right + 10, (panel.Height - label.Height) / 3);
            panel.Controls.Add(label);

            SetupResultPanelEvents(panel, label, pictureBox, url);

            return panel;
        }

        private void SetupResultPanelEvents(Panel panel, Label label, PictureBox pictureBox, string url)
        {
            Uri uri = new Uri(url);
            string domain = uri.GetLeftPart(UriPartial.Authority);

            panel.Click += (sender, e) => Process.Start(url);
            pictureBox.Click += (sender, e) => Process.Start(domain);
            label.Click += (sender, e) => Process.Start(domain);

            ToolTip toolTip = new ToolTip
            {
                AutoPopDelay = 4000,
                InitialDelay = 800,
                ReshowDelay = 500,
                ShowAlways = true
            };
            toolTip.SetToolTip(panel, "View User Profile");
            toolTip.SetToolTip(label, "Visit Website");
            toolTip.SetToolTip(pictureBox, "Visit Website");
        }

        private void UpdateTableLayoutColumns()
        {
            int availableWidth = tlpResults.Width;
            int columnCount = Math.Max(1, (availableWidth - 10) / minColumnWidth);

            if (tlpResults.ColumnCount == columnCount)
                return;

            tlpResults.SuspendLayout();

            tlpResults.ColumnCount = columnCount;
            tlpResults.ColumnStyles.Clear();

            float columnWidth = 100f / columnCount;
            for (int i = 0; i < columnCount; i++)
            {
                tlpResults.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnWidth));
            }

            tlpResults.ResumeLayout(true);
        }

        private void FilterResultsByUsername(object sender, EventArgs e)
        {
            string filterText = txtUsername.Text.ToLower();
            tlpResults.SuspendLayout();

            foreach (Control control in tlpResults.Controls)
            {
                if (control is Panel panel && panel.Tag is string websiteName)
                {
                    bool matches = websiteName.Contains(filterText);
                    panel.Visible = matches;
                }
            }

            tlpResults.ResumeLayout(true);
        }
    }
}
