using com.ironicentertainment.Common.Elements.Cameras.Positions;
using com.ironicentertainment.Common.Settings.Cameras;
using com.ironicentertainment.Elements.Player.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ironicentertainment
{
    public class TestChangeCameraPosition : MonoBehaviour
    {
        [SerializeField] private CameraPositionData _positionData;
        [SerializeField] private CameraPositionData _DefaultPosition;

        private void OnTriggerEnter(Collider other)
        {
            if (PlayerMSManager.Instance.Active)
            {
                PlayerMSManager.Instance.CamPositioner.PositionData = _positionData;
            }
            else GhostManager.Instance.CamPositioner.PositionData = _positionData;
        }

        private void OnTriggerExit(Collider other)
        {
            if (PlayerMSManager.Instance.Active)
            {
                PlayerMSManager.Instance.CamPositioner.PositionData = _DefaultPosition;
            }
            else GhostManager.Instance.CamPositioner.PositionData = _DefaultPosition;
        }
    }
}
