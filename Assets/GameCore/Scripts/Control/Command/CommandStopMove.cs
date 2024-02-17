using System.Collections;
using System.Collections.Generic;
using Game3C;
using QFramework;
using UnityEngine;
using static Game3C.Define.Control;

namespace Game3C
{
    public class CommandStopMove : AbstractCommand
    {

        public override void execute()
        {
            this.SendEvent<MoveEvent>(new MoveEvent(Vector2.zero));
        }



        public override void undo()
        {
        }
    }
}
