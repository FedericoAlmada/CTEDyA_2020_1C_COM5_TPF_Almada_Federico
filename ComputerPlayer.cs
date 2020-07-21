using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
	public class ComputerPlayer: Jugador
	{
        Estado estado;
        private ArbolGeneral<Carta> miniMax = new ArbolGeneral<Carta>(new Carta(0, 0));	

		public ComputerPlayer(){}
	
		public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
            bool turnoHumano = true;
            estado = new Estado(cartasPropias, cartasOponente, limite, turnoHumano);
			createArbol(estado);
		}

        private void createArbol(Estado estado)
        {
            if (estado.getTurnoH())
            {
                foreach (int cartaH in estado.getCartasH())
                {
                    Carta carta = new Carta(cartaH, 0);
                    ArbolGeneral<Carta> hijo = new ArbolGeneral<Carta>(carta);
                    miniMax.agregarHijo(hijo);

                    List<int> nuevasCartasH = new List<int>();
                    nuevasCartasH.AddRange(estado.getCartasH());
                    nuevasCartasH.Remove(cartaH);

                    int nuevoLimite = estado.getLimite() - cartaH;
                    estado.setLimite(nuevoLimite);

                    if (estado.getLimite() > 0)
                    {
                        if (estado.getCartasH().Count > 0) // Si no se completó de agregar nodos al arbol de jugador sigo
                        {
                            Estado nuevoEstado = new Estado(estado.getCartasIA(), nuevasCartasH, estado.getLimite(), true);
                            createArbol(nuevoEstado);
                        }
                        else // sino cambio el turno
                        {
                            Estado nuevoEstado = new Estado(estado.getCartasIA(), nuevasCartasH, nuevoLimite, false);
                            createArbol(nuevoEstado);
                        }
                    }
                    else // asigno la función heuristica al nodo/subarbol actual para dejar de seguir armando el arbol de jugadas
                    {
                        hijo.getDatoRaiz().setFuncHeuristica(1);
                    }
                }
            }
            else
            {
                foreach (int cartaIA in estado.getCartasIA())
                {
                    Carta carta = new Carta(cartaIA, 0);
                    ArbolGeneral<Carta> hijo = new ArbolGeneral<Carta>(carta);
                    miniMax.agregarHijo(hijo);

                    List<int> nuevasCartasIA = new List<int>();
                    nuevasCartasIA.AddRange(estado.getCartasIA());
                    nuevasCartasIA.Remove(cartaIA);

                    int nuevoLimite = estado.getLimite() - cartaIA;

                    if (estado.getLimite() > 0)
                    {
                        Estado nuevoEstado = new Estado(nuevasCartasIA, estado.getCartasH(), nuevoLimite, true);
                        createArbol(nuevoEstado);
                    }
                    else
                    {
                        hijo.getDatoRaiz().setFuncHeuristica(-1);
                    }
                }
            }
        }

        public override int descartarUnaCarta()
        {
            Console.WriteLine("Cartas disponibles (IA):");
            return _descartarUnaCarta(miniMax);
        }

        private int _descartarUnaCarta(ArbolGeneral<Carta> raiz)
        {
            List<ArbolGeneral<Carta>> hijos = raiz.getHijos();

            ArbolGeneral<Carta> arbolAux = new ArbolGeneral<Carta>(new Carta(0, 0));
            foreach (var hijo in hijos)
            {
                if (hijo.getDatoRaiz().getFuncHeursitica() == 1) 
				{
                    arbolAux = hijo;
				}
			}
            return arbolAux.getDatoRaiz().getCarta();
		}

		public override void cartaDelOponente(int cartaH)
		{
            Console.WriteLine("\nEl humano ha lanzado la carta: " + cartaH.ToString());
            foreach (ArbolGeneral<Carta> hijo in miniMax.getHijos())
            {
                if (hijo.getDatoRaiz().getCarta() == cartaH)
                {
                    miniMax = hijo;
                    break;
                }
            }
		}
	}
}