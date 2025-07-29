namespace OONV_1_1_Správce_Kontaktů.Iterator.Interface {
    /// <summary>
    /// Generic iterator interface defining methods to iterate over a collection.
    /// </summary>
    /// <typeparam name="T">Type of elements to iterate over.</typeparam>
    internal interface IIterator<T> {
        /// <summary>
        /// Checks if there are more elements to iterate over.
        /// </summary>
        /// <returns>True if more elements exist; otherwise, false.</returns>
        bool HasNext();

        /// <summary>
        /// Gets the next element in the iteration.
        /// </summary>
        /// <returns>The next element of type T.</returns>
        T GetNext();

        /// <summary>
        /// Gets the current index in the iteration.
        /// </summary>
        /// <returns>The zero-based index of the current element.</returns>
        int GetCurrentIndex();
    }

    /// <summary>
    /// Types of list iterators supported.
    /// </summary>
    public enum ListIteratorType {
        Alphabetical
    }
}
