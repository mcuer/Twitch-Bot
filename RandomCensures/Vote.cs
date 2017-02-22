using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RandomCensures
{
    class Vote
    {
        AutoResetEvent resetEvent;
        Thread thread;
        private List<string> votant { get; set; }
        private string[] voteValues { get; set; }
        private Stream OriginBot { get; set; }
        private int timerInSeconde { get; set; }
        private int[] voteResults { get; set; }

        public Vote(Stream OriginBot, string[] voteValues, int timerInSeconde)
        {
            this.OriginBot = OriginBot;
            this.voteValues = voteValues;
            this.votant = new List<String>();
            this.voteResults = new int[voteValues.Count()];
            for(int i = 0; i<voteResults.Count(); i++)
            {
                this.voteResults[i] = 0;
            }
            this.timerInSeconde = timerInSeconde;
            resetEvent = new AutoResetEvent(false);
            thread = new Thread(Run);
        }

        public void voteStart()
        {
            string message = "Le vote commence!";
            message += "\nLes valeurs possible sont les suivantes :";
            for(int i = 1; i < voteValues.Count()+1; i++)
            {
                message += $"\n{i} - {voteValues.ElementAt(i-1)}";
            }
            OriginBot.SendMessage("vote", message);
            resetEvent.Set();
        }

        public void voteAdd(string votant, int voteValue)
        {
            if (this.votant.Contains(votant))
            {
                return;
            }
            this.votant.Add(votant);
            this.voteResults[voteValue]++;
        }
        
        private void voteStop()
        {
            string message = "Le vote est fini!";
            message += "\nVoici les résultats :";
            for (int i = 1; i < voteValues.Count() + 1; i++)
            {
                message += $"\n{i} - {voteValues.ElementAt(i - 1)} : {voteResults.ElementAt(i-1)} vote";
            }
            OriginBot.SendMessage("vote", message);
        }

        private void Run()
        {
            resetEvent.WaitOne();
            Thread.Sleep(timerInSeconde * 1000);
            this.voteStop();
        }

    }
}
