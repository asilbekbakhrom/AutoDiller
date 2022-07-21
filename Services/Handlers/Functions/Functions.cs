
using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class Functions
{
    public static async Task Function(ITelegramBotClient client, Message message, CancellationToken token, string what_do_you_want)
    {
        var from = message.From;
        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: what_do_you_want,
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.FunctionsNames.Values.ToArray(), 4),
            parseMode: ParseMode.Html,
            cancellationToken: token);   
    }   
}