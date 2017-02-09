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
        
        public Stream (Stream str)
        {
            this.reader = str.reader;
            this.writer = str.writer;
        }

        public Stream (StreamReader reader, StreamWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }
    }
}