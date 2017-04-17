using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Allowerd.Libiray
{
    public class BaseRust
    {
        public static FieldInfo BasePlayer_FiledInfo_viewAngles { get; } = typeof(BasePlayer).GetField("viewAngles", (BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic));

        public static int Layer_Ground { get; } = LayerMask.GetMask("World", "Default", "Terrain", "Craters");
        public static int Layer_Building { get; } = Rust.Layers.PlayerBuildings;
    }
}
