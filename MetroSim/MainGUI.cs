using MetroSim.classes;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            model = new Model();
            model.init();
        }

    }
}
