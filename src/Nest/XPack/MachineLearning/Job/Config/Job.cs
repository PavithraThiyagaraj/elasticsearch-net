﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class Job
	{
		/// <summary>
		/// The analysis configuration, which specifies how to analyze the data.
		/// </summary>
		[DataMember(Name = "analysis_config")]
		public IAnalysisConfig AnalysisConfig { get; set; }

		/// <summary>
		/// Optionally specifies runtime limits for the job.
		/// </summary>
		[DataMember(Name = "analysis_limits")]
		public IAnalysisLimits AnalysisLimits { get; set; }

		/// <summary>
		/// Advanced configuration option. The time between each periodic persistence of the model.
		/// The default value is a randomized value between 3 to 4 hours, which avoids all jobs persisting
		/// at exactly the same time. The smallest allowed value is 1 hour.
		/// </summary>
		/// <remarks>
		/// For very large models (several GB), persistence could take 10-20 minutes, so do not set the value too low.
		/// </remarks>
		[DataMember(Name = "background_persist_interval")]
		public Time BackgroundPersistInterval { get; set; }

		/// <summary>
		/// The time the job was created.
		/// </summary>
		[DataMember(Name = "create_time")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset CreateTime { get; set; }

		/// <summary>
		/// Describes the format of the input data. This object is required, but it can be empty.
		/// </summary>
		[DataMember(Name = "data_description")]
		public IDataDescription DataDescription { get; set; }

		/// <summary>
		/// An optional description of the job.
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; }

		/// <summary>
		/// If the job closed or failed, this is the time the job finished, otherwise it is null.
		/// </summary>
		[DataMember(Name = "finished_time")]
		[JsonFormatter(typeof(NullableDateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset? FinishedTime { get; set; }

		/// <summary>
		/// The unique identifier for the job.
		/// </summary>
		[DataMember(Name = "job_id")]
		public string JobId { get; set; }

		/// <summary>
		/// The job type.
		/// </summary>
		/// <remarks>
		/// Reserved for future use.
		/// </remarks>
		[DataMember(Name = "job_type")]
		public string JobType { get; set; }

		/// <summary>
		/// This advanced configuration option stores model information along with the results.
		/// This adds overhead to the performance of the system and is not feasible for jobs with many entities.
		/// </summary>
		[DataMember(Name = "model_plot")]
		public IModelPlotConfig ModelPlotConfig { get; set; }

		/// <summary>
		/// A numerical character string that uniquely identifies the model snapshot.
		/// </summary>
		[DataMember(Name = "model_snapshot_id")]
		public string ModelSnapshotId { get; set; }

		/// <summary>
		/// The time in days that model snapshots are retained for the job.
		/// Older snapshots are deleted. The default value is 1 day.
		/// </summary>
		[DataMember(Name = "model_snapshot_retention_days")]
		public long? ModelSnapshotRetentionDays { get; set; }

		/// <summary>
		/// Advanced configuration option. The period over which adjustments to the score are applied, as new data
		/// is seen. The default value is the longer of 30 days or 100 bucket spans.
		/// </summary>
		[DataMember(Name = "renormalization_window_days")]
		public long? RenormalizationWindowDays { get; set; }

		/// <summary>
		/// The name of the index in which to store the machine learning results.
		/// The default value is shared (which corresponds to the index name .ml-anomalies-shared).
		/// </summary>
		[DataMember(Name = "results_index_name")]
		public string ResultsIndexName { get; set; }

		/// <summary>
		/// Advanced configuration option. The number of days for which job results are retained.
		/// Once per day at 00:30 (server time), results older than this period are deleted from Elasticsearch.
		/// The default value is null, which means results are retained.
		/// </summary>
		[DataMember(Name = "results_retention_days")]
		public long? ResultsRetentionDays { get; set; }
	}
}
