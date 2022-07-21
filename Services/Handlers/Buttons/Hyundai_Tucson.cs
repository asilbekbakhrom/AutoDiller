using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class HyundaiTucson
{
    public static async Task HyundaiTucsonInfo(ITelegramBotClient client, Message message, CancellationToken token,string hyundai_tucson,string gotomenu)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://imgd.aeplcdn.com/1056x594/n/cw/ec/39082/tucson-exterior-right-front-three-quarter.jpeg?q=75&wm=1",
                caption: "Hyundai Tucson\n8.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "tucson.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:hyundai_tucson,
            photo: stream,
            cancellationToken: token); 

        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: gotomenu,
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.GotoMenu.Values.ToArray(), 5),
            parseMode: ParseMode.Html,
            cancellationToken: token);         
    }         
}      