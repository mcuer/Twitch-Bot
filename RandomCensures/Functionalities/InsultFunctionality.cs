using System;
using System.IO;

namespace RandomCensures.Functionalities
{
    public class InsultFunctionality : Functionality
    {
        public InsultFunctionality(Bot bot)
                : base(bot, true)
        {
        }

        protected override bool IsMatchWith(Message message)
        {
            string sMot = File.ReadAllText("Insultes.txt");
            string[] mots = sMot.Split(',');
            foreach (string mot in mots)
            {
                if (message.Contains(mot))
                {
                    return true;
                }
            }

            return false;
        }

        public override void ProceedWith(Message message)
        {
            Bot.SendMessage("BannedWord", message.Speaker);
        }
    }
}