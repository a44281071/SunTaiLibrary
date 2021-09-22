﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunTaiLibrary.Controls
{
    public enum DragDirection
    {
        TopLeft = 1,
        TopCenter = 2,
        TopRight = 4,
        MiddleLeft = 16,
        MiddleCenter = 32,
        MiddleRight = 64,
        BottomLeft = 256,
        BottomCenter = 512,
        BottomRight = 1024,
    }
}
