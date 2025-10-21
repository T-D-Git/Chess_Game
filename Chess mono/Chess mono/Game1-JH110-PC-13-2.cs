
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
        Player[] Player1 = new Player[1];
        Player[] Player2 = new Player[2];
        private Texture2D tile;
        private bool whiteTurn = true;

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

            Queen[0] = new Queen(Content.Load<Texture2D>("White queen"), new Rectangle(), true, true, new Vector2(110, 110));

            Knight[0] = new Knight(Content.Load<Texture2D>("Black knight"), new Rectangle(), true, true, new Vector2(210, 210));

            Player1[0] = new Player(true);

            Player2[1] = new Player(false);
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


            Queen[0].Update(gameTime);
            Knight[0].Update(gameTime);
            base.Update(gameTime);
        }

        private void MakeMove(bool whiteTurn_)
        {
            whiteTurn_ = !whiteTurn_;
        }

        public bool isLegalMove(Vector2 tempPos, string chessPiece, Vector2 temPos2, bool isWhite, Piece piece)
        {
            Vector2 changePos = new Vector2(temPos2.X - tempPos.X, temPos2.Y - tempPos.Y);

            switch (chessPiece)
            {
                case ("Knight"):
                    {
                        if (changePos == new Vector2(-100, -200) || changePos == new Vector2(-100, 200) || changePos == new Vector2(100, 200) || changePos == new Vector2(100, -200) || changePos == new Vector2(-200, -100) || changePos == new Vector2(-200, 100) || changePos == new Vector2(200, 100) || changePos == new Vector2(200, -100))
                        {
                            if(piece.isWhite && whiteTurn)
                            {
                                whiteTurn = !whiteTurn;
                                return true;
                            }
                            else if(piece.isWhite && !whiteTurn)
                            {
                                whiteTurn = !whiteTurn;
                                return false;
                            }
                            else if(!piece.isWhite && !whiteTurn)
                            {
                                return true;
                            }
                            else
                            {
                                return 
                            }


                            whiteTurn = !whiteTurn;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case ("Queen"):
                    {
                        if (changePos == new Vector2(100, 100) || changePos == new Vector2(200, 200) || changePos == new Vector2(300, 300) || changePos == new Vector2(400, 400) || changePos == new Vector2(500, 500) || changePos == new Vector2(600, 600) || changePos == new Vector2(700, 700) || changePos == new Vector2(800, 800) || changePos == new Vector2(-100, 100) || changePos == new Vector2(-200, 200) || changePos == new Vector2(-300, 300) || changePos == new Vector2(-400, 400) || changePos == new Vector2(-500, 500) || changePos == new Vector2(-600, 600) || changePos == new Vector2(-700, 700) || changePos == new Vector2(-800, 800) || changePos == new Vector2(100, -100) || changePos == new Vector2(200, -200) || changePos == new Vector2(300, -300) || changePos == new Vector2(400, -400) || changePos == new Vector2(500, -500) || changePos == new Vector2(600, -600) || changePos == new Vector2(700, -700) || changePos == new Vector2(800, -800) || changePos == new Vector2(-100, -100) || changePos == new Vector2(-200, -200) || changePos == new Vector2(-300, -300) || changePos == new Vector2(-400, -400) || changePos == new Vector2(-500, -500) || changePos == new Vector2(-600, -600) || changePos == new Vector2(-700, -700) || changePos == new Vector2(-800, -800))
                        {
                            return true;
                            // bishop code
                        }
                        else if (changePos == new Vector2(0, 100) || changePos == new Vector2(0, 200) || changePos == new Vector2(0, 300) || changePos == new Vector2(0, 400) || changePos == new Vector2(0, 500) || changePos == new Vector2(0, 600) || changePos == new Vector2(0, 700) || changePos == new Vector2(0, 800) || changePos == new Vector2(0, -100) || changePos == new Vector2(0, -200) || changePos == new Vector2(0, -300) || changePos == new Vector2(0, -400) || changePos == new Vector2(0, -500) || changePos == new Vector2(0, -600) || changePos == new Vector2(0, -700) || changePos == new Vector2(0, -800) || changePos == new Vector2(100, 0) || changePos == new Vector2(200, 0) || changePos == new Vector2(300, 0) || changePos == new Vector2(400, 0) || changePos == new Vector2(500, 0) || changePos == new Vector2(600, 0) || changePos == new Vector2(700, 0) || changePos == new Vector2(800, 0) || changePos == new Vector2(-100, 0) || changePos == new Vector2(-200, 0) || changePos == new Vector2(-300, 0) || changePos == new Vector2(-400, 0) || changePos == new Vector2(-500, 0) || changePos == new Vector2(-600, 0) || changePos == new Vector2(-700, 0) || changePos == new Vector2(-800, 0))
                        {
                            return true;
                            // rook code
                        }
                        else
                        {
                            return false;
                        }
                    }

                default:
                    {
                        return false;
                    }
            }
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

            Queen[0].Draw(spriteBatch);
            Knight[0].Draw(spriteBatch);
            spriteBatch.End();
           

            base.Draw(gameTime);
        }
    }
}
