using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
    public class ComputerPlayer: Jugador
	{
        Estado estado;
        private ArbolGeneral<Carta> miniMax = new ArbolGeneral<Carta>(new Carta(0, 0));	// arbol de todas las jugadas
        private ArbolGeneral<Carta> jugadaActual = new ArbolGeneral<Carta>(new Carta(0, 0)); // puntero del arbol

        public ComputerPlayer(){}
	
        public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
        {
            bool turnoHumano = true;
            estado = new Estado(cartasPropias, cartasOponente, limite, turnoHumano);
            createArbol(estado, miniMax);
        }

        private void createArbol(Estado estado, ArbolGeneral<Carta> raiz)
        {
            if (estado.getTurnoH()) // turno del Humano
            {
                foreach (int cartaH in estado.getCartasH())
                {
                    Carta carta = new Carta(cartaH, 0);
                    ArbolGeneral<Carta> hijo = new ArbolGeneral<Carta>(carta);

                    raiz.agregarHijo(hijo);

                    int nuevoLimite = estado.getLimite() - cartaH;

                    if (nuevoLimite >= 0) // si no es el caso base, actualizo el estado y sigo armando el arbol.
                    {
                        List<int> nuevasCartasH = new List<int>();
                        nuevasCartasH.AddRange(estado.getCartasH());
                        nuevasCartasH.Remove(cartaH);

                        Estado nuevoEstado = new Estado(estado.getCartasIA(), nuevasCartasH, nuevoLimite, false);
                        createArbol(nuevoEstado, hijo);
                    }
                    else // si es el caso base, entonces asigno la funcion heuristica al nodo hijo.
                    {
                        raiz.getDatoRaiz().setFuncHeuristica(1);
                    }
                }
            }
            else// turno de la IA
            {
                foreach (int cartaIA in estado.getCartasIA())
                {
                    Carta carta = new Carta(cartaIA, 0);
                    ArbolGeneral<Carta> hijo = new ArbolGeneral<Carta>(carta);

                    raiz.agregarHijo(hijo);

                    int nuevoLimite = estado.getLimite() - cartaIA;

                    if (nuevoLimite >= 0) // si no es el caso base, actualizo el estado y sigo armando el arbol.
                    {
                        List<int> nuevasCartasIA = new List<int>();
                        nuevasCartasIA.AddRange(estado.getCartasIA());
                        nuevasCartasIA.Remove(cartaIA);

                        Estado nuevoEstado = new Estado(nuevasCartasIA, estado.getCartasH(), nuevoLimite, true);
                        createArbol(nuevoEstado, hijo);
                    }
                    else // si es el caso base, entonces asigno la funcion heuristica al nodo hijo.
                    {
                        hijo.getDatoRaiz().setFuncHeuristica(-1);
                    }
                }
            }
        }

        public override int descartarUnaCarta()
        {
            Console.WriteLine("Cartas disponibles (IA):");
            return _descartarUnaCarta(jugadaActual);
        }

         private int _descartarUnaCarta(ArbolGeneral<Carta> raiz)
         {
            int carta = 0;
            foreach (var hijo in raiz.getHijos())
            {
                if (hijo.getDatoRaiz().getFuncHeursitica() == 1) 
                {
                    carta = hijo.getDatoRaiz().getCarta();
                }
            }
            return carta;

        }

        public override void cartaDelOponente(int cartaH)
        {
            Console.WriteLine("\nEl humano ha lanzado la carta: " + cartaH.ToString());
            foreach (ArbolGeneral<Carta> hijo in miniMax.getHijos())
            {
                if (hijo.getDatoRaiz().getCarta() == cartaH)
                {
                    jugadaActual = hijo; // jugada actual apunta a la carta que tiro el humano
                    break;
                }
            }
        }
    }
}