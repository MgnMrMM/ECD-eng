using ECD_Engine.Patterns;
using Microsoft.Xna.Framework.Input;

namespace ECD_Engine.InputHandling
{
    public class InputHandler
    {
        public int EntityID { get; private set; }

        public InputHandler(int entityID)
        {
            EntityID = entityID;
        }

        public void HandleInput(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    InitCommand(new Thrust());
                    break;
                case Keys.Space:
                    InitCommand(new Fire());
                    break;
                case Keys.Right:
                    InitCommand(new RotateRight());
                    break;
                case Keys.Left:
                    InitCommand(new RotateLeft());
                    break;
            }
        }

        private void InitCommand(Command command)
        {
            command.Execute(EntityID);
        }
    }
}
    
