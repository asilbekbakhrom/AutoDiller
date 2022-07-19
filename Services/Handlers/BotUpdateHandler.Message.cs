using System.Web;
using bot.Helpers;
using bot.Constants;
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
            MessageType.Contact => HandleContactMessage(client, message, token),
            _ => HandleUnknownMessageAsync(client, message, token)
        };
        
        await handler;
    }

    private async Task HandleContactMessage(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Tez orada bizning operatorlar siz bilan bog'lanadi",
            cancellationToken: token);
        
        await client.SendTextMessageAsync(
            chatId: -1001775613010,
            text: $"Yangi klient ro`yxatdan o`tdi ismi: {message.Chat.FirstName} uning telefon raqami : {message.Contact.PhoneNumber}",
            cancellationToken: token
        );
        
        await Functions(client, message, token);
    }

    private Task HandleUnknownMessageAsync(ITelegramBotClient client, Message message, CancellationToken token)
    {
        _logger.LogInformation("Received message type {message.Type}", message.Type);

        return Task.CompletedTask;
    }

    private async Task HandleTextMessageAsync(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;
        _logger.LogInformation("From: {from.Firstname} ChatId: {message.Chat.Id}", from?.FirstName, message.Chat.Id);

        var handler = message.Text switch
        {
            "/start" => HandleStartAsync(client, message, token),
            "O'zbekcha" or "Русский" or "English" => Functions(client, message, token),
            // "Tesla" or "Hyundai" or "KIA" or "Chevrolet" or "BMW"  => Functions(client, message, token),
            "Test Drive dan o'tish" => TestDrive(client, message, token),
            "Shartnoma imzolash" => CarBuy(client,message,token),
            "Tesla" => TeslaModels(client, message,token),
            "Hyundai" => HyundaiModels(client, message,token),
            "KIA" => KIAModels(client, message,token),
            "Chevrolet" => ChevroletModels(client, message,token),
            "BMW" => BMWModels(client, message,token),
            "Tesla " => TeslaModelsForBuy(client, message,token),
            "Hyundai " => HyundaiModelsForBuy(client, message,token),
            "KIA " => KIAModelsForBuy(client, message,token),
            "Chevrolet " => ChevroletModelsForBuy(client, message,token),
            "BMW " => BMWModelsForBuy(client, message,token),
            "KIA Soul🚗" or "KIA K5🚗" or "KIA Niro🚗" or "KIA Sorento🚗"or"Hyundai Sonata🚗"or "Hyundai Elantra🚗" or
            "Hyundai Sontafe🚗"or"Hyundai Tucson🚗"or"Chevrolet Comaro🚗"or"Chevrolet Malibu🚗"or"Chevrolet Trailblazer🚗"
            or"Chevrolet Tahoe🚗"or"BMW X5🚗"or"BMW M5🚗"or"BMW I8🚗"or"BMW M4🚗"or"Tesla Model X🚗"or"Tesla Model 3🚗"or"Tesla Model Y🚗"
            or"Tesla Cyber Truck " => WriteAvtomobilTanlandiForBuy(client,message,token),
             "KIA Soul" or "KIA K5" or "KIA Niro" or "KIA Sorento"or"Hyundai Sonata"or "Hyundai Elantra" or
            "Hyundai Sontafe"or"Hyundai Tucson"or"Chevrolet Comaro"or"Chevrolet Malibu"or"Chevrolet Trailblazer"
            or"Chevrolet Tahoe"or"BMW X5"or"BMW M5"or"BMW I8"or"BMW M4"or"Tesla Model X"or"Tesla Model 3"or"Tesla Model Y"
            or"Tesla Cyber Truck" => WriteAvtomobilTanlandi(client,message,token),
            "ortga" => BackHome(client, message, token),
            // "🏠Bosh Menu" => 

            _ => Task.CompletedTask
        };
         
        await handler;
        
    }

    private async Task BackHome(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await Functions(client, message, token);
    }

    private async Task CarBuy(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;
        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: _localizer["choose_brand"],
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.CarNames.Values.ToArray(), 3),
            parseMode: ParseMode.Html,
            cancellationToken: token);
    }

    private async Task WriteAvtomobilTanlandi(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;
         await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Avtomobil Tanlandi",
                parseMode: ParseMode.Html,
                cancellationToken: token);  
        await GetNumber(client, message, token);
        

        

            // var send_channel = await client.SendTextMessageAsync("https://t.me/numberclients", number);   
            // await client.SendTextMessageAsync(
            //     chatId: message.Chat.Id,
            //     text: send_channel,
            //     parseMode: ParseMode.Html,
            //     cancellationToken: token);       
        // await client.SendPhotoAsync(
        //     message.Chat.Id,
        //     photo: "https://templatelab.com/wp-content/uploads/2017/08/vehicle-purchase-agreement-04.jpg",
        //     caption: "Kelishuv qog'ozini to'ldirib bizning avtosalonga keling var Test Drive Qiling",
        //     replyMarkup: new ReplyKeyboardRemove(),
        //     cancellationToken: token); 

        await GetNumber(client, message, token);
        //  await client.SendTextMessageAsync(
        //         chatId: message.Chat.Id,
        //         text: "🏠Bosh Menu",
        //         replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.Menu.Values.ToArray(), 2),
        //         parseMode: ParseMode.Html,
        //         cancellationToken: token);                           
    }
    private async Task WriteAvtomobilTanlandiForBuy(ITelegramBotClient client, Message message, CancellationToken token)
    {
        // var from = message.From;
        //  await client.SendTextMessageAsync(
        //         chatId: message.Chat.Id,
        //         text: "Avtomobil Tanlandi",
        //         parseMode: ParseMode.Html,
        //         cancellationToken: token);  
        
        await GetNumber(client, message, token);
        var number = message.Text;
        var s = await client.SendTextMessageAsync("https://t.me/numberclients", number);
        //  await client.SendTextMessageAsync(
        //         chatId: message.Chat.Id,
        //         text: "🏠Bosh Menu",
        //         replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.Menu.Values.ToArray(), 2),
        //         parseMode: ParseMode.Html,
        //         cancellationToken: token);                           
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
    private async Task BMWModelsForBuy(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "BMW turlari",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.BMWTypesForBuy.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);           
    }
    private async Task ChevroletModelsForBuy(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Chevrolet turlari",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.ChevroletTypesForBuy.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);       
    }

    private async Task KIAModelsForBuy(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "KIA turlari",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.KIATypesForBuy.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);       
    }

    private async Task HyundaiModelsForBuy(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Hyundai turlari",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.HyundaiTypesForBuy.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);          
    }

    private async Task TeslaModelsForBuy(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Tesla turlari",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.TeslaTypesForBuy.Values.ToArray(), 2),
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