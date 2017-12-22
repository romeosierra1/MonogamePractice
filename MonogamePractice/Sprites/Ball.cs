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
    public class Ball : Sprite
    {
        private Vector2? StartPosition = null;
        private bool IsPlaying;
        public Score Score;

        public Ball(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (StartPosition == null)
            {
                StartPosition = Position;
                Restart();
            }
            
            if (!Game1.GameOver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    IsPlaying = true;
                }
            }

            if (!IsPlaying)
            {
                return;
            }
            
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                {
                    continue;
                }

                if ((this.Velocity.X > 0 && this.IsCollidingLeft(sprite)) ||
                    (this.Velocity.X < 0 && this.IsCollidingRight(sprite)))
                {
                    SoundEffects[1].CreateInstance().Play();
                    this.Velocity.X = -this.Velocity.X;
                }

                if ((this.Velocity.Y > 0 && this.IsCollidingTop(sprite)) ||
                    (this.Velocity.Y < 0 && this.IsCollidingBottom(sprite)))
                {
                    SoundEffects[1].CreateInstance().Play();
                    this.Velocity.Y = -this.Velocity.Y;
                }
            }
            
            if (Position.Y <= 0 || Position.Y + Texture.Height >= Game1.GameWindowHeight)
            {
                Velocity.Y = -Velocity.Y;
            }
            
            if (Position.X + Texture.Width >= Game1.GameWindowWidth)
            {
                SoundEffects[2].CreateInstance().Play();
                Score.Score1++;
                Restart();
                if (Score.Score1 == Game1.MaxScore)
                {
                    Game1.Winner = Game1.Player1;
                    GameOver();
                }
            }
            
            if (Position.X <= 0)
            {
                SoundEffects[2].CreateInstance().Play();
                Score.Score2++;
                Restart();
                if (Score.Score2 == Game1.MaxScore)
                {
                    Game1.Winner = Game1.Player2;
                    GameOver();
                }
            }
            
            Position.X += Velocity.X * Speed.X;
            Position.Y += Velocity.Y * Speed.Y;
        }
        
        private void GameOver()
        {
            SoundEffects[0].CreateInstance().Play();
            Game1.GameOver = true;
        }
        
        private void Restart()
        {
            var direction = Game1.Random.Next(0, 4);
            
            switch (direction)
            {
                case 0:
                    Velocity = new Vector2(1, 1);
                    break;
                case 1:
                    Velocity = new Vector2(1, -1);
                    break;
                case 2:
                    Velocity = new Vector2(-1, -1);
                    break;
                case 3:
                    Velocity = new Vector2(-1, 1);
                    break;
            }

            Position = (Vector2)StartPosition;
            SetSpeed();
            IsPlaying = false;
        }
        
        private void SetSpeed()
        {
            Speed.X = Game1.Random.Next(3, 10);
            Speed.Y = Game1.Random.Next(3, 10);
        }
    }
}
