﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Timers;
using System.Diagnostics;
using System.Threading;

namespace RandomCensures
{
    public class Bot : IDisposable
    {
        public Vote vote;
        private bool enPause;
        private System.Timers.Timer timer;
        //private StreamReader reader { get; set; }
        private StreamWriter writer { get; set; }
        private string oAuth { get; set; }
        private string chatCommandId { get; set; }
        private string userName { get; set; }
        private string channelName { get; set; }
        private TcpClient tcpClient { get; set; }
        private DateTime lastMessage { get; set; }
        private Queue<string> sendMessageQueue { get;set; }
        private int wait { get; set; }
        public List<MessageUtilisateur> lMessageUtilisateur { get; set; }
        private List<IChatCommandMod> chatProcessors;
        private Reward cadeau;
        private Thread listeningThread;
        private bool stopListening;
        private AutoResetEvent listeningThreadEvent;
        private string insultes { get; set; }

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
        /// <summary>
        /// Variable d'activation de l'anti flood
        /// </summary>
        public bool antiFlood { get; set; }
        /// <summary>
        /// Variable contenant le nombre de message autorisé par période de 10s
        /// </summary>
        public int floodLimit { get; set; }

        public Bot()
        {
            this.cadeau = new RandomCensures.Reward(this, 0);
            this.lMessageUtilisateur = new List<MessageUtilisateur>();
            //this.tcpClient = new TcpClient();
            sendMessageQueue = new Queue<string>();
            this.chatCommandId = "PRIVMSG";
            listeningThreadEvent = new AutoResetEvent(false);
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
            enPause = true;
            this.userName = uName.ToLower();
            this.channelName = this.userName;
            this.oAuth = oAuth;
            Connect();
        }

        public bool IsConnected { get { return this.tcpClient != null; } }

        public void Connect()
        {
            if (IsConnected)
            {
                return;
            }

            this.tcpClient = new TcpClient("irc.twitch.tv", 6667);
            //this.reader = new StreamReader(tcpClient.GetStream(), System.Text.Encoding.UTF8);
            this.writer = new StreamWriter(tcpClient.GetStream());
            stopListening = false;
            listeningThread = new Thread(TryReceiveMessages);
            this.timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(update);
            timer.Interval = 100;
            timer.Enabled = true;
            listeningThread.Start();
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

        public void Disconnect()
        {
            if (!IsConnected)
            {
                return;
            }

            if (this.tcpClient != null)
            {
                this.tcpClient.Close();
                this.tcpClient = null;
            }

            if (this.writer != null)
            {
                this.writer.Close();
                this.writer = null;
            }

            if (listeningThread != null)
            {
                listeningThreadEvent.Reset();
                stopListening = true;
                if (!listeningThreadEvent.WaitOne(3000))
                {
                    listeningThread.Abort();
                }

                listeningThread = null;
            }

            if (timer != null)
            {
                timer.Close();
                timer = null;
            }

            //reader.Close();
        }

        /// <summary>
        /// Reconnexion du client en cas de perte
        /// </summary>
        public void Reconnect()
        {
            Disconnect();
            Connect();
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
            //if (!tcpClient.Connected)
            //{
            //    Reconnect();
            //}

            try
            {
                TrySendingMessages();
                //TryReceiveMessages();
            }
            catch (Exception ex)
            {
                throw;
            }
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
                    Debug.WriteLine(message);
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
                        Debug.WriteLine(message);
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
            using (var reader = new StreamReader(tcpClient.GetStream(), System.Text.Encoding.UTF8))
            {
                while (!stopListening)
                {
                    if (tcpClient.Available > 0 || reader.Peek() >= 0)
                    {
                        try
                        {
                            var message = reader.ReadLine();
                            Debug.WriteLine(message);
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

                    Thread.Sleep(100);
                }

                listeningThreadEvent.Set();
                reader.Close();
            }
        }

        /// <summary>
        /// Traite les messages reçus
        /// </summary>
        /// <param name="speaker">Pseudo de l'auteur du message</param>
        /// <param name="message">Son message</param>
        private void ReceiveMessage (string speaker, string message)
        {
            if (!enPause)
            {
                bool isAdmin = speaker == userName;
                switch (Content.isVerified(this, speaker, message, antiFlood, floodLimit, isAdmin))
                {
                    case Content.Verification.BannedWord:
                        SendMessage("BannedWord", speaker);
                        break;
                    case Content.Verification.Flood:
                        SendMessage("Flood", speaker);
                        break;
                    case Content.Verification.Link:
                        SendMessage("Link", speaker);
                        break;
                    case Content.Verification.OK:
                        switch (Content.isCommand(message,isAdmin))
                        {
                            case Content.Command.Hi:
                                SendMessage("!hi", $"Bonjour, {speaker}!");
                                break;
                            case Content.Command.Vote:
                                int voteValues = -1;
                                try
                                {
                                    voteValues = Convert.ToInt16(message.Split(' ')[1]);
                                }
                                catch{}
                                if (voteValues > 0)
                                {
                                    vote.voteAdd(speaker, voteValues);
                                }
                                break;
                            case Content.Command.Reward:
                                if (cadeau.started)
                                {
                                    cadeau.addParticipant(speaker);
                                }
                                break;
                            case Content.Command.AdminReward:
                                string[] splitMessage = message.Split(' ');
                                if (splitMessage[1].Equals("start"))
                                {
                                    cadeau.startReward();
                                }
                                else
                                if (splitMessage[1].Equals("stop"))
                                {
                                    cadeau.stopReward();
                                }
                                break;
                            case Content.Command.None:
                                foreach (var processor in chatProcessors)
                                {
                                    var result = processor.ProcessMessage(speaker, message);
                                    if (result != null)
                                    {
                                        SendMessage("Mods", result);
                                    }
                                }
                                break;
                        }
                        break;
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
            Disconnect();

            
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