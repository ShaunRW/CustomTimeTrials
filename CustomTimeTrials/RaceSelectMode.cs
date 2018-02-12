using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;
using NativeUI;

namespace CustomTimeTrials
{
    class RaceSelectMode : Mode
    {
        private MenuPool menuPool = new MenuPool();
        private int menuSelectedRaceIndex = 0;
        private int menuSelectedLapsIndex = 0;

        public RaceSelectMode(KeyPressTracker keyPressTracker) : base(keyPressTracker)
        {
            this.CreateMenu();
            Game.Player.CanControlCharacter = false;
        }

        public override void onTick()
        {
            menuPool.ProcessMenus();
        }

        private void CreateMenu()
        {
            var menu = new UIMenu("Custom Time Trials", "Main Menu");

            menu.AddItem(new UIMenuListItem("Select Time Trial", this.GetRaceList(), 0));
            menu.AddItem(new UIMenuListItem("Laps", this.GetLapsOptions(), 0));
            menu.AddItem(new UIMenuItem("Start"));
            menu.AddItem(new UIMenuItem("Create New Time Trial"));
            menu.RefreshIndex();

            menu.OnItemSelect += this.MenuItemSelectHandler;
            menu.OnListChange += this.onListChange;
            menu.OnMenuClose += this.onMenuCancel;

            menuPool.Add(menu);

            menu.Visible = true;

        }

        public void MenuItemSelectHandler(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (selectedItem.Text == "Start")
            {
                sender.Visible = false;

                int laps = int.Parse(this.GetLapsOptions()[this.menuSelectedLapsIndex]);
                string race = this.GetRaceList(true)[this.menuSelectedRaceIndex];
                
                this.ChangeModeTo(new RaceMode(race, laps, this.KeyPress));
                
            }
            else if (selectedItem.Text == "Create New Time Trial")
            {
                sender.Visible = false;
                this.ChangeModeTo(new EditorMode(this.KeyPress));
            }
        }

        public void onListChange(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (selectedItem.Text == "Select Time Trial")
            {
                this.menuSelectedRaceIndex = index;
            }
            else if (selectedItem.Text == "Laps")
            {
                this.menuSelectedLapsIndex = index;
            }
        }

        public void onMenuCancel(UIMenu sender)
        {
            Game.Player.CanControlCharacter = true;
            this.ChangeModeTo(new InactiveMode(this.KeyPress));
        }

        private List<dynamic> GetRaceList(bool includeExtension=false)
        {
            List<dynamic> races = new List<dynamic>();
            string[] files = System.IO.Directory.GetFiles("scripts/TimeTrials");
            foreach (string file in files)
            {
                if (includeExtension)
                {
                    races.Add(System.IO.Path.GetFileName(file));
                }
                else
                {
                    races.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                }
            }
            return races;
        }

        private List<dynamic> GetLapsOptions(int min=1, int max=100)
        {
            List<dynamic> laps = new List<dynamic>();
            for (int i=min; i<max; i++)
            {
                laps.Add(i.ToString());
            }
            return laps;
        }
    }
}
