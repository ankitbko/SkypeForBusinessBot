# BusyBot - Chat Bot for Skype for Business
A bot to automatically respond to Skype For Business IM.  
Blog post - https://ankitbko.github.io/2017/01/BusyBot-Sykpe-For-Business-Bot/

### Description
Tired of getting IM during work, I created a bot to automatically respond to the incoming message that I am busy and set my Skype status to "Busy".
This project was purely for fun.

### Features

So what does the bot do as of now? It accepts the incoming IM and -

* Responds to greetings - Hi, Hello, Good Morning etc.
* In case the person wants to call me or asks whether I am free - respond that I am busy and will talk later and set my status to Busy.
* Ignore any other messages - *Pretend I am busy*
* Exception Filter - Bot does not reply anything if sender is present in **Exception List**. I don't want to reply to my manager that I am busy if he pings me. :)

### How to use

The bot is just a console application. The bot service is not hosted as Web Api, but runs within the console applications. 
First create a new [LUIS](https://www.luis.ai/) application by importing the model json from `LuisModel` directory. Copy your LUIS model id and subscription key and paste it in `LuisModel` attribute in `LyncLuisDialog.cs`.  


The exception list is located in `App.config` in the console project. Values are *;* separated. 
