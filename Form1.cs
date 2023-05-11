namespace Gladiador
{

    public partial class Form1 : Form
    {
        private bool tokensadmitidos;
        public Form1()
        {
            tokensadmitidos = false;
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Scanner sc = new Scanner(richTextBox1.Text);
                tokensadmitidos = sc.Analizar();
                richTextBox2.Text = sc.Mensaje;
            }
            catch (Exception ex)
            {
                richTextBox2.Text = ex.Message;

            }
        }

        private void scannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnScanner.Visible = true;
            btnParser.Visible = false;
        }

        private void btnParser_Click(object sender, EventArgs e)
        {
            Sintaxis sintaxis = new Sintaxis(richTextBox1.Text);
            try
            {
                if(!tokensadmitidos)
                {
                    MessageBox.Show("Primero debes generar tokens validos para poder analizar la sintaxis");
                    return;
                }
                sintaxis.Analizar();
                richTextBox2.Text = "Programa sintacticamente correcto";
            }
            catch (Exception ex)
            {
                richTextBox2.Text = ex.Message;
            }
        }

        private void parserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnScanner.Visible = false;
            btnParser.Visible = true;
        }
    }
}