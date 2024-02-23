using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.ironicentertainment.Common.Tools.Extensions
{
    public static class RayExtensions
    {
        public static bool CheckRayCastFirstHit(this Ray ray, LayerMask layers)
        {
            return Physics.Raycast(ray, ray.direction.magnitude, layers);
        }

        public static bool CheckRayCastFirstHit(this Ray ray)
        {
            return Physics.Raycast(ray, ray.direction.magnitude);
        }
    }
}
