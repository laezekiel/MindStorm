using com.ironicentertainment.Common.Tools.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.isartdigital.Common.Settings.Controllers
{
    [CreateAssetMenu(menuName = "Settings/Input Controller", fileName = "DefaultController", order = 0)]
    public class InputManager : ScriptableObject
    {
        public enum UnityInputs { Horizontal, Vertical, Fire1, Fire2, Fire3, Jump, Mouse_X, Mouse_Y, Mouse_ScrollWheel, Submit, Cancel, Ghost, Gather, Interract, AEAxis }

        [Serializable]
        public class InputData
        {
            public UnityInputs InputName;
            public string InputValue { get { return InputName.ToString().Replace('_',' '); } }
        }

        [SerializeField] private InputData[] _Inputs;

        public InputData[] Inputs { get { return _Inputs; } }
    }
}