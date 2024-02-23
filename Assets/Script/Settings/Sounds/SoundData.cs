using System;
using System.Collections;
using UnityEngine;

namespace com.isartdigital.Common.Settings.Sounds
{
    [CreateAssetMenu(menuName = "Objects/Sound", fileName = "DefaultSound", order = 0), Serializable]
    public class SoundData : ScriptableObject
    {
        [SerializeField] protected AudioClip _Clip;

        [SerializeField] protected bool _Mute, _PlayOnAwake, _Loop;

        [SerializeField, Range(0, 1)] protected float _Volume = 1, _SpatialBlend = 1;
        [SerializeField, Range(-1, 1)] protected float _StereoPan = 0;
        [SerializeField, Range(-3, 3)] protected float _Pitch = 1;

        public virtual AudioClip Clip { get => _Clip; }

        public virtual bool Mute { get => _Mute; }
        public virtual bool PlayOnAwake { get => _PlayOnAwake; }
        public virtual bool Loop { get => _Loop; }

        public virtual float Volume { get => _Volume; }
        public virtual float StereoPan { get => _StereoPan; }
        public virtual float Pitch { get => _Pitch; }
        public virtual float SpatialBlend { get => _SpatialBlend; }


        public virtual void SetSoundData(AudioSource pSource)
        {
            _Mute = pSource.mute;
            _PlayOnAwake = pSource.playOnAwake;
            _Loop = pSource.loop;

            _Volume = pSource.volume;
            _StereoPan = pSource.panStereo;
            _Pitch = pSource.pitch;
            _SpatialBlend = pSource.spatialBlend;
        }
    }
}