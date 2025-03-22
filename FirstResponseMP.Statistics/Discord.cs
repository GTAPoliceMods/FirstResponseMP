using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DSharpPlus.Entities;
using DSharpPlus;
using SixLabors.Fonts;
using System.IO;
using System.Threading;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;
using System.Numerics;
using SixLabors.ImageSharp.Drawing;

namespace FirstResponseMP.Statistics
{
    public class Discord
    {
        private static Timer _timer;
        private static ulong initStatsMessageId = 0;

        public static async void Start()
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

            _timer = new Timer(async _ => await SendStatsMessage(discordChannel, family), null, 0, 10000);
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
            var statsData = StatisticsStore.GetStatistics();
            var allOnDutyPlayers = statsData.TotalPolice + statsData.TotalFire + statsData.TotalMedical;

            var demoNumber = FormatNumber(100);

            Font font = fontFam.CreateFont(40, FontStyle.Regular);
            Font fontSm = fontFam.CreateFont(35, FontStyle.Regular);

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

                RichTextOptions onDutyOptions = new(options)
                {
                    Font = fontSm
                };

                RichTextOptions usingResourceCountText = new(options)
                {
                    Origin = new PointF(statsImage.Width / 2, statsImage.Height / 3f),
                };

                RichTextOptions onlinePlayersText = new(options)
                {
                    Origin = new PointF(statsImage.Width / 2, statsImage.Height / 2f),
                };

                RichTextOptions onDutyPoliceText = new(onDutyOptions)
                {
                    Origin = new PointF(statsImage.Width / 2 - 150, statsImage.Height / 1.1f),
                };

                RichTextOptions onDutyFireText = new(onDutyOptions)
                {
                    Origin = new PointF(statsImage.Width / 2, statsImage.Height / 1.1f),
                };

                RichTextOptions onDutyMedicalText = new(onDutyOptions)
                {
                    Origin = new PointF(statsImage.Width / 2 + 150, statsImage.Height / 1.1f),
                };

                using Image polImage = Image.Load(File.OpenRead("./assets/icon_pol.png"));
                polImage.Configuration.SetShapeOptions(e =>
                {
                    e.ClippingOperation = ClippingOperation.None;
                });

                using Image fireImage = Image.Load(File.OpenRead("./assets/icon_fire.png"));
                fireImage.Configuration.SetShapeOptions(e =>
                {
                    e.ClippingOperation = ClippingOperation.None;
                });

                using Image emsImage = Image.Load(File.OpenRead("./assets/icon_ems.png"));
                emsImage.Configuration.SetShapeOptions(e =>
                {
                    e.ClippingOperation = ClippingOperation.None;
                });
                

                statsImage.Mutate(x =>
                {
                    x.DrawText(usingResourceCountText, $"{FormatNumber(statsData.TotalServers)} servers online", Brushes.Solid(Color.White));
                    x.DrawText(onlinePlayersText, $"{FormatNumber(statsData.TotalPlayers)} players online", Brushes.Solid(Color.White));

                    x.DrawImage(polImage, backgroundLocation: (Point)new PointF(statsImage.Width / 2.13f - 150, statsImage.Height / 1.6f), 1);
                    x.DrawText(onDutyPoliceText, $"{FormatNumber(statsData.TotalPolice)}", Brushes.Solid(Color.White));

                    x.DrawImage(fireImage, backgroundLocation: (Point)new PointF(statsImage.Width / 2.13f, statsImage.Height / 1.52f), 1);
                    x.DrawText(onDutyFireText, $"{FormatNumber(statsData.TotalFire)}", Brushes.Solid(Color.White));

                    x.DrawImage(emsImage, backgroundLocation: (Point)new PointF(statsImage.Width / 2.13f + 150, statsImage.Height / 1.52f), 1);
                    x.DrawText(onDutyMedicalText, $"{FormatNumber(statsData.TotalMedical)}", Brushes.Solid(Color.White));
                });

                FileStream saveAs = File.OpenWrite("./assets/statistics.png");

                await statsImage.SaveAsPngAsync(saveAs);

                await saveAs.DisposeAsync();

                FileStream imageOut = File.OpenRead("./assets/statistics.png");

                return imageOut;
            };
        }

        private static string FormatNumber(long num)
        {
            // Ensure number has max 3 significant digits (no rounding up can happen)
            long i = (long)Math.Pow(10, (int)Math.Max(0, Math.Log10(num) - 2));
            num = num / i * i;

            if (num >= 1000000000)
                return (num / 1000000000D).ToString("0.##") + "B";
            if (num >= 1000000)
                return (num / 1000000D).ToString("0.##") + "M";
            if (num >= 1000)
                return (num / 1000D).ToString("0.##") + "K";

            return num.ToString("#,0");
        }
    }
}
