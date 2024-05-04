using P24H.Exceptions;

namespace MyApp.Network;

public record ReceivedMessage()
{
    public record NumeroEquipe(int numero) : ReceivedMessage;
    
    public record DebutTour(int numero) : ReceivedMessage;

    public record Fin() : ReceivedMessage;
    
    public static ReceivedMessage Parse(string texte)
    {
        string[] parts = texte.Split("|");
        ReceivedMessage msg;

        if (parts[0].StartsWith("Bonjour"))
        {
            msg = new ReceivedMessage.NumeroEquipe(int.Parse(parts[1]));
        }
        else
        {
            switch (parts[0])
            {
            case "DEBUT_TOUR":
                msg = new ReceivedMessage.DebutTour(int.Parse(parts[1]));
                break;
            
            default:
                throw new UnknownMessageException(texte);
            }
        }
        
        return msg;
    }
}