namespace CreatorSuite
{
    using System;
    using System.Windows;
    using UtilityClasses;

    /// <summary>
    /// Interaction logic for EnemyCreator.xaml
    /// </summary>
    public partial class EnemyCreator : System.Windows.Controls.UserControl
    {
        public EnemyCreator()
        {
            InitializeComponent();
        }

        private bool CharacterExists(string name)
        {
            return !String.IsNullOrEmpty(name);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(PopUpHelper.WouldYouLikeToSave(CharacterExists(characterName.Text), "Character"))
            {
                //save the doc
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            PopUpHelper.SaveAndExit(CharacterExists(characterName.Text), "character");
        }
    }
}
