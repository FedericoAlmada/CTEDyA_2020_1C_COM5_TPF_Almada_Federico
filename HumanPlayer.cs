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

        public HumanPlayer(Consulta consulta)
        {
            this.consulta = consulta;
        }

		public HumanPlayer(bool random_card)
		{
			this.random_card = random_card;
		}
        
		public override void incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
			this.naipes = cartasPropias;
			this.naipesComputer = cartasOponente;
			this.limite = limite;
		}
		
		public override int descartarUnaCarta()
		{
			int carta = 0;
            string opcion;
            Console.WriteLine("Turno Humano\nCartes disponibles: ");

			for (int i = 0; i < naipes.Count; i++) 
            {
				Console.Write("(" + naipes[i].ToString() + ")");
				if (i<naipes.Count-1) 
                {
					Console.Write(" ");
				}
			}
			Console.WriteLine();
			if (!random_card) 
            {
                Console.WriteLine("El humano está evaluando su mejor opción...");
				Console.Write("\nIngrese una carta ó presione ENTER para abrir las consultas: ");
                string entrada = Console.ReadLine();
				Int32.TryParse(entrada, out carta);
				while (!naipes.Contains(carta)) 
                {
                    if (entrada == "") // Si se entra al módulo de consultas
                    {
                        Console.WriteLine("\n--------------------------------------------------------------------");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("                             CONSULTAS                   ");
                        Console.ResetColor();
                        Console.WriteLine("--------------------------------------------------------------------");
                        Console.WriteLine("Usted ha ingresado al módulo de consultas. ¿Qué desea hacer?\n\nA. Mostrar resultados a partir de este punto.\nB. Simular una secuencia de posibles jugadas.\nC. Imprimir cartas a partir de una profundidad.");
                        Console.WriteLine("S. Seguir con el juego.\nR. Reiniciar el juego\nQ. Cerrar el juego.");
                        Console.WriteLine("--------------------------------------------------------------------\n");
                        Console.Write("Ingrese una opción: ");
                        opcion = Console.ReadLine();

                        switch (opcion.ToUpper())
                        {
                            case "A":
                                consulta.consultaA();
                                Console.WriteLine("\n\nPresione una tecla para continuar.");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case "B":
                                consulta.consultaB();
                                Console.WriteLine("\n\nPresione una tecla para continuar.");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case "C":
                                consulta.consultaC();
                                Console.WriteLine("\n\nPresione una tecla para continuar.");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case "R":
                                consulta.reiniciarJuego();
                                Console.Clear();
                                break;
                            case "Q":
                                Environment.Exit(0);
                                break;
                            case "S":
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nJuego retomado.");
                                Console.ResetColor();
                                Console.WriteLine("--------------------------------------------------------------------");
                                Console.WriteLine("Turno Humano\nCartes disponibles: ");

                                for (int i = 0; i < naipes.Count; i++)
                                {
                                    Console.Write("(" + naipes[i].ToString() + ")");
                                    if (i < naipes.Count - 1)
                                    {
                                        Console.Write(" ");
                                    }
                                }
                                Console.Write("\nIngrese una carta ó presione ENTER para abrir las consultas: ");
                                entrada = Console.ReadLine();
                                Int32.TryParse(entrada, out carta);
                                break;
                            default:
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("ERROR: Opción Inválida. Por favor, vuelva a ingresar una opción.");
                                Console.ResetColor();
                                break;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("ERROR: Opción Inválida. Por favor, ingrese otro naipe: ");
                        Console.ResetColor();
                        entrada = Console.ReadLine();
                        Int32.TryParse(entrada, out carta);
                    }
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
            Random random = new Random();
            Console.WriteLine("\n\nLa Inteligencia Artificial está evaluando su mejor jugada..");
            Thread.Sleep(random.Next(1500,5000));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\nLa Inteligencia Artificial ha descartado la carta:" + carta.ToString() + "\n");
            Console.ResetColor();
		}
    }
}
