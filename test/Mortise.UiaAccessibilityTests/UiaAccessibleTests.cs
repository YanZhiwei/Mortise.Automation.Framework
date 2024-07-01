using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mortise.Accessibility.Abstractions;
using Mortise.UiaAccessibility;
using Mortise.UiaAccessibility.Extensions;
using Mortise.UiaAccessibility.Options;
using Mortise.UiaAccessibility.WeChat.Configurations;

namespace Mortise.UiaAccessibilityTests;

[TestClass]
public class UiaAccessibleTests
{
    private readonly IServiceProvider _serviceProvider;

    public UiaAccessibleTests()
    {
        _serviceProvider = new ServiceCollection()
            .AddLogging(loggingBuilder => loggingBuilder.SetMinimumLevel(LogLevel.Debug))
            .AddUiaAccessible(option => { option.AddWeChatAccessible(); }).BuildServiceProvider();
    }

    [TestMethod]
    public void UiaAccessibleTest()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var uiaAccessible = scope.ServiceProvider.GetService<Accessible>();
            Assert.IsNotNull(uiaAccessible);
            Assert.IsNotNull(uiaAccessible.Identity);
            var nativeIdentity = uiaAccessible.Identity as UiaAccessibleIdentity;
            Assert.IsNotNull(nativeIdentity);
            Assert.IsNotNull(nativeIdentity.Applications);
        }
    }

    [TestMethod]
    public void RecordTest()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var accessible = scope.ServiceProvider.GetService<Accessible>();
            if (accessible.Identity is not UiaAccessibleIdentity uiaAccessibleIdentity)
            {
                Assert.Fail("uiaAccessibleIdentity is null");
                return;
            }

            var buttonTest = uiaAccessibleIdentity.DesktopElement.FindFirstDescendant(cf => cf.ByName("一"));
            Assert.IsNotNull(buttonTest);
            accessible.Record(buttonTest);
            var actual = accessible.UniqueId;
            Assert.IsNotNull(actual);
            Assert.AreEqual("CalculatorApp|num1Button", actual);
        }
    }

    [TestMethod]
    public async Task LaunchAsyncTest()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var uiaAccessible = scope.ServiceProvider.GetService<Accessible>();
            Assert.IsNotNull(uiaAccessible);
            await uiaAccessible.LaunchAsync(new UiaLaunchOptions());
        }
    }
}