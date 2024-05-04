namespace MyApp.Network;

public class Message
{
    private MessageType type;

    public MessageType Type => this.type;

    public static Message Parse(String texte)
    {
        throw new NotImplementedException();
    }

    public String Print()
    {
        throw new NotImplementedException();
    }
}