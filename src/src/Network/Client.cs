using System.Net.Sockets;
using P24H.Exceptions;

namespace P24H.Network;

public class Client
{
    private TcpClient tcpClient;
    private StreamReader reader;
    private StreamWriter writer;
    private int numeroEquipe;
    private int actionsTour;
    private bool running = true;

    private const int MAX_NB_ACTION_TOUR = 15;

    public int NumeroEquipe => this.numeroEquipe;

    public int IndexJoueur => this.numeroEquipe - 1;

    protected virtual String NomIA => Program.NomEquipe;

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

        while (this.running)
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
            this.Send(this.NomIA);
            break;
                        
        case Notification.NumeroEquipe(int numero):
            this.numeroEquipe = numero;
            this.DebutPartie();
            break;

        case Notification.DebutTour(int numero):
            this.actionsTour = 0;
            try
            {
                this.Tour(numero);
            }
            catch (P24HException exception)
            {
                Console.WriteLine($"Tour interrompu à cause de : {exception}");
            }

            break;

        case Notification.Fin:
            this.FinPartie();
            this.running = false;
            break;
        }
    }

    public virtual void DebutPartie() { }
    public virtual void Tour(int numeroDuTour) { }
    public virtual void FinPartie() { }

    public void ExecuterCommande(ICommand commande)
    {
        this.VerifierActionEstPossible(commande.TermineTour);

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
        this.VerifierActionEstPossible(demande.TermineTour);

        this.Send(demande.BuildQueryMessage());
        String? messageText = this.Receive();
        if (messageText == null) throw new NullReferenceException("Réponse null");
        return demande.ParseResponseMessage(messageText);
    }

    private void VerifierActionEstPossible(bool termineTour)
    {
        if (this.actionsTour < 0)
        {
            throw new TourDejaFiniException();
        }
        if (this.actionsTour >= MAX_NB_ACTION_TOUR)
        {
            throw new PlusDActionsPossiblesException();
        }
        if (termineTour)
        {
            this.actionsTour = -1;
        }
        else
        {
            this.actionsTour++;
        }
    }
}