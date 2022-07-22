using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class Prices_Blank
{
    public static async Task Prices(ITelegramBotClient client, Message message, CancellationToken token,string prices_blank,string gotomenu)
    {
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "prices.pdf");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:prices_blank,
            photo: stream,
            cancellationToken: token);

        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: gotomenu,
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.GotoMenu.Values.ToArray(), 2),
            parseMode: ParseMode.Html,
            cancellationToken: token);  
    }
}     