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
        Joueur me = null;

        public DrunkedIA()
        {
            Start("localhost", 1234);
        }

        public override void Tour(int numeroDuTour)
        {
            
            ICommand command = new Reparer();
            var joueur = this.Demander(new InfosJoueurs());
            var routes = this.Demander(new InfosRoutes());

            this.me = joueur[this.IndexJoueur];


            var listRoutes = new List<List<int>>();
            var route1 = new List<int>()
                        {
                            { routes[0].Numero },
                            { routes[0].ValeurCoffre1},
                            { routes[0].ValeurCoffre2},
                            { routes[0].ValeurCoffre3},
                            { routes[0].ValeurAttaque},
                            { Convert.ToInt32(routes[0].PresenceMonstre)},
                            { 1}
                        };
            var route2 = new List<int>()
                        {
                            { routes[1].Numero },
                            { routes[1].ValeurCoffre1},
                            { routes[1].ValeurCoffre2},
                            { routes[1].ValeurCoffre3},
                            { routes[1].ValeurAttaque},
                            { Convert.ToInt32(routes[1].PresenceMonstre)},
                            { 1}
                        };
            var route3 = new List<int>()
                        {
                            { routes[2].Numero },
                            { routes[2].ValeurCoffre1},
                            { routes[2].ValeurCoffre2},
                            { routes[2].ValeurCoffre3},
                            { routes[2].ValeurAttaque},
                            { Convert.ToInt32(routes[2].PresenceMonstre)},
                            { 1}
                        };
            var route4 = new List<int>()
                        {
                            { routes[3].Numero },   
                            { routes[3].ValeurCoffre1},
                            { routes[3].ValeurCoffre2},
                            { routes[3].ValeurCoffre3},
                            { routes[3].ValeurAttaque},
                            { Convert.ToInt32(routes[3].PresenceMonstre)},
                            { 1}
                        };
            listRoutes.Add(route1);
            listRoutes.Add(route2);
            listRoutes.Add(route3);
            listRoutes.Add(route4);

            foreach (Joueur j in joueur)
            {
                int route = Convert.ToInt32(j.Activite);
                if (route <= 3)
                {
                    listRoutes[route][6]++;
                }
            }
            // On recupère les routes attaquable si moins fort + pas de monstre ^^ 
            List<List<int>> routesAttaquable = listRoutes.FindAll(list => list[4] <= me.ValeurAttaque && list[5] == 0 && list[6] <= 3);
            int numeroAttaque = -1;
            int maxButin = 0;
            foreach (List<int> route in routesAttaquable)
            {
                for (int i = route[6]; i < route.Count; i++)
                {
                    if (maxButin < route[i])
                    {
                        maxButin = route[i];
                        numeroAttaque = route[0];
                    }
                }
            }

/*
            // On recupère les routes attaquable si moins fort + pas de monstre ^^ 
            var routesAttaquable = routes.ToList().FindAll(r => r.ValeurAttaque <= me.ValeurAttaque && r.PresenceMonstre == false);
            routesAttaquable = routesAttaquable.OrderByDescending(x => x.ValeurCoffre1).ToList();*/

            // Dernier tour, on vend
            if (numeroDuTour == Constants.DERNIER_TOUR - 3 || numeroDuTour == Constants.DERNIER_TOUR - 1)
            {
                command = new Receler();
            }
            else if (numeroDuTour == Constants.DERNIER_TOUR)
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

                        Route routeAttaqueReference = routes.ToList().OrderByDescending(o => o.ValeurAttaque).ToList()[1];
                        if (this.me.ValeurAttaque <= routeAttaqueReference.ValeurAttaque && me.Score > Constants.COUT_RECRUTEMENT && me.Score > moyenneScoreJoueurs)
                        {
                            this.ExecuterCommande(new Recruter());
                        }


                        
                        // Routes attaquables
                        if (routesAttaquable.Count > aucuneRoute)
                        {
                            command = new Piller(numeroAttaque);
                        }
                        else // Aucune route à attaquer
                        {
                            command = new Receler();
                        }
                    }
                }
            }

            this.ExecuterCommande(command);
        }

        /*private Trahir Trahir(Joueur[] joueurs)
        {
            //int nbJoueur = joueurs.Count();
            //List<Joueur> tries = joueurs.OrderByDescending(r => r.Score).ToList();

            //int indexJoueur = tries.IndexOf(this.me);
            //if (indexJoueur != nbJoueur-1)
            //{
            //    indexJoueur += 1;
            //}
            //else
            //{
            //    indexJoueur -= 1;
            //}
            //Joueur j = tries[indexJoueur];
            
            //return new Trahir(j.Numero);
        }*/
    }
}
