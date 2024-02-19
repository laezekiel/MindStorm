using com.ironicentertainment.Common.Elements.Cameras.Visibility;
using com.ironicentertainment.Common.Settings.Cameras;
using com.isartdigital.Common.Elements.Moves;
using com.isartdigital.Common.Settings.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.ironicentertainment.Common.Elements.Cameras.Positions
{
    [RequireComponent(typeof(GreyOutObstacle))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private InputManager _MouseAxis;
        [SerializeField] private CameraPositionData _PositionData;
        [SerializeField] private GameObject _Target;
        [SerializeField] private float _RotationSpeed = 5f;

        private Coroutine _MoveToNewCoroutine;

        private bool _CanRotate = true; public bool CanRotate { get { return _CanRotate; } }
        private bool _IsMoving = false; public bool IsMoving { get { return _IsMoving; } }

        private float _RotatedAngle = 0;

        public CameraPositionData PositionData 
        { 
            get { return _PositionData; } 
            set 
            {
                _IsMoving = true; 
                GetComponent<GreyOutObstacle>().Data = _PositionData = value; 
                SetCameraAroundTargetToNewPosition(_PositionData, _Target); 
            } 
        }
        private void Awake()
        {
            SetCameraAroundTargetPosition(_PositionData, _Target);
        }

        private void Update()
        {
            if (CanRotate)
            {
                Quaternion rotationY = Quaternion.AngleAxis(Input.GetAxis(_MouseAxis.Inputs[0].InputValue) * _RotationSpeed, Vector3.up);

                _RotatedAngle += rotationY.eulerAngles.y;
                _RotatedAngle = (_RotatedAngle + 360) % 360;
            }

            if (_IsMoving) return;

            transform.position = _Target.transform.position + _PositionData.GetPosition(_RotatedAngle);

            transform.LookAt(_Target.transform.position);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionData"></param>
        /// <param name="target"></param>
        public void SetCameraAroundTargetToNewPosition(CameraPositionData positionData, GameObject target = null)
        {
            if (_MoveToNewCoroutine != null) { StopCoroutine(_MoveToNewCoroutine); _MoveToNewCoroutine = null; }
            
            _MoveToNewCoroutine = StartCoroutine(MoveCameraAroundTargetToNewPosition( positionData, target));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionData"></param>
        /// <param name="target"></param>
        public void SetCameraAroundTargetPosition(CameraPositionData positionData, GameObject target = null)
        {
            transform.position = target.transform.position + positionData.GetPosition();

            if (target != null) transform.LookAt(target.transform.position);
            else transform.LookAt(Vector3.zero);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionData"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public IEnumerator MoveCameraAroundTargetToNewPosition( CameraPositionData positionData, GameObject target = null)
        {
            float move = 0; 

            while (move < 2)
            {
                move += Time.deltaTime;

                transform.position = Vector3.Lerp(transform.position, target.transform.position + positionData.GetPosition(_RotatedAngle), move/2);

                if (target != null) transform.LookAt(target.transform.position);
                else transform.LookAt(Vector3.zero);

                yield return new WaitForSeconds(Time.deltaTime);
            }

            _IsMoving = false;
        }
    }
}
