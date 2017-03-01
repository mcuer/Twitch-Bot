using System;

namespace RandomCensures.Functionalities
{
    public class CommandFunctionality : Functionality
    {
        public CommandFunctionality(Bot bot)
                : base(bot, false)
        {
        }

        protected override bool IsMatchWith(Message message)
        {
            return DeterminesCommand(message).HasValue;
        }

        public override void ProceedWith(Message message)
        {
            switch (DeterminesCommand(message).Value)
            {
                case CommandValue.Hi:
                    Bot.SendMessage("!hi", $"Bonjour, {message.Speaker}!");
                    break;

                case CommandValue.Reward:
                    if (message.IsAdmin)
                    {
                        string[] splitMessage = message.Text.Split(' ');
                        if (splitMessage[1].Equals("start"))
                        {
                            Bot.cadeau.startReward();
                        }
                        else
                        if (splitMessage[1].Equals("stop"))
                        {
                            Bot.cadeau.stopReward();
                        }
                    }
                    else if (Bot.cadeau.started)
                    {
                        Bot.cadeau.addParticipant(message.Speaker);
                    }
                    break;

                case CommandValue.Vote:
                    int voteValues = -1;
                    try
                    {
                        voteValues = Convert.ToInt16(message.Text.Split(' ')[1]);
                    }
                    catch { }
                    if (voteValues > 0)
                    {
                        Bot.vote.voteAdd(message.Speaker, voteValues);
                    }
                    break;
            }
        }

        private CommandValue? DeterminesCommand(Message message)
        {
            foreach (CommandValue value in Enum.GetValues(typeof(CommandValue)))
            {
                if (message.StartsWith(string.Concat("!", value)))
                {
                    return value;
                }
            }

            return null;
        }

        public enum CommandValue
        {
            Hi,
            Reward,
            Vote
        }
    }
}