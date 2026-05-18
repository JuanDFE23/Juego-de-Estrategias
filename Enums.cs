using System;
using System.Collections.Generic;
using System.Text;

namespace Juego_de_Estrategias
{
    internal enum Jugador
    {
        Blanco,
        Negro
    }

    internal enum TipoPieza
    {
        Rey,
        Torre,
        Soldado
    }

    internal static class TurnoExtensions
    {
        // Devuelve true si el gestor indica que es el turno del jugador indicado
        public static bool EsMiTurno(this GestorTurnos gestor, Jugador jugador)
        {
            if (gestor == null) return false;
            return gestor.TurnoActual == jugador;
        }

        // Devuelve true si el gestor indica que es el turno del color de la pieza
        public static bool EsTurnoDe(this Pieza pieza, GestorTurnos gestor)
        {
            if (pieza == null || gestor == null) return false;
            return gestor.TurnoActual == pieza.Color;
        }
    }
}
