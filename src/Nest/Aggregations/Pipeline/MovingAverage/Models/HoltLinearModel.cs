﻿using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(HoltLinearModel))]
	public interface IHoltLinearModel : IMovingAverageModel
	{
		[DataMember(Name ="alpha")]
		float? Alpha { get; set; }

		[DataMember(Name ="beta")]
		float? Beta { get; set; }
	}

	public class HoltLinearModel : IHoltLinearModel
	{
		public float? Alpha { get; set; }
		public float? Beta { get; set; }
		string IMovingAverageModel.Name { get; } = "holt";
	}

	public class HoltLinearModelDescriptor
		: DescriptorBase<HoltLinearModelDescriptor, IHoltLinearModel>, IHoltLinearModel
	{
		float? IHoltLinearModel.Alpha { get; set; }
		float? IHoltLinearModel.Beta { get; set; }
		string IMovingAverageModel.Name { get; } = "holt";

		public HoltLinearModelDescriptor Alpha(float? alpha) => Assign(alpha, (a, v) => a.Alpha = v);

		public HoltLinearModelDescriptor Beta(float? beta) => Assign(beta, (a, v) => a.Beta = v);
	}
}
