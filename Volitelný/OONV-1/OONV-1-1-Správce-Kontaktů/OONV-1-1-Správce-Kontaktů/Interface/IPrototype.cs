namespace OONV_1_1_Správce_Kontaktů.Interface {
    /// <summary>
    /// Defines a prototype pattern interface for creating a copy of an object.
    /// </summary>
    /// <typeparam name="T">Type of the object to copy.</typeparam>
    internal interface IPrototype<T> {
        /// <summary>
        /// Creates a copy of the current object.
        /// </summary>
        /// <returns>A new copy of type T.</returns>
        T Copy();
    }
}
