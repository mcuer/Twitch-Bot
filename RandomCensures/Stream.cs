using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using System.Globalization;

namespace RandomCensures
{
    public class Stream
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
            //writer.WriteLine("CAP REQ :twitch.tv/membership");
            writer.WriteLine("JOIN #" + channelName);
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
                    writer.WriteLine($"{chatMessagePrefix}{message}");
                    lastMessage = DateTime.Now;
                }
            }
        }

        private void TryReceiveMessages()
        {
            if (tcpClient.Available > 0 || reader.Peek() >= 0)
            {
                var message = reader.ReadLine();
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

        private string ReceiveMessage (string speaker, string message)
        {
            string retour = $"\r\n{speaker}: {message}";
            //String.Compare("", "", true, CultureInfo);
            if (message.StartsWith("!hi"))
            {
                SendMessage($"hello, {speaker}");
            }
            if (message.StartsWith("!commande"))
            {
                SendMessage($"les commandes sont : ");
            }
            if (message.Contains("http://"))
            {
                
                SendMessage($"");
            }
            return retour;
        }

        private void SendMessage(string message)
        {
            sendMessageQueue.Enqueue(message);
        }
    }
}