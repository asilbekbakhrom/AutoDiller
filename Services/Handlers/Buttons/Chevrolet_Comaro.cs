
using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class ChevroletComaro
{
    
    public static async Task ChevroletComaroInfo(ITelegramBotClient client, Message message, CancellationToken token,string chevrolet_comaro,string gotomenu)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://app.conciergetravel.am/storage/eYB5jIugGuXdraFjSwg5OvBjtbFosd5yZR1qI7AG.jpg",
                caption: "Chevrolet Comaro\n20.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "comaro.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:chevrolet_comaro,
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