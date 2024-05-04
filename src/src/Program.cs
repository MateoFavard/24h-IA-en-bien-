using System;
using P24H.IAs.Drunked;
using P24H.IAs.FaitRien;
using P24H.Network;

namespace P24H
{
    internal class Program
    {
        // MAX 9 caractères
        public static readonly string NomEquipe = "V1";

        static void Main(string[] args)
        {
            LancerPlusieursIAs(new Client[] {new DrunkedIA(), new DrunkedIA(), new DrunkedIA(), new IAV1pillageroute()}, "localhost",1234);
        }

        static void LancerPlusieursIAs(Client[] ias, string address, int port)
        {
            List<Thread> threads = new List<Thread>();
            
            foreach (Client ia in ias)
            {
                Thread threadIA = new Thread(() => ia.Start(address, port));
                threadIA.Start();
                threads.Add(threadIA);
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }
    }
}