using System;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;
using Animancer.FSM;
using QFramework;
using static Game3C.AnimState;
namespace Game3C
{


    public class CharacterAnimComponent : AbstractComponent
    {
        public readonly StateMachine<AnimState>.WithDefault StateMachine = new StateMachine<AnimState>.WithDefault();
        [SerializeField] private AnimancerComponent animancer;
        public AnimancerComponent Animancer => animancer;


        private IdleState idleState;
        private MoveState moveState;
        public IdleState IdleState => idleState;
        public MoveState MoveState => moveState;


        [Header("动画")]
        public ClipTransition idle;

        public LinearMixerTransitionAsset.UnShared move;



        [Header("属性")]
        private MoveComponent moveComponent;
        [SerializeField] private CharacterAnimParams animParams;
        public CharacterAnimParams AnimParams => animParams;

        private void Awake()
        {
            animParams = new CharacterAnimParams();
            moveComponent = GetComponent<MoveComponent>();

            animancer = gameObject.GetComponentInParentOrChildren<AnimancerComponent>();

            idleState = new IdleState();
            moveState = new MoveState();
            idleState.Anim = this;
            moveState.Anim = this;
        }
        private void OnEnable()
        {
            StateMachine.DefaultState = idleState;

        }
        private void Update()
        {
            SetMoveParam(moveComponent.DesiredVelocity, moveComponent.MaxSpeed);

            StateMachine.CurrentState.Update();
        }

        // [SerializeField] private LinearMixerTransitionAsset.UnShared MoveMixer;
        // [SerializeField] private MixerTransition2D _Turn;
        // [SerializeField] private float changeAngle = 0;
        // private Define.AnimParams animParams = new Define.AnimParams();



        // private void OnEnable()
        // {
        //     _Animancer.Play(MoveMixer);
        // }
        // private void Update()
        // {
        // }

        private void SetMoveParam(Vector3 Velocity, float maxSpeed)
        {

            //点乘判断夹角，夹角判断转向
            if (StateMachine.CurrentState == idleState && Velocity != Vector3.zero)
                animParams.ChangeAngle = CalcAngleByVec(transform.forward, Velocity);
            // _lastSpeed = _animSpeed;
            // _animSpeed = Velocity.magnitude / maxSpeed;
            // acceleration = _animSpeed - _lastSpeed;
            animParams.Speed = Velocity.magnitude / maxSpeed;
            // animParams.changeAngle = changeAngle;
            // if (animParams.speed > 0)
            // {
            //     animState = AnimState.Moving;
            // }
            // else
            // {
            //     animState = AnimState.Idle;

            // }
        }
        private float CalcAngleByVec(Vector3 a, Vector3 b)
        {
            // 向量归一化
            a = a.normalized;
            b = b.normalized;

            // 计算 a、b 点乘结果
            float result = Vector3.Dot(a, b);
            // 通过反余弦函数获取 向量 a、b 夹角（结果为 弧度）
            float radians = Mathf.Acos(result);
            // 将弧度转换为角度
            float angle = radians * Mathf.Rad2Deg;

            //计算向量 a、b 的叉积，结果为 向量 
            Vector3 c = Vector3.Cross(a, b);

            // 计算 向量 c 在坐标轴正方向单位向量上的投影
            float vlaue = Vector3.Dot(c, Vector3.one);

            if (vlaue < 0)
            {
                angle *= -1;
            }
            return angle;
        }
    }
}
