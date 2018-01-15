## Create LINE Developers Account

[LINE Developers](https://developers.line.me/ "LINE Developers")

## Deploy MessageCollector to Azure Functions

Copy following C# script to your Function

[run.csx](https://github.com/kiyoaki/LineBotNet/blob/master/LineBotMessageCollector/run.csx "run.csx")

Edit following part in Azure Function App Settings.

![Azure Function App Settings](https://raw.githubusercontent.com/kiyoaki/LineBotNet/master/Images/ChannelSecret.PNG "Azure Function App Settings")

Edit your Function Integrate Settings to output Azure Storage queue

![Azure Function Integrate Settings](https://raw.githubusercontent.com/kiyoaki/LineBotNet/master/Images/AzureFunctionsIntegrateSettings.PNG "Azure Function Integrate Settings")

## Deploy MessageSender to Azure WebJobs

Publish [LineBotMessageWebJob Project](https://github.com/kiyoaki/LineBotNet/tree/master/LineBotMessageWebJob "LineBotMessageWebJob Project") as WebJob to your WebApps

You can confirm this response content in WebJob console.
