using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game3C
{
    public partial class AnimState
    {
        public class MoveState : AnimState
        {
            public override void OnEnterState()
            {
                Anim.Animancer.Play(Anim.move);
            }
            public override void Update()
            {
                Anim.move.State.Parameter = Anim.AnimParams.Speed;
                if (Anim.AnimParams.Speed < 0.1)
                {
                    OwnerStateMachine.TrySetState(Anim.IdleState);
                }
            }
        }
    }
}
