using System.Net.Sockets;

namespace MyApp.Network;

public class Client
{
    private TcpClient tcpClient;
    private StreamReader reader;
    private StreamWriter writer;
    private int numeroEquipe;

    public int NumeroEquipe() => this.numeroEquipe;

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
            String? messageText = this.reader.ReadLine();
            if (messageText != null)
            {
                Console.WriteLine("<<< " + messageText);
                ReceivedMessage message = ReceivedMessage.Parse(messageText);
                switch (message)
                {
                    case ReceivedMessage.NumeroEquipe(int numero):
                        this.numeroEquipe = numero;
                        this.DebutPartie();
                        break;
                        
                    case ReceivedMessage.DebutTour(int numero) :
                        this.Tour(numero);
                        break;
                    
                    case ReceivedMessage.Fin:
                        this.FinPartie();
                        break;
                }
            }
        }
    }

    public virtual void DebutPartie() { }
    
    public virtual void Tour(int numeroDuTour) { }
    
    
    public virtual void FinPartie() { }
}