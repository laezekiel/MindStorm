using com.ironicentertainment.Tools.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.isartdigital.Common.Elements.Player.Move
{
    [RequireComponent(typeof(PlayerMove))]
    public class PlayerMoveAround : MonoBehaviour
    {
        [SerializeField] private PlayerMove[] _Mover = new PlayerMove[2];

        [SerializeField,Range(0.75f,1.25f)] private float _Speed = 0.75f;
        [SerializeField,Range(1f,1.5f)] private float _SpeedMax = 1.25f;
        [SerializeField,Range(0.5f,1f)] private float _SpeedMin = 0.75f;

        [SerializeField] private bool[] _CanGetMove = { true, true, true }; public bool[] CanGetMove { get { return _CanGetMove; } }

        [SerializeField] private Animator _BodyAnimator;

        [SerializeField] private Transform _PlayerCamera;

        private bool _CanMove = true;

        public bool CanMove { get => _CanMove; set => _CanMove = value; }

        private Vector3 _Inputs, _CamForward, _CamRight, _Direction, _NewForward;

        private Quaternion _Rotation;

        void Update()
        {
            MoveForward();
        }

        void MoveForward()
        {
            _Inputs = Vector3.zero;

            if (!_CanMove) return;
            
            for (int i = 0; i < _Mover.Length; i++) if (_CanGetMove[i]) { _Inputs += _Mover[i].GetAxisStrength(); }

            _CamForward = _PlayerCamera.forward;
            _CamRight = _PlayerCamera.right;

            _CamForward.y = _CamRight.y = 0;

            _CamRight.Normalize();
            _CamForward.Normalize();

            _Direction = (_CamForward * _Inputs.z + _CamRight * _Inputs.x).normalized;


            if (_Direction != Vector3.zero)
            {
                _Rotation = Quaternion.LookRotation(_Direction);

                if (_Speed < _SpeedMax) _Speed += 0.1f * Time.deltaTime;
                else if (_Speed > _SpeedMax) _Speed = _SpeedMax;
            }
            else
            {
                if (_Speed > _SpeedMin) _Speed -= 0.5f * Time.deltaTime;
                else if (_Speed < _SpeedMin) _Speed = _SpeedMin;
            }

            transform.rotation = _Rotation;

            _NewForward = transform.forward;

            _NewForward = new Vector3(_Inputs.x * _NewForward.x, 0, _Inputs.z * _NewForward.z);

            _NewForward.Normalize();


            _BodyAnimator.SetFloat("Speed", _Speed);
            float lTemp = Mathf.Clamp01(Mathf.Abs(_NewForward.x) + Mathf.Abs(_NewForward.z));
            if (lTemp < .05f) lTemp = 0;
            _BodyAnimator.UpdateAnimatorValueHV(0, lTemp);

            transform.position = _BodyAnimator.transform.position;
            _BodyAnimator.transform.position = transform.position;

        }
    }
}
