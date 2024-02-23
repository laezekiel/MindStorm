using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.isartdigital.Common.Elements.Player.Move
{
    public class PlayerClimb : MonoBehaviour
    {
        [SerializeField] private PlayerMoveAround _MoveManager;
        [SerializeField] private bool _CanClimb = false;

        void Update()
        {
            // Logic for ladder

            _MoveManager.CanGetMove[2] = _CanClimb;
        }
    }
}
