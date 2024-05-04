using System.Net.Sockets;
using P24H.Exceptions;

namespace P24H.Network;

public class Client
{
    private TcpClient tcpClient;
    private StreamReader reader;
    private StreamWriter writer;
    private int numeroEquipe;
    private bool tourFini;

    public int NumeroEquipe => this.numeroEquipe;

    public int IndexJoueur => this.numeroEquipe - 1;

    private String? Receive()
    {
        String? messageText = this.reader.ReadLine();
        if (messageText != null)
        {
            Console.WriteLine("<<< " + messageText);
        }

        return messageText;
    }
    
    private void Send(String messageText)
    {
        Console.WriteLine(">>> " + messageText);
        this.writer.WriteLine(messageText);
    }
    
    public void Start(String address, int port)
    {
        this.tcpClient = new TcpClient();
        this.tcpClient.Connect(address, port);
        this.reader = new StreamReader(this.tcpClient.GetStream());
        this.writer = new StreamWriter(this.tcpClient.GetStream());
        this.writer.AutoFlush = true;

        bool running = true;
        while (running)
        {
            String? messageText = this.Receive();
            if (messageText != null)
            {
                try
                {
                    Notification message = Notification.Parse(messageText);
                    this.OnNotification(message);
                }
                catch (UnknownMessageException exception)
                {
                    Console.WriteLine($"Message ignoré : {exception}");
                }
            }
        }
    }

    private void OnNotification(Notification message)
    {
        switch (message)
        {
        case Notification.NomEquipe:
            this.Send(Program.NomEquipe);
            break;
                        
        case Notification.NumeroEquipe(int numero):
            this.numeroEquipe = numero;
            this.DebutPartie();
            break;

        case Notification.DebutTour(int numero):
            this.tourFini = false;
            try
            {
                this.Tour(numero);
            }
            catch (P24HException exception)
            {
                Console.WriteLine($"Tour interrompu à cuase de : {exception}");
            }

            break;

        case Notification.Fin:
            this.FinPartie();
            break;
        }
    }

    public virtual void DebutPartie() { }
    public virtual void Tour(int numeroDuTour) { }
    public virtual void FinPartie() { }

    public void ExecuterCommande(ICommand commande)
    {
        if (this.tourFini) throw new TourDejaFiniException();
        if (commande.TermineTour) this.tourFini = true;

        this.Send(commande.BuildMessage());
        String? messageText = this.Receive();
        if (messageText == null)
        {
            throw new NullReferenceException("Réponse null");
        }
        if (messageText != "OK")
        {
            throw new NOKMessageException(messageText.Split('|')[1]);
        }
    }
    
    public T Demander<T>(IQuery<T> demande)
    {
        if (this.tourFini) throw new TourDejaFiniException();
        if (demande.TermineTour) this.tourFini = true;

        this.Send(demande.BuildQueryMessage());
        String? messageText = this.Receive();
        if (messageText == null) throw new NullReferenceException("Réponse null");
        return demande.ParseResponseMessage(messageText);
    }
}