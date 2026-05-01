using System;
using System.Collections.Generic;
using System.Text;

namespace Juego_de_Estrategias
{
    internal class Rey
    {
        public Rey(Jugador color, int f, int c) : base(color, f, c) { Tipo = TipoPieza.Rey; }

        public override bool EsMovimientoValido(int nFila, int nCol, Pieza[,] tablero)
        {
            // El Rey se mueve solo 1 casilla en cualquier dirección
            return Math.Abs(nFila - Fila) <= 1 && Math.Abs(nCol - Columna) <= 1;
        }
    }
}