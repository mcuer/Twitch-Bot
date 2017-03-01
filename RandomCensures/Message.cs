using System;
using System.Collections.Generic;

namespace RandomCensures
{
    public class Message
    {
        private Message()
        {
        }

        public DateTime Date { get; private set; }
        public string Speaker { get; private set; }
        public bool IsAdmin { get; private set; }
        public string Text { get; private set; }

        public bool HasSameSpeaker(Message message)
        {
            return message.Speaker.Equals(Speaker, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool Contains(string value)
        {
            return Text.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) != -1;
        }

        public bool StartsWith(string value)
        {
            return Text.StartsWith(value, StringComparison.InvariantCultureIgnoreCase);
        }

        public IEnumerable<string> EnumerateWords()
        {
            foreach (string word in Text.Split(' '))
            {
                yield return word;
            }
        }

        public static Message Parse(string speaker, bool isAdmin, string text)
        {
            Message item = new Message();
            item.Date = DateTime.Now;
            item.Speaker = speaker;
            item.IsAdmin = isAdmin;
            item.Text = text;
            return item;
        }
    }
}