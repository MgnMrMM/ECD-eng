using System;
using ECD_Engine.Behaviours;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECD_Engine.Components
{
    public enum ComponentType
    {
        Position,
        BoxCollider,
        DrawAble,
        MoveAble,
        AnimationAble
    }

    public enum Message
    {
        CollsionWithBullet,
        CollisionWithRock,
        CollisionWithScreenEdge,
        ShotFired,
        ShipDestroyed
    }

    public abstract class Component
    {
    }

    public class Position : Component
    {
        public float PosX { get; set; }
        public float PosY { get; set; }
        public double RadianOrientation { get; set; }
        public float DirectionX => (float)Math.Cos(RadianOrientation);
        public float DirectionY => (float)Math.Sin(RadianOrientation);
        public Vector2 PosVector => new Vector2((int)PosX, (int)PosY);
        public Vector2 DirectionVector => new Vector2(DirectionX, DirectionY);
    }

    public class BoxCollider : Component
    {
        public bool IsActive { get; set; } //if is not active then it serves only as a passive collision target (can not collide but can be collided with)
        public Point Size { get; set; }
        public Point OriginPoint { get; set; }
        public Rectangle BoundingBox => new Rectangle(OriginPoint, Size);

    }

    public class DrawAble : Component
    {
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public bool IsActive { get; set; }
    }

    public class MoveAble : Component
    {
        public bool IsActive { get; set; }
        public float Acceleration { get; set; }
        public float RotationSpeed { get; set; }
        public float VelX { get; set; }
        public float VelY { get; set; }

        public Vector2 Velocity => new Vector2(VelX, VelY);
    }

    public class AnimateAble : Component
    {
        public bool IsActive { get; set; }
        public float AnimationDuration { get; set; }
    }

    public class AiControlled : Component
    { 
    }

    public class PlayerControlled : Component
    {
        public int Lives { get; set; }
    }
}
