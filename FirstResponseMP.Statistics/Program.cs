using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

using Newtonsoft.Json;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Cors;

using DSharpPlus;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using DSharpPlus.Entities;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using System.Threading.Channels;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.Fonts;

namespace FirstResponseMP.Statistics
{
    public static class Statistics
    {
        private static readonly ConcurrentDictionary<string, Tuple<ServerStats, DateTime>> ClientStatistics = new ConcurrentDictionary<string, Tuple<ServerStats, DateTime>>();
        private static readonly Timer CleanupTimer;

        static Statistics()
        {
            CleanupTimer = new Timer(_ => RemoveStaleClients(), null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
        }

        public static int UsingResourceCount => ClientStatistics.Values.Sum(s => s.Item1.UsingResourceCount);
        public static int OnlinePlayers => ClientStatistics.Values.Sum(s => s.Item1.OnlinePlayers);
        public static int OnDutyPlayers => ClientStatistics.Values.Sum(s => s.Item1.OnDutyPlayers);

        public static void UpdateStats(string connectionId, ServerStats stats)
        {
            ClientStatistics[connectionId] = new Tuple<ServerStats, DateTime>(stats, DateTime.UtcNow);
        }

        public static void RemoveStats(string connectionId)
        {
            ClientStatistics.TryRemove(connectionId, out _);
        }

        private static void RemoveStaleClients()
        {
            var now = DateTime.UtcNow;
            foreach (var key in ClientStatistics.Keys.ToList())
            {
                if ((now - ClientStatistics[key].Item2).TotalSeconds > 10)
                {
                    Console.WriteLine($"[Statistics] Removing stale client: {key}");
                    ClientStatistics.TryRemove(key, out _);
                }
            }
        }
    }

    public class ServerStats
    {
        public int UsingResourceCount { get; set; }
        public int OnlinePlayers { get; set; }
        public int OnDutyPlayers { get; set; }
    }

    public class ServerStatisticsHub : Hub
    {
        [HubMethodName("sendServerStats")]
        public async Task SendServerStats(string data)
        {
            Console.WriteLine($"[Statistics] Received stats from {Context.ConnectionId}: {data}");

            try
            {
                var stats = JsonConvert.DeserializeObject<ServerStats>(data);
                if (stats != null)
                {
                    Statistics.UpdateStats(Context.ConnectionId, stats);
                    await Clients.Caller.SendAsync("stats", "Recieved server stats");
                    //"{\"usingResourceCount\":0,\"onlinePlayers\":0,\"onDutyPlayers\":0}"
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Statistics] Error parsing stats: {ex.Message}");
            }
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            if (!httpContext.Request.Headers.ContainsKey("Authorization") || string.IsNullOrWhiteSpace(httpContext.Request.Headers["Authorization"]))
            {
                Context.Abort();
                return;
            };

            if (httpContext.Request.Headers["Authorization"] != "mclovinisstinkyingoldensoffice")
            {
                Context.Abort();
                return;
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Statistics.RemoveStats(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }

    public class GlobalStatisticsHub : Hub
    {
        [HubMethodName("getStatistics")]
        public async Task GetStatistics()
        {
            Console.WriteLine($"[Statistics] Request statistics from {Context.ConnectionId}");

            var statsData = new
            {
                usingResourceCount = Statistics.UsingResourceCount,
                onlinePlayers = Statistics.OnlinePlayers,
                onDutyPlayers = Statistics.OnDutyPlayers
            };

            await Clients.Caller.SendAsync("stats", JsonConvert.SerializeObject(statsData));
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }

    public class Program
    {
        private static Timer _timer;
        private static Timer _statsEmbedTimer;
        private static IHubContext<ServerStatisticsHub> _hubContext;

        private static readonly bool DEV_MODE = true;

        private static ulong initStatsMessageId = 0;

        static async Task Main(string[] args)
        {
            Console.Title = "First Response MP Statistics";
            Console.WriteLine("[Statistics] Server Starting...");

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                if (DEV_MODE == false)
                {
                    options.AddPolicy("CustomPolicy", policy =>
                    {
                        policy.WithOrigins("frmpstats.gtapolicemods.com")
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials();
                    });
                }
                else
                {
                    options.AddPolicy("CustomPolicy", policy =>
                    {
                        policy.WithOrigins("*")
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
                }
            });

            builder.Services.AddSignalR();

            var app = builder.Build();

            if (DEV_MODE == false)
            {
                app.Use(async (context, next) =>
                {
                    context.Request.Scheme = "https";
                    await next();
                });
            }

            app.UseCors("CustomPolicy");

            app.MapHub<ServerStatisticsHub>("/serverStatisticsHub").RequireCors("CustomPolicy");
            app.MapHub<GlobalStatisticsHub>("/globalStatisticsHub").RequireCors("CustomPolicy");

            

            Console.WriteLine($"[Statistics] Running at {(DEV_MODE ? "http://localhost:54269/" : "https://frmpstats.gtapolicemods.com/")}");

            _timer = new Timer(SendPingToClients, null, 0, 10000);

            DoRandomDiscordShit();

            await app.RunAsync(DEV_MODE == true ? "http://localhost:54269/" : "http://*:54269/");
        }

        private static async void DoRandomDiscordShit()
        {
            FontCollection collection = new();
            FontFamily family = collection.Add(File.OpenRead("./assets/trebuc.ttf"));

            DiscordClient discordClient = new DiscordClient(new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                AutoReconnect = true,
                Token = "MTM0NDUwNzU0ODIyNDQ1ODg1NA.GVp6Dw.EtTBVPh21VszIFVe8CdfEEM2DoIlQYVlXhO81A",
            });

            await discordClient.ConnectAsync();

            var discordActivity = new DiscordActivity()
            {
                ActivityType = ActivityType.Streaming,
                Name = "First Response MP Stats",
                StreamUrl = "https://gtapolicemods.com/newswire/2-development-blogs/",
            };

            await discordClient.UpdateStatusAsync(discordActivity, UserStatus.DoNotDisturb, DateTime.UtcNow);

            Console.WriteLine("[Discord] Connected!");

            DiscordGuild discordServer = await discordClient.GetGuildAsync(1289100957757607978);
            DiscordChannel discordChannel = discordServer.GetChannel(1343942710200893450);

            var allMessagesToDelete = await discordChannel.GetMessagesAsync();

            if (allMessagesToDelete.Count > 0)
            {
                await discordChannel.DeleteMessagesAsync(await discordChannel.GetMessagesAsync());
            }

            _statsEmbedTimer = new Timer(async _ => await SendStatsMessage(discordChannel, family), null, 0, 10000);
        }

        private static async Task SendStatsMessage(DiscordChannel channel, FontFamily fontFam)
        {
            var messageBuilder = new DiscordMessageBuilder()
            {
                Content = "First Response MP Global Statistics"
            };

            var statsCanvas = await DiscordStatsCanvas(fontFam);

            messageBuilder.AddFile(statsCanvas);

            if (initStatsMessageId == 0)
            {
                var newMessage = await channel.SendMessageAsync(messageBuilder);
                initStatsMessageId = newMessage.Id;
            }
            else
            {
                var initMessage = await channel.GetMessageAsync(initStatsMessageId);

                await initMessage.ModifyAsync(messageBuilder);
            }

            await statsCanvas.DisposeAsync();
        }

        private static async Task<FileStream> DiscordStatsCanvas(FontFamily fontFam)
        {
            var statsData = new
            {
                usingResourceCount = Statistics.UsingResourceCount,
                onlinePlayers = Statistics.OnlinePlayers,
                onDutyPlayers = Statistics.OnDutyPlayers
            };

            Font font = fontFam.CreateFont(40, FontStyle.Regular);

            using (Image statsImage = Image.Load(File.OpenRead("./assets/statistics_temp.png")))
            {
                RichTextOptions options = new(font)
                {
                    Origin = new PointF(statsImage.Width / 2, statsImage.Height / 2),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextAlignment = TextAlignment.Center,
                    Font = font
                };

                RichTextOptions usingResourceCountText = new(options)
                {
                    Origin = new PointF(statsImage.Width / 2, statsImage.Height / 3f),
                };

                RichTextOptions onlinePlayersText = new(options)
                {
                    Origin = new PointF(statsImage.Width / 2, statsImage.Height / 2f),
                };

                RichTextOptions onDutyPlayersText = new(options)
                {
                    Origin = new PointF(statsImage.Width / 2, statsImage.Height / 1.5f),
                };

                statsImage.Mutate(x =>
                {
                    x.DrawText(usingResourceCountText, $"{statsData.usingResourceCount} servers online", Brushes.Solid(Color.White));
                    x.DrawText(onlinePlayersText, $"{statsData.onlinePlayers} players online", Brushes.Solid(Color.White));
                    x.DrawText(onDutyPlayersText, $"{statsData.onDutyPlayers} players on duty", Brushes.Solid(Color.White));
                });

                FileStream saveAs = File.OpenWrite("./assets/statistics.png");

                await statsImage.SaveAsPngAsync(saveAs);

                await saveAs.DisposeAsync();

                FileStream imageOut = File.OpenRead("./assets/statistics.png");

                return imageOut;
            };
        }

        private static async void SendPingToClients(object state)
        {
            if (_hubContext != null)
            {
                await _hubContext.Clients.All.SendAsync("pingStatsRequest");
                Console.WriteLine("[Statistics] Sent ping to all clients.");
            }
        }
    }
}

