using System;
using System.Collections.Generic;
using System.Text;

namespace Juego_de_Estrategias
{
    internal class Rey : Pieza
    {
        public Rey(Jugador color, int f, int c) : base(color, f, c) { Tipo = TipoPieza.Rey; }

        public override bool EsMovimientoValido(int nFila, int nCol, Pieza[,] tablero)
        {
            // El Rey se mueve solo 1 casilla en cualquier dirección y no puede quedarse en la misma casilla
            int dFila = Math.Abs(nFila - Fila);
            int dCol = Math.Abs(nCol - Columna);
            if (dFila == 0 && dCol == 0) return false;
            return dFila <= 1 && dCol <= 1;
        }
    }
}