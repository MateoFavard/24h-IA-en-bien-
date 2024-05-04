namespace P24H.Model;

public class Joueur
{
    private int numero;
    private int score;
    private int valeurAttaque;
    private int vie;
    private TypeActivite activite;
    private int nbCoffres;
    private int valeurButins;

    public Joueur(int numero, int score, int valeurAttaque, int vie, TypeActivite activite, int nbCoffres,
        int valeurButins)
    {
        this.numero = numero;
        this.Score = score;
        this.valeurAttaque = valeurAttaque;
        this.vie = vie;
        this.activite = activite;
        this.nbCoffres = nbCoffres;
        this.valeurButins = valeurButins;
    }
    
    public int Numero => this.numero;

    public int Score
    {
        get => this.score;
        set { score = value; }
    }

    public int ValeurAttaque => this.valeurAttaque;

    public int Vie => this.vie;

    public TypeActivite Activite => this.activite;

    public int NbCoffres => this.nbCoffres;

    public int ValeurButins => this.valeurButins;
}