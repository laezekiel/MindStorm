using com.ironicentertainment.Elements.Player.Powers.Ghost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.ironicentertainment.Elements.Player.Managers
{
    public class PlayerMSManager : PlayerManager
    {
        private static PlayerMSManager instance;
        public static PlayerMSManager Instance { get { if (instance == null) instance = new PlayerMSManager(); return instance; } }


        private bool _GhostIsUsed = false;
        public bool GhostIsUsed {  get { return _GhostIsUsed; } set { _GhostIsUsed = value; } }


        private GhostPower _GhostContainer;
        public  GhostPower GhostContainer { get { if (_GhostContainer == null) _GhostContainer = Character.GetComponent<GhostPower>(); return _GhostContainer; } }

        public GameObject Ghost { get { return GhostContainer.Instance;} }


        void Awake()
        {
            if (instance == null) instance = this;
            else
            {
                Debug.Log($"An instance of this element : {nameof(PlayerMSManager)} already exist.");
                Destroy(this);
            }
            Active = true;
        }
    }
}
