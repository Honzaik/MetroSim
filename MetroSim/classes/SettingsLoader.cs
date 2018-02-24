using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MetroSim.classes
{
    class SettingsLoader
    {

        public static List<Stanice> nactiNastaveni(string path)
        {
            List<Stanice> seznamStanic = new List<Stanice>();
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
                    if(data.Length > 3)
                    {
                        try
                        {
                            jePrestupni = (Int32.Parse(data[3]) == 1);
                            if (data.Length == 5)
                            {
                                jeKonecna = (Int32.Parse(data[4]) == 1);
                            }
                        } catch (FormatException)
                        {
                            Console.Error.WriteLine("chybný vstup: '" + line + "'");
                        }                        
                    }
                    string id = pismeno + jmeno.Substring(0, 2);
                    Stanice stanice = new Stanice(id, pismeno, jmeno, kilometr, jePrestupni, jeKonecna);
                    seznamStanic.Add(stanice);
                }
            }
            return seznamStanic;
        } 

    }
}
