using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ironicentertainment.Common.Tools.Extensions
{
    public static class MaterialExtensions
    {
        /// <summary>
        /// Create and return a copy of the Material with alpha set as on through out the settings and the color alha set at 0.5f by default
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        public static Material GetTransluscentMaterial_ST(this Material material)
        {
            Color originalColor = material.color;

            Material translucentMaterial = new Material(material);

            translucentMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            translucentMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            translucentMaterial.SetInt("_ZWrite", 0);
            translucentMaterial.DisableKeyword("_ALPHATEST_ON");
            translucentMaterial.EnableKeyword("_ALPHABLEND_ON");
            translucentMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");

            translucentMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
            translucentMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);

            return translucentMaterial;
        }
    }
}
