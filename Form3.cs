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
			// textbox1 es solo visual en este menú
		}

		private void button1_Click(object sender, EventArgs e)
		{
			// Iniciar Partida: abrir FormJuego
			FormJuego juego = new FormJuego();
			juego.Show();
			this.Hide();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			string reglas = "Reglas básicas:\n- Mueve según la pieza.\n- Capturar el Rey provoca jaque mate.\n- Turnos alternos.";
			MessageBox.Show(reglas, "Reglas del juego", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			var top = ScoreManager.GetLeaderboard();
			if (top == null || top.Count == 0)
			{
				MessageBox.Show("No hay puntajes aún.", "Puntaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			StringBuilder sb = new StringBuilder();
			int rank = 1;
			foreach (var r in top)
			{
				sb.AppendLine($"{rank}. {r.Jugador} - {r.Victorias} victorias");
				rank++;
			}
			MessageBox.Show(sb.ToString(), "Puntaje más alto", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
