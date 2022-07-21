using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class TeslaModelX
{
    public static async Task TeslaModelXInfo(ITelegramBotClient client, Message message, CancellationToken token,string tesla_model_x_technical_feathures,string gotomenu)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://file.kelleybluebookimages.com/kbb/base/evox/StJ/11190/2017-Tesla-Model%20X-front-passenger-angle_11190_159_640x480.jpg",
                caption: "Tesla Model X\n65.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "model-x.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:tesla_model_x_technical_feathures,
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