using System;
using System.Collections.Generic;
using System.Text;

namespace Juego_de_Estrategias
{
    internal class PiezaEliminada
    {
        private void CapturarPieza(Pieza piezaCapturada)
        {
            // Si capturan al Rey, el juego termina
            if (piezaCapturada.Tipo == TipoPieza.Rey)
            {
                FinalizarJuego(piezaCapturada.Color);
            }

            // Opcional: Agregar a una lista de piezas capturadas para la interfaz
            Console.WriteLine($"Pieza {piezaCapturada.Tipo} de color {piezaCapturada.Color} capturada.");
        }

        private void FinalizarJuego(Jugador colorPerdedor)
        {
            string ganador = (colorPerdedor == Jugador.Blanco) ? "Negras" : "Blancas";
            MessageBox.Show($"¡Jaque Mate! El ganador es el jugador de las {ganador}");
            // Aquí podrías reiniciar el juego o volver al Login
        }
    }
}
