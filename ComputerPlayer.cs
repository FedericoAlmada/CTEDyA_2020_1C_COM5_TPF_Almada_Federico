using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
	public class ComputerPlayer: Jugador
	{
        ArbolGeneral<int> miniMax = new ArbolGeneral<int>(0);	
		public ComputerPlayer(){}
	
		public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
            bool turnoHumano = false;
            Estado estado = new Estado(cartasPropias, cartasOponente, limite, turnoHumano);
			createArbol(estado, miniMax);
		}
		
		private int createArbol(Estado estado, ArbolGeneral<int> raiz)
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

                // Creo una nueva lista de cartas y elimino la carta.
                List<int> nuevasCartas = new List<int>();
                nuevasCartas.AddRange(cartas);
                nuevasCartas.Remove(carta);

                // Actualizo el limite de la jugada
                estado.setLimite(estado.getLimite() - carta);
                Estado nuevoEstado;
                if (estado.getLimite() >= 0)
                {
                    if(!estado.getTurnoH())
                    {
                        bool turnoH = false;
                        nuevoEstado = new Estado(nuevasCartas, estado.getCartasH(), estado.getLimite(), turnoH); 

                        int res = createArbol(nuevoEstado, miniMax);

                        if (res > hijo.getFuncionH())
                            hijo.setFuncionH(res);
                    }
                    else
                    {
                        bool turnoH = false;
                        nuevoEstado = new Estado(estado.getCartasIA(), nuevasCartas, estado.getLimite(), turnoH); 

                        int res = createArbol(nuevoEstado, miniMax);

                        if(res > hijo.getFuncionH())
                        hijo.setFuncionH(res);
                    }
                }
                else
                {
                    if(!estado.getTurnoH())
                    {
                        hijo.setFuncionH(-1);
                        return hijo.getFuncionH();
                    }
                    else
                    {
                        hijo.setFuncionH(1);
                        return hijo.getFuncionH();
                    }
                }
            }
            return miniMax.getFuncionH();
		}

        public void agregarNodo() 
        {
            // A implementar
        }
		
		public override int descartarUnaCarta()
		{
            Console.WriteLine("Naipes disponibles (IA): ");
            return _descartarUnaCarta(miniMax);
		}

        private int _descartarUnaCarta(ArbolGeneral<int> raiz)
        {
            ArbolGeneral<int> hijoMax = new ArbolGeneral<int>(1);

            List<ArbolGeneral<int>> hijos = raiz.getHijos();
            int maxFH = 0;

            foreach (var hijo in hijos)
            {
                if (hijo.getFuncionH() >= maxFH)
                {
                    maxFH = hijo.getFuncionH();
                    hijoMax = hijo;
                    Console.Write("[C: " + hijoMax.getDatoRaiz().ToString() + "]" + "[FH: " + hijoMax.getFuncionH().ToString() + "]");
                    Console.WriteLine();
                }
            }
            Console.WriteLine("La IA ha lanzado la carta: " + hijoMax.getDatoRaiz().ToString());
            return hijoMax.getDatoRaiz();
        }
		
		public override void cartaDelOponente(int carta)
		{
            Console.WriteLine("\nEl humano ha lanzado la carta: " + carta.ToString());
		}
		
	}
}