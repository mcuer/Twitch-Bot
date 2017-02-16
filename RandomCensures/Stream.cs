using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;


namespace RandomCensures
{
    public class Stream
    {
        private StreamReader reader
        {
            get
            {
                return this.reader;
            }
            set
            {
                this.reader = value;
            }
        }

        private StreamWriter writer
        {
            get
            {
                return this.writer;
            }
            set
            {
                this.writer = value;
            }
        }

        private string oAuth
        {
            get
            {
                return this.oAuth;
            }
            set
            {
                this.oAuth = value;
            }
        }

        private string chatMessagePrefix
        {
            get
            {
                return this.chatMessagePrefix;
            }
            set
            {
                this.chatMessagePrefix = value;
            }
        }

        private string chatCommandId
        {
            get
            {
                return this.chatCommandId;
            }
            set
            {
                this.chatCommandId = value;
            }
        }

        private string userName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }

        private string channelName
        {
            get
            {
                return this.channelName;
            }
            set
            {
                this.channelName = value;
            }
        }

        private TcpClient tcpClient
        {
            get
            {
                return this.tcpClient;
            }
            set
            {
                this.tcpClient = value;
            }
        }

        private DateTime lastMessage
        {
            get
            {
                return this.lastMessage;
            }
            set
            {
                this.lastMessage = value;
            }
        }
        private Queue<string> sendMessageQueue
        {
            get
            {
                return this.sendMessageQueue;
            }
            set
            {
                this.sendMessageQueue = value;
            }
        }

        public Stream (string uName, string oAuth )
        {
            this.userName = uName.ToLower();
            this.channelName = this.userName;
            this.oAuth = oAuth;
            this.chatCommandId = "PRIVMSG";
            this.chatMessagePrefix = $":{userName}!{userName}@{userName}.tmi.twitch.tv {chatCommandId} #{channelName}";

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
            if (message.StartsWith("!hi"))
            {
                SendMessage($"hello, {speaker}");
            }
            return retour;
        }

        private void SendMessage(string message)
        {
            sendMessageQueue.Enqueue(message);
        }
    }
}