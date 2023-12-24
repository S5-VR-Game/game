using System.Collections.Generic;
using System.Linq;

namespace Game.Metrics
{
    /// <summary>
    /// Represents a metric and its statistic values.
    /// </summary>
    public class StatisticMetric : IMetricFormat<float>
    {
        private const string MaxDescription = "{0}_max";
        private const string MinDescription = "{0}_min";
        private const string AvgDescription = "{0}_avg";
        private const string Quartile1Description = "{0}_q1";
        private const string Quartile2Description = "{0}_q2";
        private const string Quartile3Description = "{0}_q3";
        private const string InterquartileRangeDescription = "{0}_iqr";

        private readonly string m_Name;
        private readonly List<float> m_Data = new();

        // statistic values of the collected data, need to be public for json serialization
        public float min;
        public float max;
        public float avg;
        public float quartile1;
        public float quartile2;
        public float quartile3;
        public float interquartileRange;

        public StatisticMetric(string name)
        {
            m_Name = name;
        }

        /// <summary>
        /// Adds a value to the collected data.
        /// </summary>
        /// <param name="value">value to be added</param>
        public void AddValue(float value)
        {
            m_Data.Add(value);
        }

        /// <summary>
        /// Returns the median value of the collected data.
        /// </summary>
        /// <returns>median value</returns>
        private float GetMedian(List<float> list)
        {
            if (!list.Any())
            {
                return 0;
            }
            
            list.Sort();
            var count = list.Count;
            if (count % 2 == 0)
            {
                return (list[count / 2 - 1] + list[count / 2]) / 2;
            }

            return list[count / 2];
        }

        public string[] GetDescriptionEntries()
        {
            return new[]
            {
                string.Format(MinDescription, m_Name),
                string.Format(MaxDescription, m_Name),
                string.Format(AvgDescription, m_Name),
                string.Format(Quartile1Description, m_Name),
                string.Format(Quartile2Description, m_Name),
                string.Format(Quartile3Description, m_Name),
                string.Format(InterquartileRangeDescription, m_Name),
            };
        }

        public float[] GetDataEntries()
        {
            EvaluateDataSet();
            return new[]
            {
                min,
                max,
                avg,
                quartile1,
                quartile2,
                quartile3,
                interquartileRange,
            };
        }

        /// <summary>
        /// Evaluates the collected data and calculates the statistic values. Stores the values in the corresponding fields.
        /// Should be called before accessing json serialization, to ensure up to date values.
        /// </summary>
        public void EvaluateDataSet()
        {
            // quartile 2 is the median
            quartile2 = GetMedian(m_Data);
            // quartile 1 is the median of the lower half of the data
            quartile1 = GetMedian(m_Data.Where(x => x < quartile2).ToList()); 
            // quartile 3 is the median of the upper half of the data
            quartile3 = GetMedian(m_Data.Where(x => x > quartile2).ToList()); 
            interquartileRange = quartile3 - quartile1;
            
            min = m_Data.Any() ? m_Data.Min() : 0;
            max = m_Data.Any() ? m_Data.Max() : 0;
            avg = m_Data.Any() ? m_Data.Average() : 0;
        }
    }
}