﻿using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(FielddataFrequencyFilter))]
	public interface IFielddataFrequencyFilter
	{
		[DataMember(Name ="max")]
		double? Max { get; set; }

		[DataMember(Name ="min")]
		double? Min { get; set; }

		[DataMember(Name ="min_segment_size")]
		int? MinSegmentSize { get; set; }
	}

	public class FielddataFrequencyFilter : IFielddataFrequencyFilter
	{
		public double? Max { get; set; }
		public double? Min { get; set; }
		public int? MinSegmentSize { get; set; }
	}

	public class FielddataFrequencyFilterDescriptor
		: DescriptorBase<FielddataFrequencyFilterDescriptor, IFielddataFrequencyFilter>, IFielddataFrequencyFilter
	{
		double? IFielddataFrequencyFilter.Max { get; set; }
		double? IFielddataFrequencyFilter.Min { get; set; }
		int? IFielddataFrequencyFilter.MinSegmentSize { get; set; }

		public FielddataFrequencyFilterDescriptor Min(double? min) => Assign(min, (a, v) => a.Min = v);

		public FielddataFrequencyFilterDescriptor Max(double? max) => Assign(max, (a, v) => a.Max = v);

		public FielddataFrequencyFilterDescriptor MinSegmentSize(int? minSegmentSize) => Assign(minSegmentSize, (a, v) => a.MinSegmentSize = v);
	}
}
