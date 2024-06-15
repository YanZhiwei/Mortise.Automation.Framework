using System.Drawing;
using System.Text.Json.Serialization;

namespace Mortise.Accessibility.Abstractions;

public class AccessibleComponent
{
    public string Name { get; set; }

    public AccessibleControlType ControlType { get; set; }

    [JsonIgnore] public Rectangle BoundingRectangle { get; set; }

    [JsonIgnore] public bool IsOffscreen { get; set; }

    [JsonIgnore] public bool IsEnabled { get; set; }

    [JsonIgnore] public double ActualHeight { get; set; }

    [JsonIgnore] public double ActualWidth { get; set; }

    [JsonIgnore] public Accessible Accessible { get; set; }

    public bool IsDialog { get; set; }

    public string Id { get; set; }

    [JsonIgnore] public object NativeElement { get; set; }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, ControlType, IsDialog);
    }
}