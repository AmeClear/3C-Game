using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game3C
{
    public partial class Define
    {
        public class Control
        {

            public class MoveEvent
            {
                public Vector2 moveParams;
                public MoveEvent(Vector2 moveParams)
                {
                    this.moveParams = moveParams;
                }
            }
            public class JumpEvent
            {

            }
            public class DashEvent
            {
                public bool isDash;
                public DashEvent(bool isDash)
                {
                    this.isDash = isDash;
                }
            }
        }
    }
}
