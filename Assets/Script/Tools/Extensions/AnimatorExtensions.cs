using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace com.ironicentertainment.Tools.Extensions
{
    public static class AnimatorExtensions
    {
        public static void UpdateAnimatorValueHV(this Animator anim, float horizontalValue, float verticalValue, string horizontale = "Horizontal", string vertical = "Vertical")
        {
            int hIndex = Animator.StringToHash(horizontale);
            int vIndex = Animator.StringToHash(vertical);
            anim.SetFloat(hIndex, horizontalValue, .05f, Time.deltaTime);
            anim.SetFloat(vIndex, verticalValue, .05f, Time.deltaTime);
        }

        public static (bool, float[]) IsZero(this Animator anim, params string[] parameters)
        {
            if (parameters.Length == 0) parameters = new string[] { "Vertical" };

            List<float> values = new List<float>();

            foreach (string parameter in parameters)
            {
                float parameterValue = anim.GetFloat(parameter);
                values.Add(parameterValue);

                if (Mathf.Abs(parameterValue) > 0.001f)
                {
                    return (false, values.ToArray());
                }
            }
            return (true, values.ToArray());

        } 
    }
}
