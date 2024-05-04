using System;

namespace P24H
{
    internal class Program
    {
        // MAX 9 caractères
        public static readonly string NomEquipe = "CACA";

        static void Main(string[] args)
        {
            IAQuiFaitRien ia = new IAQuiFaitRien();
            ia.Start("localhost", 1234);
        }
    }
}