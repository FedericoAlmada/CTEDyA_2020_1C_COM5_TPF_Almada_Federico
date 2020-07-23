
using System;

namespace juegoIA
{
	class Juego
	{
		public static void Main(string[] args)
		{
            string reset = "S";
            while (reset == "S")
            {
                Game game = new Game();
                game.play();
                Console.ReadKey(true);
                Console.WriteLine("\n¿Desea volver a jugar? (S/N)");
                reset = Console.ReadLine();
                Console.Clear();
            }
		}
	}
}