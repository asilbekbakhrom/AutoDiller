using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace bot.Services;

public partial class BotUpdateHandler
{
    private static async Task GetNumber(ITelegramBotClient botClient, Message message, CancellationToken token)
    {
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Raqamni jo'nating",
            replyMarkup: CreateContactRequestButton("Telefon raqamni ulashish📱"));

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