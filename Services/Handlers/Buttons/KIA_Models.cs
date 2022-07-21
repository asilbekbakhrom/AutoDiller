using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class KIAModels
{
    public static async Task KIA_Models(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "KIA",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.KIATypes.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);         
    }  
}