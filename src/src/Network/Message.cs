namespace MyApp.Network;


public class Message
{
    private MessageType type;
    public MessageType Type => this.type;

    private Dictionary<string, MessageType> messageType = new Dictionary<string, MessageType>
    {
        { Program.NomEquipe,  MessageType.NOM_EQUIPE_OK},
        { "DEBUT_TOUR", MessageType.DEBUT_TOUR, },
        { "FIN", MessageType.FIN },
    }

    public Message Parse(string texte)
    {
        string[] splited = texte.Split("|");
        this.type = this.messageType[splited[0]];
        return this;
    }



    public String Print()
    {
        throw new NotImplementedException();
    }
}