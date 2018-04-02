using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace MetroSim
{
    public partial class MainGUI : Form
    {
        private Nastaveni[] nastaveni;
        private Model[] modely;
        private static int POCET_MODELU_PAR = 4; //počet paralelních simulací ve vláknech
        private static int POCET_MODELU_CELKEM = 16; //celkový počet simulací
        private float prumerVysledku;
        private int dokoncenychVypoctu;

        public MainGUI()
        {
            InitializeComponent();
        }

        private void MainGUI_load(object sender, EventArgs e)
        {
            nastaveni = new Nastaveni[POCET_MODELU_PAR];
            modely = new Model[POCET_MODELU_PAR];
            for(int i = 0; i < POCET_MODELU_PAR; i++)
            {
                nastaveni[i] = new Nastaveni();
                modely[i] = new Model(this, "Model-" + i);
            }
            vyplnDropdownyANastaveni();
        }

        private void vyplnDropdownyANastaveni()
        {
            foreach(KeyValuePair<string, Stanice> k in modely[0].getSeznamStanic().stanice)
            {
                cZacatek.Items.Add(new CustomCBItem(k.Value.jmeno + " (" + k.Value.pismeno + ")", k.Key));
                cKonec.Items.Add(new CustomCBItem(k.Value.jmeno + " (" + k.Value.pismeno + ")", k.Key));
            }
            cZacatek.SelectedIndex = 0;
            cKonec.SelectedIndex = 0;

            foreach(KeyValuePair<string, string> k in modely[0].getSeznamStanic().pismenaLinek)
            {
                cLinky.Items.Add(k.Key);
                NastaveniLinky nl = new NastaveniLinky(k.Key, (int) nPocetSouprav.Value, (float) nRychlost.Value, (int) nKapacita.Value, (float) nDobaCekani.Value);
                for(int i = 0; i < POCET_MODELU_PAR; i++)
                {
                    nastaveni[i].pridejNastaveniLinky(nl);
                }
                
            }

            cLinky.SelectedIndex = 0;
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            prumerVysledku = 0;
            dokoncenychVypoctu = 0;

            lVysledek.Visible = false;
            pLoading.Visible = true;
            bStart.Enabled = false;


            for(int i = 0; i < POCET_MODELU_PAR; i++)
            {
                nastaveni[i].pocatecniStanice = modely[i].getSeznamStanic().stanice[(string)((CustomCBItem)cZacatek.SelectedItem).Value];
                nastaveni[i].konecnaStanice = modely[i].getSeznamStanic().stanice[(string)((CustomCBItem)cKonec.SelectedItem).Value];
                nastaveni[i].frekvenceLidi = (int)nFrekvenceLidi.Value;
                nastaveni[i].casPrichodu = (int)nCasPrichodu.Value;
            }

            spustSeriiVypoctu();
        }

        private void spustSeriiVypoctu()
        {
            for (int i = 0; i < POCET_MODELU_PAR; i++)
            {
                modely[i].reset();
                modely[i].nactiNastaveni(nastaveni[i]);
            }

            for (int i = 0; i < POCET_MODELU_PAR; i++)
            {
                (new Thread(modely[i].spocitej)).Start();
            }
        }
        
        delegate void BoolArgReturningVoidDelegate(bool visible);

        private void setLoadingVisibility(bool visible) //hack pro ruzna vlakna
        {
            if (pLoading.InvokeRequired)
            {
                BoolArgReturningVoidDelegate d = new BoolArgReturningVoidDelegate(setLoadingVisibility);
                Invoke(d, new object[] { visible });
            }
            else
            {
                pLoading.Visible = visible;
            }
        }

        delegate void StringArgReturningVoidDelegate(string vysledek);

        private void showVysledek(string vysledek) //hack pro ruzna vlakna
        {
            if (lVysledek.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(showVysledek);
                Invoke(d, new object[] { vysledek });
            }
            else
            {
                lVysledek.Visible = true;
                lVysledek.Text = "Doba cesty: " + vysledek + " min";
                bStart.Enabled = true;
            }
        }
    
        public void finished(float vysledek)
        {
            Console.WriteLine("konec " + vysledek);
            if(vysledek >= 0) //je platny = nedoslo k timeoutu
            {
                vysledek -= nastaveni[0].casPrichodu;
                prumerVysledku += vysledek / (float)POCET_MODELU_CELKEM;
            }
            
            dokoncenychVypoctu++;
            if(dokoncenychVypoctu == POCET_MODELU_CELKEM)
            {
                setLoadingVisibility(false);
                prumerVysledku = (float) Math.Round(prumerVysledku * 10) / 10;
                showVysledek(prumerVysledku.ToString());
            }
            else
            {
                Console.WriteLine("JESTE CHYBI " + (POCET_MODELU_CELKEM - dokoncenychVypoctu));
                if (dokoncenychVypoctu % POCET_MODELU_PAR == 0)
                {
                    Console.WriteLine("SPUSTIM SERII VYPOCTU");
                    spustSeriiVypoctu();
                }
                
            }
            
        }

        private void cLinky_SelectedIndexChanged(object sender, EventArgs e)
        {
            //nacte ulozene nastaveni linky
            NastaveniLinky nl = nastaveni[0].nastaveniLinek[(string) cLinky.SelectedItem];
            nKapacita.Value = nl.kapacitaSouprav;
            nPocetSouprav.Value = nl.pocetSouprav;
            nRychlost.Value = (decimal) nl.rychlostSouprav;
        }

        private void nastaveni_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nd = (NumericUpDown)sender;
            for(int i = 0; i < POCET_MODELU_PAR; i++)
            {
                NastaveniLinky nl = nastaveni[i].nastaveniLinek[(string)cLinky.SelectedItem];
                switch (nd.Name)
                {
                    case "nRychlost":
                        nl.rychlostSouprav = (float)nd.Value;
                        break;
                    case "nPocetSouprav":
                        nl.pocetSouprav = (int)nd.Value;
                        break;
                    case "nKapacita":
                        nl.kapacitaSouprav = (int)nd.Value;
                        break;
                    case "nDobaCekani":
                        nl.dobaCekaniVeStanici = (float)nd.Value;
                        break;
                }
            }
        }
    }
}
