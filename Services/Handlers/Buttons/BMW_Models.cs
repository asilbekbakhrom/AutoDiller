using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class BMW_Models
{
    public static async Task BMWModels(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Chevrolet",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.BMWTypes.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);         
    }  
}