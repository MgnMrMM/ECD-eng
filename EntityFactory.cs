using System.Collections.Generic;
using ECD_Engine.Components;
using ECD_Engine.Patterns;

namespace ECD_Engine
{
    public class EntityFactory : Singleton<EntityFactory>
    {
        public static int NextID { get; set; }
        public static int PlayerID { get; set; }

        private SortedList<int, List<Component>> entitySortedList;

        public EntityFactory()
        {
        }

        public EntityFactory(SortedList<int, List<Component>> entitySortedList)
        {
            this.entitySortedList = entitySortedList;
        }

        private List<Component> createLargeRock()
        {
            return new List<Component> {new DrawAble(), new BoxCollider(), new Position(), new MoveAble(), new Tag("LargeRock")};
        }

        private List<Component> createMediumRock()
        {
            return new List<Component> { new DrawAble(), new BoxCollider(), new Position(), new MoveAble(), new Tag("MediumRock") };
        }

        private List<Component> createSmallmRock()
        {
            return new List<Component> { new DrawAble(), new BoxCollider(), new Position(), new MoveAble(), new Tag("SmallRock") };
        }

        private List<Component> createPlayer()
        {
            return new List<Component> { new DrawAble(), new BoxCollider(), new Position(), new MoveAble(), new Tag("Player"), new Gun(), new PlayerControlled() };
        }

        private List<Component> createEnemyShip()
        {
            return new List<Component> { new DrawAble(), new BoxCollider(), new Position(), new MoveAble(), new Tag("Player"), new Gun(), new AiControlled() };
        }

        public void GetNextID(List<int> entitiesList)
        {
            for (int i = 0; i < entitiesList.Count + 1; i++)
            {
                if (entitiesList.TrueForAll(x => x != i))
                    NextID = i;
            }
        }

    }
}
