using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace com.ironicentertainment.Common.Elements.Lights.Fires
{
    public class FireController : LightControler
    {
        private static List<FireController> _AllInstances;
        public static List<FireController> AllInstances { get { return _AllInstances; } }

        protected override void Awake()
        {
            base.Awake();

            if (AllInstances == null)
            {
                _AllInstances = new List<FireController>();
                AllInstances.Add(this);
            }
            else if (!AllInstances.Contains(this)) AllInstances.Add(this);

        }
    }
}