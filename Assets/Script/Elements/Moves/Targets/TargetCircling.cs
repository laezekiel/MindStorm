using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.isartdigital.Common.Elements.Moves.Targets
{
    public class TargetCircling : Move
    {
        [SerializeField] private Transform _Target;
        [SerializeField] private Vector3 _Axis = Vector3.up;
        [SerializeField] private float _Distance = 5f;

        // Start is called before the first frame update
        void Start()
        {
            _Axis = _Axis.normalized;
        }

        // Update is called once per frame
        void Update()
        {
            transform.RotateAround(_Target.position, _Axis, m_Speed * Time.deltaTime);

            Vector3 lPositionToTarget = transform.position - _Target.position;
            lPositionToTarget = lPositionToTarget.normalized * _Distance;
            transform.position = _Target.position + lPositionToTarget;
        }
    }
}
