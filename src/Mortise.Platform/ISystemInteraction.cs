using System.Drawing;

namespace Mortise.Platform;

public interface ISystemInteraction
{
    /// <summary>
    ///     根据坐标获取窗口句柄
    /// </summary>
    /// <param name="point">Point</param>
    /// <returns>窗口句柄</returns>
    public IntPtr GetWindowHandle(Point point = default);
}