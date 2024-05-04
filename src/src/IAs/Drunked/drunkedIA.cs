using P24H.Model;
using P24H.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace P24H.IAs.Drunked
{
    public class DrunkedIA : Client
    {
        int vieMinimumPourSeSoigner = 4;
        int argentMinimumPourRecruter = 800;
        int nombreCoffrePourVendre = 3;
        int aucuneRoute = 0;


        public override void Tour(int numeroDuTour)
        {
            
            ICommand command = new Reparer();
            var joueur = this.Demander(new InfosJoueurs());
            var routes = this.Demander(new InfosRoutes());

            Joueur me = joueur[this.IndexJoueur];
            // On recupère les routes attaquable si moins fort + pas de monstre ^^ 
            var routesAttaquable = routes.ToList().FindAll(r => r.ValeurAttaque <= me.ValeurAttaque && r.PresenceMonstre == false);
            routesAttaquable = routesAttaquable.OrderByDescending(x => x.ValeurCoffre1).ToList();

            // Dernier tour, on vend
            if (numeroDuTour == Constants.DERNIER_TOUR - 1)
            {
                command = new Receler();
            }
            else
            {
                // j'ai pas de vie
                if (me.Vie < this.vieMinimumPourSeSoigner)
                {
                    command = new Reparer();
                }
                else // j'ai de la vie
                {
                    if (me.NbCoffres > nombreCoffrePourVendre) // Beaucoup de coffre à vendre
                    {
                        command = new Receler();
                    }
                    else // On pille ^^ 
                    {
                        // On recrute si on peut cad si on a + que le cout de recrutement
                        double moyenneScoreJoueurs = joueur.Average(r => r.Score) + 50;
                        double moyenneAttaqueRoutes = routes.Average(r => r.ValeurAttaque) + 10;

                        if (me.Score > Constants.COUT_RECRUTEMENT + 100 && me.Score > moyenneScoreJoueurs && moyenneAttaqueRoutes > me.ValeurAttaque)
                        {
                            this.ExecuterCommande(new Recruter());
                        }
                        else if (me.Score > Constants.COUT_RECRUTEMENT + 100 && me.ValeurAttaque <= moyenneAttaqueRoutes)
                        {
                            this.ExecuterCommande(new Recruter());
                        }
                        
                        // Routes attaquables
                        if (routesAttaquable.Count > aucuneRoute)
                        {
                            command = new Piller(routesAttaquable[0].Numero);
                        }
                        else // Aucune route à attaquer
                        {
                            command = this.Trahir(joueur);
                        }
                    }
                }
            }

            this.ExecuterCommande(command);
        }

        private Trahir Trahir(Joueur[] joueurs)
        {
            var listJoueurByScore = joueurs.OrderBy(r => r.Score).ToList();
            Joueur joueurATrahir = listJoueurByScore.ToList().First();
            return new Trahir(joueurATrahir.Numero);
        }
    }
}
