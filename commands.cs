using Discord;
using Discord.Commands;
using Discord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace csharp_discord_bot {
    class commands {
        [Command("run", "run c# code")]
        public class run_code : ICommand {
            [Parameter("code", false)]
            public string code { get; private set; }

            public void Execute(DiscordSocketClient client, DiscordMessage message) {
                var channel_id = message.Channel.Id;

                var regex_match = new Regex("(```)(.*?)(```)", RegexOptions.Singleline).Match(code);

                if (!regex_match.Success) {
                    client.SendMessage(channel_id, $"the code needs to start and end with ```");
                    return;
                }

                var result = code_executor.execute_code(regex_match.Groups[2].Value);

                var code_output = result.ConsoleOutput;

                var embed = new EmbedMaker() {
                    Title = "show codenz",
                    TitleUrl = "https://dotnetfiddle.net/",
                    Description = (result.HasCompilationErrors || result.HasErrors) 
                    ? $"there were errors in your code : ```\n{code_output}```" 
                    : $"code output : ```\n{code_output}```"
                };

                client.SendMessage(message.Channel.Id, "", false, embed);
            }

            dotnetfiddle code_executor = new dotnetfiddle();
        }
    }
}
