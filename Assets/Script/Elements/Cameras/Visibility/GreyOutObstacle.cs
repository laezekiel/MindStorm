using com.ironicentertainment.Common.Settings.Cameras;
using com.ironicentertainment.Common.Tools.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

namespace com.ironicentertainment.Common.Elements.Cameras.Visibility
{
    public class GreyOutObstacle : MonoBehaviour
    {
        [SerializeField] private CameraPositionData _Data;

        public CameraPositionData Data { get { return _Data; } set { _Data = value; } }

        private Coroutine _RayCastCoroutine;
        private List<Coroutine> _GreyedOutCoroutines = new List<Coroutine>();

        private Camera mainCamera;

        private List<GameObject> _HitObjects = new List<GameObject>();
        private List<GameObject> _OldHitObjects = new List<GameObject>();

        void Start()
        {
            mainCamera = GetComponent<Camera>();

            _RayCastCoroutine = StartCoroutine(SendRayCast());
        }

        private IEnumerator SendRayCast()
        {
            while (true)
            {
                Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

                RaycastHit[] hits = Physics.RaycastAll(ray, _Data.Length);

                if (_HitObjects.Count > 0) foreach (GameObject obj in _HitObjects) _OldHitObjects.Add(obj);

                _HitObjects.Clear();

                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.gameObject.CompareTag("Wall"))
                    {
                        if (!_OldHitObjects.Contains(hit.collider.gameObject)) _HitObjects.Add(hit.collider.gameObject);
                    }
                }


                bool isHit = false;
                int oldIndex = 0;
                List<GameObject> hitObjectsRemove = new List<GameObject>(); 
                List<int> CoroutinesIndex = new List<int>();

                foreach (GameObject obj in _OldHitObjects)
                {
                    foreach (RaycastHit hit in hits)
                    {
                        if (obj == hit.collider.gameObject) isHit = true;
                    }
                    if (!isHit)
                    {
                        hitObjectsRemove.Add(obj);
                    }
                    oldIndex++;
                }
                foreach (GameObject obj in hitObjectsRemove) for (int i = _OldHitObjects.Count - 1; i >= 0; i--) if(_OldHitObjects[i] == obj) _OldHitObjects.Remove(obj);

                foreach (GameObject hitObject in _HitObjects)
                {
                    for (int i = 0; i < hitObject.transform.childCount; i++)
                    {
                        _GreyedOutCoroutines.Add(StartCoroutine(MakeTransparent(hitObject.transform.GetChild(i).gameObject, hitObject)));
                    }
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        private IEnumerator MakeTransparent(GameObject obj, GameObject parent)
        {
            Renderer objRenderer = obj.GetComponent<Renderer>();

            Material originalMaterial = objRenderer.material;

            objRenderer.material = objRenderer.GetTransluscentMaterial_ST();

            while (_HitObjects.Contains(parent) || _OldHitObjects.Contains(parent))
            {
                yield return new WaitForSeconds(0.25f);
            }
            objRenderer.material = originalMaterial;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
