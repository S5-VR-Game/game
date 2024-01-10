using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Game.Tasks;
using Logging;
using Newtonsoft.Json;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Random = System.Random;
using System.Security.Cryptography;

namespace Game.Metrics
{
    /// <summary>
    /// Provides data structures for metric collections and csv export functionality.
    /// </summary>
    public class MetricData
    {
        private readonly Logger m_LOG = new Logger(new LogHandler());
        private const string LOGTag = "MetricData";
        
        private const char CsvDelimiter = ';';
        private const string ExportFileNameCsv = "collected_game_metrics.csv";
        private const string ExportFileNameJson = "collected_game_metrics_{0}.json";
        
        [JsonProperty("generalMetrics")]
        public readonly Dictionary<SingleValueMetric, object> metrics = new();
        
        [JsonProperty("taskMetrics")]
        public readonly Dictionary<GameTaskType, TaskMetrics> taskMetrics = new();

        [JsonProperty("timerTaskRemainingSeconds")]
        public readonly StatisticMetric timerTaskRemainingSeconds = new("timerTaskRemainingSeconds");
        [JsonProperty("secondsBetweenTaskSpawn")]
        public readonly StatisticMetric secondsBetweenTaskSpawn = new("secondsBetweenTaskSpawn");
        
        public MetricData()
        {
            // initialize metrics
            foreach (var metric in Enum.GetValues(typeof(SingleValueMetric)))
            {
               metrics.Add((SingleValueMetric) metric, null); 
            }
            // initialize task metrics
            foreach (var taskType in Enum.GetValues(typeof(GameTaskType)))
            {
                taskMetrics.Add((GameTaskType) taskType, new TaskMetrics((GameTaskType) taskType));
            }
        }

        /// <summary>
        /// Writes the current metrics to different files types. The files will be written to the project root directory.
        /// If a CSV file already exists, the current metrics will be appended to the file.
        /// If a JSON file already exists, it will be overwritten.
        /// </summary>
        /// <param name="gameId">game id of the current game, which is used to store the json file with an unique filename</param>
        /// <param name="csv">if true, a csv file will be generated</param>
        /// <param name="json">if true, a json file will be generated</param>
        public void WriteToFile(string gameId, bool csv = true, bool json = true)
        {
            if (csv)
            {
                // write csv string to file, include csv header row, if file does not exist
                File.AppendAllText(ExportFileNameCsv, ToCsv(includeHeader: !File.Exists(ExportFileNameCsv)));
                m_LOG.Log(LOGTag, "metric data written to csv file");
            }
            if (json)
            {
                // write json string to file
                File.WriteAllText(string.Format(ExportFileNameJson, gameId), ToJson());
                m_LOG.Log(LOGTag, "metric data written to json file");
            }
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
            metrics[singleValueMetric] = value;
        }
        
        /// <summary>
        /// Adds a value to a specific metric. The value will be added to the current value of the metric.
        /// </summary>
        /// <param name="singleValueMetric">type of metric, whose value will be changed</param>
        /// <param name="value">value that will be added to the current value of the metric</param>
        public void AddValueToMetric(SingleValueMetric singleValueMetric, double value)
        {
            var newValue = GetMetric<double>(singleValueMetric, 0) + value;
            metrics[singleValueMetric] = newValue;
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
            if (metrics[singleValueMetric] == null)
            {
                return defaultValue;
            }

            return (T) metrics[singleValueMetric];
        }
        
        /// <summary>
        /// Increases the task spawn counter metric for the given task typ
        /// </summary>
        /// <param name="taskType">task type, whose spawn count should be incremented</param>
        public void RegisterTaskSpawn(GameTaskType taskType)
        {
            taskMetrics[taskType].spawnCount++;
        }
        
        /// <summary>
        /// Increases the task completed counter metric for the given task typ
        /// </summary>
        /// <param name="taskType">task type, whose completed counter should be incremented</param>
        public void RegisterTaskSuccess(GameTaskType taskType)
        {
            taskMetrics[taskType].successCount++;
        }
        
        /// <summary>
        /// Increases the task failed counter metric for the given task typ
        /// </summary>
        /// <param name="taskType">task type, whose failed counter should be incremented</param>
        public void RegisterTaskFailure(GameTaskType taskType)
        {
            taskMetrics[taskType].failedCount++;
        }
        
        /// <summary>
        /// Adds a value to the timer task remaining seconds metric.
        /// </summary>
        /// <param name="remainingSeconds">value to be added</param>
        public void AddTimerTaskRemainingSecondsValue(float remainingSeconds)
        {
            timerTaskRemainingSeconds.AddValue(remainingSeconds);
        }
        
        /// <summary>
        /// Adds a value to the timer task remaining seconds metric.
        /// </summary>
        /// <param name="secondsBetweenTaskSpawns">value to be added</param>
        public void AddSecondsBetweenTaskSpawnValue(float secondsBetweenTaskSpawns)
        {
            secondsBetweenTaskSpawn.AddValue(secondsBetweenTaskSpawns);
        }

        /// <summary>
        /// Calculates an JSON-Representation of this Object and returns it.
        /// </summary>
        /// <returns>This Object as JSON-String.</returns>
        public string ToJson()
        {
            secondsBetweenTaskSpawn.EvaluateDataSet();
            timerTaskRemainingSeconds.EvaluateDataSet();
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Converts the current metrics to a csv string.
        /// </summary>
        /// <param name="includeHeader">if true, a csv header line will be included</param>
        /// <param name="prependNewLine">
        /// if true and <see cref="includeHeader"/> is false, a new line will be prepended.
        /// This can be used to separate the new line from the last line of the previous csv file.
        /// </param>
        /// <returns></returns>
        private string ToCsv(bool includeHeader = true, bool prependNewLine = true)
        {
            var csvText = new StringBuilder();
            var csvHeaderRow = new StringBuilder();
            
            // add metric names as header row
            csvHeaderRow.Append(metrics.Keys.ToSeparatedString(CsvDelimiter.ToString()));
            
            // add csv data row
            csvText.AppendJoin(CsvDelimiter, metrics.Values);
            
            // add statistic metrics
            AppendMetricFormat(secondsBetweenTaskSpawn, csvHeaderRow, csvText);
            AppendMetricFormat(timerTaskRemainingSeconds, csvHeaderRow, csvText);
            // add task metrics
            foreach (var taskMetricsEntry in taskMetrics.Values)
            {
                AppendMetricFormat(taskMetricsEntry, csvHeaderRow, csvText);
            }

            if (includeHeader)
            {
                // add csv header row and new line to start, if file does not exist
                csvText.Insert(0, csvHeaderRow + System.Environment.NewLine);
            }
            else if(prependNewLine)
            {
                // prepend new line, if parameter is set
                csvText.Insert(0, System.Environment.NewLine);
            }

            return csvText.ToString();
        }
    }
}