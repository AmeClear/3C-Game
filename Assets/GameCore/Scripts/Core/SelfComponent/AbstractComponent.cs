using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
namespace Game3C
{
    public abstract class AbstractComponent : MonoBehaviour, IController, ICanSendEvent
    {
        public IArchitecture GetArchitecture()
        {
            return GameCore.Interface;
        }
    }
}
