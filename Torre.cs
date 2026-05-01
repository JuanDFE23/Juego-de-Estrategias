using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Juego_de_Estrategias
{
    internal class Torre
    {
        public Torre(Jugador color, int f, int c) : base(color, f, c) { Tipo = TipoPieza.Torre; }

        public override bool EsMovimientoValido(int nFila, int nCol, Pieza[,] tablero)
        {
            // Se mueve en línea recta (misma fila o misma columna)
            return (Fila == nFila || Columna == nCol);
        }
    }
}