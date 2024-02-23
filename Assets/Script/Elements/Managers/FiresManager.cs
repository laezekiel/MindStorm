using com.ironicentertainment.Common.Elements.Lights.Fires;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.ironicentertainment.Common.Elements.Manager
{
    public class FiresManager : MonoBehaviour
    {
        private static FiresManager _Instance;
        public static FiresManager Instance { get { return _Instance; } }


        private SaveProgress _CurrentFire;

        public SaveProgress CurrentFire { get => _CurrentFire; set { if (_CurrentFire != null) StartCoroutine(_CurrentFire.Fire.CallTurnOff()); _CurrentFire = value; } }


        private void Awake()
        {
            if(_Instance == null) _Instance = this;
            else Destroy(gameObject);
        }

    }
}