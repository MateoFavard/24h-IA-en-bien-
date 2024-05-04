using P24H.Network;

namespace P24H.IAs.FaitRien;

public class IAQuiFaitRien : Client
{
    public override void Tour(int numeroDuTour)
    {
        this.Demander(new InfosJoueurs());
        this.Demander(new InfosRoutes());
        this.ExecuterCommande(new Reparer());
    }
}