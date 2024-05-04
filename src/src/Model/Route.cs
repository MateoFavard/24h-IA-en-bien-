namespace P24H.Model;

public class Route
{
    private int niveauBateau;
    private int valeurAttaque;
    private int valeurCoffre1;
    private int valeurCoffre2;
    private int valeurCoffre3;
    private bool presenceMonstre;

    public Route(int niveauBateau, int valeurAttaque, int valeurCoffre1, int valeurCoffre2, int valeurCoffre3, bool presenceMonstre)
    {
        this.niveauBateau = niveauBateau;
        this.valeurAttaque = valeurAttaque;
        this.valeurCoffre1 = valeurCoffre1;
        this.valeurCoffre2 = valeurCoffre2;
        this.valeurCoffre3 = valeurCoffre3;
        this.presenceMonstre = presenceMonstre;
    }

    public int NiveauBateau => this.niveauBateau;

    public int ValeurAttaque => this.valeurAttaque;

    public int ValeurCoffre1 => this.valeurCoffre1;

    public int ValeurCoffre2 => this.valeurCoffre2;

    public int ValeurCoffre3 => this.valeurCoffre3;

    public bool PresenceMonstre => this.presenceMonstre;
}