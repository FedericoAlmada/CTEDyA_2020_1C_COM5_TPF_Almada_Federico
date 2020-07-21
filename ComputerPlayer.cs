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
                foreach(var cartaH in estado.getCartasH())
                {
                    Carta carta = new Carta(cartaH, 0);
                    ArbolGeneral<Carta> hijo = new ArbolGeneral<Carta>(carta);
                    raiz.agregarHijo(hijo);

                    if (estado.getLimite() > 0 && estado.getCartasH().Count > 0)
                    {
                        List<int> nuevasCartas = new List<int>();
                        nuevasCartas.AddRange(estado.getCartasH());
                        nuevasCartas.Remove(cartaH);

                        int nuevoLimite = (estado.getLimite() - cartaH);
                        Estado nuevoEstado = new Estado(estado.getCartasIA(), nuevasCartas, nuevoLimite, false);
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
                    if (estado.getLimite() > 0 && estado.getCartasIA().Count > 0)
                    {
                        List<int> nuevasCartas = new List<int>();
                        nuevasCartas.AddRange(estado.getCartasIA());
                        nuevasCartas.Remove(cartaIA);

                        int nuevoLimite = (estado.getLimite() - cartaIA);
                        Estado nuevoEstado = new Estado(nuevasCartas, estado.getCartasH(), nuevoLimite, true);
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
            int cartaIA = 0;
            foreach (ArbolGeneral<Carta> hijo in raiz.getHijos())
            {
                if (hijo.getDatoRaiz().getFuncHeursitica() == 1)
                {
                    cartaIA = hijo.getDatoRaiz().getCarta();
                    break;
                }
            }
            return cartaIA;
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