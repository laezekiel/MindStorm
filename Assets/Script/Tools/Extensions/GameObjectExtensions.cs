using com.ironicentertainment.Common.Elements.Cameras.Positions;
using com.ironicentertainment.Elements.Player.Powers.ForceField.Gather;
using com.ironicentertainment.Elements.Player.Powers.Ghost;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace com.ironicentertainment.Common.Tools.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Checks if the GameObject has a Renderer Component. If not return null else:
        /// Create and return a copy of the Renderer.material with alpha set as on through out the settings and the color alha set at 0.5f by default
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        public static Material GetTransluscentMaterial_ST(this GameObject obj)
        {
            Renderer renderer = obj.GetComponent<Renderer>();

            if (renderer == null) return null;

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
            translucentMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);

            return translucentMaterial;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="active"></param>
        public static void SetNewController(this GameObject obj, bool active)
        {
            SetComponentsEnabled(obj, active,
                typeof(Transform),
                typeof(Collider),
                typeof(Renderer),
                typeof(Rigidbody),
                typeof(Animator),
                typeof(GhostPower));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="active"></param>
        public static void SetModeGather(this GameObject obj, bool active)
        {
            SetComponentsEnabled(obj, active,
                typeof(Transform),
                typeof(Collider),
                typeof(Renderer),
                typeof(Rigidbody),
                typeof(Animator),
                typeof(GatherPower));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="enabled"></param>
        /// <param name="excludedTypes"></param>
        public static void SetComponentsEnabled(GameObject obj, bool enabled, params System.Type[] excludedTypes) 
        {

            Component[] components = obj.GetComponents<Component>();

            foreach (Component component in components)
            {
                if (!excludedTypes.Contains(component.GetType()))
                {
                    if (component is Behaviour behavior)
                    {
                        behavior.enabled = enabled;
                    }
                }
            }
        }
    }
}
