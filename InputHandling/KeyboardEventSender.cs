using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ECD_Engine.InputHandling
{
    public class KeyboardEventManager : GameComponent
    {
        private readonly List<Keys> pressedKeys = new List<Keys>();
        public List<Keys> PressedKeys => pressedKeys;
        private double intervalP, intervalR;


        public KeyboardEventManager(Game game)
            : base(game)
        { }

        public override void Update(GameTime gameTime)
        {
            intervalR += gameTime.ElapsedGameTime.TotalMilliseconds;
            intervalP += gameTime.ElapsedGameTime.TotalMilliseconds;

            base.Update(gameTime);

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (Keyboard.GetState().IsKeyDown(key) && intervalP > 150)
                {
                    pressedKeys.Add(key);
                    KeyPressed?.Invoke(key);
                    intervalP = 0;
                }
                if (Keyboard.GetState().IsKeyUp(key) && intervalR > 200)
                {
                    if (pressedKeys.Contains(key))
                    {
                        pressedKeys.Remove(key);

                        KeysRealeased?.Invoke(key);
                        intervalR = 0;

                    }
                }
            }
        }

        public event Action<Keys> KeyPressed;
        public event Action<Keys> KeysRealeased;
    }
}
}
