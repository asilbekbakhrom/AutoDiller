using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class TestDrive
{
    public static async Task Test(ITelegramBotClient botClient, Message message, CancellationToken token,string choose_brand)
    {
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: choose_brand,
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.CarNames.Values.ToArray(), 2),
            parseMode: ParseMode.Html,
            cancellationToken: token);         
    }
} 
