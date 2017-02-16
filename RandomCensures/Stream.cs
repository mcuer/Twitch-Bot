using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using System.Globalization;
using System.Net;

namespace RandomCensures
{
    public class Stream : IDisposable
    {
        private StreamReader reader { get; set; }

        private StreamWriter writer { get; set; }

        private string oAuth { get; set; }

        private string chatMessagePrefix { get; set; }

        private string chatCommandId { get; set; }

        private string userName { get; set; }

        private string channelName { get; set; }

        private TcpClient tcpClient { get; set; }

        private DateTime lastMessage { get; set; }

        private Queue<string> sendMessageQueue { get;set; }

        public Stream (string uName, string oAuth )
        {
            this.tcpClient = new TcpClient();
            sendMessageQueue = new Queue<string>();
            this.userName = uName.ToLower();
            this.channelName = this.userName;
            this.oAuth = oAuth;
            this.chatCommandId = "PRIVMSG";
            this.chatMessagePrefix = $":{userName}!{userName}@{userName}.tmi.twitch.tv {chatCommandId} #{channelName} :";

        }

        public void Reconnect()
        {
            this.tcpClient = new TcpClient("irc.twitch.tv", 6667);
            this.reader = new StreamReader(tcpClient.GetStream());
            this.writer = new StreamWriter(tcpClient.GetStream());

            writer.AutoFlush = true;
            writer.WriteLine(
                    "PASS " + oAuth + Environment.NewLine
                    + "NICK " + userName + Environment.NewLine
                    + "USER " + userName + " 8 * :" + userName
                );
            writer.WriteLine("CAP REQ :twitch.tv/membership");
            //writer.WriteLine("CAP REQ :twitch.tv/tags");
            writer.WriteLine("CAP REQ :twitch.tv/commands");
            writer.WriteLine("JOIN #" + channelName);
            //writer.WriteLine(":jtv MODE #<channel> +o <user>");
            
            this.lastMessage = DateTime.Now;
        }

        public void update (object sender, EventArgs e)
        {
            if (!tcpClient.Connected)
            {
                Reconnect();
            }

            TrySendingMessages();
            TryReceiveMessages();
        }

        private void TrySendingMessages()
        {
            if (DateTime.Now - lastMessage > TimeSpan.FromSeconds(3))
            {
                if (sendMessageQueue.Count > 0)
                {
                    var message = sendMessageQueue.Dequeue();
                    Console.WriteLine(message);
                    writer.WriteLine($"{message}");
                    lastMessage = DateTime.Now;
                    if (message.Split(':')[0].Contains("CLEARCHAT"))
                    {
                        message = sendMessageQueue.Dequeue();
                        Console.WriteLine(message);
                        writer.WriteLine($"{message}");
                    }
                }
            }
        }

        private void TryReceiveMessages()
        {
            if (tcpClient.Available > 0 || reader.Peek() >= 0)
            {
                var message = reader.ReadLine();
                Console.WriteLine(message);
                var iCollon = message.IndexOf(":",1);
                if (iCollon > 0)
                {
                    var command = message.Substring(1, iCollon);
                    if (command.Contains(chatCommandId))
                    {
                        var iBang = command.IndexOf("!");
                        if (iBang > 0)
                        {
                            var speaker = command.Substring(0, iBang);
                            var chatMessage = message.Substring(iCollon + 1);

                            ReceiveMessage(speaker, chatMessage);
                        }
                    }
                }
            }
        }

        private void ReceiveMessage (string speaker, string message)
        {
            //String.Compare("", "", true, CultureInfo);
            if (message.StartsWith("!hi"))
            {
                SendMessage("!hi",$"hello, {speaker}");
            }
            if (message.StartsWith("!commande"))
            {
                SendMessage("!commande",$"les commandes sont : ");
            }
            if (message.Contains("http://"))
            {
                
                SendMessage("timeout",speaker);
            }
        }

        public void SendMessage(string command,string message)
        {
            switch (command)
            {
                case "!hi":
                    sendMessageQueue.Enqueue(chatMessagePrefix + message);
                    break;
                case "!commande":
                    sendMessageQueue.Enqueue(chatMessagePrefix + message);
                    break;
                case "timeout":
                    sendMessageQueue.Enqueue(chatMessagePrefix + "/timeout" + message + " 10");
                    sendMessageQueue.Enqueue(chatMessagePrefix + message + " vous n'avez pas respecté les régles (Ban de 15 minutes!)");
                    break;
                case "timerMessage":
                    sendMessageQueue.Enqueue(chatMessagePrefix + message);
                    break;
            }
        }

        public void Dispose()
        {
            writer.Close();
            reader.Close();
        }
    }
}