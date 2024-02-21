using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.isartdigital.Common.Elements.Moves.Targets
{
    [RequireComponent(typeof(MoveForwards))]
    [RequireComponent(typeof(MoveToward))]
    [RequireComponent(typeof(MoveTowardInCurve))]
    public class TargetPathManager : MonoBehaviour
    {
        public enum  Mode { Unique = 1, Repeated = 2 }

        [SerializeField] private Mode _Mode = Mode.Repeated;
        [SerializeField] private List<Transform> _PathList;
        [SerializeField] private List<MoveToward> _Movers = new List<MoveToward>();
        [SerializeField] private bool _PauseBtwn = false;
        [SerializeField] private float _TimeLimit = 1f;

        private Transform _CurrentTarget;
        private MoveToward _CurrentMover;
        private float _Timer = 0f;

        // Start is called before the first frame update
        void Start()
        {
            _CurrentMover = _Movers[0];
            _CurrentMover.IsOn = true;
            _CurrentTarget = _CurrentMover.Target = _PathList[0];
        }

        // Update is called once per frame
        void Update()
        {
            if ((transform.position.x >= _CurrentTarget.position.x - 0.5f && transform.position.y >= _CurrentTarget.position.y - 0.5f
                && transform.position.z >= _CurrentTarget.position.z - 0.5f && transform.position.x <= _CurrentTarget.position.x + 0.5f
                && transform.position.y <= _CurrentTarget.position.y + 0.5f && transform.position.z <= _CurrentTarget.position.z + 0.5f)

                && _PathList.Count > 1

                && _CurrentMover.IsOn)
            {
                _CurrentMover.IsOn = false;

                if (_Movers.IndexOf(_CurrentMover) < _Movers.Count - 1) _CurrentMover = _Movers[_Movers.IndexOf(_CurrentMover) + 1];
                else if (_Mode != Mode.Unique) _CurrentMover = _Movers[0];
                
                if (_PathList.IndexOf(_CurrentTarget) < _PathList.Count - 1)
                {
                    _CurrentTarget = _CurrentMover.Target = _PathList[_PathList.IndexOf(_CurrentTarget) + 1];
                }
                else if (_Mode != Mode.Unique)
                {
                    _CurrentTarget = _CurrentMover.Target = _PathList[0];
                }
            }

            if (!_CurrentMover.IsOn && _PauseBtwn)
            {
                _Timer += Time.deltaTime;
                if (_Timer >= _TimeLimit) { _Timer = 0; _CurrentMover.IsOn = true; }
            }
            else _CurrentMover.IsOn = true;
        }
    }
}
