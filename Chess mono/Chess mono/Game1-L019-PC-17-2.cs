
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Chess_mono
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GridObject[,] boardArray;

        Knight[] Knight = new Knight[1];
        Queen[] Queen = new Queen[1];
        Player[] Player1 = new Player[1];
        Player[] Player2 = new Player[2];
        private Texture2D tile;
        public bool isWhiteTurn = true;

        public static List<Piece> pieces = new List<Piece>();


        public Texture2D texture;

        public Vector2 position;

        public bool Alive;

        public Rectangle rectangle;

        public MouseState mouseState;

        public Point mousePoint;

        public bool isHovered = false;

        public bool isClicked;

        public Vector2 startPos = new Vector2();

        public bool legalMove = false;

        public string chessPiece;

        public Vector2 endPos = new Vector2();

        public bool isWhite;

        Piece currentPiece = null;

        



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

            //for (int x = 0; x < 8; x++)
            //{
            //    for (int y = 0; y < 9; y++)
            //    {
            //        boardArray[x, y] = new GridObject(x * 100, y * 100);
            //    }
            //}

        }

        protected override void LoadContent()
        {           
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tile = Content.Load<Texture2D>("White square");

           

            //Player1[0] = new Player(true);

            //Player2[1] = new Player(false);

            pieces.Add(new Queen(Content.Load<Texture2D>("White queen"), new Rectangle(), true, true, new Vector2(110, 110)));

            pieces.Add(new Knight(Content.Load<Texture2D>("Black knight"), new Rectangle(), true, false, new Vector2(210, 210)));
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

            //foreach (Piece piece in pieces)
            //{
            //    piece.Update(gameTime, ref isWhiteTurn);

            //    // Adjust the loop index to account for the removed element



            //    if (piece.Alive == false)
            //    {
            //        pieces.Remove(piece);
            //    }
            //}

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Piece piece in pieces)
            {
                piece.rectangle = piece.updateRec();
            }

            // TODO: Add your update logic here
            mouseState = Mouse.GetState();
            mousePoint = new Point(mouseState.X, mouseState.Y);



            if (mouseState.LeftButton == ButtonState.Pressed)
            {                
                
                if (currentPiece == null)
                {
                    // Find the piece that is being clicked on
                    foreach (Piece piece in pieces)
                    {
                        if (piece.rectangle.Contains(mousePoint))
                        {
                            currentPiece = piece;
                            startPos.X = (mouseState.X - (mouseState.X % 100)) + 50 - (40);
                            startPos.Y = (mouseState.Y - (mouseState.Y % 100)) + 50 - (40);
                            break;
                        }
                        
                    }
                }
                

                if (currentPiece != null)
                {
                    // Move the piece with the mouse
                    currentPiece.position = new Vector2(mouseState.X - (currentPiece.texture.Width / 2), mouseState.Y - (currentPiece.texture.Height / 2));
                }
            }
            else if (currentPiece != null)
            {
                endPos.X = (mouseState.X - (mouseState.X % 100)) + 50 - (40);
                endPos.Y = (mouseState.Y - (mouseState.Y % 100)) + 50 - (40);
                pieces.Remove(currentPiece);
                if (mouseState.LeftButton == ButtonState.Released && mouseState.X > -1 && mouseState.X < 801 && mouseState.Y > -1 && mouseState.Y < 801 && currentPiece.isLegalMove(startPos, currentPiece.chessPiece, endPos, currentPiece.isWhite, isWhiteTurn, pieces) && !currentPiece.IsPathBlocked(startPos, endPos, pieces))
                {
                    // The mouse button was released, so drop the piece
                    currentPiece.position.X = (mouseState.X - (mouseState.X % 100)) + 50 - (currentPiece.texture.Width / 2);
                    currentPiece.position.Y = (mouseState.Y - (mouseState.Y % 100)) + 50 - (currentPiece.texture.Height / 2);

                    // If the drop position contains an enemy piece, capture it
                    
                    Piece pieceAtTarget = currentPiece.GetPieceAtPosition(currentPiece.position, pieces);
                    if (pieceAtTarget != null && pieceAtTarget.isWhite != currentPiece.isWhite)
                    {
                        pieces.Remove(pieceAtTarget);
                    }
                    pieces.Add(currentPiece);
                    isWhiteTurn = !isWhiteTurn;
                    currentPiece = null;
                    isHovered = false;
                }
                else
                {
                    currentPiece.position = startPos;
                    pieces.Add(currentPiece);
                }
                currentPiece = null;
            }

            isHovered = false;




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
            foreach (Piece piece in pieces)
            {
                if (piece.Alive) // Draw only if the piece is alive
                {
                    piece.Draw(spriteBatch);
                }
            }

            
            spriteBatch.End();
           

            base.Draw(gameTime);
        }
    }
}
