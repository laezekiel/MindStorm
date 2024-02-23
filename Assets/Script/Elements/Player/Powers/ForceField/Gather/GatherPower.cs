using com.ironicentertainment.Elements.Player.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace com.ironicentertainment.Elements.Player.Powers.ForceField.Gather
{
    public class GatherPower : Power
    {
        [SerializeField, Range(1, 15)] private float _Max = 5;

        private bool _Closing = false, _Opening = false;
        public float Size { get { return 5 + Growth(_ElapsedTime); } }

        protected override void Update()
        {
            if (Checker.CanUsePower && (!_Closing && !_Opening))
            {
                if (!Checker.GatherIsUsed ) base.Update();
                else HandleInput();
            }
        }

        protected override void PowerStartEffect()
        {
            if (Input.GetButtonDown(_PowersInput.Inputs[0].InputValue))
            {
                StartCoroutine(OpenForceField());
            }
        }

        protected override void HandleInput()
        {

            if (Input.GetButton(_PowersInput.Inputs[0].InputValue))
            {
                _ElapsedTime += Time.deltaTime;
                _Instance.transform.localScale = Vector3.one * Size;
            }
            else if (Input.GetButtonDown(_PowersInput.Inputs[1].InputValue))
            {
                StartCoroutine(CloseForceField());
            }
        }

        private IEnumerator CloseForceField()
        {
            int index = 1;
            float reduce = Size;
            _Closing = true;

            while (reduce > 0)
            {
                reduce -= Time.deltaTime * index;
                if (reduce <= 0) reduce = 0;

                _Instance.transform.localScale = Vector3.one * reduce;

                index++;

                yield return new WaitForSeconds(Time.deltaTime);
            }

            Destroy(_Instance);
            Checker.GatherIsUsed = false;
            _Closing = false;
            _ElapsedTime = 0;
        }

        private IEnumerator OpenForceField()
        {
            _Opening = true;
            _Instance = Instantiate(_Prefab, transform.position, transform.rotation);

            float reduce = 0;
            float index = 1;

            _Instance.transform.localScale = Vector3.one * reduce;

            while (reduce < 5)
            {
                reduce += index * Time.deltaTime;
                if (reduce >= 5) reduce = 5;

                _Instance.transform.localScale = Vector3.one * reduce;

                index += 0.25f;

                yield return new WaitForSeconds(Time.deltaTime);
            }

            _Opening = false;
            Checker.GatherIsUsed = true;
        }

        private float Growth(float timer)
        {
            float toZero = (_Max - 5.0f);
            float a = 1 - Mathf.Lerp(0.0f, 0.5f, Mathf.Abs(toZero) / (15.0f - 5.0f));

            if (toZero < 0) a = a + (1 - a) * 2;

            return _Max / (1 + 150 * Mathf.Exp(-a * timer));
        }
    }
}
