using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Juego_de_Estrategias
{
	public partial class Form3 : Form
	{
		public Form3()
		{
			InitializeComponent();
		}

		private void Form3_Load(object sender, EventArgs e)
		{

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			// Iniciar partida: abrir la ventana del juego y ocultar el menú
			FormJuego juego = new FormJuego();
			juego.Show();
			this.Hide();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			string reglas = "Reglas básicas:\n- Torre: se mueve en líneas rectas sin saltar piezas.\n- Rey: se mueve 1 casilla en cualquier dirección.\n- Soldado: avanza 1 casilla hacia adelante y captura en diagonales delanteras.\n- Capturar el rey finaliza la partida.";
			MessageBox.Show(reglas, "Reglas del juego", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			var top = ScoreManager.GetLeaderboard();
			if (top == null || top.Count == 0)
			{
				MessageBox.Show("Aún no hay puntajes registrados", "Leaderboard", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			StringBuilder sb = new StringBuilder();
			int place = 1;
			foreach (var r in top)
			{
				sb.AppendLine($"{place}. {r.Jugador}: {r.Puntos} pts");
				place++;
			}
			MessageBox.Show(sb.ToString(), "Puntaje más alto", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			// Salir
			Application.Exit();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			var res = MessageBox.Show("¿Seguro que deseas reiniciar todos los puntajes guardados?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (res == DialogResult.Yes)
			{
				ScoreManager.ResetLeaderboard();
				MessageBox.Show("Puntajes reiniciados.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}
