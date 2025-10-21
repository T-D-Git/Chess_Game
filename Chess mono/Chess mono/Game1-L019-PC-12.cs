using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chess_mono
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Knight[] Knight = new Knight[1];
        Queen[] Queen = new Queen[1];
        private Texture2D tile;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            IsMouseVisible = true;

        }


        protected override void Initialize()
        {           
            base.Initialize();
        }

        protected override void LoadContent()
        {           
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tile = Content.Load<Texture2D>("White square");           

            Knight[0] = new Knight(Content.Load<Texture2D>("White knight"), new Rectangle(), true, new Vector2(619,719));
            Queen[0] = new Queen(Content.Load<Texture2D>("White queen"), new Rectangle(), true, new Vector2(287, 704));

        }
      
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Knight[0].Update(gameTime);
            Queen[0].Update(gameTime);

            base.Update(gameTime);
        }

      
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            for (int i = 0; i<8; i++)
            {
                for (int j = 0; j<8; j++)
                {
                    if (i % 2 == 0 && j % 2 ==1 || i % 2 == 1 && j % 2 == 0)
                    {
                        spriteBatch.Draw(tile, new Vector2(i * 100, j * 100), Color.SaddleBrown);
                    }
                    else
                    {
                        spriteBatch.Draw(tile, new Vector2(i * 100, j * 100), Color.SandyBrown);
                    }
                }
                
            }
            Knight[0].Draw(spriteBatch);
            Queen[0].Draw(spriteBatch);

            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
