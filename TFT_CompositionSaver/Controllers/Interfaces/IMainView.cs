using System.Windows.Forms;

namespace TFT_CompositionSaver.Controllers.Interfaces
{
    public interface IMainView
    {
        void SetController(MainController controller);

        UserControl BuilderUC { get; }
    }
}
