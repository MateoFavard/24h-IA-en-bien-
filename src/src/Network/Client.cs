using System.Net.Sockets;

namespace P24H.Network;

public class Client
{
    private TcpClient tcpClient;
    private StreamReader reader;
    private StreamWriter writer;
    private int numeroEquipe;

    public int NumeroEquipe() => this.numeroEquipe;

    private String? Receive()
    {
        String? messageText = this.reader.ReadLine();
        if (messageText == null)
        {
            Console.WriteLine("<<< [NULL]");
        }
        else
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
        this.tcpClient.NoDelay = true;
        this.tcpClient.Connect(address, port);
        this.reader = new StreamReader(this.tcpClient.GetStream());
        this.writer = new StreamWriter(this.tcpClient.GetStream());

        bool running = true;
        while (running)
        {
            String? messageText = this.Receive();
            if (messageText != null)
            {
                Notification message = Notification.Parse(messageText);
                switch (message)
                {
                    case Notification.NumeroEquipe(int numero):
                        this.numeroEquipe = numero;
                        this.DebutPartie();
                        break;
                        
                    case Notification.DebutTour(int numero) :
                        this.Tour(numero);
                        break;
                    
                    case Notification.Fin:
                        this.FinPartie();
                        break;
                }
            }
        }
    }

    public virtual void DebutPartie() { }
    public virtual void Tour(int numeroDuTour) { }
    public virtual void FinPartie() { }

    public void ExecuterCommande(ICommand commande)
    {
        this.Send(commande.BuildMessage());
    }
    
    public T Demander<T>(IQuery<T> demande)
    {
        this.Send(demande.BuildQueryMessage());
        String? messageText = this.Receive();
        if (messageText == null) throw new NullReferenceException("Réponse null");
        return demande.ParseResponseMessage(messageText);
    }
}