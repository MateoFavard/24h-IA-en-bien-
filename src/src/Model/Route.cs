namespace P24H.Model;

public class Route
{
    private int numero;
    private int niveauBateau;
    private int valeurAttaque;
    private int valeurCoffre1;
    private int valeurCoffre2;
    private int valeurCoffre3;
    private bool presenceMonstre;
    private int valeurMeilleurCoffreDisponible;

    public Route(int numero, int niveauBateau, int valeurAttaque, int valeurCoffre1, int valeurCoffre2,
        int valeurCoffre3, bool presenceMonstre)
    {
        this.numero = numero;
        this.niveauBateau = niveauBateau;
        this.valeurAttaque = valeurAttaque;
        this.valeurCoffre1 = valeurCoffre1;
        this.valeurCoffre2 = valeurCoffre2;
        this.valeurCoffre3 = valeurCoffre3;
        this.presenceMonstre = presenceMonstre;
        this.valeurMeilleurCoffreDisponible = valeurCoffre1;
    }

    public int Numero => this.numero;

    public int NiveauBateau => this.niveauBateau;

    public int ValeurAttaque => this.valeurAttaque;

    public int ValeurCoffre1 => this.valeurCoffre1;

    public int ValeurCoffre2 => this.valeurCoffre2;

    public int ValeurCoffre3 => this.valeurCoffre3;

    public bool PresenceMonstre => this.presenceMonstre;

    public void TrouverMeilleurCoffreDisponible(Joueur[] joueurs)
    {
        TypeActivite activitePillageCetteRoute = (TypeActivite)this.numero;
        int nbJoueursSurCetteRoute = joueurs.Count(j => j.Activite == activitePillageCetteRoute);
        switch (nbJoueursSurCetteRoute)
        {
        case 0:
            this.valeurMeilleurCoffreDisponible = this.valeurCoffre1;
            break;
        case 1:
            this.valeurMeilleurCoffreDisponible = this.valeurCoffre2;
            break;
        case 2:
            this.valeurMeilleurCoffreDisponible = this.valeurCoffre3;
            break;
        default:
            this.valeurMeilleurCoffreDisponible = 0;
            break;
        }
    }

    public int ValeurMeilleurCoffreDisponible => this.valeurMeilleurCoffreDisponible;
}