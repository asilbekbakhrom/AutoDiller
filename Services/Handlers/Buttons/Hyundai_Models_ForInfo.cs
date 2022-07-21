using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class HyundaiModelsForInfo
{
    public static async Task Hyundai_Models_ForInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Hyundai",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.HyundaiTypesForInfo.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);         
    }  
}