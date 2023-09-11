using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
                        arr[riga] = s + ";miovalore;canc logica";
                    }
                    else
                    {
                        string x = (r.Next(10, 21)).ToString();
                        arr[riga] = s + ";" + x + ";" + "0";
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

        public int Lunghmax()
        {
            int ncampi = Contacampi();
            int lungmax = 0;
            
            using(StreamReader sr = new StreamReader(nfile))
            {
                string s;
                while((s = sr.ReadLine()) != null)
                {
                    string[] dati = s.Split(';');
                    for(int i = 0; i < ncampi; i++)
                    {
                        if (dati[i].Length > lungmax)
                        {
                            lungmax = dati[i].Length;
                        }
                    }
                   
                    
                }
            }
            return lungmax;
        }

        public void Spazi()
        {
            string[] linee = File.ReadAllLines(nfile);
            using (StreamWriter sw = new StreamWriter(nfile))
            {
                for (int i = 0; i < linee.Length; i++)
                {
                    linee[i] = linee[i].PadRight(300);
                    sw.WriteLine(linee[i]);

                }


            }

        }

        public bool Modifica(string damodificare, string nuovoele)
        {
            bool trova = false;
            string[] linee = File.ReadAllLines(nfile);

            using (StreamWriter sw = new StreamWriter(nfile))
            {
                int i = 1;
                sw.WriteLine(linee[0]);
                for (; i < linee.Length; i++)
                {
                    string[] campi = linee[i].Split(';');

                    if (campi[0] == damodificare)
                    {
                        trova = true;
                        campi[0] = nuovoele;
                        linee[i] = String.Join(";", campi);
                        sw.WriteLine(linee[i]);
                        break;
                    }
                    else
                    if (campi[1] == damodificare)
                    {
                        trova = true;
                        campi[1] = nuovoele;
                        linee[i] = String.Join(";", campi);
                        sw.WriteLine(linee[i]);
                        break;
                    }
                    else
                    if (campi[4] == damodificare)
                    {
                        trova = true;
                        campi[4] = nuovoele;
                        linee[i] = String.Join(";", campi);
                        sw.WriteLine(linee[i]);
                        break;
                    }
                }
                i++;
                for (; i < linee.Length; i++)
                    sw.WriteLine(linee[i]);
            }
            return trova;
        }

        public bool CancellazioneLogica(string cancellato)
        {
           
            bool canc = false;
            string[] linee = File.ReadAllLines(nfile);
            using (StreamWriter sw = new StreamWriter(nfile))
            {
                for (int i = 0; i < linee.Length; i++)
                {
                    string[] campi = linee[i].Split(';');
                   
                        if (cancellato == campi[i])
                        {
                            campi[ncampi - 1] = "1";
                            linee[i] = String.Join(";", campi);
                            canc = true;

                            break;
                        }
                    
                    

                }
                for (int i = 0; i < linee.Length; i++)
                    sw.WriteLine(linee[i]);

            }

            return canc;

        }






    }
}

