using com.ironicentertainment.Common.Elements.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ironicentertainment.Common.Elements.Lights.Fires
{
    public class SaveProgress : MonoBehaviour
    {
        private FireController _Fire;

        public FireController Fire { get { if (_Fire == null) _Fire = GetComponent<FireController>(); return _Fire; } }

        private void OnTriggerEnter(Collider other)
        {
            if (FiresManager.Instance.CurrentFire == this) return;

            StartCoroutine(Fire.CallTurnOn());

            FiresManager.Instance.CurrentFire = this;
        }
    }
}