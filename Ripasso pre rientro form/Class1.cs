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
        public string nfile = @"Zappa.csv";
        public int ncampi;
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
    }
}
