
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace juegoIA
{

	public class HumanPlayer: Jugador
	{
        Consulta consulta = new Consulta();

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
            int opcion;

            do
            {
                Console.WriteLine("> ¿Qué desea hacer?");
                Console.WriteLine("1. Mostrar resultados a partir de esta jugada.");
                Console.WriteLine("2. Mostrar posibles jugadas a partir de una carta.");
                Console.WriteLine("3. Reiniciar juego.");
                Console.WriteLine("4. Seguir jugando.");
                Console.Write("Seleccione una opción: ");

                opcion = int.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                    {
                        //consulta.consultaA();
                        break;
                    }

                    case 3:
                    {
                        consulta.consultaC();
                        Console.ReadKey();
                        break;
                    }
                }   
            }
            while (opcion != 4);

            Console.WriteLine();
            Console.WriteLine("Turno Humano\nCartes disponibles: ");

			for (int i = 0; i < naipes.Count; i++) 
            {
				Console.Write("(" + naipes[i].ToString() + ")");
				if (i<naipes.Count-1) {
					Console.Write(" ");
				}
			}
			Console.WriteLine();
			if (!random_card) 
            {
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
            Console.WriteLine("\n\nLa Inteligencia Artificial está evaluando su mejor jugada..");
            Thread.Sleep(6000);

            Console.Write("\n-----------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\nLa Inteligencia Artificial ha descartado la carta:" + carta.ToString() + "\n");
            Console.ResetColor();
            Console.Write("-----------------------------------------------------------\n");
		}
	}
}
