using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DiscordMusicBot.Slash_Commands
{
    public class FunSL : ApplicationCommandModule
    {
        [SlashCommand("test", "Bu ilk Slash Komutu")]
        public async Task TestSlashCommand(InteractionContext ctx, [Choice("Pre-Defined Text", "Selam")]
                                                                   [Option("string","İstediğin tipte bir şey yaz")] string text)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                                                                                            .WithContent("Starting Slash Command...."));

            var embedMessage = new DiscordEmbedBuilder()
            {
                Title = text,
            };

            await ctx.Channel.SendMessageAsync(embed: embedMessage);
        }



        [SlashCommand("anket", "Anket oluştur.")]
        public async Task PollCommand(InteractionContext ctx, [Option("Soru", "Anket Başlığı")] string Question,
                                                              [Option("TimeLimit", "Bekleme Süresi")] long TimeLimit,
                                                              [Option("1.Seçenek", "1.Seçenek")] string Option1,
                                                              [Option("2.Seçenek", "2.Seçenek")] string Option2,
                                                              [Option("3.Seçenek", "3.Seçenek")] string Option3,
                                                              [Option("4.Seçenek", "4.Seçenek")] string Option4)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("..."));




            var interactvity = ctx.Client.GetInteractivity(); // Kullanıcıdan değer alındığı kısım.
            TimeSpan timer = TimeSpan.FromSeconds(TimeLimit); // Sayacın oluşturulduğu kısım.

            DiscordEmoji[] optionEmojis ={ DiscordEmoji.FromName(ctx.Client, ":one:", false),
                                           DiscordEmoji.FromName(ctx.Client, ":two:", false),
                                           DiscordEmoji.FromName(ctx.Client, ":three:", false),
                                           DiscordEmoji.FromName(ctx.Client, ":four:", false)};


            string optionStrings = optionEmojis[0] + " | " + Option1 + "\n" +
                                   optionEmojis[1] + " | " + Option2 + "\n" +
                                   optionEmojis[2] + " | " + Option3 + "\n" +
                                   optionEmojis[3] + " | " + Option4;

            var pollMessage = new DiscordEmbedBuilder()
            {
                Title = " " + Question,
                Description = optionStrings,
                Color = DiscordColor.Azure
            };

            var putReactOn = await ctx.Channel.SendMessageAsync(pollMessage);

            foreach (var emoji in optionEmojis)
            {
                await putReactOn.CreateReactionAsync(emoji);
            }

            var result = await interactvity.CollectReactionsAsync(putReactOn, timer);

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;

            foreach (var emoji in result)
            {
                if (emoji.Emoji == optionEmojis[0])
                {
                    count1++;
                }

                if (emoji.Emoji == optionEmojis[1])
                {
                    count2++;
                }

                if (emoji.Emoji == optionEmojis[2])
                {
                    count3++;
                }

                if (emoji.Emoji == optionEmojis[3])
                {
                    count4++;
                }
            }

            int totalVotes = count1 + count2 + count3 + count4;


            string resultString = optionEmojis[0] + ":  " + count1 + " Oy \n" +
                optionEmojis[1] + ":  " + count2 + " Oy \n" +
                optionEmojis[2] + ":  " + count3 + " Oy \n" +
                optionEmojis[3] + ":  " + count4 + " Oy \n\n" + "Toplam oy sayısı : " + totalVotes;

            var resultMessage = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Green,
                Title = "Oylamanın sonucu",
                Description = resultString

            };

            await ctx.Channel.SendMessageAsync(resultMessage);

        }
    }
}



 