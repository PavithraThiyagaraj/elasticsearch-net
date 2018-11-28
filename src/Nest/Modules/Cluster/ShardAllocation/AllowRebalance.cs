using System.Runtime.Serialization;
using System.Runtime.Serialization;


namespace Nest
{

	public enum AllowRebalance
	{
		/// <summary>
		/// (default) Always allow rebalancing.
		/// </summary>
		[EnumMember(Value = "always")]
		All,

		/// <summary>
		/// Only when all primaries in the cluster are allocated.
		/// </summary>
		[EnumMember(Value = "indices_primaries_active")]
		Primaries,

		/// <summary>
		/// Only when all shards (primaries and replicas) in the cluster are allocated.
		/// </summary>
		[EnumMember(Value = "indices_all_active")]
		Replicas,
	}
}
