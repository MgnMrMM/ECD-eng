using ECD_Engine.Components;

namespace ECD_Engine.Patterns
{
    public abstract class Command
    {
        public abstract void Execute(int entityID);
    }

    public class RotateRight : Command
    {
        public override void Execute(int entityID) => Subject.Instance.Notify(Message.RotateRight, entityID);
    }

    public class RotateLeft : Command
    {

        public override void Execute(int entityID) => Subject.Instance.Notify(Message.RotateLeft, entityID);
    }

    public class Thrust : Command
    {
        public override void Execute(int entityID) => Subject.Instance.Notify(Message.Thrust, entityID);
    }

    public class Fire : Command
    {
        public override void Execute(int entityID) => Subject.Instance.Notify(Message.ShotFired, entityID);
    }
}
