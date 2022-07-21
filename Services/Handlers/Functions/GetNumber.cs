
//     }

using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

public partial class GetNumber
{
    public static async Task Number(ITelegramBotClient botClient, Message message, CancellationToken token, string send_number, string phone_number)
    {
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: send_number,
            replyMarkup: CreateContactRequestButton(phone_number));         
    }


    public static ReplyKeyboardMarkup CreateContactRequestButton(string title)
    {
        ReplyKeyboardMarkup replyKeyboardMarkup = new(
            new[]
            {
                KeyboardButton.WithRequestContact(title),
            })
            {
                ResizeKeyboard = true
            };

        return replyKeyboardMarkup;
}
}