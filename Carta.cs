using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace juegoIA
{
    class Carta<int><int>
    {
        private int carta;
        private int funcHeuristica;

        public Carta(int carta, int funcHeuristica) 
        {
            this.carta = carta;
            this.funcHeuristica = funcHeuristica;
        }

        public void setCarta(int carta)
        {
            this.carta = carta;
        }
        public void setFuncHeuristica(int funcHeuristica)
        {
            this.funcHeuristica = funcHeuristica;
        }
        public int getCarta()
        {
            return carta;
        }

        public int getFuncHeursitica()
        {
            return funcHeuristica;
        }
    }
}
