using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace juegoIA
{
    class Consulta
    {
        ArbolGeneral<Carta> jugadaActualAux = new ArbolGeneral<Carta>(new Carta(0, 0));

        public Consulta(ArbolGeneral<Carta> jugada)
        {
            this.jugadaActualAux = jugada;
        }

        public Consulta() { }

        public void consultaA(ArbolGeneral<Carta> jugada) // Imprime todos los caminos que surge a partir de una raiz/jugada.
        {
            if (!jugada.esVacio())
                Console.Write("(" + jugada.getDatoRaiz().getCarta() + ", " + jugada.getDatoRaiz().getFuncHeursitica() + ")");

            foreach (var x in jugada.getHijos())
            {
                consultaA(x);
            }
        }

        public void consultaB()
        {
        }

        public void consultaC()
        {
            Console.Clear();
            Console.WriteLine("Juego reiniciado.\n");
            Game juego = new Game();
            juego.play();
        }
    }
}
