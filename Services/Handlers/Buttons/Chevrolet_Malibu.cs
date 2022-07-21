using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class ChevroletMalibu
{
    public static async Task ChevroletMalibuInfo(ITelegramBotClient client, Message message, CancellationToken token,string chevrolet_malibu,string gotomenu)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://www.ccarprice.com/products/Chevrolet_Malibu_LT_2.4L.jpg",
                caption: "Chevrolet Malibu\n2.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "malibu.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:chevrolet_malibu,
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