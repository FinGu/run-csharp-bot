using Discord;
using Discord.Gateway;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharp_discord_bot {
    class Program {
        private static void on_login(DiscordSocketClient client, LoginEventArgs args) => Console.WriteLine($"{client.User.Username} logged in");

        static void Main(string[] args) {
            DiscordSocketClient client = new DiscordSocketClient();

            client.OnLoggedIn += on_login;

            client.CreateCommandHandler("!");

            client.Login("");

            Thread.Sleep(-1);
        }
    }
}
