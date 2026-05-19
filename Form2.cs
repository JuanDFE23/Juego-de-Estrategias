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
        // 1. MATRICES A NIVEL DE CLASE (Para que todos los métodos puedan verlas)
        private Pieza[,] tableroLogico = new Pieza[8, 8];
        private Panel[,] panelesTablero = new Panel[8, 8];

        // Tamaño en píxeles de cada casilla del tablero (8x8)
        private const int tamañoCasilla = 60;

        // 2. Variables para recordar la selección del usuario
        private bool hayPiezaSeleccionada = false;
        private int filaSeleccionada = -1;
        private int columnaSeleccionada = -1;
        private Color colorOriginalPanel;

        // 3. Tu gestor de turnos
        private GestorTurnos gestor;
        // Cache de imágenes generadas para las piezas
        private Dictionary<string, Image> imagenesPiezas;

        public FormJuego()
        {
            InitializeComponent();
            gestor = new GestorTurnos();
            gestor.AlCambiarTurno += ActualizarInterfazTurno;
        }

        private void FormJuego_Load(object sender, EventArgs e)
        {
            // Ajustar el tamaño del cliente del formulario para que quepa el tablero completo
            this.ClientSize = new Size(tamañoCasilla * 8, tamañoCasilla * 8);
            // Opcional: evitar redimensionado que pueda romper el layout
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Inicializar y crear tablero
            InicializarPiezas();
            CrearTablero();
            // Generar imágenes para las piezas (placeholders estilizadas)
            CrearRecursosPiezas();
            ActualizarTableroVisual();
        }

        private void CrearTablero()
        {
            // Asegurarnos de empezar desde cero en la interfaz antes de crear paneles
            this.Controls.Clear();
            panelesTablero = new Panel[8, 8];

            // Usar la constante de clase tamañoCasilla

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

                    // NUEVO: Asegura que la imagen de la pieza se escale correctamente dentro del panel
                    Casilla.BackgroundImageLayout = ImageLayout.Zoom;

                    // ... (código previo dentro de tu for en CrearTablero)
                    this.Controls.Add(Casilla);
                    Casilla.Name = $"Casilla_{fila}_{columna}";
                    panelesTablero[fila, columna] = Casilla; // (La matriz que agregamos antes)

                    // NUEVO: Guardamos sus coordenadas usando un objeto Point (X=columna, Y=fila)
                    Casilla.Tag = new Point(columna, fila);

                    // NUEVO: Suscribimos el panel al evento Clic
                    Casilla.Click += Casilla_Click;
                }
            }
        }

        // instancia de gestor ya declarada arriba

        private void ActualizarInterfazTurno(Jugador nuevoTurno)
        {
            this.Text = "Juego de Ajedrez - Turno de: " + nuevoTurno.ToString();
        }

        // Ejemplo de cómo usarlo cuando un jugador hace un movimiento válido
        private void FinalizarMovimiento()
        {
            gestor.CambiarTurno();
        }

        // Matriz de 8x8 que guarda objetos de tipo Pieza (declarada a nivel de clase arriba)

        // Ejemplo de cómo inicializar tus piezas:
        public void InicializarPiezas()
        {
            // Limpiar cualquier estado previo
            Array.Clear(tableroLogico, 0, tableroLogico.Length);

            // Blancas (en la fila 7)
            tableroLogico[7, 4] = new Rey(Jugador.Blanco, 7, 4);
            tableroLogico[7, 2] = new Torre(Jugador.Blanco, 7, 2);
            tableroLogico[7, 5] = new Torre(Jugador.Blanco, 7, 5);

            // 4 Soldados blancos (en la fila 6)
            for (int i = 2; i < 6; i++)
                tableroLogico[6, i] = new Soldado(Jugador.Blanco, 6, i);

            // Negras (posición espejo: filas 0 y 1)
            tableroLogico[0, 4] = new Rey(Jugador.Negro, 0, 4);
            tableroLogico[0, 2] = new Torre(Jugador.Negro, 0, 2);
            tableroLogico[0, 5] = new Torre(Jugador.Negro, 0, 5);
            // Soldados negros en la fila 1
            for (int i = 2; i < 6; i++)
                tableroLogico[1, i] = new Soldado(Jugador.Negro, 1, i);
        }

        // Método público para preparar un escenario de pruebas con piezas contrarias
        public void PrepararEscenarioPruebas()
        {
            // Inicializa las piezas blancas (limpia primero)
            Array.Clear(tableroLogico, 0, tableroLogico.Length);
            InicializarPiezas();

            // Colocar algunas piezas negras para pruebas de captura
            // Colocamos una torre negra frente a un soldado blanco para verificar captura
            tableroLogico[5, 3] = new Torre(Jugador.Negro, 5, 3);
            // Colocamos un rey negro para comprobar detección de fin de juego
            tableroLogico[0, 0] = new Rey(Jugador.Negro, 0, 0);
        }

        public bool IntentarMover(int filaOrigen, int colOrigen, int filaDestino, int colDestino)
        {
            Pieza piezaAtacante = tableroLogico[filaOrigen, colOrigen];
            Pieza piezaObjetivo = tableroLogico[filaDestino, colDestino];

            // 1. Validar que haya una pieza en el origen
            if (piezaAtacante == null) return false;

            // 2. Validar que el movimiento sea legal para esa pieza
            if (!piezaAtacante.EsMovimientoValido(filaDestino, colDestino, tableroLogico)) return false;

            // 3. Lógica de captura: validación previa
            if (piezaObjetivo != null)
            {
                // Si es del mismo color, no se puede mover ahí
                if (piezaObjetivo.Color == piezaAtacante.Color)
                    return false;
            }

            // 4. Ejecutar el movimiento en la matriz
            tableroLogico[filaDestino, colDestino] = piezaAtacante;
            tableroLogico[filaOrigen, colOrigen] = null;

            // Actualizar coordenadas internas de la pieza
            piezaAtacante.Fila = filaDestino;
            piezaAtacante.Columna = colDestino;

            // Si hubo captura, procesarla ahora (después del movimiento)
            if (piezaObjetivo != null)
            {
                JuegoUtilidades.CapturarPieza(piezaObjetivo);
            }

            ActualizarTableroVisual();
            return true;
        }

        public void ReiniciarJuego()
        {
            // 1. Resetear el gestor de turnos
            gestor = new GestorTurnos();

            // Volver a suscribir el evento para actualizar la interfaz
            gestor.AlCambiarTurno += ActualizarInterfazTurno;

            // 2. Limpiar la matriz lógica (borrar piezas anteriores)
            Array.Clear(tableroLogico, 0, tableroLogico.Length);

            // 3. Volver a colocar las piezas en sus posiciones iniciales
            InicializarPiezas();

            // 4. Limpiar la interfaz gráfica y volver a dibujarla
            this.Controls.Clear(); // Quita todos los paneles actuales
            CrearTablero();        // Llama a la función que dibuja el tablero y las piezas
            ActualizarTableroVisual();

            MessageBox.Show("El juego ha sido reiniciado. Turno de Blancas.");
        }

        // Este método actualiza toda la parte gráfica basándose en la lógica
        public void ActualizarTableroVisual()
        {
            for (int fila = 0; fila < 8; fila++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Pieza pieza = tableroLogico[fila, col];
                    Panel panelActual = panelesTablero[fila, col];

                    if (pieza != null)
                    {
                        // Si hay una pieza en la lógica, le ponemos su imagen al panel
                        panelActual.BackgroundImage = ObtenerImagenPieza(pieza);
                    }
                    else
                    {
                        // Si la casilla está vacía, quitamos la imagen
                        panelActual.BackgroundImage = null;
                    }
                }
            }
        }

        // Este método traduce tus objetos lógicos a imágenes de los recursos
        private Image ObtenerImagenPieza(Pieza p)
        {
            if (p == null) return null;

            string clave = null;
            if (p is Rey) clave = $"Rey_{p.Color}";
            else if (p is Torre) clave = $"Torre_{p.Color}";
            else if (p is Soldado) clave = $"Soldado_{p.Color}";

            if (clave != null && imagenesPiezas != null && imagenesPiezas.ContainsKey(clave))
                return imagenesPiezas[clave];

            return null;
        }

        // Genera imágenes simples para cada tipo/color de pieza y las guarda en imagenesPiezas
        private void CrearRecursosPiezas()
        {
            imagenesPiezas = new Dictionary<string, Image>();
            int size = 48;

            void Agregar(string nombre, Color fondo, string letra, Color letraColor)
            {
                Bitmap bmp = new Bitmap(size, size);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.Clear(Color.Transparent);
                    using (Brush b = new SolidBrush(fondo))
                    {
                        g.FillEllipse(b, 2, 2, size - 4, size - 4);
                    }
                    using (Font f = new Font("Segoe UI", 18, FontStyle.Bold, GraphicsUnit.Pixel))
                    using (Brush br = new SolidBrush(letraColor))
                    {
                        SizeF textSize = g.MeasureString(letra, f);
                        g.DrawString(letra, f, br, (size - textSize.Width) / 2, (size - textSize.Height) / 2 - 2);
                    }
                    // borde
                    using (Pen pen = new Pen(Color.FromArgb(160, Color.Black), 1))
                    {
                        g.DrawEllipse(pen, 2, 2, size - 4, size - 4);
                    }
                }
                imagenesPiezas[nombre] = bmp;
            }

            // Blancas: fondo claro, letra negra
            Agregar("Rey_Blanco", Color.Beige, "R", Color.Black);
            Agregar("Torre_Blanco", Color.Beige, "T", Color.Black);
            Agregar("Soldado_Blanco", Color.Beige, "S", Color.Black);

            // Negras: fondo oscuro, letra blanca
            Agregar("Rey_Negro", Color.DimGray, "R", Color.White);
            Agregar("Torre_Negro", Color.DimGray, "T", Color.White);
            Agregar("Soldado_Negro", Color.DimGray, "S", Color.White);
        }

        private void Casilla_Click(object sender, EventArgs e)
        {
            // 1. Averiguar qué panel se clickeó y sus coordenadas
            Panel panelClickeado = sender as Panel;
            if (panelClickeado == null) return;
            if (panelClickeado.Tag == null) return;

            Point coordenadas = (Point)panelClickeado.Tag;

            int colClic = coordenadas.X;
            int filaClic = coordenadas.Y;

            // ESTADO 1: El jugador no tiene ninguna pieza seleccionada aún
            if (!hayPiezaSeleccionada)
            {
                // Verificar que haya una pieza en la casilla que tocó
                if (tableroLogico[filaClic, colClic] != null)
                {
                    // Solo permitir seleccionar si es el turno del color de esa pieza
                    var pieza = tableroLogico[filaClic, colClic];
                    if (!pieza.EsTurnoDe(gestor))
                    {
                        MessageBox.Show("No es el turno de ese jugador.", "Turno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    hayPiezaSeleccionada = true;
                    filaSeleccionada = filaClic;
                    columnaSeleccionada = colClic;

                    // Guardamos el color original (blanco o gris) y resaltamos la casilla en verde
                    colorOriginalPanel = panelClickeado.BackColor;
                    panelClickeado.BackColor = Color.LightGreen;
                }
            }
            // ESTADO 2: El jugador ya tenía una pieza seleccionada y eligió el destino
            else
            {
                // Si hace clic en la misma casilla de nuevo, la deseleccionamos (se arrepintió)
                if (filaClic == filaSeleccionada && colClic == columnaSeleccionada)
                {
                    DeseleccionarPieza();
                    return; // Salimos del método
                }

                // Llamamos a tu método lógico para ver si el movimiento cumple las reglas
                bool movimientoExitoso = IntentarMover(filaSeleccionada, columnaSeleccionada, filaClic, colClic);

                if (movimientoExitoso)
                {
                    // Si el movimiento fue legal, cambiamos de turno
                    FinalizarMovimiento();
                }
                else
                {
                    // Si las reglas de la pieza no lo permiten, le avisamos
                    MessageBox.Show("Movimiento no válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Sin importar si el movimiento fue exitoso o falló, limpiamos la selección visual
                DeseleccionarPieza();
            }
        }
        private void DeseleccionarPieza()
        {
            if (hayPiezaSeleccionada)
            {
                // Le devolvemos su color original al panel que estaba verde
                Panel panelAnterior = panelesTablero[filaSeleccionada, columnaSeleccionada];
                if (panelAnterior != null)
                    panelAnterior.BackColor = colorOriginalPanel;

                // Reseteamos las variables
                hayPiezaSeleccionada = false;
                filaSeleccionada = -1;
                columnaSeleccionada = -1;
            }
        }
    }
}