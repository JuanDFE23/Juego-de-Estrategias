using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Juego_de_Estrategias
{
    internal class Soldados
    {
        public Soldado(Jugador color, int f, int c) : base(color, f, c) { Tipo = TipoPieza.Soldado; }

        public override bool EsMovimientoValido(int nFila, int nCol, Pieza[,] tablero)
        {
            // Lógica simplificada: mueve 1 hacia adelante
            int direccion = (Color == Jugador.Blanco) ? -1 : 1;
            return (nCol == Columna && nFila == Fila + direccion);
        }
    }
}