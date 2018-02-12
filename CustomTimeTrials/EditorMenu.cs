using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NativeUI;

namespace CustomTimeTrials
{
    class EditorMenu
    {
        private MenuPool menuPool = new MenuPool();
        private delegate void Callback();
        private Dictionary<string, Delegate> callbacks = new Dictionary<string, Delegate>();

        private int raceTypeIndex = 0;
        private List<dynamic> raceTypes = new List<dynamic> {"Circuit", "Sprint"};

        public EditorMenu()
        {
            this.CreateMenu();
        }

        private void CreateMenu()
        {
            // Create the menu
            var menu = new UIMenu("Custom Time Trials", "Editor");

            // add the menu items
            menu.AddItem(new UIMenuListItem("Type", this.raceTypes, 0));
            menu.AddItem(new UIMenuItem("Add Checkpoint Here"));
            menu.AddItem(new UIMenuItem("Remove Last Checkpoint"));
            menu.AddItem(new UIMenuItem("Remove All Checkpoints"));
            menu.AddItem(new UIMenuItem("Save Time Trial"));
            menu.AddItem(new UIMenuItem("Exit Editor"));

            // do this
            menu.RefreshIndex();

            // assign the event callback methods.
            menu.OnItemSelect += this.OnItemSelect;
            menu.OnMenuClose += this.OnMenuCancel;
            menu.OnListChange += this.OnListChange;

            // add to the menupool
            this.menuPool.Add(menu);

            // open the menu
            menu.Visible = true;
        }

        private void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            this.InvokeCallback(selectedItem.Text);
        }

        private void OnMenuCancel(UIMenu sender)
        {
            this.InvokeCallback("Exit Editor");
        }

        private void OnListChange(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (selectedItem.Text == "Type")
            {
                this.raceTypeIndex = index;
            }
        }

        public void update()
        {
            this.menuPool.ProcessMenus();
        }

        public void SetCallback(string itemText, Action callback)
        {
            if (this.callbacks.ContainsKey(itemText))
            {
                this.callbacks[itemText] = callback;
            }
            else
            {
                this.callbacks.Add(itemText, callback);
            }
        }

        private void InvokeCallback(string key)
        {
            if (this.callbacks.ContainsKey(key))
            {
                this.callbacks[key].DynamicInvoke(null);
            }
        }

        public string GetSelectedType()
        {
            return this.raceTypes[this.raceTypeIndex];
        }

    }
}
