using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class KIA_Sorento
{
    public static async Task KiaSorentoInfo(ITelegramBotClient client, Message message, CancellationToken token,string kia_sorento,string gotomenu)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://www.ccarprice.com/products/Kia-Sorento-LX-2021.jpg",
                caption: "Kia Sorento\n13.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "sorento.pdf");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:kia_sorento,
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