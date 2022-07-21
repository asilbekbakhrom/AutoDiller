
using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class ChevroletTahoe
{
    public static async Task ChevroletTahoeInfo(ITelegramBotClient client, Message message, CancellationToken token,string chevrolet_tahoe,string gotomenu)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://di-uploads-pod1.dealerinspire.com/dalebenetchevy/uploads/2022/01/2022-Chevy-Tahoe-white-728x400.jpg",
                caption: "Chevrolet Tahoe\n7.500$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "tahoe.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:chevrolet_tahoe,
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