using System.Windows.Forms;
using TFT_CompositionSaver.Controllers;
using TFT_CompositionSaver.Controllers.Interfaces;

namespace TFT_CompositionSaver.Views.Forms
{
    public partial class Main : Form, IMainView
    {
        private MainController controller;

        public Main()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            controller.CloseApplication();
        }

        public void SetController(MainController controller)
        {
            this.controller = controller;
        }

        public UserControl BuilderUC
        {
            get { return this.flpBuilder; }
        }
    }
}
