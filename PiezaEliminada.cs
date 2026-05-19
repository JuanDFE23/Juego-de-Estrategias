using System;
using System.Collections.Generic;
using System.Text;

namespace Juego_de_Estrategias
{
    internal static class JuegoUtilidades
    {
        public static void CapturarPieza(Pieza piezaCapturada)
        {
            // Si capturan al Rey, el juego termina
            if (piezaCapturada.Tipo == TipoPieza.Rey)
            {
                FinalizarJuego(piezaCapturada.Color);
            }

            // Opcional: Agregar a una lista de piezas capturadas para la interfaz
            Console.WriteLine($"Pieza {piezaCapturada.Tipo} de color {piezaCapturada.Color} capturada.");
        }

        public static void FinalizarJuego(Jugador colorPerdedor)
        {
            string ganador = (colorPerdedor == Jugador.Blanco) ? "Negras" : "Blancas";
            MessageBox.Show($"¡Jaque Mate! El ganador es el jugador de las {ganador}");

            // Registrar victoria
            var ganadorEnum = (colorPerdedor == Jugador.Blanco) ? Jugador.Negro : Jugador.Blanco;
            ScoreManager.AddWin(ganadorEnum);

            // Intentar encontrar una instancia abierta de FormJuego y reiniciarla
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is FormJuego juego)
                {
                    // Reiniciamos posición y UI
                    juego.ReiniciarJuego();
                    break;
                }
            }
        }
    }
}
