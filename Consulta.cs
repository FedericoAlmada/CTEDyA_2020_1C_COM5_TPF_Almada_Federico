using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace juegoIA
{
    public class Consulta
    {
        List<ArbolGeneral<Carta>> camino;
        public ArbolGeneral<Carta> jugadaActual = new ArbolGeneral<Carta>(new Carta(0, 0));
        public ArbolGeneral<Carta> arbolRaiz = new ArbolGeneral<Carta>(new Carta(0, 0));

        public Consulta(){}
        public Consulta(ArbolGeneral<Carta> jugadaActual)
        {
            this.jugadaActual = jugadaActual;
            this.arbolRaiz = jugadaActual;
        }

        public void consultaA()
        {
            camino = new List<ArbolGeneral<Carta>>();
            List<ArbolGeneral<Carta>> x = _consultaA(jugadaActual);
            imprimir(x);
        }

        private List<ArbolGeneral<Carta>> _consultaA(ArbolGeneral<Carta> jugadaActual)
        {
            camino.Add(jugadaActual);

            if (jugadaActual.esHoja()) // Si se encuentra un nodo hoja, se lo agrega y se agrega un separador.
            {
                camino.Add(new ArbolGeneral<Carta>(new Carta(0, 0)));
            }
            else
            {
                foreach (ArbolGeneral<Carta> hijo in jugadaActual.getHijos())
                {
                    _consultaA(hijo);
                }
            }

            return camino;
        }

        public void imprimir(List<ArbolGeneral<Carta>> camino)
        {
            int valor = 0;
            foreach (var x in camino)
            {
                if ((valor % 2) == 0) // Si es par entonces es una carta del Humano
                {
                    if (x.getDatoRaiz().getFuncHeursitica() == 0) // si tiene 0, entonces lo descarto
                    {
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("(" + x.getDatoRaiz().getCarta() + ", " + x.getDatoRaiz().getFuncHeursitica() + ") ");
                        Console.ResetColor();
                    }
                }
                else
                {
                    if (x.getDatoRaiz().getFuncHeursitica() == 0) // si tiene 0, entonces lo descarto
                        Console.WriteLine();
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("(" + x.getDatoRaiz().getCarta() + ", " + x.getDatoRaiz().getFuncHeursitica() + ") ");
                        Console.ResetColor();
                    }
                }
                valor++;
            }
        }   

        public void consultaB()
        {
            ArbolGeneral<Carta> jugada = _consultaB(jugadaActual);
            List<ArbolGeneral<Carta>> camino = _consultaA(jugada);
            imprimir(camino);
        }

        private ArbolGeneral<Carta> _consultaB(ArbolGeneral<Carta> jugadaActual)
        {
            Console.Write("Ingrese una secuencia de cartas separadas por comas: ");
            string numeros = Console.ReadLine();

            string[] cartas = numeros.Split(','); // se Splitea las cartas que haya escrito el usuario en la secuencia
            ArbolGeneral<Carta> aux = new ArbolGeneral<Carta>(new Carta(0, 0));
            aux = jugadaActual; // aux apunta a la jugada actual

            foreach (string carta in cartas) // se recorre carta de la secuencia
            {
                foreach (ArbolGeneral<Carta> hijo in aux.getHijos()) //se reccoren los hijos del arbol aux
                {
                    if (hijo.getDatoRaiz().getCarta() == Convert.ToInt32(carta)) // si existe
                    {
                        aux = hijo; // Aux apunta a la carta actual
                        break;
                    }
                }
            }
            return aux;
        }

        public void consultaC()
        {
            Console.WriteLine("Ingrese un nivel: ");
            int nivel = int.Parse(Console.ReadLine());
            _consultaC(nivel);
        }

        private List<ArbolGeneral<Carta>> _consultaC(int nivel)
        {
            if (jugadaActual.esHoja())
                camino.Add(jugadaActual);
            else
            {
                Cola<ArbolGeneral<Carta>> c = new Cola<ArbolGeneral<Carta>>(); // Instanciamos cola
                ArbolGeneral<Carta> arbolAux;
                int contNivel = 0;
                c.encolar(jugadaActual); 	// se encola el arbol
                c.encolar(null);	// se encola el separador

                while (!c.esVacia())
                {
                    arbolAux = c.desencolar();

                    if (arbolAux != null)
                    {
                        if (nivel == contNivel) // Si  la profundidad elegida es igual a la profundidad recorrida
                        {
                            if ((contNivel % 2) == 0) // Si es par, se procesan las cartas de la IA
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write("(" + arbolAux.getDatoRaiz().getCarta() + ", " + arbolAux.getDatoRaiz().getFuncHeursitica() + ")");
                                Console.ResetColor();
                            }
                            else // Si no del humano
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("(" + arbolAux.getDatoRaiz().getCarta() + ", " + arbolAux.getDatoRaiz().getFuncHeursitica() + ")");
                                Console.ResetColor();
                            }
                        }
                        // Se encolan los hijos (si tiene)
                        foreach (ArbolGeneral<Carta> hijo in arbolAux.getHijos())
                        {
                            c.encolar(hijo);
                        }
                    }
                    // Si desencolamos null (separador):
                    else
                    {
                        contNivel++;			// se incrementa el contador de nivel.
                        if (!c.esVacia())		// Si la cola no se vacio,
                            c.encolar(null);	// se encola null (separador)
                    }

                }
            }
            return camino;
        }

        public void reiniciarJuego()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Juego reiniciado.");
            Console.ResetColor();
            Game juego = new Game();
            juego.play();
        }

        public ArbolGeneral<Carta> getJugadaActual()
        {
            return this.jugadaActual;
        }

        public void setJugadaActual(ArbolGeneral<Carta> jugadaActual)
        {
            this.jugadaActual = jugadaActual;
        }
    }
}
