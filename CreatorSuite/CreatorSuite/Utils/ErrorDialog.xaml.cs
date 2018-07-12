namespace TrainingLobbyUtils
{
    using System;
    using System.Windows;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Implements the LoginErrorDialog GUI component
    /// </summary>
    public partial class ErrorDialog : Window
    {
        #region Fields/Variables
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorDialog"/> class.
        /// </summary>
        public ErrorDialog()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods
        #region Public Methods

        /// <summary>
        /// Gets or sets the dialog result: Yes or No
        /// </summary>
        public MessageBoxResult Result { get; set; }

        /// <summary>
        /// Sets the message text displayed in the dialog.
        /// </summary>
        public string Message
        {
            set
            {
                this.MessageTextBlock.Text = value;
            }
        }

        /// <summary>
        /// Sets the title text block
        /// </summary>
        public string TitleText
        {
            set
            {
                this.TitleTextBlock.Text = value;
            }
        }


        public string Image
        {
            set
            {
                this.ErrorImage.Source = new BitmapImage(new Uri("pack://application:,,,/TrainingLobbyResources;component/Images/" + value)); 
            }
        }
        #endregion

        #region Non-Public Methods

        /// <summary>
        /// Called when the OK button is clicked: closes the window.
        /// </summary>
        /// <param name="sender">Calling object</param>
        /// <param name="e">Event arguments</param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Result = MessageBoxResult.No;
            this.Close();
        }

        #endregion
        #endregion
    }
}
