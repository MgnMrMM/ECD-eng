using System;
using System.Collections.Generic;
using ECD_Engine.Components;
using ECD_Engine.Extensions;
using ECD_Engine.Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECD_Engine.Systems
{
    public abstract class System : IObserver
    {
        protected SortedList<int, List<Component>> EntitiesSortedList;
        protected int UpdatePriority { get; set; }

        protected System (ref SortedList<int, List<Component>> entitiesSortedList, int updatePriority)
        {
            EntitiesSortedList = entitiesSortedList;
            UpdatePriority = updatePriority;
            Subject.Instance.AddObserver(this);
        }

        public abstract void Update(float deltaTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
        public virtual void OnNotify(Message message)
        {
        }
        public virtual void OnNotify<T>(Message message, T eventData)
        {
        }
        public virtual void OnNotify<T, Y>(Message message, T eventData, Y eventData2)
        {
        }
    }

    public class DrawSystem : System
    {
        private readonly Dictionary<int, double> angles = new Dictionary<int, double>();
        private readonly Dictionary<int, Vector2> positionVectors = new Dictionary<int, Vector2>();
        private readonly Dictionary<int, Texture2D> textures = new Dictionary<int, Texture2D>();

        public DrawSystem(SortedList<int, List<Component>> entitiesSortedList, int updatePriority)
            : base(ref entitiesSortedList, updatePriority)
        {
        }

        public override void Update(float deltaTime)
        {
            foreach (var entity in EntitiesSortedList)
            {
                if (entity.Value.GetComponent<DrawAble>() != null &&
                    entity.Value.GetComponent<Position>() != null)
                {
                    textures.Add(entity.Key, entity.Value.GetComponent<DrawAble>().Texture);
                    positionVectors.Add(entity.Key, entity.Value.GetComponent<Position>().PosVector);
                    angles.Add(entity.Key, entity.Value.GetComponent<Position>().RadianOrientation);
                }

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var key in EntitiesSortedList.Keys)
            {
                if (EntitiesSortedList[key].GetComponent<DrawAble>().IsActive)
                {
                    spriteBatch.Draw(textures[key], new Rectangle((int)positionVectors[key].X, (int)positionVectors[key].Y, textures[key].Width, textures[key].Height), null,
                        Color.White, (float) angles[key], new Vector2(textures[key].Width/2f, textures[key].Height/2f),
                        SpriteEffects.None, 0f);

                }

            }
        }
    }

    public class MoveSystem : System
    {

        public MoveSystem(SortedList<int, List<Component>> entitiesSortedList, int updatePriority)
            : base(ref entitiesSortedList, updatePriority)
        {
        }

        public override void Update(float deltaTime)
        {
            foreach (var entity in EntitiesSortedList)
            {
                if (entity.Value.GetComponent<MoveAble>() != null &&
                    entity.Value.GetComponent<Position>() != null &&
                    entity.Value.GetComponent<MoveAble>().IsActive)
                {
                    entity.Value.GetComponent<Position>().PosX +=
                        entity.Value.GetComponent<MoveAble>().VelX*deltaTime;
                    entity.Value.GetComponent<Position>().PosX +=
                        entity.Value.GetComponent<MoveAble>().VelX*deltaTime;
                }
            }
        }

        public override void OnNotify<T>(Message message, T entityID)
        {
            if (entityID is int && message == Message.CollisionWithScreenEdge)
            {
               // translationLogic
            }
        }
    }

    public class CollisionSystem : System
    {
        public CollisionSystem(SortedList<int, List<Component>> entitiesSortedList, int updatePriority)
            : base(ref entitiesSortedList, updatePriority)
        {
        }

        public override void Update(float deltaTime)
        {
            foreach (var entity in EntitiesSortedList)
            {
                if (entity.Value.GetComponent<BoxCollider>() == null || entity.Value.GetComponent<Position>() == null)
                    continue;
                entity.Value.GetComponent<BoxCollider>().OriginPoint =
                    new Point((int) entity.Value.GetComponent<Position>().PosX,
                        (int) entity.Value.GetComponent<Position>().PosY);

                foreach (var key in EntitiesSortedList.Keys)
                {
                    if (EntitiesSortedList[key].GetComponent<BoxCollider>() != null &&
                        EntitiesSortedList[key].GetComponent<BoxCollider>().IsActive &&
                        entity.Value.GetComponent<BoxCollider>()
                            .BoundingBox.Intersects(EntitiesSortedList[key].GetComponent<BoxCollider>().BoundingBox))
                    {
                        Subject.Instance.Notify(Message.CollisionWithObject,
                            entity.Key, key);
                    }
                        
                }
            }
        }
   }

    public class ManualControlSystem : System
    {
        private float deltaTime;

        public ManualControlSystem(SortedList<int, List<Component>> entitiesSortedList, int updatePriority)
            : base(ref entitiesSortedList, updatePriority)
        {
        }

        public override void Update(float deltaTime)
        {
            this.deltaTime = deltaTime;
        }

        public void OnThrust(int entityID)
        {
            EntitiesSortedList[entityID].GetComponent<MoveAble>().VelX +=
                EntitiesSortedList[entityID].GetComponent<MoveAble>().Acceleration*
                EntitiesSortedList[entityID].GetComponent<Position>().DirectionX*deltaTime;
            EntitiesSortedList[entityID].GetComponent<MoveAble>().VelY +=
                EntitiesSortedList[entityID].GetComponent<MoveAble>().Acceleration*
                EntitiesSortedList[entityID].GetComponent<Position>().DirectionY*deltaTime;

            EntitiesSortedList[entityID].GetComponent<MoveAble>().VelX += .99f;
            EntitiesSortedList[entityID].GetComponent<MoveAble>().VelY += .99f;
        }

        public void OnRotationRight(int entityID)
        {
            EntitiesSortedList[entityID].GetComponent<Position>().RadianOrientation -=
                EntitiesSortedList[entityID].GetComponent<MoveAble>().RotationSpeed*deltaTime;

            EntitiesSortedList[entityID].GetComponent<Position>().RadianOrientation =
                MathHelper.Clamp((float) EntitiesSortedList[entityID].GetComponent<Position>().RadianOrientation,
                    -MathHelper.Pi, MathHelper.Pi);
        }

        public void OnRotationLeft(int entityID)
        {
            EntitiesSortedList[entityID].GetComponent<Position>().RadianOrientation +=
                EntitiesSortedList[entityID].GetComponent<MoveAble>().RotationSpeed*deltaTime;

            EntitiesSortedList[entityID].GetComponent<Position>().RadianOrientation =
                MathHelper.Clamp((float) EntitiesSortedList[entityID].GetComponent<Position>().RadianOrientation,
                    -MathHelper.Pi, MathHelper.Pi);
        }
    }

    public class GunControlSystem : System
    {
        public GunControlSystem(SortedList<int, List<Component>> entitiesSortedList, int updatePriority) : base(ref entitiesSortedList, updatePriority)
        {
        }

        public override void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }
    }


}


