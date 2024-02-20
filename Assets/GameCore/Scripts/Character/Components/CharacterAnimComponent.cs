using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game3C
{

    public class CharacterAnimComponent : AbstractComponent
    {
        private float _animSpeed;
        public float AnimSpeed;
        public Animator animator;
        private void Update()
        {
            animator.SetFloat("Speed", _animSpeed);
        }
    }
}
