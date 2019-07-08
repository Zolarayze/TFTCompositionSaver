using System;
using System.Windows.Forms;
using TFT_CompositionSaver.Controllers.Interfaces;
using TFT_CompositionSaver.DAL;

namespace TFT_CompositionSaver.Controllers
{
    public class MainController
    {
        private IMainView view;
        private Data data;

        private UserControl currentUserControl;

        private IController currentController;
        private BuilderController builder;

        public MainController(IMainView view, Data data)
        {
            this.view = view;
            this.data = data;
            view.SetController(this);
        }

        public void CloseApplication()
        {
            Application.ExitThread();
        }
        
        public void LoadView()
        {
            currentController = this.GetController(typeof(BuilderController));
            this.view.BuilderUC.Visible = true;
            currentUserControl = this.view.BuilderUC;
            currentController.LoadView();
        }

        private IController GetController(Type type)
        {
            if (type == typeof(BuilderController))
            {
                if (this.builder == null)
                {
                    this.builder = new BuilderController((IBuilderView)this.view.BuilderUC, this.data);
                }
                return this.builder;
            }
            return null;
        }
    }
}
