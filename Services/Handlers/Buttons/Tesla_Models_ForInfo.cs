using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class TeslaModelsForInfo
{
    public static async Task Tesla_Models_ForInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Tesla",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.TeslaTypesForInfo.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);         
    }  
}
