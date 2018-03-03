using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MetroSim
{
    class SettingsLoader
    {

        public static SortedList<string, Stanice> nactiNastaveni(string path, Model model)
        {
            SortedList<string, Stanice> seznamStanic = new SortedList<string, Stanice>();
            StreamReader sr = new StreamReader(path);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                if(line[0] != '#') // není komentář
                {
                    string[] data = line.Split(',');
                    string pismeno = data[0].Trim();
                    string jmeno = data[1].Trim();
                    float kilometr = float.Parse(data[2], CultureInfo.InvariantCulture);
                    bool jePrestupni = false;
                    bool jeKonecna = false;
                    string prestupniPismeno = "x";
                    if(data.Length > 3)
                    {
                        try
                        {
                            jeKonecna = (Int32.Parse(data[3]) == 1);
                            if (data.Length == 6)
                            {
                                jePrestupni = (Int32.Parse(data[4]) == 1);
                                prestupniPismeno = data[5].Trim();
                            }
                        } catch (FormatException)
                        {
                            Console.Error.WriteLine("chybný vstup: '" + line + "'");
                        }                        
                    }
                    string id = pismeno + jmeno.Substring(0, 3);
                    Stanice stanice = new Stanice(id, pismeno, jmeno, kilometr, jeKonecna, jePrestupni, prestupniPismeno);
                    seznamStanic.Add(id, stanice);
                }
            }
            return seznamStanic;
        } 

    }
}
