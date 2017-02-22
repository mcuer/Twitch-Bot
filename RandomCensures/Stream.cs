using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RandomCensures
{
    public class Stream : IDisposable
    {
        private StreamReader reader { get; set; }
        private StreamWriter writer { get; set; }
        private string oAuth { get; set; }
        private string chatMessagePrefix { get; set; }
        private string chatCommandId { get; set; }
        private string userName { get; set; }
        private string channelName { get; set; }
        private TcpClient tcpClient { get; set; }
        private DateTime lastMessage { get; set; }
        private Queue<string> sendMessageQueue { get;set; }
        private int wait { get; set; }
        private List<MessageUtilisateur> lMessageUtilisateur { get; set; }
        private List<IChatCommandMod> chatProcessors;

        /// <summary>
        /// Variable de mise à jour de l'accessibilité du chat
        /// </summary>
        public bool member { get; set; }
        /// <summary>
        /// Variable d'activation de l'anti flood
        /// </summary>
        public bool antiFlood { get; set; }
        /// <summary>
        /// Variable contenant le nombre de message autorisé par période de 10s
        /// </summary>
        public int floodLimit { get; set; }

        public Stream()
        {
            lMessageUtilisateur = new List<MessageUtilisateur>();
            this.tcpClient = new TcpClient();
            sendMessageQueue = new Queue<string>();
            this.chatCommandId = "PRIVMSG";

            foreach (var file in Directory.EnumerateFiles("./Mods", ".dll"))
            {
                var assembly = Assembly.LoadFile(file);
                foreach (var type in assembly.GetTypes())
                {
                    if (typeof(IChatCommandMod).IsAssignableFrom(type))
                    {
                        chatProcessors.Add(type.GetConstructor(new Type[] { }).Invoke(null) as IChatCommandMod);
                    }
                }
            }

            chatProcessors = new List<IChatCommandMod>();
        }

        /// <summary>
        /// Initialisation du bot et connexion
        /// </summary>
        /// <param name="uName">Nom de l'utilisateur</param>
        /// <param name="oAuth">oAuth de l'utilisateur</param>
        public void Init (string uName, string oAuth )
        {
            this.userName = uName.ToLower();
            this.channelName = this.userName;
            this.oAuth = oAuth;
            this.chatMessagePrefix = $":{userName}!{userName}@{userName}.tmi.twitch.tv {chatCommandId} #{channelName} :";
            Reconnect();
        }

        /// <summary>
        /// Reconnexion du client en cas de perte
        /// </summary>
        public void Reconnect()
        {
            this.tcpClient = new TcpClient("irc.twitch.tv", 6667);
            this.reader = new StreamReader(tcpClient.GetStream());
            this.writer = new StreamWriter(tcpClient.GetStream());
            writer.AutoFlush = true;
            writer.WriteLine(
                    "PASS " + oAuth + Environment.NewLine
                    + "NICK " + userName + Environment.NewLine
                    + "USER " + userName + " 8 * :" + userName
                );
            writer.WriteLine("JOIN #" + channelName);
            wait = 2;
            this.lastMessage = DateTime.Now;
        }

        /// <summary>
        /// fonction lançant la commande de limitation d'accès au chat
        /// </summary>
        public void MemberOnly()
        {
            if (member == true)
            {
                sendMessageQueue.Enqueue(chatMessagePrefix + "/followers ");
                sendMessageQueue.Enqueue(chatMessagePrefix + "Le chat est maintenant uniquement accessible aux abonnées!");
            }
            else
            {
                sendMessageQueue.Enqueue(chatMessagePrefix + "/followersoff ");
                sendMessageQueue.Enqueue(chatMessagePrefix + "Le chat est maintenant de nouveau accessible a tous!");
            }
        }

        /// <summary>
        /// Met à jour le client, fait une tentative de reconnexion si nécessaire
        /// Lance une récéption et un envoie des messages
        /// </summary>
        public void update (object sender, EventArgs e)
        {
            if (!tcpClient.Connected)
            {
                Reconnect();
            }

            TrySendingMessages();
            TryReceiveMessages();
        }

        /// <summary>
        /// Envoie les messages au chat
        /// </summary>
        private void TrySendingMessages()
        {
            if (DateTime.Now - lastMessage > TimeSpan.FromSeconds(wait))
            {
                if (sendMessageQueue.Count > 0)
                {
                    var message = sendMessageQueue.Dequeue();
                    Console.WriteLine(message);
                    writer.WriteLine($"{message}");
                    lastMessage = DateTime.Now;
                    wait = 2;
                    if (message.Split('@')[0].Contains(userName) && message.Split(':')[2].Contains("/followers"))
                    {
                        message = sendMessageQueue.Dequeue();
                        Console.WriteLine(message);
                        writer.WriteLine($"{message}");
                        wait = 4;
                        return;
                    }
                    if (message.Split('@')[0].Contains(userName) && message.Split(':')[2].Contains("/timeout"))
                    {
                        message = sendMessageQueue.Dequeue();
                        Console.WriteLine(message);
                        writer.WriteLine($"{message}");
                        wait = 4;
                        return;
                    }
                    if (message.Split('@')[0].Contains(userName) && message.Split(':')[2].Contains("/followersoff"))
                    {
                        message = sendMessageQueue.Dequeue();
                        Console.WriteLine(message);
                        writer.WriteLine($"{message}");
                        wait = 4;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Reçois les derniers messages envoyés sur le chat
        /// </summary>
        private void TryReceiveMessages()
        {
            if (tcpClient.Available > 0 || reader.Peek() >= 0)
            {
                var message = reader.ReadLine();
                Console.WriteLine(message);
                var iCollon = message.IndexOf(":",1);
                if (iCollon > 0)
                {
                    var command = message.Substring(1, iCollon);
                    if (command.Contains(chatCommandId))
                    {
                        var iBang = command.IndexOf("!");
                        if (iBang > 0)
                        {
                            var speaker = command.Substring(0, iBang);
                            var chatMessage = message.Substring(iCollon + 1);

                            ReceiveMessage(speaker, chatMessage);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Traite les messages reçus
        /// </summary>
        /// <param name="speaker">Pseudo de l'auteur du message</param>
        /// <param name="message">Son message</param>
        private void ReceiveMessage (string speaker, string message)
        {
            //String.Compare("", "", true, CultureInfo);
            string sMot = File.ReadAllText("Insultes.txt");
            string[] mots = sMot.Split(',');
            List<string> Utilisateurs = new List<string>();
            bool found = false;
            if (speaker != userName)
            {
                for (int i = 0; i < lMessageUtilisateur.Count; i++)
                {
                    if (lMessageUtilisateur.ElementAt(i).speaker == speaker)
                    {
                        lMessageUtilisateur.ElementAt(i).nbMessage++;
                        found = true;
                        if (DateTime.Now - lMessageUtilisateur.ElementAt(i).datePremierMessage >= TimeSpan.FromSeconds(10))
                        {
                            lMessageUtilisateur.ElementAt(i).datePremierMessage = DateTime.Now;
                        }
                        else
                        if (DateTime.Now - lMessageUtilisateur.ElementAt(i).datePremierMessage < TimeSpan.FromSeconds(10) && antiFlood && lMessageUtilisateur.ElementAt(i).nbMessage > floodLimit)
                        {
                            SendMessage("flood", speaker);
                            return;
                        }
                    }
                }
                if (!found)
                {
                    lMessageUtilisateur.Add(new MessageUtilisateur(speaker, 1, DateTime.Now));
                }
                foreach (string mot in mots)
                {
                    if (message.ToLower().Contains(mot.ToLower()))
                    {
                        SendMessage("timeout", speaker);
                        return;
                    }
                }
                if (message.Contains("http"))
                {
                    SendMessage("timeout", speaker);
                    return;
                }
                if (message.StartsWith("!hi"))
                {
                    SendMessage("!hi", $"hello, {speaker}");
                    return;
                }
                if (message.StartsWith("!commande"))
                {
                    SendMessage("!commande", "les commandes sont : ");
                    return;
                }
                foreach (var processor in chatProcessors)
                {
                    var result = processor.ProcessMessage(speaker, message);
                    if (result != null)
                    {
                        SendMessage("", result);
                    }
                }
            }
        }

        /// <summary>
        /// Formate les messages à envoyer au chat
        /// </summary>
        /// <param name="command">Valeur permettant un switch pour le traitement des commandes spécifiques</param>
        /// <param name="message">Le message à écrire</param>
        public void SendMessage(string command,string message)
        {
            switch (command)
            {
                //case "!hi":
                //    sendMessageQueue.Enqueue(chatMessagePrefix + message);
                //    break;
                //case "!commande":
                //    sendMessageQueue.Enqueue(chatMessagePrefix + message);
                //    break;
                case "timeout":
                    sendMessageQueue.Enqueue(chatMessagePrefix + "/timeout " + message + " 10"); //TODO mettre à 15 minutes
                    sendMessageQueue.Enqueue(chatMessagePrefix + message + " vous n'avez pas respecté les régles (Ban de 15 minutes!)");
                    break;
                //case "timerMessage":
                //    sendMessageQueue.Enqueue(chatMessagePrefix + message);
                //    break;
                case "flood":
                    sendMessageQueue.Enqueue(chatMessagePrefix + "/timeout " + message + " 10"); //TODO A mettre à 1 minute!
                    sendMessageQueue.Enqueue(chatMessagePrefix + message + " Pas de flood!");
                    break;
                //case "vote":
                //    sendMessageQueue.Enqueue(chatMessagePrefix + message );
                //    break;
                default:
                    sendMessageQueue.Enqueue(chatMessagePrefix + message);
                    break;
            }
        }

        /// <summary>
        /// Fermeture du bot
        /// </summary>
        public void Dispose()
        {
            this.tcpClient.Close();
            writer.Close();
            reader.Close();
        }

        /// <summary>
        /// Mise à jour de l'anti flood
        /// </summary>
        /// <param name="antiFlood">Activation/désactivation de l'anti flood</param>
        /// <param name="floodLimit">Limite de message de l'anti flood</param>
        public void setAntiflood(bool antiFlood, int floodLimit)
        {
            this.antiFlood = antiFlood;
            this.floodLimit = floodLimit;
        }
    }
}