## Create LINE BOT API Trial Account

[BOT API Trial Account](https://business.line.me/ja/products/4/introduction "BOT API Trial Account")

## Deploy MessageCollector to Azure Functions

Copy following C# script to your Function

[run.csx](https://github.com/kiyoaki/LineBotNet/blob/master/LineBotMessageCollector/run.csx "run.csx")

Edit following part in this code.

```csharp
private const string ChannelSecret = "XXXXXXXXXXXXXXXXXXXXX";
```

Edit your Function Integrate Settings to output Azure Storage queue

![Azure Function Integrate Settings](https://raw.githubusercontent.com/kiyoaki/LineBotNet/master/Images/AzureFunctionsIntegrateSettings.PNG "Azure Function Integrate Settings")

## Deploy MessageSender to Azure WebJobs

Publish [LineBotMessageWebJob Project](https://github.com/kiyoaki/LineBotNet/tree/master/LineBotMessageWebJob "LineBotMessageWebJob Project") as WebJob to your WebApps

Edit your WebApp App settings like a following

![App settings](https://raw.githubusercontent.com/kiyoaki/LineBotNet/master/Images/WebJobSettings.PNG "App settings")

Add your WebJob global IP address to yout LINE Channels Server IP Whitelist

If LINE Channels Server IP Whitelist has setting error, LINE Sending messages API response status code is 403 and content is like a following.

```javascript
{"statusCode":"427","statusMessage":"Your ip address [XXX.XXX.XXX.XXX] is not allowed to access this API."}
```

You can confirm this response content in WebJob console.
