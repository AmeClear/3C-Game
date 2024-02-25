using System.Collections;
using System.Collections.Generic;
using Game3C;
using UnityEngine;
using Animancer;

namespace Game3C
{
    public partial class AnimState
    {
        public class IdleState : AnimState
        {
            bool isPlayMove = false;
            public override void OnEnterState()
            {
                base.OnEnterState();
                Anim.Animancer.Play(Anim.idle);
            }
            public override void Update()
            {
                if (Anim.AnimParams.Speed > 0.2)
                {
                    OwnerStateMachine.TrySetState(Anim.MoveState);
                }

            }
        }
    }
}
