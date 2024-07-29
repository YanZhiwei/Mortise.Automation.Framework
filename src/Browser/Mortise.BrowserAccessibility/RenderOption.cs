using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortise.BrowserAccessibility
{
    public sealed class RenderOption
    {
        public int WindowLeft { get; set; }

        public int WindowTop { get; set; }

        public int WindowWidth { get; set; }

        public int WindowHeight { get; set; }

        public int Dpi { get; set; }

        public int PageRenderWidth { get; set; }

        public int PageRenderHeight { get; set; }

        public int PageRenderX { get; set; }

        public int PageRenderY { get; set; }
    }
}
