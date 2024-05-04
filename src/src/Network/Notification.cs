using P24H.Exceptions;

namespace P24H.Network;

public record Notification()
{
    public record NumeroEquipe(int numero) : Notification;
    
    public record DebutTour(int numero) : Notification;

    public record Fin() : Notification;
    
    public static Notification Parse(string texte)
    {
        string[] parts = texte.Split("|");
        Notification msg;

        if (parts[0].StartsWith("Bonjour"))
        {
            msg = new Notification.NumeroEquipe(int.Parse(parts[1]));
        }
        else
        {
            switch (parts[0])
            {
            case "DEBUT_TOUR":
                msg = new Notification.DebutTour(int.Parse(parts[1]));
                break;
            
            default:
                throw new UnknownMessageException(texte);
            }
        }
        
        return msg;
    }
}