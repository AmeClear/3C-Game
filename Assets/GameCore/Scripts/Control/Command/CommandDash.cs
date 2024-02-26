using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using static Game3C.Define.Control;

namespace Game3C
{

    public class CommandDash : AbstractCommand
    {
        public bool isDash = false;
        public CommandDash(bool isDash) { this.isDash = isDash; }
        public override void execute()
        {
            this.SendEvent<DashEvent>(new DashEvent(this.isDash));
        }

        public override void undo()
        {
        }
    }
}
