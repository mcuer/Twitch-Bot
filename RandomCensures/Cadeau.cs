using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RandomCensures
{
    class Cadeau
    {
        private AutoResetEvent resetEvent;
        private Thread thread;
        private Random rand;
        private List<string> participant { get; set; }
        private Stream OriginBot { get; set; }
        private int timerInSeconde { get; set; }
        public bool started { get; set; }


        /// <summary>
        /// Constructeur pour lancer un cadeau
        /// </summary>
        /// <param name="OriginBot">La classe d'où le vote est lancer</param>
        /// <param name="voteValues">tableau de string comprenant toutes les valeurs possibles pour le vote</param>
        /// <param name="timerInSeconde">Durée pendant il sera possible de participer
        /// mis à 120secondes (2minutes) si aucune valeur</param>
        public Cadeau(Stream OriginBot, int timerInSeconde)
        {
            this.OriginBot = OriginBot;
            rand = new Random();
            this.participant = new List<String>();
            if (timerInSeconde > 0)
            {
                this.timerInSeconde = timerInSeconde;
            }
            else
            {
                this.timerInSeconde = 300;
            }
            started = false;
            resetEvent = new AutoResetEvent(false);
            thread = new Thread(Run);
        }

        /// <summary>
        /// Démmarre la participation
        /// </summary>
        public void cadeauStart()
        {
            string message = "Une récompense est mise en jeu!";
            OriginBot.SendMessage("cadeau", message);
            message = "Pour participer envoyer un message commençant par '!participe'!";
            OriginBot.SendMessage("cadeau", message);
            started = true;
            resetEvent.Set();
        }

        /// <summary>
        /// Surcharge de la méthode de démmarrage de la participation en spécifiant une durée
        /// </summary>
        /// <param name="timerInSeconds">Durée pendant il sera possible de participer</param>
        public void cadeauStart(int timerInSeconds)
        {
            if (timerInSeconde > 0)
            {
                this.timerInSeconde = timerInSeconde;
            }
            string message = "Une récompense est mise en jeu!";
            OriginBot.SendMessage("cadeau", message);
            message = "Pour participer envoyer un message commençant par '!participe'!";
            OriginBot.SendMessage("cadeau", message);
            started = true;
            resetEvent.Set();
        }

        /// <summary>
        /// Ajoute une participation
        /// </summary>
        /// <param name="partcipant">est le nom(pseudo) de la personne qui participer</param>
        public void cadeauAdd(string partcipant) 
        {
            if (!this.participant.Contains(partcipant))
            {
                this.participant.Add(partcipant);
            }
        }

        /// <summary>
        /// Arrete la participation en cours, et affiche le gagnant
        /// </summary>
        public void cadeauStop()
        {
            string message = "La participation pour la récompense est terminé!";
            OriginBot.SendMessage("cadeau", message);
            message = $"Le gagnant est : {participant.ElementAt(rand.Next(0, participant.Count-1))}";
            OriginBot.SendMessage("cadeau", message);
            started = false;
        }

        /// <summary>
        /// Thread stopant la participation une fois la durée écoulé
        /// </summary>
        private void Run()
        {
            resetEvent.WaitOne();
            Thread.Sleep(timerInSeconde * 1000);
            this.cadeauStop();
        }

    }
}
