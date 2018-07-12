
namespace UtilityClasses
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Windows.Media.Imaging;

    public static class PopUpHelper
    {

        public enum SPITE_LOCATIONS { SPELLS = 0, ARMOR, WEAPONS, JEWELRY, MISC, ITEMS, ENEMIES }

        static string filePathToSprites = "..\\..\\..\\..\\Assets\\GameSprites\\";
        const string standAloneImageFilepath = "..\\..\\Data\\";

        private static string ConstructImagePath(SPITE_LOCATIONS loc)
        {
            return filePathToSprites + loc.ToString() + "\\";
        }

        private static string ConstructStandAlonePath(SPITE_LOCATIONS loc)
        {
            return standAloneImageFilepath + loc.ToString() + "\\";
        }

        /// <summary>
        /// Annoying pop up to see if they want to save there current (thing)
        /// </summary>
        public static bool WouldYouLikeToCreateUnityFolder(string nameOfThingToSave)
        {
            bool save = false;
            DialogResult result3 = System.Windows.Forms.MessageBox.Show("Folder does not exist in Unity, would you like to create it now?",
            "FYI!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result3 == System.Windows.Forms.DialogResult.Yes)
            {
                Directory.CreateDirectory(nameOfThingToSave);
                save = true;

            }
            return save;
        }

        /// <summary>
        /// Annoying pop up to see if they want to save there current (thing)
        /// </summary>
        public static bool WouldYouLikeToSave(bool thingExists, string nameOfThingToSave)
        {
            bool save = false;

            //Check to make sure they saved the current thing 
            if (!thingExists)
            {
                DialogResult result3 = System.Windows.Forms.MessageBox.Show("Looks like you forgot to save your " + nameOfThingToSave + "\n would you like to save it now?",
                "FYI!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result3 == System.Windows.Forms.DialogResult.Yes)
                {
                    save = true;
                }
            }

            return save;
        }

        /// <summary>
        /// Annoying pop up to make sure they want to exit
        /// </summary>
        public static void SaveAndExit(bool thingExists, string nameOfThingToSave)
        {
            WouldYouLikeToSave(thingExists, nameOfThingToSave);

            DialogResult result3 = System.Windows.Forms.MessageBox.Show("Are you sure you want to Exit?",
                            "Character Suite", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result3 == System.Windows.Forms.DialogResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        public static BitmapImage SelectImage(SPITE_LOCATIONS loc, ref string imageFileName)
        {
            BitmapImage bi = new BitmapImage();

            //open dialog box to select the image
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            // Set default file extension 
            dlg.DefaultExt = ".png";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                var filePathToUnityImages = ConstructImagePath(loc);
                var standAlone = ConstructStandAlonePath(loc);

                //Move the file to the image resource location for UNITY
                if (Directory.Exists(filePathToUnityImages) || WouldYouLikeToCreateUnityFolder(filePathToUnityImages))
                {
                    Console.WriteLine("Moving file to location " + filePathToUnityImages);
                    bi = SelectImage(dlg, filePathToUnityImages, ref imageFileName);
                }
                else
                {
                    Console.WriteLine("Saving files to the StandAloneDirectory");

                    //If this directory doesn't exist as wel... Create it
                    if (!Directory.Exists(standAlone))
                    {
                        Directory.CreateDirectory(standAlone);
                    }

                    bi =SelectImage(dlg, standAlone, ref imageFileName);
                }
            }

            return bi;
        }

        private static BitmapImage SelectImage(Microsoft.Win32.OpenFileDialog dlg, string filePath, ref string imageFileName)
        {
            char[] bob = { '\\' };
            var parsed = dlg.FileName.Split(bob);
            var name = parsed[parsed.Length - 1];

            //Save the location
            imageFileName = filePath + name;

            try
            {
                File.Copy(dlg.FileName, imageFileName, false);
            }
            catch
            {
                DialogResult result3 = System.Windows.Forms.MessageBox.Show("An image with that name already exists, Would you like to overwrite the image?",
                 "FYI!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result3 == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        File.Copy(dlg.FileName, imageFileName, true);
                    }
                    catch
                    {
                        Console.WriteLine("Image is already in the correct Location, do not copy/move");
                    }
                }
            }

            //Some hacky weirdness to be able to load a relative path...
            var absolutePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + imageFileName;
            return new BitmapImage(new Uri(absolutePath, UriKind.RelativeOrAbsolute));
        }
    }
}
