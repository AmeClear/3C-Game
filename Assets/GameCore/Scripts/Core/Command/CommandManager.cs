using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
namespace Game3C
{
    public class CommandManager : MonoSingleton<CommandManager>
    {
        /// <summary>
        /// 限制指令数量
        /// </summary>
        public long CommandLimitNum;
        private Stack<AbstractCommand> mCommandStack;
        private float mCallBackTime;
        [HideInInspector]
        public bool IsRun = true;

        // Use this for initialization
        void Start()
        {
            mCommandStack = new Stack<AbstractCommand>();
            mCallBackTime = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (IsRun)
            {
                Control();
            }
            else
            {
                RunCallBack();
            }

        }

        private void RunCallBack()
        {
            mCallBackTime -= Time.deltaTime;
            if (mCommandStack.Count > 0 && mCallBackTime < mCommandStack.Peek().Time)
            {
                mCommandStack.Pop().undo();
            }
        }

        private AbstractCommand InputHandler()
        {
            if (Input.GetKey(KeyCode.W))
            {
            }
            if (Input.GetKey(KeyCode.S))
            {
            }
            if (Input.GetKey(KeyCode.A))
            {
            }
            if (Input.GetKey(KeyCode.D))
            {
            }
            return null;
        }

        private void Control()
        {
            mCallBackTime += Time.deltaTime;
            AbstractCommand cmd = InputHandler();
            if (mCommandStack.Count > CommandLimitNum)
                return;
            if (cmd != null)
            {
                mCommandStack.Push(cmd);
                cmd.execute();
            }
        }
    }
}
