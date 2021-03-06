﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MetroSim
{
    class StaniceLoader
    {

        private static int DOBA_PRESTUPU = 2;

        public static SortedList<string, Stanice> nactiStanice(string path)
        {
            SortedList<string, Stanice> seznam = new SortedList<string, Stanice>();
            StreamReader sr = new StreamReader(path);
            string line;
            while((line = sr.ReadLine()) != null)
            {
                if(line.Length > 0 && line[0] != '#') // není prázdný řádek ani komentář
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
                    Stanice stanice = new Stanice(id, pismeno, jmeno, kilometr, jeKonecna, jePrestupni, prestupniPismeno, DOBA_PRESTUPU);
                    seznam.Add(id, stanice);
                }
            }
            return seznam;
        } 
    }
}
