using System;
using P24H.IAs.Drunked;
using P24H.IAs.FaitRien;

namespace P24H
{
    internal class Program
    {
        // MAX 9 caractères
        public static readonly string NomEquipe = "V1";

        static void Main(string[] args)
        {
            var ia = new IAV1pillageroute();
            ia.Start("localhost", 1234);
        }
    }
}