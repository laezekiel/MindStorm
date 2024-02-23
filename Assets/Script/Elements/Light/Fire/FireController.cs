using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace com.ironicentertainment.Common.Elements.Lights.Fires
{
    public class FireController : LightControler
    {
        private static List<FireController> _AllInstances;
        public static List<FireController> AllInstances { get { return _AllInstances; } }


        [SerializeField]
        protected List<string> _ShaderParameters = new List<string>()
        {
            "_EmissionStrength",
        };

        [SerializeField] protected VisualEffect _VFX;

        protected override void Awake()
        {
            base.Awake();

            if (AllInstances == null)
            {
                _AllInstances = new List<FireController>();
                AllInstances.Add(this);
            }
            else if (!AllInstances.Contains(this)) AllInstances.Add(this);

            if (IsOn) _VFX.Play();
            else _VFX.Stop();
        }

        public override IEnumerator CallTurnOn()
        {
            float   lWaitMax = _Data.On.Clip.length,
                    lWait = 0;

            float   lOrigin = 0,
                    lEnd = 1;

            bool    lActive = false;

            while (lWait < lWaitMax)
            {
                lWait += Time.deltaTime;

                if (lWait > lWaitMax / 4 && !lActive) { lActive = true; _VFX.Play(); }

                Source.material.SetFloat(_ShaderParameters[0], LerpPartial(lOrigin, lEnd, lWait));

                yield return new WaitForSeconds(Time.deltaTime);
            }

            Source.material.SetFloat(_ShaderParameters[0], LerpPartial(lOrigin, lEnd, 1));
        }

        public override IEnumerator CallTurnOff()
        {
            float   lWaitMax = _Data.On.Clip.length,
                    lWait = 0;

            float   lOrigin = 1,
                    lEnd = 0;

            bool    lActive = true;

            while (lWait < lWaitMax)
            {
                lWait += Time.deltaTime;

                if (lWait > lWaitMax / 4 && lActive) { lActive = false; _VFX.Stop(); }

                Source.material.SetFloat(_ShaderParameters[0], LerpPartial(lOrigin, lEnd, lWait));

                yield return new WaitForSeconds(Time.deltaTime);
            }

            Source.material.SetFloat(_ShaderParameters[0], LerpPartial(lOrigin, lEnd, 1));
        }
    }
}