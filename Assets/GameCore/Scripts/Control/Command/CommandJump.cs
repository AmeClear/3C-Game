using QFramework;
using static Game3C.Define.Control;
namespace Game3C
{
    public class CommandJump : AbstractCommand
    {
        public override void execute()
        {
            this.SendEvent<JumpEvent>();
        }

        public override void undo()
        {
        }
    }
}
