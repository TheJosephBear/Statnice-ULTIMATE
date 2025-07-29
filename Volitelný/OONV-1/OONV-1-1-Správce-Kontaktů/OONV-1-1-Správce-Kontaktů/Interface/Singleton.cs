namespace OONV_1_1_Správce_Kontaktů.Interface {
    /// <summary>
    /// Provides a base class for singleton pattern implementation.
    /// Ensures only one instance of the derived class exists.
    /// </summary>
    /// <typeparam name="T">The singleton type.</typeparam>
    public abstract class Singleton<T> where T : Singleton<T>, new() {
        private static T _instance;

        /// <summary>
        /// Gets the single instance of the singleton.
        /// Creates the instance if it does not exist.
        /// </summary>
        public static T Instance {
            get {
                if (_instance == null) {
                    _instance = new T();
                }
                return _instance;
            }
        }
    }
}
