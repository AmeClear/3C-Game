using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game3C
{
    public class CharacterComponent : AbstractComponent
    {
        [HideInInspector]
        public List<AbstractComponent> components;
    }
}