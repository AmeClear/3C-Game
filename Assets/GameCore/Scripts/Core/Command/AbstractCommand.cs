using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
namespace Game3C
{
    public abstract class AbstractCommand : ICanSendEvent
    {
        protected float _time;
        public float Time
        {
            get { return _time; }
        }
        public abstract void execute();

        public IArchitecture GetArchitecture()
        {
            return GameCore.Interface;
        }

        public abstract void undo();

    }
}
