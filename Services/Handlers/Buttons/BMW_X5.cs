
using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class BMW_X5
{
    public static async Task BMWX5Info(ITelegramBotClient client, Message message, CancellationToken token,string bmw_x5,string gotomenu)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://cdni.autocarindia.com/Utils/ImageResizer.ashx?n=http://cms.haymarketindia.net/model/uploads/modelimages/X5ModelImage.jpg&w=373&h=245&q=75&c=1",
                caption: "BMW X5\n40.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "bmw-x5.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:bmw_x5,
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