using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomCensures
{
    /// <summary>
    /// Class facilitant la vérification du flood
    /// </summary>
    public class MessageUtilisateur
    {
        public string speaker { get; set; }
        public int nbMessage { get; set; }
        public DateTime datePremierMessage { get; set; }

        public MessageUtilisateur(string speaker, int nbMessage, DateTime datePremierMessage)
        {
            this.speaker = speaker;
            this.nbMessage = nbMessage;
            this.datePremierMessage = datePremierMessage;
        }
    }
}
