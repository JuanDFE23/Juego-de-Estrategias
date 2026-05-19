using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Juego_de_Estrategias
{
    internal class Soldado : Pieza
    {
        public Soldado(Jugador color, int f, int c) : base(color, f, c) { Tipo = TipoPieza.Soldado; }

        public override bool EsMovimientoValido(int nFila, int nCol, Pieza[,] tablero)
        {
            int direccion = (Color == Jugador.Blanco) ? -1 : 1;

            // Movimiento hacia adelante una casilla (debe estar vacía)
            if (nCol == Columna && nFila == Fila + direccion)
            {
                return tablero[nFila, nCol] == null;
            }

            // Captura en diagonal hacia adelante (una casilla diagonaly) solo si hay pieza enemiga
            if (Math.Abs(nCol - Columna) == 1 && nFila == Fila + direccion)
            {
                Pieza objetivo = tablero[nFila, nCol];
                if (objetivo != null && objetivo.Color != this.Color)
                    return true;
            }

            return false;
        }
    }
}