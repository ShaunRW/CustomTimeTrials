using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NativeUI;

using GTA;

namespace CustomTimeTrials.NativeMenu
{
    class Menu
    {
        private sealed class TrackedListItem
        {
            public int index;
            public List<dynamic> Items;
            public dynamic SelectedItem
            {
                get { return this.Items[this.index]; }
            }
            public TrackedListItem(List<dynamic> items)
            {
                this.index = 0;
                this.Items = items;
            }
        }

        private UIMenu menu;
        private MenuPool _MenuPool = new MenuPool();
        private delegate void Callback();
        private Dictionary<string, Delegate> callbacks = new Dictionary<string, Delegate>();
        private Dictionary<string, TrackedListItem> trackedMenuLists = new Dictionary<string, TrackedListItem>();

        public Menu(string menuTitle, string menuSubTitle, Action onExitCallback)
        {
            // Create the menu
            this.menu = new UIMenu(menuTitle, menuSubTitle);

            // assign the event callback methods.
            this.menu.OnItemSelect += this.OnItemSelect;
            this.menu.OnMenuClose += this.OnMenuCancel;
            this.menu.OnListChange += this.OnListChange;

            // add the menu to the menu pool so it can process events
            this._MenuPool.Add(this.menu);

            // set the on menu exit callback.
            this.SetCallback("__onexit__", onExitCallback);
        }


        public void AddButton(string text, Action callback)
        {
            // Add the button to the menu.
            this.AddMenuItemWithCallback(new UIMenuItem(text), callback);
        }

        public void AddListButton(string text, List<dynamic> listItems, Action callback=null)
        {
            int initialIndex = 0;

            // Add the list item button to the menu.
            this.AddMenuItemWithCallback(new UIMenuListItem(text, listItems, initialIndex), callback);

            // Add keep track of the selected index.

            this.trackedMenuLists.Add(text, new TrackedListItem(listItems));
        }

        public string GetSelectedItem(string listItemText)
        {
            if (this.trackedMenuLists.ContainsKey(listItemText))
            {
                return this.trackedMenuLists[listItemText].SelectedItem;
            }
            return null;
        }

        public void Show()
        {
            if (this.menu.Visible == false)
            {
                this.menu.RefreshIndex();
                this.menu.Visible = true;
            }
        }

        public void Update()
        {
            this._MenuPool.ProcessMenus();
        }

        private void AddMenuItemWithCallback(UIMenuItem item, Action callback = null)
        {
            // Add the button to the menu.
            this.menu.AddItem(item);

            // Add the callback to the callback list if it is not null
            if (callback != null)
            {
                this.SetCallback(item.Text, callback);
            }
        }

        private void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            this.InvokeCallback(selectedItem.Text);
        }

        private void OnMenuCancel(UIMenu sender)
        {
            this.InvokeCallback("__onexit__");
        }

        private void OnListChange(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (this.trackedMenuLists.ContainsKey(selectedItem.Text))
            {
                this.trackedMenuLists[selectedItem.Text].index = index;
            }
        }

        private void SetCallback(string itemText, Action callback)
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

    }
}
