namespace Juego_de_Estrategias
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtUsuario = new TextBox();
            txtContraseña = new TextBox();
            label2 = new Label();
            btnLogin = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(217, 202);
            label1.Name = "label1";
            label1.Size = new Size(99, 32);
            label1.TabIndex = 0;
            label1.Text = "Usuario:";
            label1.Click += label1_Click;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(421, 199);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(200, 39);
            txtUsuario.TabIndex = 1;
            // 
            // txtContraseña
            // 
            txtContraseña.Location = new Point(421, 312);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.Size = new Size(200, 39);
            txtContraseña.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(208, 312);
            label2.Name = "label2";
            label2.Size = new Size(139, 32);
            label2.TabIndex = 3;
            label2.Text = "Contraseña:";
            // 
            // btnLogin
            // 
            btnLogin.DialogResult = DialogResult.Retry;
            btnLogin.Location = new Point(395, 500);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(262, 76);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Iniciar Partida";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1093, 810);
            Controls.Add(btnLogin);
            Controls.Add(label2);
            Controls.Add(txtContraseña);
            Controls.Add(txtUsuario);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtUsuario;
        private TextBox txtContraseña;
        private Label label2;
        private Button btnLogin;
    }
}
