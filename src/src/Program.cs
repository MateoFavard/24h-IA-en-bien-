using System;
using P24H.IAs.Drunked;
using P24H.IAs.FaitRien;

namespace P24H
{
    internal class Program
    {
        // MAX 9 caractères
        public static readonly string NomEquipe = "CACA";

        static void Main(string[] args)
        {
            var ia = new DrunkedIA();
            ia.Start("localhost", 1234);
        }
    }
}