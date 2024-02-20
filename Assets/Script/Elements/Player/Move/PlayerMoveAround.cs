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

        [SerializeField] private bool[] _CanMove = { true, true, true }; public bool[] CanMove { get { return _CanMove; } }

        [SerializeField] private Animator _BodyAnimator;

        [SerializeField] private Transform _PlayerCamera;

        private Vector3 _Inputs, _CamForward, _CamRight, _Direction, _NewForward;

        private Quaternion _Rotation;

        void Update()
        {
            MoveForward();
        }

        void MoveForward()
        {
            _Inputs = Vector3.zero;

            if (_CanMove[0]) for (int i = 0; i < _Mover.Length; i++) if (_CanMove[i + 1]) { _Inputs += _Mover[i].GetAxisStrength(); }

            if (_Inputs != Vector3.zero) _Inputs /= _Mover.Length;

            _CamForward = _PlayerCamera.forward;
            _CamRight = _PlayerCamera.right;

            _CamForward.y = 0;

            _Direction = (_CamForward * _Inputs.z + _CamRight * _Inputs.x).normalized;
            _Rotation = transform.rotation;


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

            _BodyAnimator.UpdateAnimatorValueHV(0, Mathf.Clamp01(Mathf.Abs(_NewForward.x) + Mathf.Abs(_NewForward.z)));

            transform.position = _BodyAnimator.transform.position;
            _BodyAnimator.transform.position = transform.position;

        }
    }
}
