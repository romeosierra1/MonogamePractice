using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogamePractice.Sprites
{
    public class Sprite : Component
    {
        protected Texture2D _texture;
        public Vector2 Postition { get; set; }

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)Postition.X, (int)Postition.Y, _texture.Width, _texture.Height); }
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Postition, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
