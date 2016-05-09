using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ECD_Engine
{
    public enum Difficulty
    {
        Easy = 100,
        Medium = 60,
        Hard = 30
    }
    public static class GlobalSettings
    {
        public static Difficulty Difficulty { get; set; } = Difficulty.Medium;
        public static int Acceleration { get; set; }
        public static int MaxX { get; set; }
        public static int MaxY { get; set; }
        public static int MinX { get; set; }
        public static int MinY { get; set; }
    }
}
