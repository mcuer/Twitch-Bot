using System;

namespace RandomCensures.Functionalities
{
    public abstract class Functionality
    {
        public Functionality(Bot bot, bool excludeAdmin)
        {
            Bot = bot;
            ExcludeAdmin = excludeAdmin;
            IsActive = true;
        }

        public Bot Bot { get; private set; }

        public bool ExcludeAdmin { get; private set; }

        public bool IsActive { get; set; }

        protected abstract bool IsMatchWith(Message message);

        public bool IsMatch(Message message)
        {
            if (!IsActive || ExcludeAdmin)
            {
                return false;
            }

            return IsMatchWith(message);
        }

        public abstract void ProceedWith(Message message);
    }
}