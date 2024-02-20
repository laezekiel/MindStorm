using com.ironicentertainment.Elements.Player.Managers;
using com.isartdigital.Common.Settings.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ironicentertainment.Elements.Player.Powers
{
    public class Power : MonoBehaviour
    {
        [SerializeField] protected InputManager _PowersInput;
        [SerializeField] protected GameObject _Prefab;

        protected GameObject _Instance = null; public GameObject Instance { get { return _Instance; } }


        protected float _ElapsedTime = 0;


        public PlayerManager Checker
        {
            get
            {
                if (PlayerMSManager.Instance.Active)
                {
                    return PlayerMSManager.Instance;
                }
                else
                {
                    return GhostManager.Instance;
                }
            }
        }


        // TODO: Create GameState Enum in a GameManger script and Add a [SerializeField] protected GameState _PowerMode here

        protected virtual void Update()
        {
            CastPower();
        }

        void CastPower()
        {
            if (Input.GetButtonDown(_PowersInput.Inputs[0].InputValue))
            {
                // TODO: Set GameManager mode to _PowerMode
                PowerStartEffect();
            }
        }

        protected virtual void PowerStartEffect()
        {

        }

        protected virtual void HandleInput()
        {

        }
    }
}
