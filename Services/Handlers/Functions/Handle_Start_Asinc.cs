
using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;

public partial class HandleStartAsync
{
    public static async Task Start(ITelegramBotClient client, Message message, CancellationToken token,string greeting)
    {
        var from = message.From;
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: greeting,
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.LanguageNames.Values.ToArray(), 3),
                cancellationToken: token);       
    }     
}
