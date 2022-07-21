
using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class HyundaiElantra
{
    public static async Task HyundaiElantraInfo(ITelegramBotClient client, Message message, CancellationToken token,string hyundai_elantra,string gotomenu)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://stimg.cardekho.com/images/carexteriorimages/630x420/Hyundai/Hyundai-Elantra-2012-2015/4026/1561179105571/front-left-side-47.jpg?imwidth=420&impolicy=resize",
                caption: "Hyundai Elantra\n12.500$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "Elantra.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:hyundai_elantra,
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