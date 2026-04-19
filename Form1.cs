namespace Juego_de_Estrategias
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuarioCorrecto = "Maestro";
            string contraseñaCorrecta = "1234";

            if (txtUsuario.Text == usuarioCorrecto && txtContraseña.Text == contraseñaCorrecta)
            {
                MessageBox.Show("!Bienvenido al juego!");
                FormJuego partida = new FormJuego();
                partida.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos. Inténtalo de nuevo.");
            }
        }
    }
}
