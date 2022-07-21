using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class TeslaModelXPlaid
{
    public static async Task TeslaModelXPlaidInfo(ITelegramBotClient client, Message message, CancellationToken token,string tesla_model_x_plaid_technical_feathures, string gotomenu)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://www.ccarprice.com/products/Tesla_Model_X_Plaid_2022_1.jpg",
                caption: "Tesla Model X Plaid\n80.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "model-x-plaid.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:tesla_model_x_plaid_technical_feathures,
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