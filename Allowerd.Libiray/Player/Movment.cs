using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Allowerd.Libiray.Player
{
    public class Movment
    {
        public static void SetRotation(BasePlayer player, Vector3 eulerAngles) => BaseRust.BasePlayer_FiledInfo_viewAngles.SetValue(player, eulerAngles);

        public static void SetPosition(BasePlayer player, Vector3 pos)
        {
            SetRotation(player, Quaternion.LookRotation(player.transform.position, pos).eulerAngles);
            player.MovePosition(pos);
        }
    }
}
