using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogamePractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePractice.Sprites
{
    public class Sprite
    {
        protected Texture2D _texture;

        public Vector2 Postition;
        public float Speed;
        public Color Color = Color.White;

        public Input Input;

        public bool IsRemoved;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Postition.X, (int)Postition.Y, _texture.Width, _texture.Height);
            }
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Postition, Color);
        }
    }
}
