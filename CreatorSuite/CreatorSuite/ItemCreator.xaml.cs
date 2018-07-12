namespace CreatorSuite
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using UtilityClasses;
    using System.Linq;
    using System.Xml;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Interaction logic for ItemCreator.xaml
    /// </summary>
    public partial class ItemCreator : System.Windows.Controls.UserControl
    {
        private enum ITEM_TAB { Weapon = 0, Armor, Jewelry, Misc, End_Tab = 4 };
        private ITEM_TAB selectedTab;

        //The xml generated lists of items
        public ObservableCollection<Weapon> weaponCollection { get; set; }
        public ObservableCollection<Armor> armorCollection { get; set; }
        public ObservableCollection<Jewelry> jewelryCollection { get; set; }
        public ObservableCollection<Misc> miscCollection { get; set; }

        /// <summary>
        /// CTOR
        /// </summary>
        public ItemCreator()
        {
            InitializeComponent();
            this.DataContext = this;

            LoadItemConfigs();
            SelectItemType("Weapon");
        }

        private void LoadItemConfigs()
        {
            //lets new them out first.
            weaponCollection = new ObservableCollection<Weapon>();
            armorCollection = new ObservableCollection<Armor>();
            jewelryCollection = new ObservableCollection<Jewelry>();
            miscCollection = new ObservableCollection<Misc>();

            //Test BS
            var wep = new Weapon();
            wep.itemName = "new wep";
            weaponCollection.Add(wep);

            //Actually load our xmls
        }

        /// <summary>
        /// Select the type of the specific item
        /// </summary>
        /// <param name="type"></param>
        private void SelectItemType(string type)
        {
            if (WeaponDetails != null && ArmorDetails != null && JewleryDetails != null && MiscDetials != null)
            {
                WeaponDetails.Visibility = ArmorDetails.Visibility = JewleryDetails.Visibility = MiscDetials.Visibility = Visibility.Hidden;
                switch (type.ToLower())
                {
                    case "weapon":
                        WeaponDetails.Visibility = Visibility.Visible;
                        break;
                    case "armor":
                        ArmorDetails.Visibility = Visibility.Visible;
                        break;
                    case "jewelry":
                        JewleryDetails.Visibility = Visibility.Visible;
                        break;
                    case "misc":
                        MiscDetials.Visibility = Visibility.Visible;
                        break;
                    default:
                        WeaponDetails.Visibility = Visibility.Visible;
                        break;
                }

                itemName.Text = "Default " + type;
            }
        }

        /// <summary>
        /// Check number input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNumberInput(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var box = (sender as System.Windows.Controls.TextBox);

            decimal number;
            if (box != null)
            {
                if (!decimal.TryParse(box.Text, out number) || number < -999 || number > 999)
                {
                    if (!string.IsNullOrEmpty(box.Text) && box.Text != "-")
                    {
                        box.Clear();
                        box.Text = "0";
                        System.Windows.Forms.MessageBox.Show("Please enter a number value between -999 to 999");
                    }
                }
            }
        }

        /// <summary>
        /// selection changed on the item type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemtype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var item = (e.AddedItems[0] as System.Windows.Controls.ComboBoxItem);
                SelectItemType(item.Content.ToString());
            }
        }

        /// <summary>
        /// Based on the current tab see if an item exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool ItemExists(string name)
        {
            bool found = !string.IsNullOrEmpty(name);

            if (found)
            {
                var list = getListboxFromTab(selectedTab);
                found = list.Items.Contains(name);
            }

            return found;
        }

        /// <summary>
        /// Save button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //This has to be based off of the current tab we are on.

            switch (selectedTab)
            {
                //save here
                case ITEM_TAB.Armor:
                    break;

                default:
                    if (PopUpHelper.WouldYouLikeToSave(ItemExists(itemName.Text), "item"))
                    {

                    }
                    break;
                    {

                    }
            }
        }

        /// <summary>
        /// Export all items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_All_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < (int)ITEM_TAB.End_Tab; i++)
            {
                //Loop over all the tabs and select them
                selectedTab = (ITEM_TAB)i;
                WriteOutXML(false);
            }

            System.Windows.Forms.MessageBox.Show("Items successfully exported!");
        }

        /// <summary>
        /// Export the selected tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            WriteOutXML();
        }

        /// <summary>
        /// Exit is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            PopUpHelper.SaveAndExit(ItemExists(itemName.Text), "item");
        }

        /// <summary>
        /// When weapon type is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Weapon_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox item = sender as System.Windows.Controls.ComboBox;
            if (item != null)
            {
                if (Weapon_Class.Items != null)
                {
                    Weapon_Class.Items.Clear();
                }

                Weapon_Class.IsEnabled = true;

                var content = (item.SelectedValue as ComboBoxItem).Content.ToString();

                switch (content)
                {
                    case "One Handed":
                        Weapon_Class.Items.Add("Axe");
                        Weapon_Class.Items.Add("Dagger");
                        Weapon_Class.Items.Add("Mace");
                        Weapon_Class.Items.Add("Sword");
                        Weapon_Class.Items.Add("Wand");
                        break;
                    case "Two Handed":
                        Weapon_Class.Items.Add("Axe");
                        Weapon_Class.Items.Add("Bow");
                        Weapon_Class.Items.Add("Mace");
                        Weapon_Class.Items.Add("Staff");
                        Weapon_Class.Items.Add("Sword");
                        break;
                }
                Weapon_Class.SelectedItem = Weapon_Class.Items[0];
            }
        }

        /// <summary>
        /// When an image is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectImageClicked(object sender, RoutedEventArgs e)
        {
            string fname = "";

            itemImage.Source = PopUpHelper.SelectImage(GetEnumFromItemTab(selectedTab), ref fname);
        }

        /// <summary>
        /// Get the Sprite location from the tab
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        private PopUpHelper.SPITE_LOCATIONS GetEnumFromItemTab(ITEM_TAB tab)
        {
            PopUpHelper.SPITE_LOCATIONS loc = PopUpHelper.SPITE_LOCATIONS.ITEMS;

            switch (tab)
            {
                case ITEM_TAB.Weapon:
                    loc = PopUpHelper.SPITE_LOCATIONS.WEAPONS;
                    break;
                case ITEM_TAB.Armor:
                    loc = PopUpHelper.SPITE_LOCATIONS.ARMOR;
                    break;
                case ITEM_TAB.Jewelry:
                    loc = PopUpHelper.SPITE_LOCATIONS.JEWELRY;
                    break;
                case ITEM_TAB.Misc:
                    loc = PopUpHelper.SPITE_LOCATIONS.MISC;
                    break;
            }

            return loc;
        }

        /// <summary>
        /// Simple helper to return the listbox of the currect tab we are on
        /// weapon0, armor1, jewel2, misc3 
        /// </summary>
        /// <param name="tab"></param>
        /// <returns>the list box (cotent) of the selected tab</returns>
        private ListBox getListboxFromTab(ITEM_TAB tab)
        {
            ListBox mybox = new ListBox();

            switch (tab)
            {
                case ITEM_TAB.Weapon:
                    mybox = weaponList;
                    break;
                case ITEM_TAB.Armor:
                    mybox = armorList;
                    break;
                case ITEM_TAB.Jewelry:
                    mybox = JewelryList;
                    break;
                case ITEM_TAB.Misc:
                    mybox = miscList;
                    break;
            }

            return mybox;
        }

        /// <summary>
        /// When the tab selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                //weapon0, armor1, jewel2, misc3 
                selectedTab = (ITEM_TAB)(sender as TabControl).SelectedIndex;

                //change the display for what we are creating
                SelectItemType(selectedTab.ToString());
            }
        }

        /// <summary>
        /// Rename the item based on the tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRename_Click(object sender, RoutedEventArgs e)
        {
            switch (selectedTab)
            {
                case ITEM_TAB.Weapon:
                    {
                        renameItem<Weapon>(weaponList, weaponCollection);
                        break;
                    }
                case ITEM_TAB.Armor:
                    {
                        renameItem<Armor>(armorList, armorCollection);
                        break;
                    }
                case ITEM_TAB.Jewelry:
                    {
                        renameItem<Jewelry>(JewelryList, jewelryCollection);
                        break;
                    }
                case ITEM_TAB.Misc:
                    {
                        renameItem<Misc>(miscList, miscCollection);
                        break;
                    }
            }
        }

        private void renameItem<T>(ListBox box, ObservableCollection<T> collection) where T : MyInterface
        {
            T item = (T)box.SelectedItem;
            if (item != null)
            {
                var found = collection.Where(obj => obj.itemName == item.itemName);
                var rename = found.FirstOrDefault();
                if (rename != null)
                {
                    collection.Remove(rename);
                    rename.itemName = itemName.Text;
                    collection.Add(rename);
                }
            }
        }

        private void removeItem<T>(ListBox box, ObservableCollection<T> collection) where T : MyInterface
        {
            T item = (T)box.SelectedItem;
            if (item != null)
            {
                var found = collection.Where(obj => obj.itemName == item.itemName);
                collection.Remove(found.FirstOrDefault());
            }
        }

        private bool createNew<T>(ObservableCollection<T> collection, Func<T> func) where T : MyInterface
        {
            bool created = false;

            if (collection.Where(obj => obj.itemName == itemName.Text).FirstOrDefault() == null)
            {
                created = true;
                var m = func.DynamicInvoke();
                collection.Add((T)m);
            }
            return created;
        }

        /// <summary>
        /// Delete the item based on the tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (selectedTab)
            {
                case ITEM_TAB.Weapon:
                    {
                        removeItem<Weapon>(weaponList, weaponCollection);
                        break;
                    }
                case ITEM_TAB.Armor:
                    {
                        removeItem<Armor>(armorList, armorCollection);
                        break;
                    }
                case ITEM_TAB.Jewelry:
                    {
                        removeItem<Jewelry>(JewelryList, jewelryCollection);
                        break;
                    }
                case ITEM_TAB.Misc:
                    {
                        removeItem<Misc>(miscList, miscCollection);
                        break;
                    }
            }
        }

        /// <summary>
        /// Write out the xml
        /// </summary>
        /// <param name="showPopup"></param>
        private void WriteOutXML(bool showPopup = true)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = false;

            var selectedList = getListboxFromTab(selectedTab);

            if (selectedList != null && selectedList.Items.Count > 0)
            {
                if (showPopup && PopUpHelper.WouldYouLikeToSave(ItemExists(itemName.Text), selectedTab.ToString()))
                {
                    //OnSaveClicked(this, null);
                }

                //GetfilePath from selected tab, 
                var configPath = LoadAppConfigs.filePathToData + selectedTab.ToString() + "Config.xml";
                XmlWriter writer = XmlWriter.Create(configPath, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement(selectedTab.ToString() + "s");

                //Add all of our spells we have created
                foreach (var item in selectedList.Items)
                {
                    writer.WriteStartElement(selectedTab.ToString());
                    writer.WriteAttributeString("itemName", item.ToString());
                    writer.WriteAttributeString("id", "0");
                    writer.WriteElementString("isEquipped", "0");
                    writer.WriteElementString("rarity", "0");
                    writer.WriteElementString("itemClass", "0");
                    writer.WriteElementString("type", "0");
                    writer.WriteElementString("imagePath", "x.png");

                    //output the specifics based on the tab we are in
                    switch (selectedTab)
                    {
                        case ITEM_TAB.Weapon:
                            writer.WriteElementString("minDmgLow", "1.1");
                            writer.WriteElementString("minDmgHigh", "1.1");
                            writer.WriteElementString("maxDmgLow", "1.1");
                            writer.WriteElementString("maxDmgHigh", "1.1");
                            writer.WriteElementString("weaponSpeed", "1.1");
                            break;
                        case ITEM_TAB.Armor:
                            writer.WriteElementString("armorMin", "0");
                            writer.WriteElementString("armorMax", "100");
                            break;
                        case ITEM_TAB.Jewelry:
                            writer.WriteElementString("effectedStat", "0");
                            writer.WriteElementString("value", "0");
                            break;
                        case ITEM_TAB.Misc:
                            writer.WriteElementString("displayText", "0");
                            break;
                    }
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();

                if (showPopup)
                {
                    System.Windows.Forms.MessageBox.Show(selectedTab.ToString() + "s successfully exported!");
                }
            }
            else
            {
                //Something is wrong no spells added or to create
                System.Windows.Forms.MessageBox.Show("Somethings went wrong, no items where found. Are you sure you hit the Save Button?");
            }
        }

        private Weapon createWeapon()
        {
            Weapon item = new Weapon();
            item.itemName = itemName.Text;
            item.type = Weapon_Type.Text;
            item.itemClass = Weapon_Class.Text;
            item.rarity = Weapon_Rarity.Text;
            item.minDmgLow = MinDmgLow.Text;
            item.minDmgHigh = MinDmgHigh.Text;
            item.maxDmgLow = MaxDmgLow.Text;
            item.maxDmgHigh = MaxDmgHigh.Text;
            item.weaponSpeed = Weapon_Speed.Text;
            item.isEquipped = "false";
            item.imagePath = "";

            return item;
        }
        private Armor createArmor()
        {
            Armor item = new Armor();
            item.itemName = itemName.Text;
            item.type = Armor_Type.Text;
            item.itemClass = Armor_Class.Text;
            item.armorMax = Armor_Max.Text;
            item.armorMin = Armor_Min.Text;
            item.rarity = Armor_Rarity.Text;
            item.isEquipped = "false";
            item.imagePath = "";
            return item;
        }
        private Jewelry createJewelry()
        {
            Jewelry item = new Jewelry();
            item.itemName = itemName.Text;
            item.itemClass = Jewel_Class.Text;
            item.rarity = Jewel_Rarity.Text;
            item.value = "need a field for this";
            item.isEquipped = "false";
            item.imagePath = "";
            return item;
        }
        private Misc createMisc()
        {
            Misc item = new Misc();
            item.itemName = itemName.Text;
            item.itemClass = Misc_Class.Text;
            item.rarity = Misc_Rarity.Text;
            item.displayText = Misc_Text.Text;
            item.isEquipped = "False";
            item.imagePath = "";
            return item;
        }

        /// <summary>
        /// Create item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateItem_Click(object sender, RoutedEventArgs e)
        {
            bool created = false;
            switch (selectedTab)
            {
                case ITEM_TAB.Weapon:
                    {
                        created = createNew<Weapon>(weaponCollection, createWeapon);
                        break;
                    }
                case ITEM_TAB.Armor:
                    {
                        created = createNew<Armor>(armorCollection, createArmor);
                        break;
                    }
                case ITEM_TAB.Jewelry:
                    {
                        created = createNew<Jewelry>(jewelryCollection, createJewelry);
                        break;
                    }
                case ITEM_TAB.Misc:
                    {
                        created = createNew<Misc>(miscCollection, createMisc);
                        break;
                    }
            }

            if (!created)
            {
                System.Windows.Forms.MessageBox.Show("There is already a " + selectedTab.ToString() + " with the name " + itemName.Text);
            }
        }
    }
}
