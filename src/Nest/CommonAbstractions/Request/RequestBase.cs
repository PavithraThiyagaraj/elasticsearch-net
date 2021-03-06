using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IRequest
	{
		[IgnoreDataMember]
		HttpMethod HttpMethod { get; }

		[IgnoreDataMember]
		RouteValues RouteValues { get; }

		// TODO refactor RequestParameters
		[IgnoreDataMember]
		IRequestParameters RequestParametersInternal { get; }

		string GetUrl(IConnectionSettingsValues settings);
	}

	public interface IRequest<TParameters> : IRequest
		where TParameters : class, IRequestParameters, new()
	{
		/// <summary>
		/// Used to describe request parameters that are not part of the body. e.g. query string, connection configuration
		/// overrides, etc.
		/// </summary>
		[IgnoreDataMember]
		TParameters RequestParameters { get; set; }
	}

	public abstract class RequestBase<TParameters> : IRequest<TParameters> where TParameters : class, IRequestParameters, new()
	{
		protected RequestBase() => Initialize();

		protected RequestBase(Func<RouteValues, RouteValues> pathSelector)
		{
			pathSelector(RequestState.RouteValues);
			Initialize();
		}

		protected virtual HttpMethod HttpMethod => RequestState.RequestParameters.DefaultHttpMethod;

		[IgnoreDataMember]
		protected IRequest<TParameters> RequestState => this;

		[IgnoreDataMember]
		HttpMethod IRequest.HttpMethod => HttpMethod;

		[IgnoreDataMember]
		TParameters IRequest<TParameters>.RequestParameters { get; set; } = new TParameters();

		[IgnoreDataMember]
		RouteValues IRequest.RouteValues { get; } = new RouteValues();

		internal abstract ApiUrls ApiUrls { get;  }

		string IRequest.GetUrl(IConnectionSettingsValues settings) => ApiUrls.Resolve(RequestState.RouteValues, settings);

		IRequestParameters IRequest.RequestParametersInternal => RequestState.RequestParameters;

		// TODO remove this is only used to make sure requests set typed_keys automatically, find better approach for this
		protected virtual void Initialize() { }

		protected TOut Q<TOut>(string name) => RequestState.RequestParameters.GetQueryStringValue<TOut>(name);

		protected void Q(string name, object value) => RequestState.RequestParameters.SetQueryString(name, value);
	}

	public abstract partial class PlainRequestBase<TParameters> : RequestBase<TParameters>
		where TParameters : class, IRequestParameters, new()
	{
		protected PlainRequestBase() { }

		protected PlainRequestBase(Func<RouteValues, RouteValues> pathSelector) : base(pathSelector) { }

		/// <summary>
		/// Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		public IRequestConfiguration RequestConfiguration
		{
			get => RequestState.RequestParameters.RequestConfiguration;
			set => RequestState.RequestParameters.RequestConfiguration = value;
		}
	}

	/// <summary>
	///  Base class for all Request descriptor types
	/// </summary>
	public abstract partial class RequestDescriptorBase<TDescriptor, TParameters, TInterface> : RequestBase<TParameters>, IDescriptor
		where TDescriptor : RequestDescriptorBase<TDescriptor, TParameters, TInterface>, TInterface
		where TParameters : RequestParameters<TParameters>, new()
	{
		private readonly TDescriptor _descriptor;

		protected RequestDescriptorBase() => _descriptor = (TDescriptor)this;

		protected RequestDescriptorBase(Func<RouteValues, RouteValues> pathSelector) : base(pathSelector) => _descriptor = (TDescriptor)this;

		protected TInterface Self => _descriptor;

		protected TDescriptor Assign<TValue>(TValue value, Action<TInterface, TValue> assign) => Fluent.Assign(_descriptor, value, assign);

		protected TDescriptor AssignParam(Action<TParameters> assigner)
		{
			assigner?.Invoke(RequestState.RequestParameters);
			return _descriptor;
		}

		protected TDescriptor Qs(Action<TParameters> assigner)
		{
			assigner?.Invoke(RequestState.RequestParameters);
			return _descriptor;
		}

		protected TDescriptor Qs(string name, object value)
		{
			Q(name, value);
			return _descriptor;
		}

		/// <summary>
		/// Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		public TDescriptor RequestConfiguration(Func<RequestConfigurationDescriptor, IRequestConfiguration> configurationSelector)
		{
			var rc = RequestState.RequestParameters.RequestConfiguration;
			RequestState.RequestParameters.RequestConfiguration = configurationSelector?.Invoke(new RequestConfigurationDescriptor(rc)) ?? rc;
			return _descriptor;
		}

		/// <summary>
		/// Hides the <see cref="Equals" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj) => base.Equals(obj);

		/// <summary>
		/// Hides the <see cref="GetHashCode" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode() => base.GetHashCode();

		/// <summary>
		/// Hides the <see cref="ToString" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString() => base.ToString();
	}
}
