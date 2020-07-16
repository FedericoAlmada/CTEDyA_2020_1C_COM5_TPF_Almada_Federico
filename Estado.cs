using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace juegoIA
{
    class Estado
    {
        private List<int> naipesHumano = new List<int>();
        private List<int> naipesComputer = new List<int>();
        private int limite;
        private bool turnoHumano;

        public List<int> getCartasH()
        {
            return naipesHumano;
        }

        public List<int> getCartasIA()
        {
            return naipesComputer;
        }

        public int getLimite()
        {
            return limite;
        }

        public bool getTurnoH()
        {
            return turnoHumano;
        }

        public void setCartasH(List<int> cartasH)
        {
            this.naipesHumano = cartasH;
        }

        public void setCartasIA(List<int> cartasIA)
        {
            this.naipesComputer = cartasIA;
        }

        public void setLimite(int limite)
        {
            this.limite = limite;
        }

        public void setTurnoH(bool turnoH)
        {
            this.turnoHumano = turnoH;
        }
    }
}
