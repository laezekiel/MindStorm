using com.ironicentertainment.Common.Elements.Cameras.Positions;
using com.ironicentertainment.Common.Tools.Extensions;
using com.ironicentertainment.Elements.Player.Powers.ForceField.Gather;
using com.ironicentertainment.Tools.Extensions;
using com.isartdigital.Common.Elements.Player.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.ironicentertainment.Elements.Player.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private GameObject _Camera, _Character, _Parent, _Body;
        public GameObject Camera { get { if (_Camera == null) _Camera = transform.GetChild(0).gameObject; return _Camera; } }
        public GameObject Character { get { if (_Character == null) _Character = transform.GetChild(1).gameObject; return _Character; } }
        public GameObject Parent { get { if (_Parent == null) _Parent = transform.parent.gameObject; return _Parent; } }
        public GameObject Body { get { if (_Body == null) _Body = Character.transform.GetChild(0).gameObject; return _Body; } }


        private PlayerMoveAround _Mover;
        public PlayerMoveAround Mover { get { if (_Mover == null) _Mover = Character.GetComponent<PlayerMoveAround>(); return _Mover; } }

        public bool IsMoving { get { return Mover.CanGetMove[0]; } set { Mover.CanGetMove[0] = value; } }


        private Animator _Anim;
        public Animator Anim { get { if (_Anim == null) _Anim = Body.GetComponent<Animator>(); return _Anim; } }

        public bool CanUsePower { get { return Anim.IsZero().Item1; } }


        private CameraController _CamPositioner;
        public CameraController CamPositioner { get { if(_CamPositioner == null) _CamPositioner = Camera.GetComponent<CameraController>(); return _CamPositioner; } }


        private bool _Active = true;
        public bool Active { get { return _Active; } set { IsMoving = _Active = value; Character.SetNewController(value); Camera.SetNewController(value);  } }


        private bool _GatherIsUsed = false;
        public bool GatherIsUsed { get { return _GatherIsUsed; } set { _GatherIsUsed = value; Character.SetModeGather(!value); } } 


        private GatherPower _GatherContainer;
        public GatherPower GatherContainer { get { if (_GatherContainer == null) _GatherContainer = Character.GetComponent<GatherPower>(); return _GatherContainer; } }
    }
}
