using P24H.Model;

namespace P24H.Network;

public interface IQuery<T>
{
    public String BuildQueryMessage();
    
    public T ParseResponseMessage(String texte);
    
    public bool TermineTour { get; }
}

public class InfosJoueurs : IQuery<Joueur[]>
{
    private static readonly Dictionary<String, TypeActivite> activites = new Dictionary<string, TypeActivite>
    {
        ["PILLER1"] = TypeActivite.Piller1,
        ["PILLER2"] = TypeActivite.Piller2,
        ["PILLER3"] = TypeActivite.Piller3,
        ["PILLER4"] = TypeActivite.Piller4,
        ["ATTAQUE"] = TypeActivite.Attaque,
        ["RECELE"] = TypeActivite.Recele,
        ["REPARATION"] = TypeActivite.Reparation,
        ["AUCUNE"] = TypeActivite.Aucune,
    };
    
    public string BuildQueryMessage()
    {
        return "JOUEURS";
    }

    public Joueur[] ParseResponseMessage(string texte)
    {
        String[] texteJoueurs = texte.Split('|');
        Joueur[] joueurs = new Joueur[texteJoueurs.Length];
        
        for (int i = 0; i < texteJoueurs.Length; i++)
        {
            string texteJoueur = texteJoueurs[i];
            String[] infos = texteJoueur.Split(";");
            
            joueurs[i] = new Joueur(
                int.Parse(infos[0]),
                int.Parse(infos[1]),
                int.Parse(infos[2]),
                activites[infos[3]],
                int.Parse(infos[4]),
                int.Parse(infos[5])
            );
        }

        return joueurs;
    }

    public bool TermineTour => false;
}

public class InfosRoutes : IQuery<Route[]>
{
    public string BuildQueryMessage()
    {
        return "ROUTES";
    }

    public Route[] ParseResponseMessage(string texte)
    {
        String[] texteRoutes = texte.Split('|');
        Route[] routes = new Route[texteRoutes.Length];
        
        for (int i = 0; i < texteRoutes.Length; i++)
        {
            string texteJoueur = texteRoutes[i];
            String[] infos = texteJoueur.Split(";");
            
            routes[i] = new Route(
                int.Parse(infos[0]),
                int.Parse(infos[1]),
                int.Parse(infos[2]),
                int.Parse(infos[3]),
                int.Parse(infos[4]),
                infos[5].ToUpper().Equals("TRUE")
            );
        }

        return routes;
    }

    public bool TermineTour => false;
}