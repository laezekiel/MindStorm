using com.isartdigital.Common.Settings.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.isartdigital.Common.Elements.Player.Move
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private InputManager _HorizontalVertical;
        public enum Directions
        {
            Forward_Backward = 1 , Left_Right = 2, Up_Down = 3
        }
        
        [SerializeField] private Directions _Direction;

        public bool IsPressed { get => GetAxisStrength() != Vector3.zero; }

        public Vector3 GetAxisStrength()
        {
            switch (_Direction)
            {
                case Directions.Forward_Backward:
                    return new Vector3(0, 0, Input.GetAxis(_HorizontalVertical.Inputs[1].InputValue));
                case Directions.Left_Right:
                    return new Vector3(Input.GetAxis(_HorizontalVertical.Inputs[0].InputValue), 0,0);
                case Directions.Up_Down:
                    return new Vector3(0, Input.GetAxis(_HorizontalVertical.Inputs[1].InputValue), 0);
                default:
                    return Vector3.zero;
            }
        }

    }
}
