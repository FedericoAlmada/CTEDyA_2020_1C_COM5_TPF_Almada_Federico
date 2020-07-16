using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
	public class ComputerPlayer: Jugador
	{		
		public ComputerPlayer(){}
	
		public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
            ArbolGeneral<int> miniMax = new ArbolGeneral<int>(0);
            bool turnoHumano = false;
            Estado estado = new Estado(cartasPropias, cartasOponente, limite, turnoHumano);
			createArbol(estado, miniMax);
		}
		
		public int createArbol(Estado estado, ArbolGeneral<int> raiz)
		{
            // Declaro una lista vacia de cartas, las cuales serán utilizadas en este metodo.
            List<int> cartas = new List<int>();

            // Si es el turno del Humano, copio sus cartas en la lista de cartas creada.
            if(estado.getTurnoH())
                cartas.AddRange(estado.getCartasH());
            // Sino, copio las cartas de la Inteligencia Artificial.
            else
                cartas.AddRange(estado.getCartasIA());

            foreach(int carta in cartas)
            {
                // Creo un subarbol para cada carta de la lista.
                ArbolGeneral<int> hijo = new ArbolGeneral<int>(carta);

                // Agrego cada subarbol al arbol MiniMax.
                raiz.agregarHijo(hijo);
                
                // Actualizo estado nuevo de la jugada.
                Estado nuevoEstado;

                int nuevoLimite = estado.getLimite() - carta;
                estado.setLimite(nuevoLimite);

                // Creo una nueva lista de cartas y elimino la carta.
                List<int> nuevasCartas = new List<int>();
                nuevasCartas.AddRange(cartas);
                nuevasCartas.Remove(carta);

                // Actualizo el nuevo estado de la jugada dependiendo el turno actual.
                if (estado.getTurnoH())
                {
                    bool turnoH = false;
                    nuevoEstado = new Estado(estado.getCartasIA(), nuevasCartas, nuevoLimite, turnoH);
                }
                else
                {
                    bool turnoH = true;
                    nuevoEstado = new Estado(nuevasCartas, estado.getCartasH(), nuevoLimite, turnoH);
                }

                // Caso base, asignación de la Función Heuristica.
                if (estado.getLimite() < 0)
                {
                    if (estado.getTurnoH())
                        hijo.setFuncionH(1);
                    else
                        hijo.setFuncionH(-1);

                }
                // Caso recursivo.
                else
                {
                    createArbol(nuevoEstado, hijo);
                }
            }
            return 0;
		}
		
		
		public override int descartarUnaCarta()
		{
			//Implementar
			return 0;
		}
		
		public override void cartaDelOponente(int carta)
		{
			//implementar
			
		}
		
	}
}