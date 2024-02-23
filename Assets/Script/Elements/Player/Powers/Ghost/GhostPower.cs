using com.ironicentertainment.Common.Tools.Extensions;
using com.ironicentertainment.Elements.Player.Managers;
using com.ironicentertainment.Tools.Extensions;
using com.isartdigital.Common.Settings.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace com.ironicentertainment.Elements.Player.Powers.Ghost
{
    public class GhostPower : Power
    {
        [SerializeField,Range(1,5)] private float _Offset = 3;
        //[SerializeField] private LayerMask _LimitLayers;


        private Vector3 _PositionOffset;


        private float cos10 = Mathf.Cos(Mathf.Deg2Rad * 10);
        private float sin10 = Mathf.Sin(Mathf.Deg2Rad * 10);


        protected override void Update()
        {
            if (Checker.CanUsePower)
            {
                if (!PlayerMSManager.Instance.GhostIsUsed) base.Update();
                else HandleInput();
            }
        }

        protected override void PowerStartEffect()
        {
            if (Input.GetButtonDown(_PowersInput.Inputs[0].InputValue))
            {
                StartCoroutine(CheckPosition());
            }
        }

        private IEnumerator CheckPosition()
        {
            bool clearBefore, clearMiddle, clearAfter;
            clearBefore = clearMiddle = clearAfter = false;
               
            Vector3 position = transform.position;
            Vector3 directionBefore = Vector3.zero, directionMiddle, directionAfter = Vector3.zero;
            
            directionMiddle = transform.forward;

            while (!clearBefore || !clearMiddle || !clearAfter)
            {
                directionMiddle.Normalize();

                directionBefore = new Vector3(
                    cos10 * directionMiddle.x - sin10 * directionMiddle.z,
                    directionMiddle.y,
                    sin10 * directionMiddle.x + cos10 * directionMiddle.z
                ) * _Offset;

                directionAfter = new Vector3(
                    cos10 * directionMiddle.x + sin10 * directionMiddle.z,
                    directionMiddle.y,
                    -sin10 * directionMiddle.x + cos10 * directionMiddle.z
                ) * _Offset;

                directionMiddle *= _Offset;


                clearBefore = !new Ray(transform.position, directionBefore).CheckRayCastFirstHit();
                clearMiddle = !new Ray(transform.position, directionMiddle).CheckRayCastFirstHit();
                clearAfter = !new Ray(transform.position, directionAfter).CheckRayCastFirstHit();


                if(!clearBefore || !clearMiddle || !clearAfter) directionMiddle = directionAfter;
                

                yield return new WaitForSeconds(Time.deltaTime/36);
            }

            if (_Prefab != null)
            {
                Debug.Log(_PositionOffset);
                PlayerMSManager.Instance.GhostIsUsed = true;

                _Instance = Instantiate(_Prefab, directionMiddle + position, transform.rotation);

                Instance.transform.LookAt(transform.position);
            }
        }

        private void SwitchToGhost()
        {
            PlayerMSManager.Instance.Active = !PlayerMSManager.Instance.Active;

            GhostManager.Instance.Active = !GhostManager.Instance.Active;
        }

        private void KillGhost()
        {
            if(!PlayerMSManager.Instance.Active) SwitchToGhost();

            PlayerMSManager.Instance.GhostIsUsed = false;

            if (Instance != null) Destroy(Instance);
        }

        protected override void HandleInput()
        {
            if (Input.GetButton(_PowersInput.Inputs[0].InputValue))
            {
                _ElapsedTime += Time.deltaTime;
            }

            if (Input.GetButtonUp(_PowersInput.Inputs[0].InputValue))
            {
                if (_ElapsedTime < 1) SwitchToGhost();
                else KillGhost();
                _ElapsedTime = 0;
            }
        }
    }
}
