using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RandomCensures
{
    public class Vote
    {
        AutoResetEvent resetEvent;
        Thread thread;
        private List<string> votant { get; set; }
        private string[] voteValues { get; set; }
        private Bot OriginBot { get; set; }
        private int timerInSeconde { get; set; }
        private int[] voteResults { get; set; }
        public bool started { get; set; }

        /// <summary>
        /// Constructeur pour lancer un vote
        /// </summary>
        /// <param name="OriginBot">La classe d'où le vote est lancer</param>
        /// <param name="voteValues">tableau de string comprenant toutes les valeurs possibles pour le vote</param>
        /// <param name="timerInSeconde">Durée pendant il sera possible de voter
        /// mis à 120secondes (2minutes) si aucune valeur</param>
        public Vote(Bot OriginBot, string[] voteValues, int timerInSeconde)
        {
            this.OriginBot = OriginBot;
            this.voteValues = voteValues;
            this.votant = new List<String>();
            started = false;
            this.voteResults = new int[voteValues.Count()];
            for(int i = 0; i<voteResults.Count(); i++)
            {
                this.voteResults[i] = 0;
            }
            if (timerInSeconde > 0)
            {
                this.timerInSeconde = timerInSeconde;
            }else
            {
                this.timerInSeconde = 300;
            }
            resetEvent = new AutoResetEvent(false);
            thread = new Thread(Run);
        }
        
        /// <summary>
        /// Démmarre le vote en affichant les possibilités
        /// </summary>
        public void voteStart()
        {
            string message = "Le vote commence!";
            OriginBot.SendMessage("vote", message);
            message = "Les valeurs possible sont les suivantes :";
            OriginBot.SendMessage("vote", message);
            for (int i = 1; i < voteValues.Count()+1; i++)
            {
                message = $"{i} - {voteValues.ElementAt(i-1)}";
                OriginBot.SendMessage("vote", message);
            }
            started = true;
            message = "Pour participer tapez \"!vote \" avec le numéro de votre choix";
            OriginBot.SendMessage("vote", message);
            resetEvent.Set();
        }

        /// <summary>
        /// Démmarre le vote en affichant les possibilités
        /// </summary>
        public void voteStart(int timerInSeconds)
        {
            if (timerInSeconde > 0)
            {
                this.timerInSeconde = timerInSeconde;
            }
            string message = "Le vote commence!";
            OriginBot.SendMessage("vote", message);
            message = "Les valeurs possible sont les suivantes :";
            OriginBot.SendMessage("vote", message);
            for (int i = 1; i < voteValues.Count() + 1; i++)
            {
                message = $"{i} - {voteValues.ElementAt(i - 1)}";
                OriginBot.SendMessage("vote", message);
            }
            started = true;
            message = "Pour participer tapez \"!vote \" avec le numéro de votre choix";
            OriginBot.SendMessage("vote", message);
            resetEvent.Set();
        }

        /// <summary>
        /// Ajoute un vote
        /// </summary>
        /// <param name="votant">est le nom(pseudo) de la personne qui vote</param>
        /// <param name="voteValue">est l'index correspondant à son choix pour le vote</param>
        public void voteAdd(string votant, int voteValue)
        {
            bool inList = false;
            foreach (string nom in this.votant)
            {
                if (nom.Equals(votant))
                {
                    inList = true;
                }
            }
            if (!inList)
            {
                this.votant.Add(votant);
                this.voteResults[voteValue-1]++;
            }
        }
        
        /// <summary>
        /// Arrete le vote en cours, et affiche les résultats
        /// </summary>
        public void voteStop()
        {
            string message = "Le vote est fini!";
            OriginBot.SendMessage("vote", message);
            message = "Voici les résultats :";
            OriginBot.SendMessage("vote", message);
            for (int i = 1; i < voteValues.Count() + 1; i++)
            {
                message = $"{i} - {voteValues.ElementAt(i - 1)} : {voteResults.ElementAt(i-1)} vote";
                OriginBot.SendMessage("vote", message);
            }
            started = false;
        }

        /// <summary>
        /// Thread stopant le vote une fois la durée écoulé
        /// </summary>
        private void Run()
        {
            resetEvent.WaitOne();
            Thread.Sleep(timerInSeconde * 1000);
            this.voteStop();
        }

    }
}
