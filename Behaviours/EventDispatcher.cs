using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ECD_Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECD_Engine.Behaviours
{
    public interface ICommunicate
    {
        void OnEvent();
    }

    public class EventDispatcher 
    {
        private List<Component> components;
        private List<System> behaviours;

        public void Subscribe(Message message, Delegate function)
        { 
            
        }
    }

}
