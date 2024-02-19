using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.isartdigital.Common.Elements.Rotations
{
    public class FollowLook : MonoBehaviour
    {
        [SerializeField] public Transform _Target = default;
        [SerializeField] private float _Speed;
        [SerializeField] private Vector3 _Axis = Vector3.up;

        private Quaternion _RotToTarget;
        private Vector3 _Position;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (!_Target) return;

            _Position = Vector3.ProjectOnPlane(_Target.position - transform.position, _Axis);

            _RotToTarget = Quaternion.LookRotation(_Position, _Axis);

            transform.rotation = Quaternion.Slerp(transform.rotation, _RotToTarget, Time.deltaTime * _Speed);
        }
    }
}
