﻿using System;
using P24H.IAs.FaitRien;

namespace P24H
{
    internal class Program
    {
        // MAX 9 caractères
        public static readonly string NomEquipe = "CACA";

        static void Main(string[] args)
        {
            IAV1pillageroute ia = new IAV1pillageroute();
            ia.Start("localhost", 1234);
        }
    }
}