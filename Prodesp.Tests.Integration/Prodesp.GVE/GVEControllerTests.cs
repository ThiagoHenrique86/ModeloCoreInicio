using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Prodesp.Tests.Integration.Prodesp.GVE;

public class GVEControllerTests : IntegrationTest
{
    public GVEControllerTests(ApiWebApplicationFactory fixture)
      : base(fixture) { }

    
    [Fact]
    public async Task GET_retrieves_GVE()
    {
        var response = await _client.GetAsync("/api/Test/consulta-todos-gve");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        
    }
}

