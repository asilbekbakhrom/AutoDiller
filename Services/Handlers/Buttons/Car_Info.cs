
using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class CarInfo
{
    public static async Task Car_Info(ITelegramBotClient client, Message message, CancellationToken token, string choose_brand)
    {
       var from = message.From;
        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: choose_brand,
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.CarNamesForInfo.Values.ToArray(), 2),
            parseMode: ParseMode.Html,
            cancellationToken: token);
}   
} 