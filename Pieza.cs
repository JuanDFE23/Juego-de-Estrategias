using System;
using System.Collections.Generic;
using System.Text;

namespace Juego_de_Estrategias
{
    internal class Pieza
    {
        public Jugador Color { get; set; }
        public TipoPieza Tipo { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }

        public Pieza(Jugador color, int fila, int columna)
        {
            Color = color;
            Fila = fila;
            Columna = columna;
        }
        public abstract bool EsMovimientoValido(int nuevaFila, int nuevaColumna, Pieza[,] tablero);
    }
}