using System.Collections;
using System.Collections.Generic;
using Game3C;
using QFramework;
using UnityEngine;
using static Game3C.Define.Control;

namespace Game3C
{
    public class CommandMove : AbstractCommand
    {
        private Vector2 moveParams;
        public Vector2 Move { set { moveParams = value; } }
        public CommandMove(Vector2 moveParams)
        {
            this.moveParams = moveParams;
        }
        public override void execute()
        {
            this.SendEvent<MoveEvent>(new MoveEvent(moveParams));
        }



        public override void undo()
        {
        }
    }
}
