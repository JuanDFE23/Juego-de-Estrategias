using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Juego_de_Estrategias
{
    internal class Torre : Pieza
    {
        public Torre(Jugador color, int f, int c) : base(color, f, c) { Tipo = TipoPieza.Torre; }

        public override bool EsMovimientoValido(int nFila, int nCol, Pieza[,] tablero)
        {
            // No puede quedarse en la misma casilla
            if (nFila == Fila && nCol == Columna) return false;

            // Se mueve en línea recta (misma fila o misma columna)
            if (Fila == nFila)
            {
                // Movimiento horizontal: comprobar que no haya piezas entre origen y destino
                int paso = (nCol > Columna) ? 1 : -1;
                for (int c = Columna + paso; c != nCol; c += paso)
                {
                    if (tablero[Fila, c] != null) return false;
                }
                return true;
            }
            else if (Columna == nCol)
            {
                // Movimiento vertical: comprobar que no haya piezas entre origen y destino
                int paso = (nFila > Fila) ? 1 : -1;
                for (int f = Fila + paso; f != nFila; f += paso)
                {
                    if (tablero[f, Columna] != null) return false;
                }
                return true;
            }

            return false;
        }
    }
}