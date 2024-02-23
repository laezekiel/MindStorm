using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.isartdigital.Common.Elements.Moves
{
    public class MoveForwards : Move
    {
        private Vector3 _Direction = Vector3.zero;

        [SerializeField] public float Speed = 1f;

        // Start is called before the first frame update
        void Start()
        {
            _Direction = transform.forward;
        }

        // Update is called once per frame
        void Update()
        {
            if (IsOn) transform.position += _Direction.normalized * Speed * (m_Speed * Time.deltaTime);
        }
    }
}
