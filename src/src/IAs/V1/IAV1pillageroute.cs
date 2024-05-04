using P24H.Model;
using P24H.Network;

namespace P24H.IAs.FaitRien;

public class IAV1pillageroute : Client
{
    public override void Tour(int numeroDuTour)
    {
        Joueur[] listeJoueur = this.Demander(new InfosJoueurs());
        Route[] listRoute = this.Demander(new InfosRoutes());
        
        //partie recrutement
        if (numeroDuTour < 100)
        {
            if (listeJoueur[this.IndexJoueur].Score > 500)
            {
                foreach (Joueur joueur in listeJoueur)
                {
                    if (joueur.ValeurAttaque > listeJoueur[this.IndexJoueur].ValeurAttaque)
                    {
                        while (joueur.ValeurAttaque > listeJoueur[this.IndexJoueur].ValeurAttaque || listeJoueur[this.IndexJoueur].Score > 500 )
                        {
                            this.ExecuterCommande(new Recruter());
                        }
                    }
                }
                foreach (Route route in listRoute)
                {
                    if (route.ValeurAttaque > listeJoueur[this.IndexJoueur].ValeurAttaque)
                    {
                        while (route.ValeurAttaque > listeJoueur[this.IndexJoueur].ValeurAttaque || listeJoueur[this.IndexJoueur].Score > 500 )
                        {
                            this.ExecuterCommande(new Recruter());
                        }
                    }
                }
            }
        }
        //partie vente
        if (numeroDuTour == 119 && listeJoueur[this.IndexJoueur].NbCoffres > 0)
        {
            this.ExecuterCommande(new Receler());
        }
        if (listeJoueur[this.IndexJoueur].NbCoffres >= 3)
        {
            this.ExecuterCommande(new Receler());
        }
        //partie réparation
        if (listeJoueur[this.IndexJoueur].Vie <= 3)
        {
            this.ExecuterCommande(new Reparer());
        }
        //partie trahison/route
        Joueur meilleurJoueur = listeJoueur[0];
        foreach (Joueur joueur in listeJoueur)
        {
            if (joueur.ValeurAttaque < listeJoueur[this.IndexJoueur].ValeurAttaque &&
                joueur.Activite != TypeActivite.Aucune && joueur.Activite != TypeActivite.Reparation &&
                joueur.Activite != TypeActivite.Recele)
            {
                if (joueur.ValeurButins > meilleurJoueur.ValeurButins)
                {
                    meilleurJoueur = joueur;
                }
            }
        }

        Route meilleurRoute = listRoute[0];
        foreach (Route route in listRoute)
        {
            if (route.ValeurAttaque < listeJoueur[this.IndexJoueur].ValeurAttaque && !route.PresenceMonstre)
            {
                if (route.ValeurCoffre1 > meilleurRoute.ValeurCoffre1)
                {
                    meilleurRoute = route;
                }
            }
        }
        if(meilleurRoute.ValeurCoffre1 > ((meilleurJoueur.ValeurButins/meilleurJoueur.NbCoffres)*meilleurJoueur.NbCoffres-1))
        {
            this.ExecuterCommande(new Piller(meilleurRoute.ValeurCoffre1));
        }
        else
        {
            this.ExecuterCommande(new Trahir(meilleurJoueur.NbCoffres));
        }
    }
}