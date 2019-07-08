using System.Windows.Forms;
using TFT_CompositionSaver.Enums;

namespace TFT_CompositionSaver.Controllers.Interfaces
{
    public interface IBuilderView
    {
        void SetController(BuilderController controller);

        ListBox CompositionList { get; set; }

        FlowLayoutPanel FlpTemplate { get; }

        void SetVisibleReadEdit(ActionMode actionMode);
    }
}
