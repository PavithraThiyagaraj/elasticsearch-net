﻿using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatFielddata
{
	public class CatFielddataUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/fielddata")
					.Fluent(c => c.CatFielddata())
					.Request(c => c.CatFielddata(new CatFielddataRequest()))
					.FluentAsync(c => c.CatFielddataAsync())
					.RequestAsync(c => c.CatFielddataAsync(new CatFielddataRequest()))
				;

			var fields = new[] { "name", "startedOn" };

			await GET("/_cat/fielddata/name%2CstartedOn")
					.Fluent(c => c.CatFielddata(f => f.Fields<Project>(p => p.Name, p => p.StartedOn)))
					.Request(c => c.CatFielddata(new CatFielddataRequest(fields)))
					.FluentAsync(c => c.CatFielddataAsync(f => f.Fields<Project>(p => p.Name, p => p.StartedOn)))
					.RequestAsync(c => c.CatFielddataAsync(new CatFielddataRequest(fields)))
				;
		}
	}
}
