using System;
using System.Collections.Generic;
using System.Text;

namespace Juego_de_Estrategias
{
    internal class GestorTurnos
    {
        public Jugador TurnoActual { get; private set; } = Jugador.Blanco;

        // Evento opcional para avisar a la interfaz que el turno cambió
        public event Action<Jugador> AlCambiarTurno;

        // Método para cambiar el turno
        public void CambiarTurno()
        {
            if (TurnoActual == Jugador.Blanco)
                TurnoActual = Jugador.Negro;
            else
                TurnoActual = Jugador.Blanco;

            // Disparamos el evento para que la interfaz se entere
            AlCambiarTurno?.Invoke(TurnoActual);
        }

        // Método para validar si el jugador que intenta mover es el correcto
        public bool EsTurnoDe(Jugador jugador)
        {
            return TurnoActual == jugador;
        }
    }
}