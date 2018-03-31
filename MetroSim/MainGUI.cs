using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace MetroSim
{
    public partial class MainGUI : Form
    {

        //private Model model;
        private Nastaveni nastaveni;
        private Model[] modely;
        private Random rand;
        private static int POCET_MODELU = 10;
        private double prumerVysledku;
        private int dokoncenychVypoctu;

        public MainGUI()
        {
            InitializeComponent();
        }

        private void MainGUI_load(object sender, EventArgs e)
        {
            rand = new Random();
            nastaveni = new Nastaveni();
            modely = new Model[POCET_MODELU];
            for(int i = 0; i < POCET_MODELU; i++)
            {
                modely[i] = new Model(this, rand, "Model-" + i);
                modely[i].init();
            }
            //model = new Model(this);
            //model.init();
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
                NastaveniLinky nl = new NastaveniLinky(k.Key, (int) nPocetSouprav.Value, (float) nRychlost.Value, (int) nKapacita.Value, (int) nDobaCekani.Value);
                nastaveni.pridejNastaveniLinky(nl);
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

            for(int i = 0; i < POCET_MODELU; i++)
            {
                nastaveni.pocatecniStanice = modely[i].getSeznamStanic().stanice[(string)((CustomCBItem)cZacatek.SelectedItem).Value];
                nastaveni.konecnaStanice = modely[i].getSeznamStanic().stanice[(string)((CustomCBItem)cKonec.SelectedItem).Value];

                nastaveni.frekvenceLidi = (int)nFrekvenceLidi.Value;
                nastaveni.casPrichodu = (int)nCasPrichodu.Value;
                modely[i].reset();
                modely[i].nactiNastaveni(nastaveni);
            }

            for(int i = 0; i < POCET_MODELU; i++)
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
    
        public void finished(int vysledek)
        {
            Console.WriteLine("konec " + vysledek);
            if(vysledek >= 0) //je platny = nedoslo k timeoutu
            {
                vysledek -= nastaveni.casPrichodu;
                prumerVysledku += ((double)vysledek / (double)POCET_MODELU);
            }
            
            dokoncenychVypoctu++;
            if(dokoncenychVypoctu == POCET_MODELU)
            {
                setLoadingVisibility(false);
                showVysledek(prumerVysledku.ToString());
            }
            else
            {
                Console.WriteLine("JESTE CHYBI " + (POCET_MODELU - dokoncenychVypoctu));
            }
            
        }

        private void cLinky_SelectedIndexChanged(object sender, EventArgs e)
        {
            //nacte ulozene nastaveni linky
            NastaveniLinky nl = nastaveni.nastaveniLinek[(string) cLinky.SelectedItem];
            nKapacita.Value = nl.kapacitaSouprav;
            nPocetSouprav.Value = nl.pocetSouprav;
            nRychlost.Value = (decimal) nl.rychlostSouprav;
        }

        private void nastaveni_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nd = (NumericUpDown)sender;
            NastaveniLinky nl = nastaveni.nastaveniLinek[(string) cLinky.SelectedItem];
            switch (nd.Name)
            {
                case "nRychlost":
                    nl.rychlostSouprav = (float) nd.Value;
                    break;
                case "nPocetSouprav":
                    nl.pocetSouprav = (int) nd.Value;
                    break;
                case "nKapacita":
                    nl.kapacitaSouprav = (int)nd.Value;
                    break;
                case "nDobaCekani":
                    nl.dobaCekaniVeStanici = (int)nd.Value;
                    break;
            }
        }
    }
}
