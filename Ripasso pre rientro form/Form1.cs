using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public Form1()
        {
            InitializeComponent();
            f = new Funzioni();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int contacampi = f.Contacampi();
            MessageBox.Show("Il numero dei campi è: " + contacampi);
        }
    }
}
