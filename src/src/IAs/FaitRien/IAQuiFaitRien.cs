using P24H.Model;
using P24H.Network;

namespace P24H.IAs.FaitRien;

public class IAQuiFaitRien : Client
{
    public override void Tour(int numeroDuTour)
    {
        Joueur[] joueurs = this.Demander(new InfosJoueurs());
        foreach (Joueur joueur in joueurs)
        {
            if (joueur.NbCoffres > 0)
            {
                this.ExecuterCommande(new Trahir(joueur.Numero));
                return;
            }
        }
    }
}