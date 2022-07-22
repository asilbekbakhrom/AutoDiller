using bot.Helpers;
using bot.Constants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Globalization;

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
            text: _localizer["connect-with-operator"],
            parseMode: ParseMode.Html,
            cancellationToken: token);  

        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: _localizer["gotomenu"],
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.GotoMenu.Values.ToArray(), 5),
            parseMode: ParseMode.Html,
            cancellationToken: token);  

        await client.SendTextMessageAsync(
            chatId: -1001775613010,
            text: $"Yangi klient ro`yxatdan o`tdi ismi: {message.Chat.FirstName} uning telefon raqami : {message.Contact.PhoneNumber}",
            cancellationToken: token
        );
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
            "/start" => HandleStartAsync.Start(client, message, token,_localizer["greeting", message.Chat.FirstName]),
            "O'zbekcha🇺🇿" or "Русский🇷🇺" or "English🏴󠁧󠁢󠁥󠁮󠁧󠁿" => HandleLanguageAsync(client,message,token),
            "Biz haqimizda" or "О нас" or "About Us" => AboutUs.AboutBot(client,message,token,_localizer["gotomenu"],_localizer["about-us"]),
            "Test Drive" or "Тест Драйв" => TestDrive.Test(client,message,token,_localizer["choose_brand"]),
            "Shartnoma imzolash" or "Подписать котракт" or "Signing the contract" => CarBuy.Car_Buy(client,message,token,_localizer["choose_brand"]),
            "Avtomobil haqidagi ma`lumot" or "Информация о машине" or "Information about car" => CarInfo.Car_Info(client,message,token,_localizer["choose_brand"]),
            "Avtomobil Narxlari" or "Цены автомобилей" or "Car Prices" => Prices_Blank.Prices(client,message,token,_localizer["prices-blank"],_localizer["gotomenu"]),
            "Change Language" or "Tilni o'zgartirish" or "Изменить язык" => HandleStartAsync.Start(client, message, token,_localizer["greeting", message.Chat.FirstName]),
            "Tesla" => TeslaModels.Tesla_Models(client, message,token),
            "Hyundai" => HyundaiModels.Hyundai_Models(client, message,token),
            "KIA" => KIAModels.KIA_Models(client,message,token),
            "Chevrolet" => Chevrolet_Models.CheroletModels(client, message,token),
            "BMW" => BMW_Models.BMWModels(client, message,token),
            "Tesla Inc" => TeslaModelsForInfo.Tesla_Models_ForInfo(client, message,token),
            "Hyundai Inc" => HyundaiModelsForInfo.Hyundai_Models_ForInfo(client, message,token),
            "KIA Inc" => KIAModelsForInfo.KIA_Models_ForInfo(client, message,token),
            "Chevrolet Inc" => ChevroletModelsInfo.Chevrolet_Models_ForInfo(client, message,token),
            "BMW Inc" => BMWModelsForInfo.BMW_Models_ForInfo(client, message,token),
            "🏠" => Back(client,message,token),
            "KIA Soul" or "KIA K5" or "KIA Niro" or "KIA Sorento"or"Hyundai Sonata"or "Hyundai Elantra" or
            "Hyundai Sontafe"or"Hyundai Tucson"or"Chevrolet Comaro"or"Chevrolet Malibu"or"Chevrolet Trailblazer"
            or"Chevrolet Tahoe"or"BMW X5"or"BMW M5"or"BMW I8"or"BMW M4"or"Tesla Model X"or"Tesla Model 3"or"Tesla Model Y"
            or"Tesla Cyber Truck" => WriteAvtomobilTanlandi(client,message,token),
            "KIA Soul Info" => KIA_Soul.KiaSoulInfo(client,message,token,_localizer["kia-soul"],_localizer["gotomenu"]),
            "KIA K5 Info" => KIA_K5.KiaK5Info(client,message,token,_localizer["kia-k5"],_localizer["gotomenu"]), 
            "KIA Stringer Info" => KIA_Stringer.KiaStringerInfo(client,message,token,_localizer["kia-stringer"],_localizer["gotomenu"]),
            "KIA Sorento Info" => KIA_Sorento.KiaSorentoInfo(client,message,token,_localizer["kia_sorento"],_localizer["gotomenu"]),
            "Hyundai Sonata Info" => HyundaiSonata.HyundaiSonataForInfo(client,message,token,_localizer["hyundai-sonata"],_localizer["gotomenu"]),
            "Hyundai Elantra Info" => HyundaiElantra.HyundaiElantraInfo(client,message,token,_localizer["hyundai-elantra"],_localizer["gotomenu"]),
            "Hyundai Sontafe Info" => HyundaiSontafe.HyundaiSontafeInfo(client,message,token,_localizer["hyundai-sontafe"],_localizer["gotomenu"]), 
            "Hyundai Tucson Info" => HyundaiTucson.HyundaiTucsonInfo(client,message,token,_localizer["hyundai-tucson"],_localizer["gotomenu"]),
            "Chevrolet Comaro Info" => ChevroletComaro.ChevroletComaroInfo(client,message,token,_localizer["chevrolet-comaro"],_localizer["gotomenu"]),
            "Chevrolet Malibu Info" => ChevroletMalibu.ChevroletMalibuInfo(client,message,token,_localizer["chevrolet-malibu"],_localizer["gotomenu"]), 
            "Chevrolet Trailblazer Info" => ChevroletTrailblazer.ChevroletTrailblazerInfo(client,message,token,_localizer["chevrolet-trailblzer"],_localizer["gotomenu"]),
            "Chevrolet Tahoe Info" => ChevroletTahoe.ChevroletTahoeInfo(client,message,token,_localizer["chevrolet-tahoe"],_localizer["gotomenu"]),
            "BMW X5 Info" => BMW_X5.BMWX5Info(client,message,token,_localizer["bmw-x5"],_localizer["gotomenu"]),
            "BMW M5 Info" => BMW_M5.BMWM5Info(client,message,token,_localizer["bmw-m5"],_localizer["gotomenu"]),
            "BMW I8 Info" => BMW_I8.BMWI8Info(client,message,token,_localizer["bmw-i8"],_localizer["gotomenu"]), 
            "BMW M4 Info" => BMW_M4.BMWM4Info(client,message,token,_localizer["bmw-m4"],_localizer["gotomenu"]),
            "Tesla Model X Info" => TeslaModelX.TeslaModelXInfo(client,message,token,_localizer["tesla-model-x-technical-feathures"],_localizer["gotomenu"]),
            "Tesla Model 3 Info" => TeslaModel3.TeslaModel3Info(client,message,token,_localizer["tesla-model-3-technical-feathures"],_localizer["gotomenu"]),
            "Tesla Model Y Info" => TeslaModelY.TeslaModelYInfo(client,message,token,_localizer["tesla-model-y-technical-feathures"],_localizer["gotomenu"]), 
            "Tesla Model X Plaid Info" => TeslaModelXPlaid.TeslaModelXPlaidInfo(client,message,token,_localizer["tesla-model-x-plaid-technical-feathures"],_localizer["gotomenu"]),
            _ => Task.CompletedTask
        };
         
        await handler;
        
    }
    

    private async Task WriteAvtomobilTanlandi(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: _localizer["car-selected"],
                parseMode: ParseMode.Html,
                cancellationToken: token);  
        await GetNumber.Number(client, message, token,_localizer["send-number"],_localizer["phone-number"]);                     
    }
    public Dictionary<string, string> FunctionsNames => new()
    {
        { "td", _localizer["test-drive"] },
        { "soa", _localizer["propose"] },
        { "at", _localizer["car-informations"] },
        { "nj", _localizer["prices"] },
        { "chl", _localizer["change-language"] },
        {"us",_localizer["about-us"]}
    };
    private async Task HandleLanguageAsync(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var cultureString = StringConstants.LanguageNames.FirstOrDefault(v => v.Value == message.Text).Key;
        await _userService.UpdateLanguageCodeAsync(message?.From?.Id, cultureString);
        CultureInfo.CurrentCulture=new CultureInfo(cultureString);
        CultureInfo.CurrentUICulture=new CultureInfo(cultureString);
        
        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: _localizer["language-selected"],
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(FunctionsNames.Values.ToArray(), 2),
            cancellationToken: token);
    }
    private async Task Function(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;
        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: _localizer["what-do-you-want"],
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(FunctionsNames.Values.ToArray(), 4),
            parseMode: ParseMode.Html,
            cancellationToken: token);   
    } 
    private async Task Back(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await Function(client, message, token);
    }
}    