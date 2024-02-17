using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using static Game3C.Define.Control;

namespace Game3C
{

    public class InputComponent : AbstractComponent
    {

        private Stack<AbstractCommand> mCommandStack;
        private float mCallBackTime;
        [HideInInspector]
        public bool IsRun = true;

        private Vector2 move = Vector2.zero;

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

            move.x = Input.GetAxis("Horizontal");
            move.y = Input.GetAxis("Vertical");
            if (Input.GetButtonDown("Jump"))
            {
                return new CommandJump();
            }
            if (move == Vector2.zero)
            {
                this.SendEvent<MoveEvent>(new MoveEvent(Vector2.zero));
                return null;
            }
            return new CommandMove(move);
        }

        private void Control()
        {
            mCallBackTime += Time.deltaTime;
            AbstractCommand cmd = InputHandler();

            if (cmd != null)
            {
                mCommandStack.Push(cmd);
                cmd.execute();
            }
        }
    }
}
