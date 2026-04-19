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
}
