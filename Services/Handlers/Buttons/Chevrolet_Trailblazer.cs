using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class ChevroletTrailblazer
{
    public static async Task ChevroletTrailblazerInfo(ITelegramBotClient client, Message message, CancellationToken token, string chevrolet_trailblazer, string gotomenu)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://inv.assets.sincrod.com/RTT/Chevrolet/2022/5611373/default/ext_GAZ_deg02.jpg",
                caption: "Chevrolet Trailblazer\n5.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "trailblazer.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:chevrolet_trailblazer,
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