using com.isartdigital.Common.Settings.Lights;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace com.ironicentertainment.Common.Elements.Lights
{
    public class LightControler : MonoBehaviour
    {
        [SerializeField]
        protected List<string> _ShaderParameters = new List<string>()
        {
            "_EmissionStrength",
        };

        [SerializeField] protected VisualEffect _VFX;
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

            if (IsOn) _VFX.Play();
            else _VFX.Stop();
        }


        public virtual IEnumerator CallTurnOn() 
        {
            float lWaitMax = _Data.On.Clip.length,
                    lWait = 0;

            float lOrigin = 0,
                    lEnd = 1;

            bool lActive = false;

            while (lWait < lWaitMax)
            {
                lWait += Time.deltaTime;

                if (lWait > lWaitMax / 4 && !lActive) { lActive = true; _VFX.Play(); }

                Source.material.SetFloat(_ShaderParameters[0], LerpPartial(lOrigin, lEnd, lWait));

                yield return new WaitForSeconds(Time.deltaTime);
            }

            Source.material.SetFloat(_ShaderParameters[0], LerpPartial(lOrigin, lEnd, 1));
        }

        public virtual IEnumerator CallTurnOff() 
        {
            float lWaitMax = _Data.On.Clip.length,
                    lWait = 0;

            float lOrigin = 1,
                    lEnd = 0;

            bool lActive = true;

            while (lWait < lWaitMax)
            {
                lWait += Time.deltaTime;

                if (lWait > lWaitMax / 4 && lActive) { lActive = false; _VFX.Stop(); }

                Source.material.SetFloat(_ShaderParameters[0], LerpPartial(lOrigin, lEnd, lWait));

                yield return new WaitForSeconds(Time.deltaTime);
            }

            Source.material.SetFloat(_ShaderParameters[0], LerpPartial(lOrigin, lEnd, 1));
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