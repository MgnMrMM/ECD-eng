using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ECD_Engine.Components;

namespace ECD_Engine
{
    public enum ShapeType
    {
        O = 2,
        L = 0,
        J = 1,
        I = 3,
        S = 4,
        Z = 5,
        T = 6
    }

    public class EntityFactory
    {
        public static int NextID { get; set; }

        private SortedList<int, List<Component>> entitySortedList;

        public EntityFactory(SortedList<int, List<Component>> entitySortedList)
        {
            this.entitySortedList = entitySortedList;
        }

        private List<Component> createBrick()
        {
            return new List<Component> {new DrawAble(), new BoxCollider(), new Position(), new MoveAble()};
        }

        private List<Component> Createshape(ShapeType shapeType)
        {
            
        }

    }
}
