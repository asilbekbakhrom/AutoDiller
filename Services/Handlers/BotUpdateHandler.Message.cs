using System.Web;
using bot.Constants;
using bot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace bot.Services;

public partial class BotUpdateHandler
{
    private async Task HandleMessageAsync(ITelegramBotClient client, Message? message, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(message);

        var from = message.From;
        _logger.LogInformation("Received message from {from.Firstname}", from?.FirstName);

        var handler = message.Type switch
        {
            MessageType.Text => HandleTextMessageAsync(client, message, token),
            _ => HandleUnknownMessageAsync(client, message, token)
        };
        
        await handler;
    }

    private Task HandleUnknownMessageAsync(ITelegramBotClient client, Message message, CancellationToken token)
    {
        _logger.LogInformation("Received message type {message.Type}", message.Type);

        return Task.CompletedTask;
    }

    private async Task HandleTextMessageAsync(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;
        _logger.LogInformation("From: {from.Firstname}", from?.FirstName);

        var handler = message.Text switch
        {
            "/start" => HandleStartAsync(client, message, token),
            "O'zbekcha" or "Русский" or "English" => Functions(client, message, token),
            // "Tesla" or "Hyundai" or "KIA" or "Chevrolet" or "BMW"  => Functions(client, message, token),
            "Test Drive dan o'tish" => TestDrive(client, message, token),
            // "Shartnoma online ariza" => CarBuy(client,message,token),
            "Tesla" => TeslaModels(client, message,token),
            "Hyundai" => HyundaiModels(client, message,token),
            "KIA" => KIAModels(client, message,token),
            "Chevrolet" => ChevroletModels(client, message,token),
            "BMW" => BMWModels(client, message,token),
            "KIA Soul" or "KIA K5" or "KIA Niro" or "KIA Sorento"or"Hyundai Sonata"or "Hyundai Elantra" or
            "Hyundai Sontafe"or"Hyundai Tucson"or"Chevrolet Comaro"or"Chevrolet Malibu"or"Chevrolet Trailblazer"
            or"Chevrolet Tahoe"or"BMW X5"or"BMW M5"or"BMW I8"or"BMW M4"or"Tesla Model X"or"Tesla Model 3"or"Tesla Model Y"
            or"Tesla Cyber Truck" => WriteAvtomobilTanlandi(client,message,token),
            // "🏠Bosh Menu" => 
            _ => Task.CompletedTask
        };
         
        await handler;
        
    }

    // private async Task CarBuy(ITelegramBotClient client, Message message, CancellationToken token)
    // {
    //     var from = message.From;
    //     await client.SendTextMessageAsync(
    //         chatId: message.Chat.Id,
    //         text: _localizer["choose_brand"],
    //         replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.CarNames.Values.ToArray(), 3),
    //         parseMode: ParseMode.Html,
    //         cancellationToken: token);
    // }

    private async Task WriteAvtomobilTanlandi(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;
         await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Avtomobil Tanlandi",
                parseMode: ParseMode.Html,
                cancellationToken: token);  
        await client.SendPhotoAsync(
            message.Chat.Id,
            photo: "https://dealersupply.com/wp-content/uploads/2016/02/DS-582-2022-scaled.jpg",
            caption: "Kelishuv qog'ozini to'ldirib bizning avtosalonga keling var Test Drive Qiling",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token); 
         await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "🏠Bosh Menu",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.Menu.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);                           
    }

    private async Task BMWModels(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "BMW turlari",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.BMWTypes.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);           
    }

    private async Task ChevroletModels(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Chevrolet turlari",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.ChevroletTypes.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);       
    }

    private async Task KIAModels(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "KIA turlari",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.KIATypes.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);       
    }

    private async Task HyundaiModels(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Hyundai turlari",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.HyundaiTypes.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);          
    }

    private async Task TeslaModels(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Tesla turlari",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.TeslaTypes.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);         
    }

    private async Task Functions(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;
        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Nima Qilmoqchisiz:",
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.FunctionsNames.Values.ToArray(), 4),
            parseMode: ParseMode.Html,
            cancellationToken: token);   
    }

    private async Task TestDrive(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;
        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: _localizer["choose_brand"],
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.CarNames.Values.ToArray(), 3),
            parseMode: ParseMode.Html,
            cancellationToken: token);                
    }

    private async Task HandleLanguageAsync(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var cultureString = StringConstants.LanguageNames.FirstOrDefault(v => v.Value == message.Text).Key;
        await _userService.UpdateLanguageCodeAsync(message?.From?.Id, cultureString); 
        var from = message.From;
        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: _localizer["choose_brand"],
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.CarNames.Values.ToArray(), 3),
            parseMode: ParseMode.Html,
            cancellationToken: token);
    }

    private async Task HandleStartAsync(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: _localizer["greeting"],
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.LanguageNames.Values.ToArray(), 3),
                parseMode: ParseMode.Html,
                cancellationToken: token);       
    }
}