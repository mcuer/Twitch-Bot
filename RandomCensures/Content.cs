using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomCensures
{
    public class Content
    {
        public enum Verification { OK, Flood, Link, BannedWord }
        public enum Command { Hi, Vote, Reward, AdminReward, None }

        /// <summary>
        /// Utilise les méthodes isLink, isBannedWord, isFlood pour tester si le message est valide
        /// </summary>
        /// <param name="Origin">Le bot d'origin</param>
        /// <param name="speaker">L'utilisateur à vérifier</param>
        /// <param name="message">Le message à vérifier</param>
        /// <param name="antiFlood">Défini si l'anti flood est activé ou non</param>
        /// <param name="floodLimit">Le nombre de message autorisé sur la période de temps</param>
        /// <param name="isAdmin">Défini si l'auteur du message est un admin ou non</param>
        public static Verification isVerified(Bot Origin, string speaker, string message, bool antiFlood, int floodLimit, bool isAdmin)
        {
            if (isAdmin)
            {
                if (antiFlood)
                {
                    if (isFlood(Origin, speaker, floodLimit))
                    {
                        return Verification.Flood;
                    }
                }
                if (isLink(message))
                {
                    return Verification.Link;
                }
                else
                if (isBannedword(message))
                {
                    return Verification.BannedWord;
                } 
            }
            return Verification.OK;
        }

        public static Command isCommand(string message, bool isAdmin)
        {
            if (isAdmin)
            {
                if (isReward(message))
                {
                    return Command.AdminReward;
                }
            }else
            {
                if (isHi(message))
                {
                    return Command.Hi;
                }else
                if (isVote(message))
                {
                    return Command.Vote;
                }else
                if (isReward(message))
                {
                    return Command.Reward;
                }
            }
            return Command.None;
        }

        /// <summary>
        /// Vérifie si l'utilisateur a envoyé trop de message sur une courte période de temps
        /// </summary>
        /// <param name="Origin">Le bot d'origin</param>
        /// <param name="speaker">L'utilisateur à vérifier</param>
        /// <param name="floodLimit">Le nombre de message autorisé sur la période de temps</param>
        private static bool isFlood(Bot Origin, string speaker, int floodLimit)
        {
            bool found = false;
            for (int i = 0; i < Origin.lMessageUtilisateur.Count; i++)
            {
                if (Origin.lMessageUtilisateur.ElementAt(i).speaker == speaker)
                {
                    Origin.lMessageUtilisateur.ElementAt(i).nbMessage++;
                    found = true;
                    if (DateTime.Now - Origin.lMessageUtilisateur.ElementAt(i).datePremierMessage >= TimeSpan.FromSeconds(10))
                    {
                        Origin.lMessageUtilisateur.ElementAt(i).datePremierMessage = DateTime.Now;
                    }
                    else
                    if (DateTime.Now - Origin.lMessageUtilisateur.ElementAt(i).datePremierMessage < TimeSpan.FromSeconds(10) && Origin.lMessageUtilisateur.ElementAt(i).nbMessage > floodLimit)
                    {
                        return true;
                    }
                }
            }
            if (!found)
            {
                Origin.lMessageUtilisateur.Add(new MessageUtilisateur(speaker, 1, DateTime.Now));
            }
            return false;
        }

        /// <summary>
        /// Vérifie si le message contient un lien vers un site
        /// </summary>
        /// <param name="message">Le message à vérifier</param>
        private static bool isLink(string message)
        {
            foreach (string mot in message.Split(' '))
            {
                Uri uriResult;
                bool result = Uri.TryCreate(mot, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if (result)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Vérifie si le message contient un mot intérdit
        /// </summary>
        /// <param name="message">Le message à vérifier</param>
        private static bool isBannedword(string message)
        {
            string sMot = File.ReadAllText("Insultes.txt");
            string[] mots = sMot.Split(',');
            foreach (string mot in mots)
            {
                if (message.ToLower().Contains(mot.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool isHi(string message)
        {
            return (message.StartsWith("!hi") || message.StartsWith("!Hi") || message.StartsWith("!HI"));
        }

        private static bool isVote(string message)
        {
            return (message.StartsWith("!vote") || message.StartsWith("!Vote"));
        }

        private static bool isReward(string message)
        {
            return (message.StartsWith("!vote") || message.StartsWith("!Vote"));
        }
    }
}
