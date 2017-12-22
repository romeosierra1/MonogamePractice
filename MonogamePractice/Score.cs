using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePractice
{
    public class Score
    {
        public int Score1;
        public int Score2;
        
        private SpriteFont Font;

        public Score(SpriteFont font)
        {
            Font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Game1.Player1 + ": " + Score1.ToString(), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(Font, Game1.Player2 + ": " + Score2.ToString(), new Vector2(700, 10), Color.White);

            spriteBatch.DrawString(Font, "Pong", new Vector2(380, 30), Color.White);
        }

        public void DrawWinner(SpriteBatch spriteBatch, string Winner)
        {
            spriteBatch.DrawString(Font, Winner + " is winner", new Vector2(330, 10), Color.White);
        }
    }
}
