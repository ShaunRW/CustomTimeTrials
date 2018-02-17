using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomTimeTrials.NativeMenu;

namespace CustomTimeTrials.EditorState
{
    class EditorUI
    {
        private NativeMenu.Menu menu;

        public void CreateMenu(Action onMenuExitCallback, Action onAddCheckpointCallback, Action onRemoveLastCheckpointCallback, Action onRemoveAllCheckpointsCallback, Action onSaveCallback)
        {
            this.menu = new NativeMenu.Menu("Custom Time Trials", "Editor", onMenuExitCallback);

            this.menu.AddListButton("Type", new List<dynamic>{"Circuit", "Sprint"});
            this.menu.AddButton("Add Checkpoint Here", onAddCheckpointCallback);
            this.menu.AddButton("Remove Last Checkpoint", onRemoveLastCheckpointCallback);
            this.menu.AddButton("Remove All Checkpoint", onRemoveAllCheckpointsCallback);
            this.menu.AddButton("Save Time Trial", onSaveCallback);

            this.menu.Show();
        }

        public void UpdateMenu()
        {
            this.menu.Update();
        }

        public string GetRaceType()
        {
            return this.menu.GetSelectedItem("Type");
        }
    }
}
