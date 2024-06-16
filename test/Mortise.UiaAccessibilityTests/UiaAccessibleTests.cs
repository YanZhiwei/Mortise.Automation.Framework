using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mortise.Accessibility.Abstractions;
using Mortise.UiaAccessibility;
using Mortise.UiaAccessibility.Extensions;
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
}