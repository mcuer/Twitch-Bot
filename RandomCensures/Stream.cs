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
        
        public Stream ()
        {
            TcpClient tcpClient = new TcpClient("irc.twitch.tv", 6667);
            this.reader = new StreamReader(tcpClient.GetStream());
            this.writer = new StreamWriter(tcpClient.GetStream());
        }
    }
}