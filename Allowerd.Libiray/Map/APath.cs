using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Allowerd.Libiray.Map
{
    public class APath
    {
        public static Vector3 GetGround(Vector3 point)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(new Ray(new Vector3(point.x, 200, point.z), Vector3.down), out hitInfo, float.PositiveInfinity, BaseRust.Layer_Ground))
                return hitInfo.point;
            else
                return Vector3.zero;
        }

        public static Vector3 GetBuildingForGround(Vector3 pointGround)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(new Ray(new Vector3(pointGround.x, pointGround.y + 100, pointGround.z), Vector3.down), out hitInfo, float.PositiveInfinity, BaseRust.Layer_Building))
                return hitInfo.point;
            return Vector3.zero;
        }
    }
}
