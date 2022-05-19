using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using Xunit;

namespace Prodesp.Tests.Integration;

[Trait("Category", "Integration")]
    public abstract class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
{
    //private readonly Checkpoint _checkpoint = new Checkpoint
    //{
    //    SchemasToInclude = new[] {
    //    "Playground"
    //},
    //    WithReseed = true
    //};

    protected readonly ApiWebApplicationFactory _factory;
    protected readonly HttpClient _client;

    public IntegrationTest(ApiWebApplicationFactory fixture)
    {
        _factory = fixture;

        var clientOptions = new WebApplicationFactoryClientOptions();
        clientOptions.AllowAutoRedirect = true;
        clientOptions.BaseAddress = new Uri("http://localhost:93");
        clientOptions.HandleCookies = true;
        clientOptions.MaxAutomaticRedirections = 7;

        _client = _factory.CreateClient(clientOptions);

        //_client = _factory.CreateClient();

        // if needed, reset the DB
        //_checkpoint.Reset(_factory.Configuration.GetConnectionString("SQL")).Wait();
    }
}