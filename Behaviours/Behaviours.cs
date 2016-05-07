using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ECD_Engine.Components;
using ECD_Engine.Extensions;
using Microsoft.Xna.Framework.Graphics;

namespace ECD_Engine.Behaviours
{
    public abstract class System
    {
        protected SortedList<int, List<Component>> EntitiesSortedList;
        protected int UpdatePriority { get; set; }

        protected System(SortedList<int, List<Component>> entitiesSortedList, int updatePriority)
        {
            this.EntitiesSortedList = entitiesSortedList;
            UpdatePriority = updatePriority;
        }

        public abstract void Update(float deltaTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
    
    public class DrawSystem : System
    {
        private Dictionary<int, double> angles = new Dictionary<int, double>();
        private Dictionary<int, Vector2> positionVectors = new Dictionary<int, Vector2>();
        private Dictionary<int, Texture2D> textures = new Dictionary<int, Texture2D>();

        public DrawSystem(SortedList<int, List<Component>> entitiesSortedList, int updatePriority) : base(entitiesSortedList, updatePriority)
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
                }

            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var key in EntitiesSortedList.Keys)
            {
                if (EntitiesSortedList[key].GetComponent<DrawAble>().IsActive)
                {
                    spriteBatch.Draw(textures[key], new Rectangle(0, 0, textures[key].Width, textures[key].Height), null,
                        Color.White, (float) angles[key], new Vector2(textures[key].Width/2f, textures[key].Height/2f),
                        SpriteEffects.None, 0f);

                }

        }

    }

    public class MoveSystem : System
    {
        private float deltaTime;

        public MoveSystem (SortedList<int, List<Component>> entitiesSortedList, int updatePriority) : base(entitiesSortedList, updatePriority)
        {
        }

        public override void Update(float deltaTime)
        {
            this.deltaTime = deltaTime;

            foreach (var entity in EntitiesSortedList)
            {
                if (entity.Value.GetComponent<MoveAble>() != null && 
                    entity.Value.GetComponent<Position>() != null &&
                    entity.Value.GetComponent<MoveAble>().IsActive)
                {
                    entity.Value.GetComponent<Position>().PosX +=
                        entity.Value.GetComponent<MoveAble>().VelX * deltaTime;
                    entity.Value.GetComponent<Position>().PosX +=
                        entity.Value.GetComponent<MoveAble>().VelX * deltaTime;
                }
            }
        }

        public void OnThrust(int entityID)
        {
            EntitiesSortedList[entityID].GetComponent<MoveAble>().VelX +=
                EntitiesSortedList[entityID].GetComponent<MoveAble>().Acceleration * EntitiesSortedList[entityID].GetComponent<Position>().DirectionX * deltaTime;
            EntitiesSortedList[entityID].GetComponent<MoveAble>().VelY +=
                EntitiesSortedList[entityID].GetComponent<MoveAble>().Acceleration * EntitiesSortedList[entityID].GetComponent<Position>().DirectionY * deltaTime;

          EntitiesSortedList[entityID].GetComponent<MoveAble>().VelX += .99f;
          EntitiesSortedList[entityID].GetComponent<MoveAble>().VelY += .99f;


        }

        public void OnRotationRight (int entityID)
        {
            EntitiesSortedList[entityID].GetComponent<Position>().RadianOrientation-=
               EntitiesSortedList[entityID].GetComponent<MoveAble>().RotationSpeed * deltaTime;

            EntitiesSortedList[entityID].GetComponent<Position>().RadianOrientation =
                MathHelper.Clamp((float)EntitiesSortedList[entityID].GetComponent<Position>().RadianOrientation, -MathHelper.Pi, MathHelper.Pi);
        }

        public void OnRotationLeft (int entityID)
        {
            EntitiesSortedList[entityID].GetComponent<Position>().RadianOrientation +=
               EntitiesSortedList[entityID].GetComponent<MoveAble>().RotationSpeed * deltaTime;

            EntitiesSortedList[entityID].GetComponent<Position>().RadianOrientation =
                MathHelper.Clamp((float)EntitiesSortedList[entityID].GetComponent<Position>().RadianOrientation, -MathHelper.Pi, MathHelper.Pi);
        }

    }

    public class CollisionSystem : System
    {
        public CollisionSystem(SortedList<int, List<Component>> entitiesSortedList, int updatePriority) : base(entitiesSortedList, updatePriority)
        {
        }

        public override void Update(float deltaTime)
        {
            foreach (var entity in EntitiesSortedList)
            {
                if (entity.Value.GetComponent<BoxCollider>() != null &&
                    entity.Value.GetComponent<Position>() != null)
                {
                    entity.Value.GetComponent<BoxCollider>().OriginPoint =
                        new Point((int)entity.Value.GetComponent<Position>().PosX, (int)entity.Value.GetComponent<Position>().PosY);
                    
                }
            }
        }

        public Action<BoxCollider> CollisionWithFloor;
        public Action<BoxCollider> CollisionWithWall;
        public Action<BoxCollider> CollisionWithCeiling;

        
    }




}
