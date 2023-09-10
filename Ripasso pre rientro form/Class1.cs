using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ripasso_pre_rientro_form
{
    public class Funzioni
    {
        public struct Campi
        {
            public string comune;
            public string provincia;
            public int anno;
        }

        public Campi c;
        public string nfile = @"Zappa.csv";
        public int ncampi;
        public int recordLenght = 64;
        public int Contacampi(char sep = ';')
        {

            string[] linea = File.ReadAllLines(nfile);

            ncampi = linea[0].Split(sep).Length;

            return ncampi;
        }
        public void Aggiuntacoda(string contenuto)
        {
            var oStream = new FileStream(nfile, FileMode.Append, FileAccess.Write, FileShare.Read);
            StreamWriter sw = new StreamWriter(oStream);
            sw.WriteLine(contenuto);
            sw.Close();
        }

        public Campi sep(char sep = ';')
        {
            Campi c;
            string[] linee = File.ReadAllLines(nfile);

            string[] campi = linee[1].Split(sep);
            c.comune = campi[0];
            c.provincia = campi[1];
            c.anno = int.Parse(campi[4]);
            return c;
        }

        public int Ricerca(string nome, string nfile)
        {

            int riga = 0;
            using (StreamReader sr = File.OpenText(nfile))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] dati = s.Split(';');

                    if (dati[0] == nome)
                    {
                        sr.Close();
                        return riga;
                    }
                    if (dati[1] == nome)
                    {
                        sr.Close();
                        return riga;
                    }
                    if (dati[4] == nome)
                    {
                        sr.Close();
                        return riga;
                    }

                    riga++;
                }
                sr.Close();
            }
            return -1;
        }

        public void Aggiuntacampo(string nfile)
        {
            Random r = new Random();
            string[] arr = new string[1000];
            int riga = 0;
            using (StreamReader sr = new StreamReader(nfile))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    if (riga == 0)
                    {
                        arr[riga] = s + ";miovalore";
                    }
                    else
                    {
                        string x = (r.Next(10, 21)).ToString();
                        arr[riga] = s + ";" + x;
                    }
                    riga++;
                   
                }
            }
            using (StreamWriter sw = new StreamWriter(nfile))
            {
                riga = 0;
                while (arr[riga] != null)

                {
                    sw.WriteLine(arr[riga]);
                    riga++;
                }
            }
        }


    }
    }

