using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;


namespace RandomCensures
{
    public class Stream
    {
        public StreamReader reader
        {
            get
            {
                return this.reader;
            }
            private set
            {
                this.reader = value;
            }
        }
            
        public StreamWriter writer
        {
            get
            {
                return this.writer;
            }
            private set
            {
                this.writer = value;
            }
        }

        public string oAuth
        {
            get
            {
                return this.oAuth;
            }
            private set
            {
                this.oAuth = value;
            }
        }

        public string chatMessagePrefix
        {
            get
            {
                return this.chatMessagePrefix;
            }
            private set
            {
                this.chatMessagePrefix = value;
            }
        }

        public string chatCommandId
        {
            get
            {
                return this.chatCommandId;
            }
            private set
            {
                this.chatCommandId = value;
            }
        }

        public string userName
        {
            get
            {
                return this.userName;
            }
            private set
            {
                this.userName = value;
            }
        }

        public string channelName
        {
            get
            {
                return this.channelName;
            }
            private set
            {
                this.channelName = value;
            }
        }

        public TcpClient tcpClient
        {
            get
            {
                return this.tcpClient;
            }
            private set
            {
                this.tcpClient = value;
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

        private void TryReceiveMessages()
        {
            throw new NotImplementedException();
        }

        private void TrySendingMessages()
        {
            throw new NotImplementedException();
        }
    }
}