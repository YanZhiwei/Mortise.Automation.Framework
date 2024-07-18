using System.Drawing;
using Mortise.Accessibility.Abstractions;
using Mortise.BrowserAccessibility;
using Mortise.Platform;
using Tenon.Mapper.Abstractions;

namespace Mortise.ChromiumAccessibility;

public class ChromiumAccessibleDetector : AccessibleDetector
{
    private readonly ChromiumAccessible _chromiumAccessible;
    private readonly BrowserDescriptor _descriptor;
    private readonly ISystemInteraction _systemInteraction;
    public readonly Dictionary<string, IChromiumAccessibleDetector> DetectorProviders;
    protected readonly IObjectMapper Mapper;

    public ChromiumAccessibleDetector(ChromiumAccessible chromiumAccessible, ISystemInteraction systemInteraction,
        IObjectMapper mapper,
        IEnumerable<IChromiumAccessibleDetector> detectorProviders, BrowserDescriptor descriptor)
    {
        _chromiumAccessible = chromiumAccessible;
        _systemInteraction = systemInteraction ?? throw new ArgumentNullException(nameof(systemInteraction));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _descriptor = descriptor;
        var chromiumAccessibleDetectors =
            detectorProviders as IChromiumAccessibleDetector[] ?? detectorProviders.ToArray();
        if (chromiumAccessibleDetectors.Any())
            DetectorProviders =
                chromiumAccessibleDetectors.ToDictionary(key => key.Descriptor.IdentityString, value => value);
    }

    public override AccessibleComponent? FromPoint(Point location)
    {
        var runningProcess = _systemInteraction.GetProcess(location);
        if (runningProcess == null) return null;
        var processName = runningProcess.ProcessName;

        var detectorKey =
            DetectorProviders?.Keys?.FirstOrDefault(c =>
                c.Contains(processName, StringComparison.OrdinalIgnoreCase));
        if (string.IsNullOrEmpty(detectorKey))
            return null;
        return DetectorProviders[detectorKey]?.FromHoveredElement(_chromiumAccessible, location, runningProcess);
    }

    public override async Task<AccessibleComponent?> FromPointAsync(Point location)
    {
        var runningProcess = _systemInteraction.GetProcess(location);
        if (runningProcess == null) return null;
        var processName = runningProcess.ProcessName;

        var detectorKey =
            DetectorProviders?.Keys?.FirstOrDefault(c =>
                c.Contains(processName, StringComparison.OrdinalIgnoreCase));
        if (string.IsNullOrEmpty(detectorKey))
            return null;
        return await DetectorProviders[detectorKey]
            ?.FromHoveredElementAsync(_chromiumAccessible, location, runningProcess);
    }

    public override AccessibleComponent? DtoAccessibleComponent(object element, Accessible? accessibility = null)
    {
        throw new NotImplementedException();
    }
}