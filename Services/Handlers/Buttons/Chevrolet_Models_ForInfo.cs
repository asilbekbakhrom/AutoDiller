using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class ChevroletModelsInfo
{
    public static async Task Chevrolet_Models_ForInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Chevrolet",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.ChevroletTypesForInfo.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);         
    }  
}