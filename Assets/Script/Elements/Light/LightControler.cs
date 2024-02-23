using com.isartdigital.Common.Settings.Lights;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ironicentertainment.Common.Elements.Lights
{
    public class LightControler : MonoBehaviour
    {
        [SerializeField] protected LightData _Data;

        [SerializeField] protected Renderer _Source;

        private EventHandler _TurnOn, _TurnOff;


        public Renderer Source { get => _Source; }
        public EventHandler TurnOn { get => _TurnOn; set => _TurnOn = value; }
        public EventHandler TurnOff { get => _TurnOff; set => _TurnOff = value; }

        public bool IsOn 
        { 
            get => _Data.IsOn; 
            set { _Data.IsOn = value; 
                if (value) TurnOn?.Invoke(this, new EventArgs()); 
                else TurnOff?.Invoke(this, new EventArgs()); } 
        }


        protected virtual void Awake()
        {
            TurnOn += (sender, e) => StartCoroutine(CallTurnOn());
            TurnOff += (sender, e) => StartCoroutine(CallTurnOff());
        }


        public virtual IEnumerator CallTurnOn() 
        {
            float   lWaitMax = _Data.On.Clip.length,
                    lTime = lWaitMax / 10,
                    lWait = 0;

            while (lWait < lWaitMax) 
            {
                lWait += lTime;

                yield return new WaitForSeconds(lTime);
            }
        }

        public virtual IEnumerator CallTurnOff() 
        {
            float   lWaitMax = _Data.Off.Clip.length,
                    lTime = lWaitMax / 10,
                    lWait = 0;

            while (lWait < lWaitMax) 
            {
                lWait += lTime;

                yield return new WaitForSeconds(lTime);
            }
        }

        protected T LerpPartial<T>(T pOrigin, T pEnd, float pTime) where T : struct
        {
            Type targetType = typeof(T);

            switch (targetType.Name)
            {
                case "Single":
                    return (T)(object)Mathf.Lerp((float)(object)pOrigin, (float)(object)pEnd, pTime);
                case "Quaternion":
                    return (T)(object)Quaternion.Slerp((Quaternion)(object)pOrigin, (Quaternion)(object)pEnd, pTime);
                case "Vector2":
                    return (T)(object)Vector2.Lerp((Vector2)(object)pOrigin, (Vector2)(object)pEnd, pTime);
                case "Vector3":
                    return (T)(object)Vector3.Lerp((Vector3)(object)pOrigin, (Vector3)(object)pEnd, pTime);
                default:
                    return default;
            }
        }

    }
}