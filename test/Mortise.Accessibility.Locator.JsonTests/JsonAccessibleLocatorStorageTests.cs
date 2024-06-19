using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mortise.Accessibility.Abstractions;
using Mortise.Accessibility.Locator.Abstractions;
using Mortise.Accessibility.Locator.Json.Extensions;

namespace Mortise.Accessibility.Locator.JsonTests;

[TestClass]
public class JsonAccessibleLocatorStorageTests
{
    private readonly IServiceProvider _serviceProvider;

    public JsonAccessibleLocatorStorageTests()
    {
        _serviceProvider = new ServiceCollection()
            .AddLogging(loggingBuilder => loggingBuilder.SetMinimumLevel(LogLevel.Debug))
            .AddJsonLocatorStorage().BuildServiceProvider();
    }

    [TestMethod]
    public void AddTest()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var uiaAccessible = scope.ServiceProvider.GetService<IAccessibleLocatorStorage>();
          
        }
    }

    [TestMethod]
    public void RemoveTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void SaveTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void GetCountTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ContainsTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void SetTest()
    {
        Assert.Fail();
    }
}