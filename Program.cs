﻿
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using WethaBot.Methods;

namespace WeathaBot
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var client = new TelegramBotClient("6499335199:AAGDmg7pg2VCwdK2vQZ335k4hAvBmfzMsoM");
                
            client.StartReceiving(TelegramCommandHandler.Update, TelegramCommandHandler.Error);

            Console.WriteLine("Bot started. Press any key to stop...");
            
            Console.ReadLine();

        }
    }
}
