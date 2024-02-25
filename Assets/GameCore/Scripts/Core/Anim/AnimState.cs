using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer.FSM;
using Animancer;
namespace Game3C
{
    public abstract partial class AnimState : State, IOwnedState<AnimState>
    {
        public CharacterAnimComponent Anim;
        public StateMachine<AnimState> OwnerStateMachine => Anim.StateMachine;
        public virtual void Update()
        {

        }
    }
}
