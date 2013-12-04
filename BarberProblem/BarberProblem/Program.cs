using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarberProblem
{
    class Program
    {

        static int tamano = 0;
        static Semaphore ocupados = new Semaphore(0, 5);
        static Semaphore libres = new Semaphore(5, 5);

        static Random r = new Random();
        delegate void Manejador();

        static void Main(string[] args)
        {
            Thread ElTijeras = new Thread(Barbero);
            Thread ElMocho = new Thread(Dechabetado);

            Manejador manejaBarbero = new Manejador(ElTijeras.Start);
            Manejador manejaMocho = new Manejador(ElMocho.Start);

            manejaBarbero();
            manejaMocho();
        }

        static void Barbero()
        {
            while (true)
            {
                Thread.Sleep(r.Next(100,1000));
                if (tamano > 0)
                {
                    ocupados.WaitOne();
                    Console.WriteLine("Debo despertar y cortarle el cabello a {0} personas", tamano);
                    libres.Release();
                    tamano--;
                }
                else
                {
                    Console.WriteLine("A la meme lolo");
                }
            }
        }

        static void Dechabetado()
        {
            while (true)
            {
                Thread.Sleep(r.Next(100, 1000));
                if (tamano < 4)
                {
                    libres.WaitOne();
                    tamano++;
                    Console.WriteLine("Llego nuevo cliente, {0} personas a atender", tamano);
                    ocupados.Release();
                    
                }

                else
                {
                    Console.WriteLine("Meh, hay mucha gente regresare despues");
                }
            }
        }
    }
}
