using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymize
{
    class Program
    {
        static void Main(string[] args)
        {
            String csv_filepath = @"C:\Users\Utilizador\Dropbox\IST\Tese de Doutoramento\Cadeiras\Sistema de Gestão de Processos\Projecto\Event Log\Finais\xporter.csv";

            //Reads all text from the csv file, and then splits it by lines, removing the first one
            string text = File.ReadAllText(csv_filepath);
            string[] lines = text.Split('\n');
            lines = lines.Skip(1).ToArray();

            //Variable to store the next resource ID
            int next_resource_id = 1;
            //Resources stored
            //String[] resources = new String[lines.Count()]; 
            Dictionary<String, int> resources = new Dictionary<string, int>();

            //Replace the files
            string[] laux;
            foreach (String l in lines)
            {
                laux = l.Split(',');

                // Replaces all occurences of the assignee, if it hasn't been done yet
                // First needs to remove \r from the end
                laux[2] = laux[2].Replace("\r", string.Empty);
                if (!resources.ContainsKey(laux[2]))
                {
                    text = text.Replace(laux[2], "R" + next_resource_id);
                    resources.Add(laux[2], next_resource_id);
                    next_resource_id++;
                }

                // Replaces all occurences of the reporter, if it hasn't been done yet
                // First needs to remove \r from the end
                laux[7] = laux[7].Replace("\r", string.Empty);
                if (!resources.ContainsKey(laux[7]))
                {
                    text = text.Replace(laux[7], "R" + next_resource_id);
                    resources.Add(laux[7], next_resource_id);
                    next_resource_id++;
                }

            }

            File.WriteAllText(@"C:\Users\Utilizador\Dropbox\IST\Tese de Doutoramento\Cadeiras\Sistema de Gestão de Processos\Projecto\Event Log\Finais\xporter2.csv", text);

        }
    }
}
