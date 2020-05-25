using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
	public class ComputerPlayer: Jugador
	{
		private List<int> naipesHumano = new List<int>();
		private List<int> naipesComputer = new List<int>();
		ArbolGeneral<int> arbol = new ArbolGeneral<int>(0);
		private int limite;		
		
		public ComputerPlayer(){}
	
		public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
			this.naipesComputer = cartasPropias;
			this.naipesHumano = cartasOponente;
			this.limite = limite;				
			bool turnoHumano = false;
			inicializarArbol(naipesComputer, naipesHumano, limite, turnoHumano, arbol);
		}
		
		public int inicializarArbol(List<int> cartasPropias, List<int> cartasOponente, int limite, bool turnoHumano, ArbolGeneral<int> raiz)
		{
			// Implementando...
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