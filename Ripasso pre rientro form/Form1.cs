using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ripasso_pre_rientro_form
{
    public partial class Form1 : Form
    {
      
        Funzioni f;
        public string nfile;
        public Form1()
        {
            InitializeComponent();
            f = new Funzioni();
            nfile = @"Zappa.csv";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int contacampi = f.Contacampi();
            MessageBox.Show("Il numero dei campi è: " + contacampi);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f.Aggiuntacoda(textBox1.Text);
            MessageBox.Show("Elemento aggiunto al file");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Funzioni.Campi c = f.sep();
            MessageBox.Show(c.comune + " " + c.provincia + " " + c.anno);
        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            int pos = f.Ricerca(textBox2.Text, nfile);
            if (pos == -1)
            {
                MessageBox.Show("Elemento non presente!");
            }
            else
            {
                MessageBox.Show($"Elemento presente in riga {pos}");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            f.Aggiuntacampo(nfile);
            MessageBox.Show("E' stato aggiunto il campo");
        }
    }
}
