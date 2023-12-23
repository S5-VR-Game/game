namespace Game.Metrics
{
    /// <summary>
    /// Represents the functionality to export the description and data entries of a metric.
    /// Each description entry should describe its corresponding data entry at the same index.
    /// </summary>
    /// <typeparam name="T">datatype of the data entries</typeparam>
    public interface IMetricFormat<out T>
    {
        /// <summary>
        /// Returns the description header columns for this metric.
        /// </summary>
        /// <returns>returned as array, where each value represents a header entry</returns>
        public string[] GetDescriptionEntries();

        /// <summary>
        /// Returns the data columns for this task metrics.
        /// </summary>
        /// <returns>returned as array, where each value represents a data entry</returns>
        public T[] GetDataEntries();
    }
}