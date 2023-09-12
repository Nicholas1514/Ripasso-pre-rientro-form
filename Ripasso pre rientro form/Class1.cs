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
            public string regione;
            public string nome;
            public int anno;
            public string data;
            public float identificatore;
            public string longitudine;
            public string latitudine;
        }

        public Campi c;
        public string nfile = @"Zappa.csv";
        public int ncampi;
        public int recordLenght = 124;
        public string sep = ";";
        public int Contacampi(char sep = ';')
        {

            string[] linea = File.ReadAllLines(nfile);

            ncampi = linea[0].Split(sep).Length;

            return ncampi;
        }
      

        public string Record(Campi c, int lunghrecord, string sp = ";")
        {
            
            return (c.comune + sp + c.provincia + sp + c.regione + sp + c.nome + sp + c.anno + sp + c.data + sp + c.identificatore + sp + c.longitudine + sp + c.latitudine ).PadRight(lunghrecord - 4) + "##\r\n";

        }

        public void AggProd(string riga, string nomefile)
        {
            
            var oStream = new FileStream(nomefile, FileMode.Append, FileAccess.Write, FileShare.Read);
            BinaryWriter writer = new BinaryWriter(oStream);
            char[] linea = riga.ToCharArray();
            writer.Write(linea);
            writer.Close();
            oStream.Close();
        }
        //valori dei campi settati per l'aggiunta
        public Campi sp(string c1, string c2, string c3, string c4, string c5, string c6, string c7, string c8, string c9, char sep = ';')
        {
            Campi c;
            c.comune = c1;
            c.provincia = c2;
            c.regione = c3;
            c.nome = c4;
            c.anno = int.Parse(c5);
            c.data = c6;
            c.identificatore = float.Parse(c7);
            c.longitudine = c8;
            c.latitudine = c9;
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
                    linee[i] = linee[i].PadRight(120);
                    sw.WriteLine(linee[i]);

                }


            }

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

        public Campi ModProd(string prodottostringa, string c1, string c2, string c3, string c4, string c5, string c6, string c7, string c8, string c9, string sp)
        {
           
            Campi c;
            string[] div = prodottostringa.Split(sp[0]);
            c.comune = c1;
            c.provincia = c2;
            c.regione = c3;
            c.nome = c4;
            c.anno = int.Parse(c5);
            c.data = c6;
            c.identificatore = float.Parse(c7);
            c.longitudine = c8;
            c.latitudine = c9;
            return c;
        }

        public void Modifica(string ricerca,string c1,string c2, string c3, string c4, string c5, string c6, string c7, string c8, string c9,  string nfile, string sep, int l)
        {
            Campi c;
            string line;
            byte[] br;
            var file = new FileStream(nfile, FileMode.Open, FileAccess.ReadWrite);
            BinaryReader reader = new BinaryReader(file);
            BinaryWriter writer = new BinaryWriter(file);
            file.Seek(0, SeekOrigin.Begin);
            while (file.Position < file.Length)
            {
                br = reader.ReadBytes(l);
                line = Encoding.ASCII.GetString(br, 0, br.Length);
                string[] div = line.Split(sep[0]);
                if (div[0] == ricerca)
                {
                    c = ModProd(line, c1, c2, c3, c4, c5, c6, c7,  c8, c9 ,sep);
                    line = Record(c, l);
                    file.Seek(-l, SeekOrigin.Current);
                    char[] linea = line.ToCharArray();
                    writer.Write(linea);
                }
            }
            reader.Close();
            writer.Close();
            file.Close();
        }





    }
}

