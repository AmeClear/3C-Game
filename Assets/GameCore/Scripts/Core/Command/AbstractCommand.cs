using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game3C
{
    public abstract class AbstractCommand
    {
        protected float _time;
        public float Time
        {
            get { return _time; }
        }
        public virtual void execute() { }
        public virtual void undo() { }

    }
}
