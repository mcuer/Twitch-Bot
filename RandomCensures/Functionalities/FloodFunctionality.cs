using System;
using System.Collections.Generic;

namespace RandomCensures.Functionalities
{
    public class FloodFunctionality : Functionality
    {
        public const int DefaultLimitPeriod = 10;
        public const int DefaultLimitCount = 10;

        public FloodFunctionality(Bot bot)
            : base(bot, true)
        {
            Messages = new List<Message>();
            LimitPeriod = DefaultLimitPeriod;
            LimitCount = DefaultLimitCount;
        }

        private List<Message> Messages { get; set; }
        public int LimitPeriod { get; set; }
        public int LimitCount { get; set; }

        protected override bool IsMatchWith(Message message)
        {
            Messages.Add(message);

            DateTime oldestDate = DateTime.Now.Subtract(TimeSpan.FromSeconds(LimitPeriod));
            int count = 0;
            for (int i = Messages.Count - 1; i >= 0; i--)
            {
                if (Messages[i].Date < oldestDate)
                {
                    Messages.RemoveAt(i);
                    continue;
                }

                if (Messages[i].HasSameSpeaker(message))
                {
                    count++;
                    if (count >= LimitCount)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override void ProceedWith(Message message)
        {
            Bot.SendMessage("Flood", message.Speaker);
        }
    }
}