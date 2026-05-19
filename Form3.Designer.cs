namespace Juego_de_Estrategias
{
	partial class Form3
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			textBox1 = new TextBox();
			textBox2 = new TextBox();
			button1 = new Button();
			button2 = new Button();
			button3 = new Button();
			button4 = new Button();
			SuspendLayout();
			// 
			// textBox1
			// 
			textBox1.Location = new Point(24, 24);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(200, 32);
			textBox1.TabIndex = 0;
			textBox1.Text = "Menu";
			textBox1.ReadOnly = true;
			// 
			// textBox2
			// 
			textBox2.Location = new Point(515, 91);
			textBox2.Name = "textBox2";
			textBox2.Size = new Size(125, 27);
			textBox2.TabIndex = 1;
			textBox2.Text = "menu";
			// 
			// button1
			// 
			button1.Location = new Point(24, 80);
			button1.Name = "button1";
			button1.Size = new Size(200, 40);
			button1.TabIndex = 2;
			button1.Text = "Iniciar Partida";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.Location = new Point(24, 140);
			button2.Name = "button2";
			button2.Size = new Size(200, 40);
			button2.TabIndex = 3;
			button2.Text = "Ver reglas del juego";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// button3
			// 
			button3.Location = new Point(24, 200);
			button3.Name = "button3";
			button3.Size = new Size(200, 40);
			button3.TabIndex = 4;
			button3.Text = "Ver puntaje más alto";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// button4
			// 
			button4.Location = new Point(24, 260);
			button4.Name = "button4";
			button4.Size = new Size(200, 40);
			button4.TabIndex = 5;
			button4.Text = "Salir";
			button4.UseVisualStyleBackColor = true;
			button4.Click += button4_Click;
			// 
			// Form3
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(671, 528);
			Controls.Add(button4);
			Controls.Add(button3);
			Controls.Add(button2);
			Controls.Add(button1);
			Controls.Add(textBox2);
			Controls.Add(textBox1);
			Name = "Form3";
			Text = "Form3";
			Load += Form3_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox textBox1;
		private TextBox textBox2;
		private Button button1;
		private Button button2;
		private Button button3;
		private Button button4;
	}
}