using System;
using System.Windows.Forms;

namespace MetroSim
{
    public partial class MainGUI : Form
    {

        private Model model;

        public MainGUI()
        {
            InitializeComponent();
        }

        private void MainGUI_load(object sender, EventArgs e)
        {
            model = new Model();
            model.init();
            vyplnDropdown();
        }

        private void vyplnDropdown()
        {
            foreach(Stanice s in model.getSeznamStanic())
            {
                cZacatek.Items.Add(s.jmeno + " (" + s.pismeno + ")");
                cKonec.Items.Add(s.jmeno + " (" + s.pismeno + ")");
            }
        }

    }
}
