using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Game.Tasks;
using Unity.VisualScripting;

namespace Game.Metrics
{
    /// <summary>
    /// Provides data structures for metric collections and csv export functionality.
    /// </summary>
    public class MetricData
    {
        private const char CsvDelimiter = ';';
        private const string ExportFileName = "metrics.csv";
        
        private readonly Dictionary<SingleValueMetric, object> m_Metrics = new();
        
        private readonly Dictionary<GameTaskType, TaskMetrics> m_TaskMetrics = new();
        
        private readonly StatisticMetric m_TimerTaskRemainingSeconds = new("timerTaskRemainingSeconds");
        private readonly StatisticMetric m_SecondsBetweenTaskSpawn = new("secondsBetweenTaskSpawn");
        
        public MetricData()
        {
            // initialize metrics
            foreach (var metric in Enum.GetValues(typeof(SingleValueMetric)))
            {
               m_Metrics.Add((SingleValueMetric) metric, null); 
            }
            // initialize task metrics
            foreach (var taskType in Enum.GetValues(typeof(GameTaskType)))
            {
                m_TaskMetrics.Add((GameTaskType) taskType, new TaskMetrics((GameTaskType) taskType));
            }
        }
        
        /// <summary>
        /// Writes the current metrics to a csv file. If no file with the <see cref="ExportFileName"/> exists, a new
        /// file will be created and the metrics will be written as csv header row.
        /// </summary>
        public void WriteToFile()
        {
            StringBuilder csvText = new StringBuilder();
            StringBuilder csvHeaderRow = new StringBuilder();
            
            // add metric names as header row
            csvHeaderRow.Append(m_Metrics.Keys.ToSeparatedString(CsvDelimiter.ToString()));
            
            // add csv data row
            csvText.AppendJoin(CsvDelimiter, m_Metrics.Values);
            
            // add statistic metrics
            AppendMetricFormat(m_SecondsBetweenTaskSpawn, csvHeaderRow, csvText);
            AppendMetricFormat(m_TimerTaskRemainingSeconds, csvHeaderRow, csvText);
            // add task metrics
            foreach (var taskMetrics in m_TaskMetrics.Values)
            {
                AppendMetricFormat(taskMetrics, csvHeaderRow, csvText);
            }

            // add new line, to separate csv data from header row or from previous data in the file
            csvText.Insert(0, System.Environment.NewLine);
            // add csv header row to start, if file does not exist
            if (!File.Exists(ExportFileName))
            {
                csvText.Insert(0, csvHeaderRow);
            }
            
            // write csv string to file
            File.AppendAllText(ExportFileName, csvText.ToString());
        }
        
        /// <summary>
        /// Appends the metric to the given string builders and adds a csv delimiter before the metric.
        /// </summary>
        /// <param name="metricFormat">formattable metric</param>
        /// <param name="headerStringBuilder">string builder for csv header row</param>
        /// <param name="dataStringBuilder">string builder for current csv data row</param>
        /// <typeparam name="T">type of the data entries of the metric</typeparam>
        private void AppendMetricFormat<T>(IMetricFormat<T> metricFormat, StringBuilder headerStringBuilder, StringBuilder dataStringBuilder)
        {
            headerStringBuilder.Append(CsvDelimiter);
            dataStringBuilder.Append(CsvDelimiter);
            headerStringBuilder.AppendJoin(CsvDelimiter, metricFormat.GetDescriptionEntries());
            dataStringBuilder.AppendJoin(CsvDelimiter, metricFormat.GetDataEntries());
        }
        
        /// <summary>
        /// Changes the value of a specific metric.
        /// </summary>
        /// <param name="singleValueMetric">type of metric, whose value will be changed</param>
        /// <param name="value">value that will be set</param>
        /// <typeparam name="T">data type of the metric</typeparam>
        public void SetMetric<T>(SingleValueMetric singleValueMetric, T value)
        {
            m_Metrics[singleValueMetric] = value;
        }
        
        /// <summary>
        /// Adds a value to a specific metric. The value will be added to the current value of the metric.
        /// </summary>
        /// <param name="singleValueMetric">type of metric, whose value will be changed</param>
        /// <param name="value">value that will be added to the current value of the metric</param>
        public void AddValueToMetric(SingleValueMetric singleValueMetric, double value)
        {
            var newValue = GetMetric<double>(singleValueMetric, 0) + value;
            m_Metrics[singleValueMetric] = newValue;
        }
        
        /// <summary>
        /// Increments the numeric value of a specific metric. The given metric should be a numeric type.
        /// </summary>
        /// <param name="singleValueMetric">type of metric, whose value will be incremented</param>
        public void IncrementMetricValue(SingleValueMetric singleValueMetric)
        {
            AddValueToMetric(singleValueMetric, 1);
        }
        
        /// <summary>
        /// Returns the current value of a given metric.
        /// </summary>
        /// <param name="singleValueMetric">type of the metric</param>
        /// <param name="defaultValue">default value, that should be returned, if no value was set yet</param>
        /// <typeparam name="T">data type of the metric</typeparam>
        /// <returns>current stored value of the metric</returns>
        public T GetMetric<T>(SingleValueMetric singleValueMetric, T defaultValue)
        {
            if (m_Metrics[singleValueMetric] == null)
            {
                return defaultValue;
            }

            return (T) m_Metrics[singleValueMetric];
        }
        
        /// <summary>
        /// Increases the task spawn counter metric for the given task typ
        /// </summary>
        /// <param name="taskType">task type, whose spawn count should be incremented</param>
        public void RegisterTaskSpawn(GameTaskType taskType)
        {
            m_TaskMetrics[taskType].spawnCount++;
        }
        
        /// <summary>
        /// Increases the task completed counter metric for the given task typ
        /// </summary>
        /// <param name="taskType">task type, whose completed counter should be incremented</param>
        public void RegisterTaskSuccess(GameTaskType taskType)
        {
            m_TaskMetrics[taskType].successCount++;
        }
        
        /// <summary>
        /// Increases the task failed counter metric for the given task typ
        /// </summary>
        /// <param name="taskType">task type, whose failed counter should be incremented</param>
        public void RegisterTaskFailure(GameTaskType taskType)
        {
            m_TaskMetrics[taskType].failedCount++;
        }
        
        /// <summary>
        /// Adds a value to the timer task remaining seconds metric.
        /// </summary>
        /// <param name="remainingSeconds">value to be added</param>
        public void AddTimerTaskRemainingSecondsValue(float remainingSeconds)
        {
            m_TimerTaskRemainingSeconds.AddValue(remainingSeconds);
        }
        
        /// <summary>
        /// Adds a value to the timer task remaining seconds metric.
        /// </summary>
        /// <param name="secondsBetweenTaskSpawns">value to be added</param>
        public void AddSecondsBetweenTaskSpawnValue(float secondsBetweenTaskSpawns)
        {
            m_SecondsBetweenTaskSpawn.AddValue(secondsBetweenTaskSpawns);
        }
    }
}