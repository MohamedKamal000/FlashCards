using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using Application.Dtos.UserDtos;
using IntegrationTests.TestCases;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests;

public class AuthEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    
    public AuthEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory,ClassData(typeof(RegisterTestCases))]
    public async Task GivenValidRegisterInput_WhenRegistering_ReturnOk(RegisterUserDto dto)
    {
        
        // Arrange
        var client = _factory.CreateClient();
        string url = "/api/Auth/RegisterUser";

        var serialized =  JsonSerializer.Serialize(dto);
        HttpContent content = new StringContent(serialized,null,"application/json");
        
        // Act
        var response = await client.PostAsync(url, content, TestContext.Current.CancellationToken);

        //Assert
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        
        
        
        if (response.StatusCode != HttpStatusCode.OK)
        {
            string body = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
            string error = $"""
                            EndPoint: {url}
                            StatusCode: {response.StatusCode}
                            Input: {dto}
                            Body: {body}
                            """;
            await File.WriteAllTextAsync("D:\\RiderSolutions\\FlashCards\\IntegrationTests\\LastTestFailDetails.txt", 
                error,
                TestContext.Current.CancellationToken);            
        }
        else
        {
            string body = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
            var result = JsonNode.Parse(body);
            await File.WriteAllTextAsync("D:\\RiderSolutions\\FlashCards\\IntegrationTests\\LastTestFailDetails.txt", 
                result["data"]["accessToken"].AsValue().ToString(),
                TestContext.Current.CancellationToken);            
            
        }
    }

}