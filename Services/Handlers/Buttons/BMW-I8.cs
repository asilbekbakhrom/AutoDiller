
using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class BMW_I8
{
    public static async Task BMWI8Info(ITelegramBotClient client, Message message, CancellationToken token,string bmw_i8,string gotomenu)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://cache1.pakwheels.com/system/car_generation_pictures/6403/original/BMW_i8_Front.jpg?1650871814",
                caption: "BMW I8\n50.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "bmw-i8.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:bmw_i8,
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