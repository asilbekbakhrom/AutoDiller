
using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class KIA_Stringer
{
    public static async Task KiaStringerInfo(ITelegramBotClient client, Message message, CancellationToken token,string kia_stringer,string gotomenu)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://www.avtogermes.ru/images/marks/kia/stinger/i-restajling/colors/swp/3d63e05b572c6f130050bb63ecad5792.png",
                caption: "Kia Stringer\n12.500$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "stringer.pdf");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:kia_stringer,
            photo: stream,
            cancellationToken: token);

        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text:gotomenu,
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.GotoMenu.Values.ToArray(), 5),
            parseMode: ParseMode.Html,
            cancellationToken: token);         
    }
     
}     