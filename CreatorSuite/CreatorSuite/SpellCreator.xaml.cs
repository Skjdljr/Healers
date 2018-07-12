using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Xml;
using UtilityClasses;

namespace CreatorSuite
{
    /// <summary>
    /// Interaction logic for SpellCreator.xaml
    /// </summary>
    public partial class SpellCreator : System.Windows.Controls.UserControl
    {
        private SpellConfig config;
        private LoadAppConfigs loadAppConfig = new LoadAppConfigs();
        private string imageFileName { get; set; }
        private const string filePathToConfig = "..\\..\\Data\\XML\\SpellConfig.xml";
        public ObservableCollection<SpellConfigSpell> spellsToCreate { get; set; }
        
        public SpellCreator()
        {
            InitializeComponent();
            this.DataContext = this;

            spellsToCreate = new ObservableCollection<SpellConfigSpell>();
            
            imageFileName = "";
            LoadXml();
        }

        /// <summary>
        /// When you create the click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSaveClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            //add to the list of created spells
            SpellConfigSpell simple = new SpellConfigSpell();

            CheckSpellNumberValues();

            simple.Name = spellName.Text;
            simple.ID = 0;
            simple.resourceCost = Convert.ToInt32(resourceCost.Text);
            simple.power = Convert.ToDecimal(power.Text);
            simple.coolDown = Convert.ToDecimal(coolDown.Text);
            simple.castTime = Convert.ToDecimal(castTime.Text);
            simple.radius = Convert.ToInt32(radiusField.Text);
            simple.duration = Convert.ToDecimal(durationField.Text);
            simple.interval = Convert.ToInt32(intervalField.Text);
            simple.effectType = effectType.IsEnabled ? effectType.Text : "NA";
            simple.spellType = spellType.Text;
            simple.spellSchool = spellSchool.Text;
            simple.classType = classType.Text;
            simple.imagePath = imageFileName;

            var absolutePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + imageFileName;
            try
            {
                testerImage.Source = new BitmapImage(new Uri(absolutePath, UriKind.RelativeOrAbsolute));
            }
            catch (DirectoryNotFoundException dne)
            {
                testerImage.Source = new BitmapImage();
                throw (dne);
            }

            if (simple.Name == "")
            {
                System.Windows.Forms.MessageBox.Show("Please select a name for your spell!");
            }
            else
            {
                if (!SpellExists(simple))
                {
                    spellsToCreate.Add(simple);
                }
                else
                {
                    DialogResult result3 = System.Windows.Forms.MessageBox.Show("Are you sure you want to override this spell?",
                    "A spell with that name already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result3 == System.Windows.Forms.DialogResult.Yes)
                    {
                        OverrideSpell(simple);
                    }
                }

                //The spell was either created or we overwrote it, so lets update the display and make our newly created/updated spell the selected one in the list
                ListOfSpells.SelectedIndex = ListOfSpells.Items.Count - 1;
                ListOfSpells.Items.Refresh();
            }
        }

        private void CheckSpellNumberValues()
        {
            FillBlankTextBoxValues(resourceCost);
            FillBlankTextBoxValues(power);
            FillBlankTextBoxValues(coolDown);
            FillBlankTextBoxValues(castTime);
            FillBlankTextBoxValues(radiusField);
            FillBlankTextBoxValues(durationField);
        }

        private void FillBlankTextBoxValues(System.Windows.Controls.TextBox tb)
        {
            if (string.IsNullOrEmpty(tb.Text) || tb.Text == "-")
            {
                tb.Text = "0";
            }
        }

        /// <summary>
        /// Delete the spell out of the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeleteClicked(object sender, RoutedEventArgs e)
        {
            var index = ListOfSpells.SelectedIndex;

            if (index >= 0)
            {
                var spell = ListOfSpells.Items.GetItemAt(index) as SpellConfigSpell;

                //Remove the item from the spells to be added
                spellsToCreate.Remove(spell);

                if (spellsToCreate.Count == 0)
                {
                    ClearDisplaySpell();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please select a spell to delete!");
            }
        }

        private void ClearDisplaySpell()
        {
            spellName.Text = "";
            resourceCost.Text = "0";
            power.Text = "0";
            coolDown.Text = "0";
            castTime.Text = "0";
            radiusField.Text = "0";
            durationField.Text = "0";
            intervalField.Text = "0";
            effectType.SelectedIndex = 0;
            spellType.SelectedIndex = 0;
            spellSchool.SelectedIndex = 0;
            classType.SelectedIndex = 0;
            imageFileName = "";
            testerImage.Source = new BitmapImage(new Uri(imageFileName, UriKind.RelativeOrAbsolute));
        }

        private void OnRenameClicked(object sender, RoutedEventArgs e)
        {
            var index = ListOfSpells.SelectedIndex;

            if (index >= 0)
            {
                var display = ListOfSpells.Items.GetItemAt(index) as SpellConfigSpell;
                var data = FindSpell(display);

                //rename our display
                display.Name = this.spellName.Text;

                //rename what gets saved
                data.Name = this.spellName.Text;

                ListOfSpells.Items.Refresh();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please select a spell to rename!");
            }
        }

        /// <summary>
        /// When you select an image for your spell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectImageClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            string fileName = imageFileName;
            testerImage.Source = UtilityClasses.PopUpHelper.SelectImage(PopUpHelper.SPITE_LOCATIONS.SPELLS, ref fileName);
            imageFileName = fileName;
        }

        /// <summary>
        /// Load the xml file!
        /// </summary>
        private void LoadXml()
        {
            // if the file doesn't exist create a blank one
            if (!File.Exists(filePathToConfig))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.NewLineOnAttributes = false;

                XmlWriter writer = XmlWriter.Create(filePathToConfig, settings);
                writer.Flush();
                writer.Close();
            }
            else
            {
                //Loads the xml! And its data
                if (loadAppConfig.LoadConfig<SpellConfig>(filePathToConfig))
                {
                    this.config = (SpellConfig)loadAppConfig.Settings;

                    if (this.config.Spell != null)
                    {
                        foreach (var spell in this.config.Spell)
                        {
                            //Add each of the items, to be created list. 
                            spellsToCreate.Add(spell);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Save our data
        /// </summary>
        private void ExportToFile()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = false;

            if (spellsToCreate != null && spellsToCreate.Count > 0)
            {
                if (PopUpHelper.WouldYouLikeToSave(SpellExists(spellName.Text), "item"))
                {
                    OnSaveClicked(this, null);
                }

                XmlWriter writer = XmlWriter.Create(filePathToConfig, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("SpellConfig");

                //Add all of our spells we have created
                foreach (var spell in spellsToCreate)
                {
                    writer.WriteStartElement("Spell");
                    writer.WriteAttributeString("Name", spell.Name);
                    writer.WriteAttributeString("ID", spell.ID.ToString());
                    writer.WriteElementString("resourceCost", spell.resourceCost.ToString());
                    writer.WriteElementString("power", spell.power.ToString());
                    writer.WriteElementString("coolDown", spell.coolDown.ToString());
                    writer.WriteElementString("castTime", spell.castTime.ToString());
                    //Used for all special effects of the spell
                    writer.WriteStartElement("SpellEffects");
                    writer.WriteElementString("effect", spell.effectType);
                    writer.WriteElementString("radius", spell.radius.ToString());
                    writer.WriteElementString("duration", spell.duration.ToString());
                    writer.WriteElementString("interval", spell.interval.ToString());
                    writer.WriteElementString("spellType", spell.spellType);
                    writer.WriteElementString("spellSchool", spell.spellSchool);
                    writer.WriteEndElement();

                    writer.WriteElementString("classType", spell.classType);
                    writer.WriteElementString("imagePath", spell.imagePath);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();

                System.Windows.Forms.MessageBox.Show("Spells successfully exported!");

            }
            else
            {
                //Something is wrong no spells added or to create
                System.Windows.Forms.MessageBox.Show("Somethings went wrong, no spells where found. Are you sure you hit the Save Button?");
            }
        }

        /// <summary>
        /// When the spell is selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSpellSelected(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedSpell = (sender as System.Windows.Controls.ListBox).SelectedValue as SpellConfigSpell;

            if (selectedSpell != null)
            {
                //Even though we have the spell from the list we need the actual data
                DisplaySpell(FindSpell(selectedSpell));
            }
        }

        /// <summary>
        /// Update our fields with the new spell
        /// </summary>
        /// <param name="spell"></param>
        private void DisplaySpell(SpellConfigSpell spell)
        {
            spellName.Text = spell.Name;
            resourceCost.Text = spell.resourceCost.ToString();
            power.Text = spell.power.ToString();
            coolDown.Text = spell.coolDown.ToString();
            castTime.Text = spell.castTime.ToString();
            radiusField.Text = spell.radius.ToString();
            durationField.Text = spell.duration.ToString();
            intervalField.Text = spell.interval.ToString();
            spellType.Text = spell.spellType;
            spellSchool.Text = spell.spellSchool;
            classType.Text = spell.classType;
            imageFileName = spell.imagePath;

            try
            {
                //Some hacky weirdness to be able to load a relative path...
                var absolutePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + imageFileName;
                testerImage.Source = new BitmapImage(new Uri(absolutePath, UriKind.RelativeOrAbsolute));
            }
            catch (Exception dnfe)
            {
                testerImage.Source = new BitmapImage();
                throw (dnfe);
            }
        }

        /// <summary>
        /// OverRide our spell
        /// </summary>
        /// <param name="updatedSpell"></param>
        private void OverrideSpell(SpellConfigSpell updatedSpell)
        {
            for (int i = 0; i < spellsToCreate.Count; i++)
            {
                var spell = spellsToCreate[i];
                if (spell.Name == updatedSpell.Name)
                {
                    //we have a spell to update, remove old replace with new
                    spellsToCreate.Remove(spell);
                    spellsToCreate.Add(updatedSpell);
                    break;
                }
            }
            ListOfSpells.SelectedIndex = ListOfSpells.Items.Count - 1;
            ListOfSpells.Items.Refresh();
        }

        /// <summary>
        /// Find our spell by string name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private SpellConfigSpell FindSpell(string name)
        {
            return spellsToCreate.Where(spell => spell.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Find our spell by string name (passing in a spell)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private SpellConfigSpell FindSpell(SpellConfigSpell simple)
        {
            return spellsToCreate.Where(spell => spell.Name == simple.Name).FirstOrDefault();
        }

        /// <summary>
        /// See if our spell exists
        /// </summary>
        /// <param name="simple"></param>
        /// <returns></returns>
        private bool SpellExists(SpellConfigSpell simple)
        {
            return (null != FindSpell(simple));
        }

        /// <summary>
        /// See if our spell exists
        /// </summary>
        /// <param name="simple"></param>
        /// <returns></returns>
        private bool SpellExists(string spellName)
        {
            return (null != FindSpell(spellName));
        }

        private void OnFloatInput(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var box = (sender as System.Windows.Controls.TextBox);
            float number;
            if (box != null)
            {
                if (!float.TryParse(box.Text, out number) || number < 0 || number > 999)
                {
                    if (!string.IsNullOrEmpty(box.Text) && box.Text != "-")
                    {
                        //make sure there isn't more then one .
                        if (box.Text.IndexOf('.') == box.Text.LastIndexOf('.'))
                        {
                            box.Clear();
                            box.Text = "0";
                            System.Windows.Forms.MessageBox.Show("Please enter a number value between 0 to 999");
                        }
                        else
                        {
                            box.Clear();
                            box.Text = "0";
                            System.Windows.Forms.MessageBox.Show("Please only have one . in your float");
                        }
                    }
                }
            }
        }

        private void OnPositiveNumberInput(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var box = (sender as System.Windows.Controls.TextBox);

            int number;
            if (box != null)
            {
                if (!int.TryParse(box.Text, out number) || number < -999 || number > 999)
                {
                    if (!string.IsNullOrEmpty(box.Text))
                    {
                        box.Clear();
                        box.Text = "0";
                        System.Windows.Forms.MessageBox.Show("Please enter an integer between 0-999");
                    }
                }
            }

        }

        private void SelectSpellType(string type)
        {
            durationField.Visibility = effectType.Visibility = radiusField.Visibility = intervalField.Visibility = Visibility.Hidden;

            switch (type)
            {
                case "Heal":
                    spellSchool.Visibility = Visibility.Hidden;
                    effectType.Visibility = Visibility.Visible;
                    radiusField.Visibility = Visibility.Visible;
                    durationField.Visibility = Visibility.Visible;
                    intervalField.Visibility = Visibility.Visible;
                    break;
                case "Debuff":
                case "Buff":
                    spellSchool.Visibility = Visibility.Visible;
                    durationField.Visibility = Visibility.Visible;
                    effectType.Visibility = Visibility.Visible;
                    break;
                case "Damage":
                    effectType.Visibility = Visibility.Visible;
                    durationField.Visibility = Visibility.Visible;
                    radiusField.Visibility = Visibility.Visible;
                    intervalField.Visibility = Visibility.Visible;
                    break;
                case "Crowd Control":
                    durationField.Visibility = Visibility.Visible;
                    radiusField.Visibility = Visibility.Visible;
                    break;
            }

        }

        private void OnSpellTypeChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var item = (e.AddedItems[0] as System.Windows.Controls.ComboBoxItem);
                SelectSpellType(item.Content.ToString());
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            OnSaveClicked(sender, e);
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            ExportToFile();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            PopUpHelper.SaveAndExit(SpellExists(spellName.Text), "spell");
        }

        private void CreateSpell_Click(object sender, RoutedEventArgs e)
        {
            OnSaveClicked(sender, e);
        }
    }
}
