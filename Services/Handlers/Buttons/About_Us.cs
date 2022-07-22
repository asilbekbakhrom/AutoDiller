using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public partial class AboutUs
{
    public static async Task AboutBot(ITelegramBotClient botClient, Message message, CancellationToken token,string gotomenu,string AboutUs)
    {
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: AboutUs,
            parseMode: ParseMode.Html,
            cancellationToken: token);  
        
          await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: gotomenu,
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.GotoMenu.Values.ToArray(), 2),
            parseMode: ParseMode.Html,
            cancellationToken: token);         
    }
} 
