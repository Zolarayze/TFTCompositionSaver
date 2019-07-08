using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TFT_CompositionSaver.Controllers.Interfaces;
using TFT_CompositionSaver.DAL;
using TFT_CompositionSaver.Enums;
using TFT_CompositionSaver.Helper;
using TFT_CompositionSaver.Models;
using TFT_CompositionSaver.Views.UserControls;

namespace TFT_CompositionSaver.Controllers
{
    public class BuilderController : IController
    {
        private Data data;
        private List<Composition> compositions;
        private IBuilderView view;
        private List<FlowLayoutPanel> flpComps;
        private FlowLayoutPanel currentFlp;
        private ActionMode currentActionMode;

        public event EventHandler ActionModeSwitch;

        public BuilderController(IBuilderView view, Data data)
        {
            this.data = data;
            this.view = view;
            view.SetController(this);
            flpComps = new List<FlowLayoutPanel>();
        }

        private void InitializeCompositions()
        {
            for (int i = 0; i < compositions.Count; i++)
            {
                Composition comp = compositions.FirstOrDefault(c => c.id == i);
                if (comp == null)
                {
                    return;
                }

                string flpName = "flp" + i;
                FlowLayoutPanel newFlp = this.view.FlpTemplate.Clone();
                newFlp.Name = flpName;

                for (int j = 0; j < comp.championSets.Count; j++)
                {
                    ChampRow cr = new ChampRow(newFlp, comp.championSets[j], j, comp.championSets.Count - 1);
                    this.ActionModeSwitch += cr.ActionModeSwitched;
                    if (currentActionMode == ActionMode.Read)
                    {
                        cr.SetActionMode(currentActionMode);
                    }

                    newFlp.Controls.Add(cr);
                }

                AddChampRow acr = new AddChampRow();
                this.ActionModeSwitch += acr.ActionModeSwitched;
                acr.AddedChampRow += (sender, e) => this.AddedChampRow(sender, e, newFlp);
                if (currentActionMode == ActionMode.Read)
                {
                    acr.Visible = false;
                }
                newFlp.Controls.Add(acr);

                this.flpComps.Add(newFlp);
            }
        }

        private void UpdateViewWithModelValues(List<Composition> compositions)
        {
            // Setters of viewmodel
            ListBox lbx = new ListBox();
            foreach (Composition comp in compositions)
            {
                lbx.Items.Add(comp.name);
            }
            view.CompositionList = lbx;
        }

        public void LoadView()
        {
            compositions = this.data.Compositions;
            currentActionMode = compositions.Count == 0 ? ActionMode.Edit : ActionMode.Read;

            UpdateViewWithModelValues(compositions);
            this.InitializeCompositions();
            this.view.SetVisibleReadEdit(currentActionMode);
        }

        public void SetModelData()
        {
            this.RetrieveChampSets();
            data.Compositions = compositions;
            data.SaveData();
        }

        public void SetNewName(int index, string name)
        {
            if (this.compositions.Any(f => f.id == index))
            {
                this.compositions[index].name = name;
            }
        }

        public void AddChampionSets(int selectedIndex, string name)
        {
            Composition comp = compositions.FirstOrDefault(c => c.id == selectedIndex) ?? this.CreateComposition(selectedIndex, name);

            string flpName = "flp" + selectedIndex;

            FlowLayoutPanel newFlp = this.flpComps.FirstOrDefault(f => f.Name == flpName);

            if (currentFlp != null)
            {
                if (currentFlp == newFlp)
                {
                    return;
                }
                currentFlp.Visible = false;
            }

            // Called when adding a new composition
            if (newFlp == null)
            {
                newFlp = this.view.FlpTemplate.Clone();
                newFlp.Name = flpName;

                for (int i = 0; i < comp.championSets.Count; i++)
                {
                    ChampRow cr = new ChampRow(newFlp, comp.championSets[i], i, comp.championSets.Count - 1);
                    this.ActionModeSwitch += cr.ActionModeSwitched;
                    newFlp.Controls.Add(cr);
                }

                AddChampRow acr = new AddChampRow();
                acr.AddedChampRow += (sender, e) => this.AddedChampRow(sender, e, newFlp);
                this.ActionModeSwitch += acr.ActionModeSwitched;
                newFlp.Controls.Add(acr);

                this.flpComps.Add(newFlp);
            }

            currentFlp = newFlp;
            currentFlp.Visible = true;
        }

        public void RetrieveChampSets()
        {
            if (this.compositions.Count != this.flpComps.Count)
            {
                return;
            }

            for (int cI = 0; cI < this.compositions.Count; cI++)
            {
                this.compositions[cI].championSets.Clear();

                FlowLayoutPanel flp = this.flpComps[cI];

                for (int i = 0; i < flp.Controls.Count; i++)
                {
                    if (!(flp.Controls[i] is ChampRow))
                    {
                        continue;
                    }
                    ChampionSet champSet = new ChampionSet();
                    ChampRow champRow = (ChampRow)flp.Controls[i];

                    champSet.championId = champRow.GetChampionId();
                    champSet.itemIds = champRow.GetItemIds();
                    champSet.swapChecked = champRow.GetSwapChecked();
                    champSet.stars = champRow.GetStarAmount();
                    this.compositions[cI].championSets.Add(champSet);
                }
            }
        }

        private Composition CreateComposition(int index, string name)
        {
            Composition comp = new Composition()
            {
                id = index,
                name = name
            };
            this.compositions.Add(comp);
            return comp;
        }

        public void RemoveComposition(int index)
        {
            Composition comp = this.compositions.FirstOrDefault(c => c.id == index);
            if (comp != null)
            {
                this.compositions.Remove(comp);
            }

            string flpName = "flp" + index;
            FlowLayoutPanel flp = this.flpComps.FirstOrDefault(f => f.Name == flpName);
            if (flp != null)
            {
                flp.Controls.Clear();
                flp.Dispose();
                this.flpComps.Remove(flp);
            }

            for (int i = index; i < this.compositions.Count; i++)
            {
                this.compositions[i].id -= 1;
            }

            for (int i = index; i < this.flpComps.Count; i++)
            {
                this.flpComps[i].Name = "flp" + i;
            }
        }

        public void ShowReadEdit(ActionMode actionMode)
        {
            // val = true = edit
            // val = false = read
            this.currentActionMode = actionMode;
            
            this.ActionModeSwitch?.Invoke(actionMode, EventArgs.Empty);
        }

        public ActionMode GetCurrentActionMode()
        {
            return this.currentActionMode;
        }

        public void AddedChampRow(object sender, EventArgs e, FlowLayoutPanel flp)
        {
            AddChampRow addRow = (AddChampRow)sender;
            ChampRow cr = new ChampRow(flp);
            this.ActionModeSwitch += cr.ActionModeSwitched;
            flp.Controls.Add(cr);
            flp.Controls.SetChildIndex(addRow, flp.Controls.Count - 1);
            flp.ScrollControlIntoView(addRow);
        }
    }
}
