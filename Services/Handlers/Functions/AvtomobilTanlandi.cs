using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class CarSelected
{
    private async Task WriteAvtomobilTanlandi(ITelegramBotClient client, Message message, CancellationToken token,string car_selected,string send_number,string phone_number)
    {
        var from = message.From;
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: car_selected,
                parseMode: ParseMode.Html,
                cancellationToken: token);  
        await GetNumber.Number(client, message, token,send_number,phone_number);                     
    }
}    