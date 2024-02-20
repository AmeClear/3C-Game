using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game3C
{

    public class CharacterAnimComponent : AbstractComponent
    {

        [SerializeField]
        private float changeAngle = 0;
        private float _lastSpeed = 0;
        [SerializeField]
        private float _animSpeed = 0;
        [SerializeField]
        private float acceleration = 0;

        public Animator animator;
        private void Update()
        {

            animator.SetFloat("Speed", _animSpeed);
            animator.SetFloat("Acceleration", acceleration);
            animator.SetFloat("ChangeAngle", changeAngle);

        }
        public void SetMoveParam(Vector3 Velocity, float maxSpeed)
        {
            //点乘判断夹角，夹角判断转向
            if (Velocity != Vector3.zero)
                changeAngle = CalcAngleByVec(transform.forward, Velocity);
            _lastSpeed = _animSpeed;
            _animSpeed = Velocity.magnitude / maxSpeed;
            acceleration = _animSpeed - _lastSpeed;
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

            // //计算向量 a、b 的叉积，结果为 向量 
            // Vector3 c = Vector3.Cross(a, b);

            // Vector3 normal = new Vector3(1, 1, 1);
            // normal = normal.normalized;
            // // 计算 向量 c 在坐标轴正方向单位向量上的投影
            // float vlaue = Vector3.Dot(c, normal);

            // if (vlaue < 0)
            // {
            //     angle *= -1;
            // }
            return angle;
        }
    }
}
