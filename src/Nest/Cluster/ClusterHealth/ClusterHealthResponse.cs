﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class ClusterHealthResponse : ResponseBase
	{
		[DataMember(Name = "active_primary_shards")]
		public int ActivePrimaryShards { get; internal set; }

		[DataMember(Name = "active_shards")]
		public int ActiveShards { get; internal set; }

		[DataMember(Name = "cluster_name")]
		public string ClusterName { get; internal set; }

		[DataMember(Name = "indices")]
		[JsonFormatter(typeof(ResolvableReadOnlyDictionaryFormatter<IndexName, IndexHealthStats>))]
		public IReadOnlyDictionary<IndexName, IndexHealthStats> Indices { get; internal set; } =
			EmptyReadOnly<IndexName, IndexHealthStats>.Dictionary;

		[DataMember(Name = "initializing_shards")]
		public int InitializingShards { get; internal set; }

		[DataMember(Name = "number_of_data_nodes")]
		public int NumberOfDataNodes { get; internal set; }

		[DataMember(Name = "number_of_nodes")]
		public int NumberOfNodes { get; internal set; }

		[DataMember(Name = "number_of_pending_tasks")]
		public int NumberOfPendingTasks { get; internal set; }

		[DataMember(Name = "relocating_shards")]
		public int RelocatingShards { get; internal set; }

		[DataMember(Name = "status")]
		public Health Status { get; internal set; }

		[DataMember(Name = "timed_out")]
		public bool TimedOut { get; internal set; }

		[DataMember(Name = "unassigned_shards")]
		public int UnassignedShards { get; internal set; }
	}
}
