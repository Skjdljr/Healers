namespace UtilityClasses
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    public class LoadAppConfigs
    {
        public static string filePathToData = "..\\..\\Data\\";

        /// <summary>
        /// Configuration data container that supports native C# XML parsing
        /// </summary>
        private Object config = null;

        // private void CreateMenuButtons<T>(ObservableCollection<T> any
        public LoadAppConfigs()
        {

        }

        /// <summary>
        /// Gets the training lobby configuration data
        /// </summary>
        public Object Settings
        {
            get
            {
                return this.config;
            }
        }
        /// <summary>
        /// Loads application configuration data into memory.
        /// </summary>
        public bool LoadConfig<T>(string configFile)
        {

            bool success = false;

            var methodInfo = System.Reflection.MethodBase.GetCurrentMethod();
            string method = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;

            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                using (FileStream reader = new FileStream(configFile, FileMode.Open, FileAccess.Read))
                {
                    this.config = (T)ser.Deserialize(reader);
                    success = true;
                }
            }
            catch (Exception e)
            {
                success = false;
            }

            return success;
        }
    }
}
