using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mortise.Accessibility.Abstractions;
using Mortise.Accessibility.Locator.Abstractions;
using Mortise.Accessibility.Locator.Json.Extensions;
using Mortise.UiaAccessibility;
using Mortise.UiaAccessibility.Converters;
using Tenon.Serialization.Abstractions;

namespace Mortise.Accessibility.Locator.JsonTests;

[TestClass]
public class JsonAccessibleLocatorStorageTests
{
    private readonly IServiceProvider _serviceProvider;

    public JsonAccessibleLocatorStorageTests()
    {
        _serviceProvider = new ServiceCollection()
            .AddLogging(loggingBuilder => loggingBuilder.SetMinimumLevel(LogLevel.Debug))
            .AddJsonLocator(option => { option.UseLocalStorage(); },
                [new UiaAccessibleConverter(), new UiaAccessibleComponentConverter()])
            .BuildServiceProvider();
    }

    [TestMethod]
    public void AddTest()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var locatorJsonString1 = @"{
  ""uniqueId"": ""num1Button"",
  ""fileName"": ""CalculatorApp"",
  ""provider"": ""Uia"",
  ""platform"": ""Win32NT"",
  ""version"": ""3.0.0"",
  ""components"": [
    {
      ""className"": ""ApplicationFrameWindow"",
      ""name"": ""\u8BA1\u7B97\u5668"",
      ""controlType"": ""Window"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": ""Windows.UI.Core.CoreWindow"",
      ""name"": ""\u8BA1\u7B97\u5668"",
      ""controlType"": ""Window"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": null,
      ""name"": null,
      ""controlType"": ""Custom"",
      ""isDialog"": false,
      ""id"": ""NavView"",
      ""isPassword"": false
    },
    {
      ""className"": ""LandmarkTarget"",
      ""name"": null,
      ""controlType"": ""Group"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": ""NamedContainerAutomationPeer"",
      ""name"": ""\u6570\u5B57\u952E\u76D8"",
      ""controlType"": ""Group"",
      ""isDialog"": false,
      ""id"": ""NumberPad"",
      ""isPassword"": false
    },
    {
      ""className"": ""Button"",
      ""name"": ""\u4E00"",
      ""controlType"": ""Button"",
      ""isDialog"": false,
      ""id"": ""num1Button"",
      ""isPassword"": false
    }
  ]
}";

            var locatorJsonString2 = @"{
  ""uniqueId"": ""num2Button"",
  ""fileName"": ""CalculatorApp"",
  ""provider"": ""Uia"",
  ""platform"": ""Win32NT"",
  ""version"": ""3.0.0"",
  ""components"": [
    {
      ""className"": ""ApplicationFrameWindow"",
      ""name"": ""\u8BA1\u7B97\u5668"",
      ""controlType"": ""Window"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": ""Windows.UI.Core.CoreWindow"",
      ""name"": ""\u8BA1\u7B97\u5668"",
      ""controlType"": ""Window"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": null,
      ""name"": null,
      ""controlType"": ""Custom"",
      ""isDialog"": false,
      ""id"": ""NavView"",
      ""isPassword"": false
    },
    {
      ""className"": ""LandmarkTarget"",
      ""name"": null,
      ""controlType"": ""Group"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": ""NamedContainerAutomationPeer"",
      ""name"": ""\u6570\u5B57\u952E\u76D8"",
      ""controlType"": ""Group"",
      ""isDialog"": false,
      ""id"": ""NumberPad"",
      ""isPassword"": false
    },
    {
      ""className"": ""Button"",
      ""name"": ""\u4E00"",
      ""controlType"": ""Button"",
      ""isDialog"": false,
      ""id"": ""num2Button"",
      ""isPassword"": false
    }
  ]
}";
            var locatorStorage = scope.ServiceProvider.GetRequiredService<IAccessibleLocatorStorage>();
            var serializer = scope.ServiceProvider.GetRequiredService<ISerializer>();
            var accessible1 = serializer.DeserializeObject<Accessible>(locatorJsonString1);
            var accessible2 = serializer.DeserializeObject<Accessible>(locatorJsonString2);
            Assert.IsTrue(locatorStorage.Add(accessible1));
            Assert.IsTrue(locatorStorage.Add(accessible2));
            Assert.IsFalse(locatorStorage.Add(accessible2));
        }
    }

    [TestMethod]
    public void SaveTest()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var locatorJsonString1 = @"{
  ""uniqueId"": ""num1Button"",
  ""fileName"": ""CalculatorApp"",
  ""provider"": ""Uia"",
  ""platform"": ""Win32NT"",
  ""version"": ""3.0.0"",
  ""components"": [
    {
      ""className"": ""ApplicationFrameWindow"",
      ""name"": ""\u8BA1\u7B97\u5668"",
      ""controlType"": ""Window"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": ""Windows.UI.Core.CoreWindow"",
      ""name"": ""\u8BA1\u7B97\u5668"",
      ""controlType"": ""Window"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": null,
      ""name"": null,
      ""controlType"": ""Custom"",
      ""isDialog"": false,
      ""id"": ""NavView"",
      ""isPassword"": false
    },
    {
      ""className"": ""LandmarkTarget"",
      ""name"": null,
      ""controlType"": ""Group"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": ""NamedContainerAutomationPeer"",
      ""name"": ""\u6570\u5B57\u952E\u76D8"",
      ""controlType"": ""Group"",
      ""isDialog"": false,
      ""id"": ""NumberPad"",
      ""isPassword"": false
    },
    {
      ""className"": ""Button"",
      ""name"": ""\u4E00"",
      ""controlType"": ""Button"",
      ""isDialog"": false,
      ""id"": ""num1Button"",
      ""isPassword"": false
    }
  ]
}";

            var locatorJsonString2 = @"{
  ""uniqueId"": ""num2Button"",
  ""fileName"": ""CalculatorApp"",
  ""provider"": ""Uia"",
  ""platform"": ""Win32NT"",
  ""version"": ""3.0.0"",
  ""components"": [
    {
      ""className"": ""ApplicationFrameWindow"",
      ""name"": ""\u8BA1\u7B97\u5668"",
      ""controlType"": ""Window"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": ""Windows.UI.Core.CoreWindow"",
      ""name"": ""\u8BA1\u7B97\u5668"",
      ""controlType"": ""Window"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": null,
      ""name"": null,
      ""controlType"": ""Custom"",
      ""isDialog"": false,
      ""id"": ""NavView"",
      ""isPassword"": false
    },
    {
      ""className"": ""LandmarkTarget"",
      ""name"": null,
      ""controlType"": ""Group"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": ""NamedContainerAutomationPeer"",
      ""name"": ""\u6570\u5B57\u952E\u76D8"",
      ""controlType"": ""Group"",
      ""isDialog"": false,
      ""id"": ""NumberPad"",
      ""isPassword"": false
    },
    {
      ""className"": ""Button"",
      ""name"": ""\u4E00"",
      ""controlType"": ""Button"",
      ""isDialog"": false,
      ""id"": ""num2Button"",
      ""isPassword"": false
    }
  ]
}";
            var locatorJsonString3 = @"{
  ""uniqueId"": ""num1Button"",
  ""fileName"": ""Calculator"",
  ""provider"": ""Uia"",
  ""platform"": ""Win32NT"",
  ""version"": ""3.0.0"",
  ""components"": [
    {
      ""className"": ""ApplicationFrameWindow"",
      ""name"": ""\u8BA1\u7B97\u5668"",
      ""controlType"": ""Window"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": ""Windows.UI.Core.CoreWindow"",
      ""name"": ""\u8BA1\u7B97\u5668"",
      ""controlType"": ""Window"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": null,
      ""name"": null,
      ""controlType"": ""Custom"",
      ""isDialog"": false,
      ""id"": ""NavView"",
      ""isPassword"": false
    },
    {
      ""className"": ""LandmarkTarget"",
      ""name"": null,
      ""controlType"": ""Group"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": ""NamedContainerAutomationPeer"",
      ""name"": ""\u6570\u5B57\u952E\u76D8"",
      ""controlType"": ""Group"",
      ""isDialog"": false,
      ""id"": ""NumberPad"",
      ""isPassword"": false
    },
    {
      ""className"": ""Button"",
      ""name"": ""\u4E00"",
      ""controlType"": ""Button"",
      ""isDialog"": false,
      ""id"": ""num1Button"",
      ""isPassword"": false
    }
  ]
}";

            var locatorJsonString4 = @"{
  ""uniqueId"": ""num2Button"",
  ""fileName"": ""Calculator"",
  ""provider"": ""Uia"",
  ""platform"": ""Win32NT"",
  ""version"": ""3.0.0"",
  ""components"": [
    {
      ""className"": ""ApplicationFrameWindow"",
      ""name"": ""\u8BA1\u7B97\u5668"",
      ""controlType"": ""Window"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": ""Windows.UI.Core.CoreWindow"",
      ""name"": ""\u8BA1\u7B97\u5668"",
      ""controlType"": ""Window"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": null,
      ""name"": null,
      ""controlType"": ""Custom"",
      ""isDialog"": false,
      ""id"": ""NavView"",
      ""isPassword"": false
    },
    {
      ""className"": ""LandmarkTarget"",
      ""name"": null,
      ""controlType"": ""Group"",
      ""isDialog"": false,
      ""id"": null,
      ""isPassword"": false
    },
    {
      ""className"": ""NamedContainerAutomationPeer"",
      ""name"": ""\u6570\u5B57\u952E\u76D8"",
      ""controlType"": ""Group"",
      ""isDialog"": false,
      ""id"": ""NumberPad"",
      ""isPassword"": false
    },
    {
      ""className"": ""Button"",
      ""name"": ""\u4E00"",
      ""controlType"": ""Button"",
      ""isDialog"": false,
      ""id"": ""num2Button"",
      ""isPassword"": false
    }
  ]
}";

            var locatorStorage = scope.ServiceProvider.GetRequiredService<IAccessibleLocatorStorage>();
            var serializer = scope.ServiceProvider.GetRequiredService<ISerializer>();
            var accessible1 = serializer.DeserializeObject<UiaAccessible>(locatorJsonString1);
            var accessible2 = serializer.DeserializeObject<UiaAccessible>(locatorJsonString2);
            var accessible3 = serializer.DeserializeObject<UiaAccessible>(locatorJsonString3);
            var accessible4 = serializer.DeserializeObject<UiaAccessible>(locatorJsonString4);
            Assert.IsTrue(locatorStorage.Add(accessible1));
            Assert.IsTrue(locatorStorage.Add(accessible2));
            Assert.IsTrue(locatorStorage.Add(accessible3));
            Assert.IsTrue(locatorStorage.Add(accessible4));
            locatorStorage.Save();
        }
    }


    [TestMethod]
    public void LoadTest()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var locatorStorage = scope.ServiceProvider.GetRequiredService<IAccessibleLocatorStorage>();
            var actual = locatorStorage.Load();
            Assert.IsNotNull(actual);
        }
    }
}