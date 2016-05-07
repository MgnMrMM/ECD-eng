using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECD_Engine.Components;

namespace ECD_Engine.Extensions
{
    public static class Extensions
    {
        public static T GetComponent<T>(this List<Component> source) //since each entity can have only one component of certain type
            where T : class

        {
            return source.OfType<T>().Select(item => item).FirstOrDefault();
        }
    }
}
