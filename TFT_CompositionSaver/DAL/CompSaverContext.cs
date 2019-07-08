using System.ComponentModel;
using System.Windows.Forms;
using TFT_CompositionSaver.Controllers;
using TFT_CompositionSaver.Views.Forms;

namespace TFT_CompositionSaver.DAL
{
    public class CompSaverContext : ApplicationContext
    {
        private MainController mainController;
        private Data data;

        public CompSaverContext()
        {
            Loading loading = new Loading();
            loading.Show();

            var bgw = new BackgroundWorker();
            bgw.DoWork += (_, __) =>
            {
                // Initialization
                data = new Data();
                ApiData.Instance();

                // Load data
                if (!data.LoadData())
                {
                    data.SaveData();
                    data.LoadData();
                }
            };

            bgw.RunWorkerCompleted += (_, __) =>
            {
                // Set up view
                Main view = new Main();
                view.Visible = false;

                // Set up controller and show view
                mainController = new MainController(view, data);
                mainController.LoadView();

                loading.Close();
                view.ShowDialog();
                view.Focus();
            };
            bgw.RunWorkerAsync();
        }
    }
}
