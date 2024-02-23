using com.isartdigital.Common.Settings.Sounds;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace com.isartdigital.Common.Settings.Lights
{
    [CreateAssetMenu(menuName = "Objects/Light", fileName = "DefaultLight", order = 0)]
    public class LightData : ScriptableObject
    {
        //[SerializeField] protected Material _Source;

        [SerializeField] protected bool _IsOn;

        [SerializeField] protected SoundData _Loop, _On, _Off;

        //[SerializeField] protected VisualEffectAsset _VisualEffect;

        //[SerializeField] protected VFXParameters<float>[] _VFX_ParameterF = new VFXParameters<float>[0];
        //[SerializeField] protected VFXParameters<Color>[] _VFX_ParameterC = new VFXParameters<Color>[0];
        //[SerializeField] protected VFXParameters<Vector3>[] _VFX_ParameterV3 = new VFXParameters<Vector3>[0];
        //[SerializeField] protected VFXParameters<Texture2D>[] _VFX_ParameterT2D = new VFXParameters<Texture2D>[0];
        //[SerializeField] protected VFXParameters<Gradient>[] _VFX_ParameterG = new VFXParameters<Gradient>[0];


        //[Serializable]
        //public struct VFXParameters<T>
        //{
        //    [SerializeField] private string _Name;
        //    [SerializeField] private T _Value;

        //    public VFXParameters (string pName, T pValue)
        //    {
        //        _Name = pName;
        //        _Value = pValue;
        //    }

        //    public string Name { get => _Name; }

        //    public T Value { get => _Value; }
        //}

        //public virtual VisualEffectAsset VisualEffect { get => _VisualEffect; }

        //public virtual Material Source { get => _Source; }

        public bool IsOn { get => _IsOn; set => _IsOn = value; }

        public virtual SoundData Loop { get => _Loop; }

        public virtual SoundData On { get => _On; }

        public virtual SoundData Off { get => _Off; }
    }
}