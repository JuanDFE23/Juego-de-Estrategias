using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Juego_de_Estrategias
{
    public partial class FormJuego : Form
    {
        public FormJuego()
        {
            InitializeComponent();
            gestor = new GestorTurnos();

            // Suscribirse al evento para actualizar un Label o título en la interfaz
            gestor.AlCambiarTurno += ActualizarInterfazTurno;
        }

        private void FormJuego_Load(object sender, EventArgs e)
        {
            CrearTablero();
        }

        private void CrearTablero()
        {
            int tamañoCasilla = 60;

            for (int fila = 0; fila < 8; fila++)
            {
                for (int columna = 0; columna < 8; columna++)
                {
                    Panel Casilla = new Panel();
                    Casilla.Size = new Size(tamañoCasilla, tamañoCasilla);
                    Casilla.Location = new Point(columna * tamañoCasilla, fila * tamañoCasilla);

                    if ((fila + columna) % 2 == 0)
                    {
                        Casilla.BackColor = Color.White;

                    }
                    else
                    {
                        Casilla.BackColor = Color.Gray;
                    }
                    this.Controls.Add(Casilla);
                    Casilla.Name = $"Casilla_{fila}_{columna}";
                }
            }
        }

        private GestorTurnos gestor; // Instancia de nuestra clase

        private void ActualizarInterfazTurno(Jugador nuevoTurno)
        {
            this.Text = "Juego de Ajedrez - Turno de: " + nuevoTurno.ToString();
        }

        // Ejemplo de cómo usarlo cuando un jugador hace un movimiento válido
        private void FinalizarMovimiento()
        {
            gestor.CambiarTurno();
        }

        // Matriz de 8x8 que guarda objetos de tipo Pieza
        Pieza[,] tableroLogico = new Pieza[8, 8];

        // Ejemplo de cómo inicializar tus piezas:
        void InicializarPiezas()
        {
            // Blancas (en la fila 7)
            tableroLogico[7, 4] = new Rey(Jugador.Blanco, 7, 4);
            tableroLogico[7, 0] = new Torre(Jugador.Blanco, 7, 0);
            tableroLogico[7, 7] = new Torre(Jugador.Blanco, 7, 7);

            // 4 Soldados blancos (en la fila 6)
            for (int i = 2; i < 6; i++)
                tableroLogico[6, i] = new Soldado(Jugador.Blanco, 6, i);
        }

        public bool IntentarMover(int filaOrigen, int colOrigen, int filaDestino, int colDestino)
        {
            Pieza piezaAtacante = tableroLogico[filaOrigen, colOrigen];
            Pieza piezaObjetivo = tableroLogico[filaDestino, colDestino];

            // 1. Validar que haya una pieza en el origen
            if (piezaAtacante == null) return false;

            // 2. Validar que el movimiento sea legal para esa pieza
            if (!piezaAtacante.EsMovimientoValido(filaDestino, colDestino, tableroLogico)) return false;

            // 3. Lógica de captura
            if (piezaObjetivo != null)
            {
                // Si es del mismo color, no se puede mover ahí
                if (piezaObjetivo.Color == piezaAtacante.Color)
                    return false;

                // Si es color opuesto, la capturamos
                CapturarPieza(piezaObjetivo);
            }

            // 4. Ejecutar el movimiento en la matriz
            tableroLogico[filaDestino, colDestino] = piezaAtacante;
            tableroLogico[filaOrigen, colOrigen] = null;

            // Actualizar coordenadas internas de la pieza
            piezaAtacante.Fila = filaDestino;
            piezaAtacante.Columna = colDestino;

            return true;
        }

        private void ReiniciarJuego()
        {
            // 1. Resetear el gestor de turnos
            gestor = new GestorTurnos();

            // 2. Limpiar la matriz lógica (borrar piezas anteriores)
            Array.Clear(tableroLogico, 0, tableroLogico.Length);

            // 3. Volver a colocar las piezas en sus posiciones iniciales
            InicializarPiezas();

            // 4. Limpiar la interfaz gráfica y volver a dibujarla
            this.Controls.Clear(); // Quita todos los paneles actuales
            CrearTablero();        // Llama a la función que dibuja el tablero y las piezas

            MessageBox.Show("El juego ha sido reiniciado. Turno de Blancas.");
        }
    }
}