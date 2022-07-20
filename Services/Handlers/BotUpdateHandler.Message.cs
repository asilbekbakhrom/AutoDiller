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
            "O'zbekcha" or "Русский" or "English" => HandleLanguageAsync(client,message,token),
            // "Tesla" or "Hyundai" or "KIA" or "Chevrolet" or "BMW"  => Functions(client, message, token),
            "Test Drive" => TestDrive(client, message, token),
            "📄✍️" => CarBuy(client,message,token),
            "🚘ℹ️" => CarInfo(client,message,token),
            "💲" => Prices(client,message,token),
            "Tesla" => TeslaModels(client, message,token),
            "Hyundai" => HyundaiModels(client, message,token),
            "KIA" => KIAModels(client, message,token),
            "Chevrolet" => ChevroletModels(client, message,token),
            "BMW" => BMWModels(client, message,token),
            "Tesla Inc" => TeslaModelsForInfo(client, message,token),
            "Hyundai Inc" => HyundaiModelsForInfo(client, message,token),
            "KIA Inc" => KIAModelsForInfo(client, message,token),
            "Chevrolet Inc" => ChevroletModelsInfo(client, message,token),
            "BMW Inc" => BMWModelsForInfo(client, message,token),
            "KIA Soul" or "KIA K5" or "KIA Niro" or "KIA Sorento"or"Hyundai Sonata"or "Hyundai Elantra" or
            "Hyundai Sontafe"or"Hyundai Tucson"or"Chevrolet Comaro"or"Chevrolet Malibu"or"Chevrolet Trailblazer"
            or"Chevrolet Tahoe"or"BMW X5"or"BMW M5"or"BMW I8"or"BMW M4"or"Tesla Model X"or"Tesla Model 3"or"Tesla Model Y"
            or"Tesla Cyber Truck" => WriteAvtomobilTanlandi(client,message,token),
            "KIA Soul Info" => KiaSoulInfo(client,message,token),
            "KIA K5 Info" => KiaK5Info(client,message,token), 
            "KIA Stringer Info" => KiaStringerInfo(client,message,token),
            "KIA Sorento Info" => KiaSorentoInfo(client,message,token),
            "Hyundai Sonata Info" => HyundaiSonataForInfo(client,message,token),
            "Hyundai Elantra Info" => HyundaiElantraInfo(client,message,token),
            "Hyundai Sontafe Info" => HyundaiSontafeInfo(client,message,token), 
            "Hyundai Tucson Info" => HyundaiTucsonInfo(client,message,token),
            "Chevrolet Comaro Info" => ChevroletComaroInfo(client,message,token),
            "Chevrolet Malibu Info" => ChevroletMalibuInfo(client,message,token), 
            "Chevrolet Trailblazer Info" => ChevroletTrailblazerInfo(client,message,token),
            "Chevrolet Tahoe Info" => ChevroletTahoeInfo(client,message,token),
            "BMW X5 Info" => BMWX5Info(client,message,token),
            "BMW M5 Info" => BMWM5Info(client,message,token),
            "BMW I8 Info" => BMWI8Info(client,message,token), 
            "BMW M4 Info" => BMWM4Info(client,message,token),
            "Tesla Model X Info" => TeslaModelXInfo(client,message,token),
            "Tesla Model 3 Info" => TeslaModel3Info(client,message,token),
            "Tesla Model Y Info" => TeslaModelYInfo(client,message,token), 
            "Tesla Model X Plaid Info" => TeslaModelXPlaidInfo(client,message,token),
            _ => Task.CompletedTask
        };
         
        await handler;
        
    }

    private async Task Prices(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "prices.pdf");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Narxlar blank listi",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);
    }

    private async Task KiaSoulInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://cars.usnews.com/pics/size/776x517/images/Auto/izmo/i159614707/2022_kia_soul_angularfront.jpg",
                caption: "Kia Soul\n15.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "soul.pdf");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Kia Soul texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);
    }
    private async Task KiaK5Info(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://www.ccarprice.com/products/Kia_K5_GT_DCT_2022.jpg",
                caption: "Kia K5\n17.500$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "K5.pdf");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Kia K5 texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);
    }
        private async Task KiaStringerInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://www.avtogermes.ru/images/marks/kia/stinger/i-restajling/colors/swp/3d63e05b572c6f130050bb63ecad5792.png",
                caption: "Kia Stringer\n12.500$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "stringer.pdf");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Kia Stringer texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);
    }

    private async Task KiaSorentoInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://www.ccarprice.com/products/Kia-Sorento-LX-2021.jpg",
                caption: "Kia Sorento\n13.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "sorento.pdf");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Kia Sorento texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);
    }
        private async Task HyundaiSonataForInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://www.ccarprice.com/products/Hyundai_Sonata_Hybrid_SEL_2021.jpg",
                caption: "Hyundai Sonata\n10.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "sonata.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Hyundai Sonata texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);
    }
    
    private async Task HyundaiElantraInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://stimg.cardekho.com/images/carexteriorimages/630x420/Hyundai/Hyundai-Elantra-2012-2015/4026/1561179105571/front-left-side-47.jpg?imwidth=420&impolicy=resize",
                caption: "Hyundai Elantra\n12.500$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "Elantra.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Hyundai Elantra texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);
    }
    
    private async Task HyundaiSontafeInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://di-enrollment-api.s3.amazonaws.com/hyundai/models/2021/santa-fe/trims/SEL.jpg",
                caption: "Hyundai Santafe\n8.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "santafe.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Hyundai Sontafe texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);
    }

    private async Task HyundaiTucsonInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://imgd.aeplcdn.com/1056x594/n/cw/ec/39082/tucson-exterior-right-front-three-quarter.jpeg?q=75&wm=1",
                caption: "Hyundai Tucson\n8.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "tucson.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Hyundai Tucson texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);    
    }

    private async Task ChevroletComaroInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://app.conciergetravel.am/storage/eYB5jIugGuXdraFjSwg5OvBjtbFosd5yZR1qI7AG.jpg",
                caption: "Chevrolet Comaro\n20.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "comaro.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Chevrolet Comaro texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);    
    }

    private async Task ChevroletMalibuInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://www.ccarprice.com/products/Chevrolet_Malibu_LT_2.4L.jpg",
                caption: "Chevrolet Malibu\n2.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "malibu.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Chevrolet Malibu texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);    
    }

    private async Task ChevroletTrailblazerInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://www.ccarprice.com/products/Chevrolet_Malibu_LT_2.4L.jpg",
                caption: "Chevrolet malibu\n2.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "malibu.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Chevrolet Malibu texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);    
    }

    private async Task ChevroletTrailbluzerInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://nuravto.uz/uploads/elrbWmVdMY.png",
                caption: "Chevrolet Trailblazer\n5.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "trailblazer.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Chevrolet Trailblazer texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);    
    }
    
    private async Task ChevroletTahoeInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://di-uploads-pod1.dealerinspire.com/dalebenetchevy/uploads/2022/01/2022-Chevy-Tahoe-white-728x400.jpg",
                caption: "Chevrolet Tahoe\n7.500$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "tahoe.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Chevrolet Tahoe texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);    
    }

     private async Task BMWX5Info(ITelegramBotClient client, Message message, CancellationToken token)
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
            caption:"BMW X5 texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);    
    }

    private async Task BMWM5Info(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://i.pinimg.com/736x/3f/50/31/3f5031cf0a8c3dfc6e43dd9ad9118ea5.jpg",
                caption: "BMW M5\n45.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "bmw-m5.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"BMW M5 texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);    
    }

     private async Task BMWI8Info(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://cache1.pakwheels.com/system/car_generation_pictures/6403/original/BMW_i8_Front.jpg?1650871814",
                caption: "BMW I8\n50.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "bmw-i8.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"BMW I8 texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);    
    }

     private async Task BMWM4Info(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://mysterio.yahoo.com/mysterio/api/DC61DE4194B85A5FAF9FBC58E9B11C82DA03FA242D54878E86A2AB780AF287F1/autoblog/resizefill_w788_h525;quality_80;format_webp;cc_31536000;/https://s.aolcdn.com/commerce/autodata/images/USC80BMC642A021001.jpg",
                caption: "BMW M4\n35.000$",
        parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "bmw-m4.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"BMW M4 texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);    
    }

    private async Task TeslaModelXInfo(ITelegramBotClient client, Message message, CancellationToken token)
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
            caption:"Teska Model X texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);    
    }

    private async Task TeslaModel3Info(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://media.ed.edmunds-media.com/tesla/model-3/2022/oem/2022_tesla_model-3_sedan_performance_fq_oem_1_815.jpg",
                caption: "Tesla Model 3\n70.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "model-3.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Teska Model 3 texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);    
    }

     private async Task TeslaModelYInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;   
        await client.SendPhotoAsync(
                chatId: from.Id,
                photo: "https://www.ccarprice.com/products/Tesla_Model_Y_Long_Range_2021_Price_Specs.jpg",
                caption: "Tesla Model Y\n62.000$",
                parseMode: ParseMode.Html);
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(root, "model-y.png");
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath, token);

        using var stream = new MemoryStream(bytes);

        await client.SendPhotoAsync(
            message.Chat.Id,
            caption:"Teska Model Y texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);    
    }

     private async Task TeslaModelXPlaidInfo(ITelegramBotClient client, Message message, CancellationToken token)
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
            caption:"Teska Model X Plaid texnik xususiyatlari",
            photo: stream,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: token);    
    }

    private async Task CarInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
       var from = message.From;
        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: _localizer["choose_brand"],
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.CarNamesForInfo.Values.ToArray(), 5),
            parseMode: ParseMode.Html,
            cancellationToken: token);
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
        // await GetNumber(client, message, token);
        

        

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
    private async Task BMWModels(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "BMW",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.BMWTypes.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);           
    }

    private async Task ChevroletModels(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Chevrolet",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.ChevroletTypes.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);       
    }

    private async Task KIAModels(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "KIA",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.KIATypes.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);       
    }

    private async Task HyundaiModels(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Hyundai",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.HyundaiTypes.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);          
    }

    private async Task TeslaModels(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Tesla",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.TeslaTypes.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);         
    }

      private async Task BMWModelsForInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "BMW",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.BMWTypesForInfo.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);           
    }

    private async Task ChevroletModelsInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Chevrolet turlari",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.ChevroletTypesForInfo.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);       
    }

    private async Task KIAModelsForInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "KIA",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.KIATypesForInfo.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);       
    }

    private async Task HyundaiModelsForInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Hyundai",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.HyundaiTypesForInfo.Values.ToArray(), 2),
                parseMode: ParseMode.Html,
                cancellationToken: token);          
    }

    private async Task TeslaModelsForInfo(ITelegramBotClient client, Message message, CancellationToken token)
    {
        await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Tesla",
                replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.TeslaTypesForInfo.Values.ToArray(), 2),
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

    private async Task HandleLanguageAsync(ITelegramBotClient client, Message message, CancellationToken token)
    {
        
        await client.SendTextMessageAsync(chatId: message.Chat.Id,
        text: "✔️",
        replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.FunctionsNames.Values.ToArray(), 4),
        cancellationToken: token);

        var cultureString = StringConstants.LanguageNames.FirstOrDefault(v => v.Value == message.Text).Key;
        await _userService.UpdateLanguageCodeAsync(message?.From?.Id, cultureString);

    }

    private async Task Functions(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var from = message.From;
        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: _localizer["what-do-you-want"],
            replyMarkup: MarkupHelpers.GetReplyKeyboardMarkup(StringConstants.FunctionsNames.Values.ToArray(), 4),
            parseMode: ParseMode.Html,
            cancellationToken: token);   
    }
}