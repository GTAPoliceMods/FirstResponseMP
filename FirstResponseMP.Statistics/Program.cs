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
    public class Program
    {
        public static readonly bool DEV_MODE = false;

        static async Task Main(string[] args)
        {
            Console.Title = "First Response MP Statistics";
            Console.WriteLine("[Statistics] Server Starting...");

            Websocket.Start();

            Discord.Start();

            await Task.Delay(-1);
        }
    }
}

