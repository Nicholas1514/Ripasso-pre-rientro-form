﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Ripasso_pre_rientro_form.Funzioni;

namespace Ripasso_pre_rientro_form
{
    
    public partial class Form1 : Form
    {
        public struct Campi
        {
            public string comune;
            public string provincia;
            public string regione;
            public string nome;
            public int anno;
            public string data;
            public float identificatore;
            public string longitudine;
            public string latitudine;
        }

        Campi c;

        Funzioni f;
        public string nfile;
        public int recordLenght;
        public string sep;
        public Form1()
        {
            InitializeComponent();
            f = new Funzioni();
            nfile = @"Zappa.csv";
            sep = ";";
            recordLenght = 124;
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
            
           
           
           
            f.c =f.sp(textBox1.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text, textBox12.Text, textBox13.Text);
            f.AggProd(f.Record(f.c, recordLenght), nfile);
            MessageBox.Show("I campi sono stati aggiunti al file");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*Funzioni.Campi c = f.sp();
            MessageBox.Show(c.comune + " " + c.provincia + " " + c.anno);
            */
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

        private void button6_Click(object sender, EventArgs e)
        {
            int lung = f.Lunghmax();
            MessageBox.Show(lung.ToString());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            f.Spazi();
            MessageBox.Show("Ogni record ha la stessa dimensione");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int ncampi = f.Contacampi();
            dataGridView1.Rows.Clear();
            string[] linee = File.ReadAllLines(nfile);
            string[] campi = linee[0].Split(';');
            dataGridView1.Rows.Add(campi[0], campi[1], campi[2]);
            for (int i = 0; i < linee.Length; i++)
            {

                campi = linee[i].Split(';');


                //condizione per gli elementi che vengono aggiunti in coda
                if (campi[1] == "")
                {
                    dataGridView1.Rows.Add(campi[0]);
                }

                if (campi[ncampi - 1] == "0")
                {
                    dataGridView1.Rows.Add(campi[0], campi[1], campi[2]);
                }




            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
      
            f.Modifica(textBox4.Text, textBox1.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text, textBox12.Text, textBox13.Text, nfile, sep, recordLenght);
            MessageBox.Show("Record modificato");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bool canc = f.CancellazioneLogica(textBox5.Text);
            if(canc == true)
            {
                MessageBox.Show("Elemento cancellato");
            }
            else
            {
                MessageBox.Show("Elemento non presente nel file");
            }
        }
    }
}
