using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ironicentertainment.Common.Tools.Extensions
{
    public static class RendererExtensions
    {
        /// <summary>
        /// Create and return a copy of the Renderer.material with alpha set as on through out the settings and the color alha set at 0.5f by default
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        public static  Material GetTransluscentMaterial_ST( this Renderer renderer, float alpha = 0.5f )
        {
            Material originalMaterial = renderer.material;
            Color originalColor = originalMaterial.color;

            Material translucentMaterial = new Material(originalMaterial);

            translucentMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            translucentMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            translucentMaterial.SetInt("_ZWrite", 0);
            translucentMaterial.DisableKeyword("_ALPHATEST_ON");
            translucentMaterial.EnableKeyword("_ALPHABLEND_ON");
            translucentMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");

            translucentMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
            translucentMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            return translucentMaterial;
        }
    }
}
