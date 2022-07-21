using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class HyundaiModels
{
    public static async Task Hyundai_Models(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Hyundai",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.HyundaiTypes.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);         
    }  
}