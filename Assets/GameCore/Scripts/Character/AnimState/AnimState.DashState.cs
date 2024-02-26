using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game3C
{
    public partial class AnimState
    {
        public class DashState : AnimState
        {
            public override void OnEnterState()
            {
                Anim.Animancer.Play(Anim.dash);
            }
            public override void Update()
            {
                Anim.dash.State.Parameter = Anim.AnimParams.Speed;
                if (Anim.AnimParams.Speed < 0.1)
                {
                    OwnerStateMachine.TrySetState(Anim.IdleState);
                }
            }
        }
    }
}
