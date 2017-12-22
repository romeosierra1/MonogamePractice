using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogamePractice.Models;
using MonogamePractice.Sprites;
using System;
using System.Collections.Generic;

namespace MonogamePractice
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public static int GameWindowWidth;        
        public static int GameWindowHeight;

        public static Random Random;
        public const int MaxScore = 2;
        public static bool GameOver;
        public const string Player1 = "Player1";
        public const string Player2 = "Player2";
        public static string Winner;
        private Score Score;
        private List<Sprite> Sprites;
        private List<SoundEffect> SoundEffects;
        Texture2D divider;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            GameWindowWidth = graphics.PreferredBackBufferWidth;
            GameWindowHeight = graphics.PreferredBackBufferHeight;

            Random = new Random();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            var batLeftTexture = Content.Load<Texture2D>("images/BatLeft");
            var batRightTexture = Content.Load<Texture2D>("images/BatRight");
            var ballTexture = Content.Load<Texture2D>("images/Ball");

            Score = new Score(Content.Load<SpriteFont>("fonts/SpriteFont1"));
            
            SoundEffects = new List<SoundEffect>();
            SoundEffects.Add(Content.Load<SoundEffect>("sounds/applause1"));
            SoundEffects.Add(Content.Load<SoundEffect>("sounds/click"));
            SoundEffects.Add(Content.Load<SoundEffect>("sounds/ding"));
            
            Sprites = new List<Sprite>()
            {
                new Bat(batLeftTexture)
                {
                    Speed = new Speed(),
                    Position = new Vector2(20, (GameWindowHeight / 2) - (batLeftTexture.Height / 2)),
                    Input = new Input()
                    {
                        Up = Keys.A,
                        Down = Keys.Z
                    }
                },
                new Bat(batRightTexture)
                {
                    Speed = new Speed(),
                    Position = new Vector2(GameWindowWidth - 20 - batRightTexture.Width, (GameWindowHeight / 2) - (batRightTexture.Height / 2)),
                    Input = new Input()
                    {
                        Up = Keys.Up,
                        Down = Keys.Down
                    }
                },
                new Ball(ballTexture)
                {
                    Speed = new Speed(),
                    Position = new Vector2((GameWindowWidth / 2) - (ballTexture.Width / 2), (GameWindowHeight / 2) - (ballTexture.Height / 2)),
                    Score = Score,
                    SoundEffects = SoundEffects
                }
            };
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            
            if (GameOver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    Score.Score1 = 0;
                    Score.Score2 = 0;
                    GameOver = false;
                    
                    foreach (var sprite in Sprites)
                    {
                        if (sprite is Bat)
                        {
                            sprite.Position = (Vector2)(sprite as Bat).StartPosition;
                        }
                    }
                }
            }

            foreach (var sprite in Sprites)
            {
                sprite.Update(gameTime, Sprites);
            }

            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.Clear(Color.RoyalBlue);

            spriteBatch.Begin();

            divider = new Texture2D(GraphicsDevice, 1, 1);
            divider.SetData(new[] { Color.Black });

            spriteBatch.Draw(divider, new Rectangle((GameWindowWidth / 2) - 3, 0, 5, GameWindowHeight), Color.Black);

            foreach (var sprite in Sprites)
            {
                sprite.Draw(spriteBatch);
            }

            Score.Draw(spriteBatch);

            if (GameOver)
            {
                Score.DrawWinner(spriteBatch, Winner);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
