using P24H.Network;

namespace P24H.IAs.FaitRien;

public class IAQuiFaitRien : Client
{
    public override void Tour(int numeroDuTour)
    {
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine($"demander infos #{i+1}");
            this.Demander(new InfosJoueurs());
        }
    }
}