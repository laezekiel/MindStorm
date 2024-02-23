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
        [SerializeField] private InputManager _Axis;
        [SerializeField] private CameraPositionData _PositionData;
        [SerializeField] private GameObject _Target;
        [SerializeField] private float _RotationSpeed = 5f, _PosResetTick = 0.05f;

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


        private void OnDrawGizmos()
        {
            if (_PositionData != null && _Target != null)
            {
                StartCoroutine(MoveCameraAroundTargetToNewPosition(_PositionData, _Target));
            }
            else if (_PositionData != null)
            {
                SetCameraAroundTargetPosition(_PositionData, null);
            }
        }

        private void Start()
        {
            StartCoroutine(MoveCamera());
        }

        private void Update()
        {
            if (CanRotate)
            {
                Quaternion rotationY = Quaternion.AngleAxis(Input.GetAxis(_Axis.Inputs[0].InputValue) * _RotationSpeed, Vector3.up);

                _RotatedAngle += rotationY.eulerAngles.y;
                _RotatedAngle = (_RotatedAngle + 360) % 360;
            }
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
        public void SetCameraAroundTargetToNewPosition(CameraPositionData positionData, GameObject target = null, float pTime = 2)
        {
            if (_MoveToNewCoroutine != null) { StopCoroutine(_MoveToNewCoroutine); _MoveToNewCoroutine = null; }

            _MoveToNewCoroutine = StartCoroutine(MoveCameraAroundTargetToNewPosition(positionData, target, pTime));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionData"></param>
        /// <param name="target"></param>
        public void SetCameraAroundTargetPosition(CameraPositionData positionData, GameObject target = null)
        {
            if (target != null)
            {
                transform.position = target.transform.position + positionData.GetPosition();
                transform.LookAt(target.transform.position);
            }
            else 
            {
                transform.position = positionData.GetPosition();
                transform.LookAt(Vector3.zero); 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionData"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public IEnumerator MoveCameraAroundTargetToNewPosition( CameraPositionData positionData, GameObject target = null, float pTime = 2)
        {
            float move = 0; 

            while (move < pTime)
            {
                move += Time.deltaTime;

                transform.position = Vector3.Lerp(transform.position, target.transform.position + positionData.GetPosition(_RotatedAngle), move / pTime);

                if (target != null) transform.LookAt(target.transform.position);
                else transform.LookAt(Vector3.zero);

                yield return new WaitForSeconds(Time.deltaTime);
            }

            _IsMoving = false;
        }

        public IEnumerator MoveCamera()
        {
            float lTime = _PosResetTick;

            SetCameraAroundTargetToNewPosition(_PositionData, _Target, lTime);

            yield return new WaitForSeconds(lTime);

            StartCoroutine(MoveCamera());
        }
    }
}
