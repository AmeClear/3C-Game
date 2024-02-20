using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game3C
{
    [RequireComponent(typeof(CharacterAnimComponent))]
    [RequireComponent(typeof(MoveComponent))]
    public class CharacterComponent : AbstractComponent
    {

        private MoveComponent moveComponent;
        private CharacterAnimComponent animComponent;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            moveComponent = GetComponent<MoveComponent>();
            animComponent = GetComponent<CharacterAnimComponent>();
        }
        private void Update()
        {
            animComponent.SetMoveParam(moveComponent.DesiredVelocity, moveComponent.MaxSpeed);
        }
    }
}
