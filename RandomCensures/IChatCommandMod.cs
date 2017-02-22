using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomCensures
{
    /// <summary>
    /// Interface pour ajouter des commandes supplémentaires
    /// </summary>
    public interface IChatCommandMod
    {
        /// <summary>
        /// Servira au traitement des messages reçu par le mod
        /// </summary>
        /// <param name="speaker">L'auteur du message</param>
        /// <param name="message">Le message</param>
        /// <returns>Devra retourner le message que le mod voudra faire écrire en réponse</returns>
        string ProcessMessage(string speaker, string message);
    }
}
