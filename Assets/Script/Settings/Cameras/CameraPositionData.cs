using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ironicentertainment.Common.Settings.Cameras
{
    [CreateAssetMenu(menuName = "Settings/Camera Position", fileName = "DefaultCamera", order = 0)]
    public class CameraPositionData : ScriptableObject
    {
        [SerializeField] private float _Length = 0, _Height = 0;
        [SerializeField] private float _Angle;

        public float Length { get { return _Length; } }
        public float Height { get { return _Height; } }
        public float Base {  get { return Mathf.Sqrt((_Length * _Length) - (_Height * _Height)); } }

        public float Angle { get { return _Angle; } }

        public Vector3 GetPosition(float angle = 0)
        {
            float totalAngle = _Angle + angle;
            float angleInRadians = Mathf.Deg2Rad * totalAngle;

            float x = Base * Mathf.Cos(angleInRadians);
            float z = Base * Mathf.Sin(angleInRadians);

            return new Vector3(x, _Height, z);            
        }
    }
}
