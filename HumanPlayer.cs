
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace juegoIA
{

	public class HumanPlayer: Jugador
	{
		private List<int> naipes = new List<int>();
		private List<int> naipesComputer = new List<int>();
		private int limite;
		private bool random_card = false;
		
		
		public HumanPlayer(){}
		
		public HumanPlayer(bool random_card)
		{
			this.random_card = random_card;
		}
		
		public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
			this.naipes = cartasPropias;
			this.naipesComputer = cartasOponente;
			this.limite = limite;
		}
		
		public override int descartarUnaCarta()
		{
			int carta = 0;
			Console.WriteLine("Turno Humano\nCartes disponibles: ");
			for (int i = 0; i < naipes.Count; i++) 
            {
				Console.Write("(" + naipes[i].ToString() + ")");
				if (i<naipes.Count-1) {
					Console.Write(" ");
				}
			}
		
			Console.WriteLine();
			if (!random_card) {
                Console.WriteLine("\nEl humano está evaluando su mejor opción...");
				Console.Write("\nIngrese una carta: ");
				string entrada = Console.ReadLine();
				
				Int32.TryParse(entrada, out carta);
				while (!naipes.Contains(carta)) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
					Console.Write("ERROR: Opción Inválida. Por favor, ingrese otro naipe: ");
                    Console.ResetColor();
					entrada = Console.ReadLine();
					Int32.TryParse(entrada, out carta);
				}
			}
            else 
            {
				var random = new Random();
				int index = random.Next(naipes.Count);
				carta = naipes[index];
				Console.Write("Ingrese naipe: " + carta.ToString());
			}
			
			return carta;
		}
		
		public override void cartaDelOponente(int carta)
        {
            Console.WriteLine("\n\nLa Inteligencia Artificial está evaluando su mejor opción...");
            Thread.Sleep(6000);

            Console.Write("\n**********************************************");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\nLa Inteligencia Artificial ha lanzado la carta:" + carta.ToString() + "\n");
            Console.ResetColor();
            Console.Write("**********************************************\n");
		}
		
	}
}
