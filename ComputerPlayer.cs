using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
	public class ComputerPlayer: Jugador
	{
        private ArbolGeneral<Carta> miniMax = new ArbolGeneral<Carta>(new Carta(0, 0));	

		public ComputerPlayer(){}
	
		public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
            bool turnoHumano = true;
            Estado estado = new Estado(cartasPropias, cartasOponente, limite, turnoHumano);
			createArbol(estado, miniMax);
		}
		
		private ArbolGeneral<Carta> createArbol(Estado estado, ArbolGeneral<Carta> raiz)
		{
            if (estado.getTurnoH())
            {
                foreach (int cartaH in estado.getCartasH())
                {
                    Carta carta = new Carta(cartaH, 0);
                    ArbolGeneral<Carta> hijo = new ArbolGeneral<Carta>(carta);
                    raiz.agregarHijo(hijo);
                    if (estado.getLimite() > 0)
                    {
                        Estado nuevoEstado = new Estado();
                        nuevoEstado.setLimite(estado.getLimite() - cartaH);
                        nuevoEstado.setCartasH(estado.getCartasH());
                        nuevoEstado.getCartasH().Remove(cartaH);
                        nuevoEstado.setTurnoH(false);
                        this.createArbol(nuevoEstado, raiz);
                    }
                    else
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
                    raiz.agregarHijo(hijo);
                    if (estado.getLimite() > 0)
                    {
                        Estado nuevoEstado = new Estado();
                        nuevoEstado.setLimite(estado.getLimite() - cartaIA);
                        nuevoEstado.setCartasIA(estado.getCartasIA());
                        nuevoEstado.getCartasIA().Remove(cartaIA);
                        nuevoEstado.setTurnoH(true);
                        this.createArbol(nuevoEstado, raiz);
                    }
                    else
                    {
                        hijo.getDatoRaiz().setFuncHeuristica(-1);
                    }
                }
            }
            return miniMax;
        }
		
		public override int descartarUnaCarta()
		{
            Console.WriteLine("Naipes disponibles (IA): ");
            return _descartarUnaCarta(miniMax);
		}

        private int _descartarUnaCarta(ArbolGeneral<Carta> raiz)
        {
            int funcHeuristicaMax = 0;
            foreach (ArbolGeneral<Carta> hijo in raiz.getHijos())
            {
                //Implementado metodo de descarte
            }
        }
		
		public override void cartaDelOponente(int cartaH)
		{
            Console.WriteLine("\nEl humano ha lanzado la carta: " + cartaH.ToString());

			foreach(ArbolGeneral<Carta> hijo in miniMax.getHijos())
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