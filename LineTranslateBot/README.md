## Deploy LineTranslateBot to Azure WebJobs

Publish this project as WebJob to your WebApps

Edit your WebApp App settings like a following

![App settings](https://raw.githubusercontent.com/kiyoaki/LineBotNet/master/Images/WebJobSettingsForTranslateBot.PNG "App settings")

Add your WebJob global IP address to yout LINE Channels Server IP Whitelist

If LINE Channels Server IP Whitelist has setting error, LINE Sending messages API response status code is 403 and content is like a following.

```javascript
{"statusCode":"427","statusMessage":"Your ip address [XXX.XXX.XXX.XXX] is not allowed to access this API."}
```
