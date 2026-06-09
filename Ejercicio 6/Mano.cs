using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazoDeCartas
{
    public class Mano
    {
        public List<Carta> mano;

        public Mano()
        {
            mano = new List<Carta>();
        }

        Mano? recibirCarta(Mazo mazo, Mano mano)
        {
            if (mazo.RobarCarta(mazo) != null)
            {
                mano.mano.Add(mazo.RobarCarta(mazo));
                return mano;
            }

            Console.WriteLine($"No quedan cartas en el mazo");
            return mano;
        }

        public void MostrarMano(Mano mano)
        {
            for (int i = 0; i < (mano.mano.Count); i++)
            {
                Console.WriteLine(mano.mano[i].palos + " " + mano.mano[i].nro);
            }
        }

        public int CantidadDeCartas(Mano mano)
        {
            if (mano.mano.Count == 0)
            {
                Console.WriteLine($"No hay cartas en la mano");
                return 0;
            }
            else
            {
                return mano.mano.Count;
            }
        }
    }
}
