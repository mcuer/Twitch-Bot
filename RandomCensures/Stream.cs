using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using System.Globalization;
using System.Net;
using System.Linq;
using System.Text;

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

        public bool member { get; set; }

        public bool antiFlood { get; set; }
        public int floodLimit { get; set; }

       /* public string motAJouter { get; set; }*/

        private List<MessageUtilisateur> lMessageUtilisateur { get; set; }

        public Stream()
        {
            lMessageUtilisateur = new List<MessageUtilisateur>();
            this.tcpClient = new TcpClient();
            sendMessageQueue = new Queue<string>();
            this.chatCommandId = "PRIVMSG";
        }

        public void Init (string uName, string oAuth )
        {
            this.userName = uName.ToLower();
            this.channelName = this.userName;
            this.oAuth = oAuth;
            this.chatMessagePrefix = $":{userName}!{userName}@{userName}.tmi.twitch.tv {chatCommandId} #{channelName} :";
            Reconnect();
        }

        public void Disconnect()
        {
            this.tcpClient.Close();
            Dispose();
        }

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
            writer.WriteLine("CAP REQ :twitch.tv/membership");
            //writer.WriteLine("CAP REQ :twitch.tv/tags");
            writer.WriteLine("CAP REQ :twitch.tv/commands");
            writer.WriteLine("JOIN #" + channelName);
            //writer.WriteLine(":jtv MODE #<channel> +o <user>");

            this.lastMessage = DateTime.Now;
        }

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

        public void update (object sender, EventArgs e)
        {
            if (!tcpClient.Connected)
            {
                Reconnect();
            }

            TrySendingMessages();
            TryReceiveMessages();
        }

        private void TrySendingMessages()
        {
            if (DateTime.Now - lastMessage > TimeSpan.FromSeconds(5))
            {
                if (sendMessageQueue.Count > 0)
                {
                    var message = sendMessageQueue.Dequeue();
                    Console.WriteLine(message);
                    writer.WriteLine($"{message}");
                    lastMessage = DateTime.Now;
                    if (message.Split('@')[0].Contains(userName) && message.Split(':')[2].Contains("/followers"))
                    {
                        message = sendMessageQueue.Dequeue();
                        Console.WriteLine(message);
                        writer.WriteLine($"{message}");
                        return;
                    }
                    if (message.Split('@')[0].Contains(userName) && message.Split(':')[2].Contains("/timeout"))
                    {
                        message = sendMessageQueue.Dequeue();
                        Console.WriteLine(message);
                        writer.WriteLine($"{message}");
                        return;
                    }
                    if (message.Split('@')[0].Contains(userName) && message.Split(':')[2].Contains("/followersoff"))
                    {
                        message = sendMessageQueue.Dequeue();
                        Console.WriteLine(message);
                        writer.WriteLine($"{message}");
                        return;
                    }
                }
            }
        }

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

        private void ReceiveMessage (string speaker, string message)
        {
            //String.Compare("", "", true, CultureInfo);
            /*File.WriteAllText("Insultes.txt", motAJouter);*/
            string sMot = File.ReadAllText("Insultes.txt");
            string[] mots = sMot.Split(',');
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
                    SendMessage("!commande", $"les commandes sont : ");
                    return;
                }
            }
        }

        public void SendMessage(string command,string message)
        {
            switch (command)
            {
                case "!hi":
                    sendMessageQueue.Enqueue(chatMessagePrefix + message);
                    break;
                case "!commande":
                    sendMessageQueue.Enqueue(chatMessagePrefix + message);
                    break;
                case "timeout":
                    sendMessageQueue.Enqueue(chatMessagePrefix + "/timeout " + message + " 10"); // A mettre à 15 minutes
                    sendMessageQueue.Enqueue(chatMessagePrefix + message + " vous n'avez pas respecté les régles (Ban de 15 minutes!)");
                    break;
                case "timerMessage":
                    sendMessageQueue.Enqueue(chatMessagePrefix + message);
                    break;
                case "flood":
                    sendMessageQueue.Enqueue(chatMessagePrefix + "/timeout " + message + " 10"); //A mettre à 1 minute!
                    sendMessageQueue.Enqueue(chatMessagePrefix + message + " Pas de flood!");
                    break;
            }
        }
        public void Dispose()
        {
            writer.Close();
            reader.Close();
        }

        public void setAntiflood(bool antiFlood, int floodLimit)
        {
            this.antiFlood = antiFlood;
            this.floodLimit = floodLimit;
        }
        /*public void setAjoutMot(string motAJouter)
        {
            this.motAJouter = motAJouter;

        }*/
    }
}