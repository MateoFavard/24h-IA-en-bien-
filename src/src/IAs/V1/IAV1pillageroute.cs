using P24H.Model;
using P24H.Network;

namespace P24H.IAs.FaitRien;

public class IAV1pillageroute : Client
{
    protected override string NomIA => "Terror";

    public override void Tour(int numeroDuTour)
    {
        Joueur[] listeJoueur = this.Demander(new InfosJoueurs());
        Route[] listRoute = this.Demander(new InfosRoutes());

        foreach(Route route in listRoute)
        {
            route.TrouverMeilleurCoffreDisponible(listeJoueur);
        }
        
        //partie recrutement
        if (numeroDuTour < 95)
        {
            if (listeJoueur[this.IndexJoueur].Score > 500)
            {
                foreach (Joueur joueur in listeJoueur)
                {
                    if (joueur.ValeurAttaque > listeJoueur[this.IndexJoueur].ValeurAttaque)
                    {
                        while (joueur.ValeurAttaque > listeJoueur[this.IndexJoueur].ValeurAttaque && listeJoueur[this.IndexJoueur].Score > 500 )
                        {
                            this.ExecuterCommande(new Recruter());
                            listeJoueur[this.IndexJoueur].Score -= Constants.COUT_RECRUTEMENT;
                        }
                    }
                }
                foreach (Route route in listRoute)
                {
                    if (route.ValeurAttaque > listeJoueur[this.IndexJoueur].ValeurAttaque)
                    {
                        while (route.ValeurAttaque > listeJoueur[this.IndexJoueur].ValeurAttaque && listeJoueur[this.IndexJoueur].Score > 500 )
                        {
                            this.ExecuterCommande(new Recruter());
                            listeJoueur[this.IndexJoueur].Score -= Constants.COUT_RECRUTEMENT;
                        }
                    }
                }
            }
        }
        //partie vente
        if (numeroDuTour == 120 && listeJoueur[this.IndexJoueur].NbCoffres > 0)
        {
            this.ExecuterCommande(new Receler());
        }

        int TotalButin = 0;
        foreach(Joueur joueur in listeJoueur)
        {
            TotalButin += joueur.ValeurButins;
        }

        if (listeJoueur[this.IndexJoueur].ValeurButins >= (TotalButin/ listeJoueur.Length))
        {
            this.ExecuterCommande(new Receler());
        }
        else{

            //partie réparation
            if (listeJoueur[this.IndexJoueur].Vie <= 3)
            {
                this.ExecuterCommande(new Reparer());
            }
            else
            {
                //partie trahison/route
                Joueur meilleurJoueur = new Joueur(this.NumeroEquipe,0,0,5,TypeActivite.Aucune,0,0);
                foreach (Joueur joueur in listeJoueur)
                {
                    if (joueur.ValeurAttaque < listeJoueur[this.IndexJoueur].ValeurAttaque &&
                        ((joueur.Activite == TypeActivite.Piller1) || (joueur.Activite == TypeActivite.Piller2) || (joueur.Activite == TypeActivite.Piller3) || (joueur.Activite == TypeActivite.Piller4) || (joueur.Activite == TypeActivite.Piller4)))
                    {
                        if (joueur.ValeurButins > meilleurJoueur.ValeurButins)
                        {
                            meilleurJoueur = joueur;
                        }
                    }
                }

                Route meilleurRoute = new Route(10,1,1000,0,0,0,true);
                foreach (Route route in listRoute)
                {
                    if (route.ValeurAttaque < listeJoueur[this.IndexJoueur].ValeurAttaque && !route.PresenceMonstre)
                    {
                        if (route.ValeurMeilleurCoffreDisponible > meilleurRoute.ValeurMeilleurCoffreDisponible)
                        {
                            meilleurRoute = route;
                        }
                    }
                }

                if (meilleurRoute.ValeurCoffre1 >
                    (meilleurJoueur.NbCoffres == 0 ? 0 : (meilleurJoueur.ValeurButins / meilleurJoueur.NbCoffres) * listeJoueur.Count(j=>j.Activite.EstPillage()))) // * nbGens qui attaque
                {
                    this.ExecuterCommande(new Piller(meilleurRoute.Numero));
                }
                else
                {
                    if (meilleurJoueur.Numero != this.NumeroEquipe)
                    {
                        this.ExecuterCommande(new Trahir(meilleurJoueur.Numero));
                    }
                    else
                    {
                        this.ExecuterCommande(new Receler());
                    }
                    
                }
            }
        }
    }
}