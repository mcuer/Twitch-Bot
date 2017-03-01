using System;

namespace RandomCensures.Functionalities
{
    public class LinkFunctionality : Functionality
    {
        public LinkFunctionality(Bot bot)
                : base(bot, true)
        {
        }

        protected override bool IsMatchWith(Message message)
        {
            foreach (string word in message.EnumerateWords())
            {
                Uri uriResult;
                bool result = Uri.TryCreate(word, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if (result)
                {
                    return true;
                }
            }

            return false;
        }

        public override void ProceedWith(Message message)
        {
            Bot.SendMessage("Link", message.Speaker);
        }
    }
}