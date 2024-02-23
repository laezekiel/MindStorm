using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.isartdigital.Common.Elements.Moves.Targets
{
    public class MoveToward : Move
    {
        [SerializeField] private Transform _Target; public Transform Target { get { return _Target; } set { _Target = value; } }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (IsOn) transform.position = Vector3.MoveTowards(transform.position, _Target.position, m_Speed * Time.deltaTime);
        }
    }
}
