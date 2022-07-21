using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class BackHome
{
    public static async Task Back(ITelegramBotClient client, Message message, CancellationToken token,string what_do_you_want)
    {
        await Functions.Function(client, message, token,what_do_you_want);
    }
}