using com.isartdigital.Common.Settings.Sounds;
using UnityEditor;
using UnityEngine;

namespace com.ironicentertainment.Tools.Extensions
{
    public static class AudioSourceExtensions
    {
        public static void SetSound(this AudioSource pSource, SoundData pSound)
        {
            pSource.clip = pSound.Clip;

            pSource.volume = pSound.Volume;
            pSource.pitch = pSound.Pitch;
            pSource.panStereo = pSound.StereoPan;
            pSource.spatialBlend = pSource.spatialBlend;

            pSource.mute = pSound.Mute;
            pSource.playOnAwake = pSound.PlayOnAwake;
            pSource.loop = pSound.Loop;
        }
    }
}