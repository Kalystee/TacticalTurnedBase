using PNN.Enums;

namespace PNN.Entities.Events
{
    public class IClickable_OnClickArgs
    {
        public readonly MouseButton clickButton;

        public IClickable_OnClickArgs(MouseButton clickButton)
        {
            this.clickButton = clickButton;
        }

        public IClickable_OnClickArgs(int clickButton)
        {
            this.clickButton = (MouseButton)clickButton;
        }
    }
}