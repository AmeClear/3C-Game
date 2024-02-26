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
        private Vector2 lastMove = Vector2.zero;


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

        private float dashholdTime = 0;
        private float dashholdLimitTime = 0.2f;
        private bool DashHold()
        {

            dashholdTime += Time.deltaTime;
            Debug.LogError(dashholdTime);
            if (dashholdTime >= dashholdLimitTime)
            {
                return true;
            }

            return false;

        }
        private void MoveHandler()
        {
            lastMove = move;
            move.x = Input.GetAxis("Horizontal");
            move.y = Input.GetAxis("Vertical");
            AbstractCommand cmd = null;

            if (move == Vector2.zero)
            {
                if (lastMove != Vector2.zero)
                    this.SendEvent<MoveEvent>(new MoveEvent(Vector2.zero));
            }
            else
                cmd = new CommandMove(move);
            if (cmd != null)
            {
                mCommandStack.Push(cmd);
                cmd.execute();
            }
        }
        private void DashHandler()
        {
            AbstractCommand cmd = null;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (DashHold())
                {
                    cmd = new CommandDash(true);
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if (!DashHold())
                {
                    cmd = new CommandDash(false);
                }
                dashholdTime = 0;
            }
            if (cmd != null)
            {
                mCommandStack.Push(cmd);
                cmd.execute();
            }
        }
        private void KeyDownHandler()
        {
            AbstractCommand cmd = null;

            if (Input.GetButtonDown("Jump"))
            {
                cmd = new CommandJump();
            }
            if (cmd != null)
            {
                mCommandStack.Push(cmd);
                cmd.execute();
            }
        }


        private void Control()
        {
            mCallBackTime += Time.deltaTime;
            MoveHandler();
            DashHandler();
            KeyDownHandler();

        }
    }
}
