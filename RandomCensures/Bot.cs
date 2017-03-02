﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using System.Reflection;
using System.Timers;
using RandomCensures.Functionalities;

namespace RandomCensures
{
    public class Bot : IDisposable
    {
        public Vote vote;
        private bool enPause;
        private Timer timer;
        private StreamReader reader { get; set; }
        private StreamWriter writer { get; set; }
        private string oAuth { get; set; }
        private string chatCommandId { get; set; }
        private string userName { get; set; }
        private string channelName { get; set; }
        private TcpClient tcpClient { get; set; }
        private DateTime lastMessage { get; set; }
        private Queue<string> sendMessageQueue { get;set; }
        private int wait { get; set; }
        private List<IChatCommandMod> chatProcessors;
        public Reward cadeau;

        private FunctionalityList Functionalities { get; set; }

        public event EventHandler<string> OnMessageReceived = null;

        private string chatMessagePrefix
        {
            get
            {
                return $":{userName}!{userName}@{userName}.tmi.twitch.tv {chatCommandId} #{channelName} :";
            }
        }

        /// <summary>
        /// Variable de mise à jour de l'accessibilité du chat
        /// </summary>
        public bool member { get; set; }

        public Bot()
        {
            this.cadeau = new RandomCensures.Reward(this, 0);
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

            Functionalities = FunctionalityList.CreateAllFunctionalities(this);
            Functionalities.FindByType<FloodFunctionality>().IsActive = false;
        }
        
        /// <summary>
        /// Initialisation du bot et connexion
        /// </summary>
        /// <param name="uName">Nom de l'utilisateur</param>
        /// <param name="oAuth">oAuth de l'utilisateur</param>
        public void Init (string uName, string oAuth )
        {
            this.timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(update);
            timer.Interval = 100;
            timer.Enabled = true;
            enPause = true;
            this.userName = uName.ToLower();
            this.channelName = this.userName;
            this.oAuth = oAuth;
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
        private void update (object source, ElapsedEventArgs e)
        {
            if (!tcpClient.Connected)
            {
                Reconnect();
            }

            TrySendingMessages();
            TryReceiveMessages();
        }

        /// <summary>
        /// Démarre le bot
        /// </summary>
        public void start()
        {
            enPause = false;
        }

        /// <summary>
        /// Met le bot en pause
        /// </summary>
        public void stop()
        {
            enPause = true;
        }
        /// <summary>
        /// Envoie les messages au chat
        /// </summary>
        private void TrySendingMessages()
        {
            //if (DateTime.Now - lastMessage > TimeSpan.FromSeconds(wait))
            //{
                if (sendMessageQueue.Count > 0)
                {
                    var message = sendMessageQueue.Dequeue();
                    string messageAT = message.Split('@')[0];
                    string messageDot = message.Split(':')[2];
                    Console.WriteLine(message);
                    writer.WriteLine($"{message}");
                    lastMessage = DateTime.Now;
                    wait = 2;
                    if (messageAT.Contains(userName) 
                        && ( messageDot.Contains("/followersoff") 
                            || messageDot.Contains("/followers") 
                            || messageDot.Contains("/timeout")
                            || messageDot.Contains("récompense")
                            )
                        ) 
                    {
                        message = sendMessageQueue.Dequeue();
                        Console.WriteLine(message);
                        writer.WriteLine($"{message}");
                        wait = 4;
                        return;
                    }
                }
            //}
        }

        /// <summary>
        /// Reçois les derniers messages envoyés sur le chat
        /// </summary>
        private void TryReceiveMessages()
        {
            if (tcpClient.Available > 0 || reader.Peek() >= 0)
            {
                try
                {
                    var message = reader.ReadLine();
                    OnMessageReceived?.Invoke(this, message);

                    var iCollon = message.IndexOf(":", 1);
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
                catch (Exception e)
                {
                }
            }
        }

        /// <summary>
        /// Traite les messages reçus
        /// </summary>
        /// <param name="speaker">Pseudo de l'auteur du message</param>
        /// <param name="message">Son message</param>
        private void ReceiveMessage(string speaker, string text)
        {
            if (!enPause)
            {
                return;
            }

            bool isAdmin = speaker == userName;
            Message message = Message.Parse(speaker, isAdmin, text);
            Functionality functionality = Functionalities.FindMatch(message);
            if (functionality != null)
            {
                functionality.ProceedWith(message);
            }
            else
            {
                foreach (var processor in chatProcessors)
                {
                    var result = processor.ProcessMessage(speaker, text);
                    if (result != null)
                    {
                        SendMessage("Mods", result);
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
            if (!enPause)
            {
                switch (command)
                {
                    case "BannedWord":
                        sendMessageQueue.Enqueue(chatMessagePrefix + "/timeout " + message + " 10"); //TODO mettre à 15 minutes
                        sendMessageQueue.Enqueue(chatMessagePrefix + message + " vous n'avez pas respecté les régles (Ban de 15 minutes!)");
                        break;
                    case "Link":
                        sendMessageQueue.Enqueue(chatMessagePrefix + "/timeout " + message + " 10"); //TODO mettre à 15 minutes
                        sendMessageQueue.Enqueue(chatMessagePrefix + message + " vous n'avez pas respecté les régles (Ban de 15 minutes!)");
                        break;
                    case "Flood":
                        sendMessageQueue.Enqueue(chatMessagePrefix + "/timeout " + message + " 10"); //TODO A mettre à 1 minute!
                        sendMessageQueue.Enqueue(chatMessagePrefix + message + " Pas de flood!");
                        break;
                    default:
                        sendMessageQueue.Enqueue(chatMessagePrefix + message);
                        break;
                } 
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
            FloodFunctionality functionality= Functionalities.FindByType<FloodFunctionality>();
            functionality.IsActive = antiFlood;
            functionality.LimitCount = floodLimit;
        }
    }
}