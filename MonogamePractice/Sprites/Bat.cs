using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePractice.Sprites
{
    public class Bat : Sprite
    {        
        public Vector2? StartPosition = null;

        public Bat(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {            
            Speed.Y = 5;
            
            if (StartPosition == null)
            {
                StartPosition = Position;
            }
            
            if (Keyboard.GetState().IsKeyDown(Input.Up))
            {
                Velocity.Y = -Speed.Y;
            }
            
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
            {
                Velocity.Y = Speed.Y;
            }

            Position += Velocity;

            Position.Y = MathHelper.Clamp(Position.Y, 0, Game1.GameWindowHeight - Texture.Height);

            Velocity = Vector2.Zero;
        }
    }
}
