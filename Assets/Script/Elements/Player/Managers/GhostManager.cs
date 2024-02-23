using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.ironicentertainment.Elements.Player.Managers
{
    public class GhostManager : PlayerManager
    {
        private static GhostManager instance;
        public static GhostManager Instance { get { if (instance == null) instance = new GhostManager(); return instance; } }


        private GameObject _Player;
        public GameObject Player { get { return _Player; } set { _Player = value; } }


        void Awake()
        {
            if (instance == null) instance = this;
            else
            {
                Debug.Log($"An instance of this element : {nameof(GhostManager)} already exist.");
                Destroy(this);
            }
            Active = false;
        }
    }
}
