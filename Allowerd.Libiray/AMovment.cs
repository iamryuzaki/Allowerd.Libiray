using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Allowerd.Libiray
{
    public class AMovment
    {
        public static void SetRotation(BasePlayer player, Vector3 eulerAngles, bool networkUpdate = true)
        {
            BaseRust.BasePlayer_FiledInfo_viewAngles.SetValue(player, eulerAngles);
            if (networkUpdate)
                player.SendNetworkUpdate();
        }

        public static Quaternion GetRotation(Vector3 fromPosition, Vector3 targetPosition) => Quaternion.LookRotation(targetPosition - fromPosition);

        public static void SetPosition(BasePlayer player, Vector3 pos)
        {
            SetRotation(player, Quaternion.LookRotation(pos - player.transform.position).eulerAngles, false);
            player.MovePosition(pos);
        }

        public static Vector3 GetForward(Vector3 fromPosition, Vector3 targetPosition, float speedPerTick)
        {
            return (targetPosition - fromPosition) / (Vector3.Distance(targetPosition, fromPosition) / speedPerTick);
        }
    }
}
