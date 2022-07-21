using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class BMWModelsForInfo
{
    public static async Task BMW_Models_ForInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "BMW",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.BMWTypesForInfo.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);         
    }  
}