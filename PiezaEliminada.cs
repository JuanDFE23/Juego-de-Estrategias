using System;
using System.Collections.Generic;
using System.Text;

namespace Juego_de_Estrategias
{
    internal static class JuegoUtilidades
    {
        public static void CapturarPieza(Pieza piezaCapturada)
        {
            // Registrar la captura en el ScoreManager
            // El captor es el jugador contrario al color de la pieza capturada
            Jugador captor = (piezaCapturada.Color == Jugador.Blanco) ? Jugador.Negro : Jugador.Blanco;
            ScoreManager.RegisterCapture(captor, piezaCapturada.Tipo);

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

            // Determinar ganador/perdedor y obtener resumen de la partida actual
            var ganadorEnum = (colorPerdedor == Jugador.Blanco) ? Jugador.Negro : Jugador.Blanco;
            var resumenGanador = ScoreManager.GetScoreSummary(ganadorEnum);
            var resumenPerdedor = ScoreManager.GetScoreSummary(colorPerdedor);
            // Mostrar resumen de puntuación de la partida
            MessageBox.Show($"Puntuación final:\n\nGanador ({ganadorEnum}): {resumenGanador.puntos} pts (Soldados: {resumenGanador.soldados}, Torres: {resumenGanador.torres}, Reyes: {resumenGanador.reyes})\n\n" +
                            $"Perdedor ({colorPerdedor}): {resumenPerdedor.puntos} pts (Soldados: {resumenPerdedor.soldados}, Torres: {resumenPerdedor.torres}, Reyes: {resumenPerdedor.reyes})");

            // Sumar los puntos obtenidos en esta partida al registro persistente
            ScoreManager.AddGamePoints(ganadorEnum, resumenGanador.puntos);
            ScoreManager.AddGamePoints(colorPerdedor, resumenPerdedor.puntos);

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
