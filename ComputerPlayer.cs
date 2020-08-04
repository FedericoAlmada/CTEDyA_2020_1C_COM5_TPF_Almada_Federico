using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
    public class ComputerPlayer: Jugador
    {
        Estado estado;
        private ArbolGeneral<Carta> miniMax = new ArbolGeneral<Carta>(new Carta(0, 0));	// arbol de todas las jugadas
        private ArbolGeneral<Carta> jugadaActual = new ArbolGeneral<Carta>(new Carta(0, 0)); // puntero del arbol

        public ComputerPlayer(){}

        public ComputerPlayer(Consulta consulta)
        {
            this.consulta = consulta;
        }

        public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
        {
            bool turnoHumano = true;
            estado = new Estado(cartasPropias, cartasOponente, limite, turnoHumano);
            createArbol(estado, miniMax);
            jugadaActual = miniMax;
            consulta.setJugadaActual(jugadaActual);
        }

        private void createArbol(Estado estado, ArbolGeneral<Carta> raiz)
        {
            if (estado.getTurnoH()) // turno del Humano
            {
                foreach (int cartaH in estado.getCartasH())
                {
                    Carta carta = new Carta(cartaH, 0);
                    ArbolGeneral<Carta> hijo = new ArbolGeneral<Carta>(carta);

                    raiz.agregarHijo(hijo);

                    int nuevoLimite = estado.getLimite() - cartaH;

                    if (nuevoLimite >= 0) // si no es el caso base, actualizo el estado y sigo armando el arbol.
                    {
                        List<int> nuevasCartasH = new List<int>();
                        nuevasCartasH.AddRange(estado.getCartasH());
                        nuevasCartasH.Remove(cartaH);

                        Estado nuevoEstado = new Estado(estado.getCartasIA(), nuevasCartasH, nuevoLimite, false);
                        createArbol(nuevoEstado, hijo);

                        // Función heuristica MiniMax
                        bool max = false;

                        foreach (var nodo in hijo.getHijos())
                        {
                            if (nodo.getDatoRaiz().getFuncHeursitica() == 1) // Si existe al menos un hijo con FH +1, se maximiza
                            {
                                max = true;
                                break; // agrego un break para cortar la busqueda.
                            }
                        }

                        if(max)
                            hijo.getDatoRaiz().setFuncHeuristica(1);

                        else
                            hijo.getDatoRaiz().setFuncHeuristica(-1);
                    }
                    else // si es el caso base, entonces asigno la funcion heuristica al nodo hijo.
                    {
                        hijo.getDatoRaiz().setFuncHeuristica(1); // Pierde el humano, gana la IA.
                    }
                }
            }
            else// turno de la IA
            {
                foreach (int cartaIA in estado.getCartasIA())
                {
                    Carta carta = new Carta(cartaIA, 0);
                    ArbolGeneral<Carta> hijo = new ArbolGeneral<Carta>(carta);

                    raiz.agregarHijo(hijo);

                    int nuevoLimite = estado.getLimite() - cartaIA;

                    if (nuevoLimite >= 0) // si no es el caso base, actualizo el estado y sigo armando el arbol.
                    {
                        List<int> nuevasCartasIA = new List<int>();
                        nuevasCartasIA.AddRange(estado.getCartasIA());
                        nuevasCartasIA.Remove(cartaIA);

                        Estado nuevoEstado = new Estado(nuevasCartasIA, estado.getCartasH(), nuevoLimite, true);
                        createArbol(nuevoEstado, hijo); // Se sigue armando el arbol hasta que haya un nodo hoja.

                        // Función heuristica MiniMax
                        bool min = false;

                        foreach (var nodo in hijo.getHijos()) // Se recorren los hijos del hijo raiz
                        {
                            if (nodo.getDatoRaiz().getFuncHeursitica() == -1) // Si existe al menos un hijo con FH -1, se minimiza
                            {
                                min = true;
                                break; // Agrego un break para cortar la busqueda.
                            }
                        }

                        if (min)
                            hijo.getDatoRaiz().setFuncHeuristica(-1);

                        else
                            hijo.getDatoRaiz().setFuncHeuristica(1);
                    }
                    else // si es el caso base, entonces asigno la funcion heuristica al nodo hijo.
                    {
                        hijo.getDatoRaiz().setFuncHeuristica(-1); // Pierde la IA, gana el Humano.
                    }
                }
            }
        }

        public override int descartarUnaCarta()
        {
            // Dependiendo el valor de la función heuristica, si es a favor de la IA, se muestra de color verde, sino de rojo.

            Console.WriteLine("Turno IA\nCartas disponibles:");

            foreach (var carta in estado.getCartasIA()) // Se recorren todas las cartas que tenga disponible la IA
            {
                var funcHeuristicaAux = 0; 

                foreach (var nodo in jugadaActual.getHijos()) 
                {
                    if (carta == nodo.getDatoRaiz().getCarta())
                        funcHeuristicaAux = nodo.getDatoRaiz().getFuncHeursitica();
                }

                if (funcHeuristicaAux == 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("(" + carta + ", " + "+" + funcHeuristicaAux + ") ");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("(" + carta + ", " + funcHeuristicaAux + ") ");
                    Console.ResetColor();
                }
            }
            return _descartarUnaCarta(jugadaActual);
        }

        private int _descartarUnaCarta(ArbolGeneral<Carta> raiz)
        {
            // Con el siguiente algoritmo se guardan las cartas donde la IA tiene una jugada aseguarada para ganar el juego.

            Random random = new Random();
            List<ArbolGeneral<Carta>> opciones = new List<ArbolGeneral<Carta>>(); // Lista que almacena todas las posibilidades de victoria.
            int i = 0;

            // Se crea la lista de opciones de cartas donde la IA tiene asegurada una victoria.
            foreach (var hijo in raiz.getHijos())
            {
                if (hijo.getDatoRaiz().getFuncHeursitica() == 1) // Si tiene un hijo con FH +1, entonces se lo agrega a la lista de opciones.
                    opciones.Add(hijo);
            }

            if (opciones.Count == 0) // Si la IA no tiene una jugada asegurada de victoria, entonces tira la última carta.
            {
                foreach (var hijo in raiz.getHijos())
                {
                    if (hijo.getDatoRaiz().getFuncHeursitica() == -1)
                        jugadaActual = hijo;
                }
            }
            else // Si la IA tiene opciones aseguradas para ganar.
            {
                int opcion = random.Next(1, opciones.Count); // Se crea un valor random entre 1 y la cantidad de opciones que haya.
                foreach (var o in opciones) // Se recorren todas las opciones.
                {
                    i++; // Se incrementa el contador a medida que se recorren las opciones.

                    if (i == opcion) // Si el contador es igual a el número de opción aleatoria.
                    {
                        jugadaActual = o; // Entonces, jugadaActual apunta a esa opción.
                        break;
                    }
                }
            }
            return jugadaActual.getDatoRaiz().getCarta(); // Se retorna la carta elegida.
        }

        public override void cartaDelOponente(int cartaH)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nEl humano ha descartado la carta: " + cartaH.ToString() + "\n");
            Console.ResetColor();

            foreach (ArbolGeneral<Carta> hijo in jugadaActual.getHijos())
            {
                if (hijo.getDatoRaiz().getCarta() == cartaH)
                {
                    jugadaActual = hijo; // jugada actual apunta a la carta que tiro el humano
                    break;
                }
            }
            consulta.setJugadaActual(jugadaActual);
        }

        public void setConsulta(Consulta consulta)
        {
            this.consulta = consulta;
        }
    }
}