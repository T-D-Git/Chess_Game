
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;

namespace Chess_mono
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;       

        private TimeSpan whiteTimeRemaining;
        private TimeSpan blackTimeRemaining;

        private SoundEffect moveSoundEffect;
        private SoundEffect captureSoundEffect;
        private SoundEffect checkSoundEffect;
        private SoundEffect checkMateSoundEffect;
        private SoundEffect castleSoundEffect;

        private Texture2D tile;
        private Texture2D CheckMate;
        private Texture2D Check;
        private Texture2D WhiteW;
        private Texture2D BlackW;

        public bool isWhiteTurn = true;        

        bool isCheck = false;
        bool isCheckMate = false;

        private bool isPromoting = false;
        private Piece promotingPawn = null;
        private Texture2D queenTexture, rookTexture, bishopTexture, knightTexture;
        private Rectangle promotionArea;

        private Texture2D singlePlayerButtonTexture;
        private Texture2D multiplayerButtonTexture;
        private Texture2D easyButtonTexture;
        private Texture2D mediumButtonTexture;
        private Texture2D hardButtonTexture;
        private Texture2D backToMainMenu;
        private Rectangle singlePlayerButtonRect;
        private Rectangle multiplayerButtonRect;
        private Rectangle easyButtonRect;
        private Rectangle mediumButtonRect;
        private Rectangle hardButtonRect;
        private Rectangle backToMainMenuRect;
        private bool singlePlayer = false;



        private Texture2D bulletButtonTexture, blitzButtonTexture, rapidButtonTexture;
        private Rectangle bulletButtonRect, blitzButtonRect, rapidButtonRect;

        private Texture2D drawButton, resignButton, drawResult;
        private Rectangle drawRect, resignRect, drawResultRect;


        // Increment values for Bullet and Blitz
        private TimeSpan bulletIncrement = TimeSpan.FromSeconds(1);
        private TimeSpan blitzIncrement = TimeSpan.FromSeconds(2);


        private List<Vector2> legalMovesForSelectedPiece = new List<Vector2>();

        public static List<Piece> pieces = new List<Piece>();
        public MouseState mouseState;
        public Point mousePoint;
        public bool isHovered = false;
        public Vector2 startPos = new Vector2();
        public Vector2 endPos = new Vector2();
        Piece currentPiece = null;

        bool whiteWins = false;
        bool blackWins = false;

        public enum GameState
        {
            Menu,
            SinglePlayer,
            Multiplayer,
            Playing,
            EndGame,
            Draw
        }

        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }

        public enum MultiplayerMode
        {
            None,
            Bullet,
            Blitz,
            Rapid
        }


        MultiplayerMode currentMultiplayerMode = MultiplayerMode.None;
        GameState currentState = GameState.Menu;
        Difficulty currentDifficulty = Difficulty.Easy;
       


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        private void ResetGame()
        {
            isWhiteTurn = true;
            isCheck = false;
            isCheckMate = false;
            whiteWins = false;
            blackWins = false;
            singlePlayer = false;
            isPromoting = false;
            promotingPawn = null;

            currentState = GameState.Menu;
            currentMultiplayerMode = MultiplayerMode.None;
            currentDifficulty = Difficulty.Easy;

            pieces.Clear();
            InitializePieces();

            // Reset the timers if you have them
            whiteTimeRemaining = TimeSpan.Zero;
            blackTimeRemaining = TimeSpan.Zero;
        }

        private void InitializePieces()
        {
            for (int i = 0; i < 8; i++)
            {
                pieces.Add(new Pawn(Content.Load<Texture2D>("White pawn"), new Rectangle(), true, true, new Vector2(i * 100 + 10, 610), "Pawn"));
            }

            pieces.Add(new Knight(Content.Load<Texture2D>("White knight"), new Rectangle(), true, true, new Vector2(110, 710), "Knight"));

            pieces.Add(new Knight(Content.Load<Texture2D>("White knight"), new Rectangle(), true, true, new Vector2(610, 710), "Knight"));

            pieces.Add(new Queen(Content.Load<Texture2D>("White queen"), new Rectangle(), true, true, new Vector2(310, 710), "Queen"));

            pieces.Add(new King(Content.Load<Texture2D>("White king"), new Rectangle(), true, true, new Vector2(410, 710), "King", false));

            pieces.Add(new Rook(Content.Load<Texture2D>("White rook"), new Rectangle(), true, true, new Vector2(10, 710), "Rook", false));

            pieces.Add(new Rook(Content.Load<Texture2D>("White rook"), new Rectangle(), true, true, new Vector2(710, 710), "Rook", false));

            pieces.Add(new Bishop(Content.Load<Texture2D>("White bishop"), new Rectangle(), true, true, new Vector2(210, 710), "Bishop"));

            pieces.Add(new Bishop(Content.Load<Texture2D>("White bishop"), new Rectangle(), true, true, new Vector2(510, 710), "Bishop"));

            //black pieces

            for (int i = 0; i < 8; i++)
            {
                pieces.Add(new Pawn(Content.Load<Texture2D>("Black pawn"), new Rectangle(), true, false, new Vector2(i * 100 + 10, 110), "Pawn"));
            }

            pieces.Add(new Queen(Content.Load<Texture2D>("Black queen"), new Rectangle(), true, false, new Vector2(310, 10), "Queen"));

            pieces.Add(new Knight(Content.Load<Texture2D>("Black knight"), new Rectangle(), true, false, new Vector2(110, 10), "Knight"));

            pieces.Add(new Knight(Content.Load<Texture2D>("Black knight"), new Rectangle(), true, false, new Vector2(610, 10), "Knight"));

            pieces.Add(new King(Content.Load<Texture2D>("Black king"), new Rectangle(), true, false, new Vector2(410, 10), "King", false));

            pieces.Add(new Rook(Content.Load<Texture2D>("Black rook"), new Rectangle(), true, false, new Vector2(10, 10), "Rook", false));

            pieces.Add(new Rook(Content.Load<Texture2D>("Black rook"), new Rectangle(), true, false, new Vector2(710, 10), "Rook", false));

            pieces.Add(new Bishop(Content.Load<Texture2D>("Black bishop"), new Rectangle(), true, false, new Vector2(210, 10), "Bishop"));

            pieces.Add(new Bishop(Content.Load<Texture2D>("Black bishop"), new Rectangle(), true, false, new Vector2(510, 10), "Bishop"));

        }

        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {           
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tile = Content.Load<Texture2D>("White square");

            Check = Content.Load<Texture2D>("Check image");
            CheckMate = Content.Load<Texture2D>("Checkmate image");

            bulletButtonTexture = Content.Load<Texture2D>("Bullet");
            blitzButtonTexture = Content.Load<Texture2D>("Blitz");
            rapidButtonTexture = Content.Load<Texture2D>("Rapid");

            moveSoundEffect = Content.Load<SoundEffect>("Chess move");
            captureSoundEffect = Content.Load<SoundEffect>("Chess capture");
            checkSoundEffect = Content.Load<SoundEffect>("Chess check");
            checkMateSoundEffect = Content.Load<SoundEffect>("Chess checkmate");
            castleSoundEffect = Content.Load<SoundEffect>("Chess castle");

            drawButton = Content.Load<Texture2D>("Draw");
            resignButton = Content.Load<Texture2D>("Resign");
            drawResult = Content.Load<Texture2D>("DrawResult");

            drawRect = new Rectangle(875, 650, 100, 100);
            resignRect = new Rectangle(1025, 650, 100, 100);
            drawResultRect = new Rectangle(375, 325, 450, 150);
            backToMainMenuRect = new Rectangle(10, 650, 699, 141);


            WhiteW = Content.Load<Texture2D>("White wins");
            BlackW = Content.Load<Texture2D>("Black wins");
            backToMainMenu = Content.Load<Texture2D>("backToMenu");
        
            bulletButtonRect = new Rectangle(290, 220, 200, 123);
            blitzButtonRect = new Rectangle(500, 220, 200, 123);
            rapidButtonRect = new Rectangle(710, 220, 200, 123);

            queenTexture = Content.Load<Texture2D>("Queen promotion");
            rookTexture = Content.Load<Texture2D>("Rook promotion");
            bishopTexture = Content.Load<Texture2D>("Bishop promotion");
            knightTexture = Content.Load<Texture2D>("Knight promotion");

            singlePlayerButtonTexture = Content.Load<Texture2D>("Singleplayer");
            multiplayerButtonTexture = Content.Load<Texture2D>("Multiplayer");
            easyButtonTexture = Content.Load<Texture2D>("Easy");
            mediumButtonTexture = Content.Load<Texture2D>("Medium");
            hardButtonTexture = Content.Load<Texture2D>("Hard");

            singlePlayerButtonRect = new Rectangle(400, 350, 400, 40); 
            multiplayerButtonRect = new Rectangle(400, 450, 400, 40);
            easyButtonRect = new Rectangle(500, 300, 200, 50);
            mediumButtonRect = new Rectangle(500, 400, 200, 50);
            hardButtonRect = new Rectangle(500, 500, 200, 50);

            spriteFont = Content.Load<SpriteFont>("MyFont");

            InitializePieces();
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

            if (currentState == GameState.Menu)
            {
                if (singlePlayerButtonRect.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
                {
                    currentState = GameState.SinglePlayer;
                }
                else if (multiplayerButtonRect.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
                {
                    currentState = GameState.Multiplayer; 
                }
            }
            else if (currentState == GameState.SinglePlayer)
            {
                if (easyButtonRect.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
                {
                    currentDifficulty = Difficulty.Easy;
                    singlePlayer = true;
                    currentState = GameState.Playing;
                }
                else if (mediumButtonRect.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
                {
                    currentDifficulty = Difficulty.Medium;
                    singlePlayer = true;
                    currentState = GameState.Playing;
                }
                else if (hardButtonRect.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
                {
                    currentDifficulty = Difficulty.Hard;
                    singlePlayer = true;
                    currentState = GameState.Playing;
                }
            }
            else if (currentState == GameState.Multiplayer)
            {
                if (bulletButtonRect.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
                {
                    currentMultiplayerMode = MultiplayerMode.Bullet;
                    whiteTimeRemaining = TimeSpan.FromMinutes(1);
                    blackTimeRemaining = TimeSpan.FromMinutes(1);
                    currentState = GameState.Playing;
                }
                else if (blitzButtonRect.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
                {
                    currentMultiplayerMode = MultiplayerMode.Blitz;
                    whiteTimeRemaining = TimeSpan.FromMinutes(3);
                    blackTimeRemaining = TimeSpan.FromMinutes(3);
                    currentState = GameState.Playing;
                }
                else if (rapidButtonRect.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
                {
                    currentMultiplayerMode = MultiplayerMode.Rapid;
                    whiteTimeRemaining = TimeSpan.FromMinutes(10);
                    blackTimeRemaining = TimeSpan.FromMinutes(10);
                    currentState = GameState.Playing;
                }
            }
            if (currentState == GameState.Playing && currentMultiplayerMode != MultiplayerMode.None)
            {
                if (isWhiteTurn)
                {
                    whiteTimeRemaining -= gameTime.ElapsedGameTime;
                }
                else
                {
                    blackTimeRemaining -= gameTime.ElapsedGameTime;
                }

                // Ensure time doesn't go negative
                whiteTimeRemaining = TimeSpan.FromTicks(Math.Max(0, whiteTimeRemaining.Ticks));
                blackTimeRemaining = TimeSpan.FromTicks(Math.Max(0, blackTimeRemaining.Ticks));

                if (whiteTimeRemaining <= TimeSpan.Zero)
                {
                    blackWins = true;
                    currentState = GameState.EndGame;
                }
                else if (blackTimeRemaining <= TimeSpan.Zero)
                {
                    whiteWins = true;
                    currentState = GameState.EndGame;
                }               
            }
            if(currentState == GameState.Playing)
            {
                if (drawRect.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
                {
                    currentState = GameState.Draw;
                }
                if (resignRect.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (isWhiteTurn)
                    {
                        blackWins = true;
                    }
                    else
                    {
                        whiteWins = true;
                    }
                    currentState = GameState.EndGame;
                }
            }

            foreach (Piece piece in pieces)
            {
                piece.rectangle = piece.updateRec();
            }

            mouseState = Mouse.GetState();
            mousePoint = new Point(mouseState.X, mouseState.Y);

            if ((currentState == GameState.Draw || currentState == GameState.EndGame) && backToMainMenuRect.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
            {
                ResetGame();
            }

            //legal move maiking logic, drag and drop
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
                            legalMovesForSelectedPiece = Move.getLegalMovesForPiece(piece, pieces, isWhiteTurn);
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
                endPos.X = (mouseState.X - (mouseState.X % 100)) + 10;
                endPos.Y = (mouseState.Y - (mouseState.Y % 100)) + 10;               
                if (mouseState.LeftButton == ButtonState.Released && (MoveMethods.legalMove(startPos, currentPiece.name, endPos, currentPiece.isWhite, isWhiteTurn, pieces, currentPiece)))
                {
                    bool castle = false;
                    Piece pieceAtTarget = MoveMethods.GetPieceAtPosition(endPos, pieces);
                    if (MoveMethods.castling(startPos, endPos, isWhiteTurn, pieces, currentPiece))
                    {
                        MoveMethods.castle(startPos, endPos, isWhiteTurn, pieces, currentPiece);
                        castle = true;
                    }
                    else
                    {
                        
                        pieces.Remove(pieceAtTarget);

                        currentPiece.position.X = endPos.X;
                        currentPiece.position.Y = endPos.Y;
                    }

                    //if pawn is in position to promote record position of the pawn and set can promote to true
                    if (MoveMethods.canPromote(currentPiece))
                    {
                        isPromoting = true;
                        promotingPawn = currentPiece;
                        // Set the promotion area (covering the whole screen for now)
                        promotionArea = new Rectangle(0, 0, 800, 800);
                    }

                    if(currentPiece is Rook)
                    {
                        Rook.HasMoved((Rook)currentPiece);
                    }
                    if (currentPiece is King)
                    {
                        King.HasMoved((King)currentPiece);
                    }
                    isWhiteTurn = !isWhiteTurn;


                    //time increment logic
                    if (currentMultiplayerMode == MultiplayerMode.Bullet)
                    {
                        if (isWhiteTurn) blackTimeRemaining += bulletIncrement;
                        else whiteTimeRemaining += bulletIncrement;
                    }
                    else if (currentMultiplayerMode == MultiplayerMode.Blitz)
                    {
                        if (isWhiteTurn) blackTimeRemaining += blitzIncrement;
                        else whiteTimeRemaining += blitzIncrement;
                    }
                    currentPiece = null;
                    isHovered = false;

                    //sound logic
                    if (MoveMethods.checkMate(isWhiteTurn, pieces))
                    {
                        checkMateSoundEffect.Play();
                    }
                    else if (castle)
                    {
                        castleSoundEffect.Play();
                    }
                    else if (pieceAtTarget != null)
                    {
                        captureSoundEffect.Play();
                    }
                    else if(MoveMethods.detCheck(pieces, isWhiteTurn))
                    {
                        checkSoundEffect.Play();
                    }
                    else
                    {
                        moveSoundEffect.Play();
                    }


                    

                    if (MoveMethods.checkMate(isWhiteTurn, pieces))
                    {
                        isCheckMate = true;
                        if (!isWhiteTurn)
                        {
                            whiteWins = true;                            
                        }
                        else
                        {
                            blackWins = true;
                        }
                        currentState = GameState.EndGame;
                    }
                    if (Move.getLegalMoves(isWhiteTurn, pieces).Count == 0 && !MoveMethods.detCheck(pieces, isWhiteTurn))
                    {                        
                        currentState = GameState.Draw;
                    }

                    //cut
                    if (currentState == GameState.EndGame || currentState == GameState.Draw)
                    {
                        return;
                    }

                    if (MoveMethods.detCheck(pieces, isWhiteTurn) && !MoveMethods.checkMate(isWhiteTurn, pieces))
                    {
                        isCheck = true;
                    }
                    else
                    {
                        isCheck = false;
                    }

                    if (singlePlayer)
                    {

                        if (!isWhiteTurn && !MoveMethods.checkMate(isWhiteTurn, pieces))
                        {
                            int depth;
                            switch (currentDifficulty)
                            {
                                case Difficulty.Easy:
                                    depth = 1;
                                    break;
                                case Difficulty.Medium:
                                    depth = 2;
                                    break;
                                case Difficulty.Hard:
                                    depth = 3;
                                    break;
                                default:
                                    depth = 3;
                                    break;
                            }


                            Move computerMove = minimax.findBestMove(pieces, depth, isWhiteTurn);
                            if (computerMove != null)
                            {
                                // Update the game state according to the best move


                                // Capture the piece at the target position if one exists
                                Piece capturedPiece = MoveMethods.GetPieceAtPosition(computerMove.destination, pieces);
                                if (capturedPiece != null)
                                {
                                    captureSoundEffect.Play();
                                    pieces.Remove(capturedPiece);
                                }
                                Vector2 originalPos = computerMove.piece.position;
                                computerMove.piece.position = computerMove.destination;

                                isWhiteTurn = !isWhiteTurn;

                                if (MoveMethods.checkMate(isWhiteTurn, pieces))
                                {
                                    isCheckMate = true;
                                    checkMateSoundEffect.Play();
                                }
                                else
                                {
                                    isCheckMate = false;
                                }

                                if (MoveMethods.detCheck(pieces, isWhiteTurn))
                                {
                                    isCheck = true;
                                    checkSoundEffect.Play();
                                }
                                else
                                {
                                    isCheck = false;
                                    if (capturedPiece == null) moveSoundEffect.Play();
                                }
                            }
                        }
                    }
                }
                else
                {
                    currentPiece.position = startPos;
                }
                currentPiece = null;
            }

            isHovered = false;

            if (isPromoting)
            {
                // Handle promotion logic
                MouseState mouseState = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Vector2 clickPosition = new Vector2(mouseState.X, mouseState.Y);
                    if (promotionArea.Contains(clickPosition))
                    {
                        PromotePawn(clickPosition, promotingPawn);
                        isPromoting = false;
                    }
                }
                return; // Skip the rest of the update logic if promoting
            }

            if (mouseState.LeftButton == ButtonState.Released)
            {
                legalMovesForSelectedPiece.Clear();
            }

            base.Update(gameTime);

        }



        private void PromotePawn(Vector2 clickPosition, Piece piece)
        {
            string promotionChoice = "";
            if (new Rectangle(0, 0, 400, 400).Contains(clickPosition)) promotionChoice = "Queen";
            else if (new Rectangle(400, 400, 400, 400).Contains(clickPosition)) promotionChoice = "Rook";
            else if (new Rectangle(0, 400, 400, 400).Contains(clickPosition)) promotionChoice = "Bishop";
            else if (new Rectangle(400, 0, 400, 400).Contains(clickPosition)) promotionChoice = "Knight";

            Vector2 position = piece.position;
            bool isWhite = piece.isWhite;
            pieces.Remove(piece);

       
            switch (promotionChoice)
            {
                case "Queen":
                    if (isWhite)
                    {
                        pieces.Add(new Queen(Content.Load<Texture2D>("White queen"), new Rectangle(), true, true, position, "Queen"));
                    }
                    else
                    {
                        pieces.Add(new Queen(Content.Load<Texture2D>("Black queen"), new Rectangle(), true, false, position, "Queen"));
                    }
                    break;
                case "Rook":
                    if (isWhite)
                    {
                        pieces.Add(new Rook(Content.Load<Texture2D>("White rook"), new Rectangle(), true, true, position, "Rook", false));
                    }
                    else
                    {
                        pieces.Add(new Rook(Content.Load<Texture2D>("Black rook"), new Rectangle(), true, false, position, "Rook", false));
                    }
                    break;
                case "Bishop":
                    if (isWhite)
                    {
                        pieces.Add(new Bishop(Content.Load<Texture2D>("White bishop"), new Rectangle(), true, true, position, "Bishop"));
                    }
                    else
                    {
                        pieces.Add(new Bishop(Content.Load<Texture2D>("Black bishop"), new Rectangle(), true, false, position, "Bishop"));}
                    break;
                case "Knight":
                    if (isWhite)
                    {
                        pieces.Add(new Knight(Content.Load<Texture2D>("White knight"), new Rectangle(), true, true, position, "Knight"));
                    }
                    else
                    {
                        pieces.Add(new Knight(Content.Load<Texture2D>("Black knight"), new Rectangle(), true, false, position, "Knight"));
                    }
                    break;
            }

            promotingPawn = null;
        }


        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PeachPuff);
            spriteBatch.Begin();
            if (currentState == GameState.Menu)
            {
                spriteBatch.Draw(singlePlayerButtonTexture, singlePlayerButtonRect, Color.White);
                spriteBatch.Draw(multiplayerButtonTexture, multiplayerButtonRect, Color.White);
            }
            else if (currentState == GameState.SinglePlayer)
            {
                spriteBatch.Draw(easyButtonTexture, easyButtonRect, Color.White);
                spriteBatch.Draw(mediumButtonTexture, mediumButtonRect, Color.White);
                spriteBatch.Draw(hardButtonTexture, hardButtonRect, Color.White);
            }
            else if(currentState == GameState.Multiplayer)
            {                
                spriteBatch.Draw(bulletButtonTexture, bulletButtonRect, Color.White);
                spriteBatch.Draw(blitzButtonTexture, blitzButtonRect, Color.White);
                spriteBatch.Draw(rapidButtonTexture, rapidButtonRect, Color.White);                            
            }
            else if (currentState == GameState.Draw)
            {
                spriteBatch.Draw(drawResult, drawResultRect, Color.White);
            }
            
            if (currentState == GameState.Playing)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Vector2 tilePos = new Vector2(i * 100, j * 100);
                        Color tileColor = ((i + j) % 2 == 0) ? Color.SandyBrown : Color.SaddleBrown;

                        Vector2 piecePos = new Vector2(tilePos.X + 10, tilePos.Y + 10);

                        // Check if the current tile is a legal move
                        if (currentPiece != null && legalMovesForSelectedPiece.Contains(piecePos))
                        {
                            tileColor = Color.Yellow;
                        }

                        spriteBatch.Draw(tile, tilePos, tileColor);
                    }
                }
                foreach (Piece piece in pieces)
                {
                    if (piece.Alive) // Draw only if the piece is alive
                    {
                        piece.Draw(spriteBatch);
                    }
                }

                spriteBatch.Draw(drawButton, drawRect, Color.White);
                spriteBatch.Draw(resignButton, resignRect, Color.White);

                if (currentState == GameState.Playing && currentMultiplayerMode != MultiplayerMode.None)
                {
                    string whiteTimeText = whiteTimeRemaining.ToString(@"mm\:ss");
                    string blackTimeText = blackTimeRemaining.ToString(@"mm\:ss");

                    spriteBatch.DrawString(spriteFont, whiteTimeText, new Vector2(810, 10), Color.White);
                    spriteBatch.DrawString(spriteFont, blackTimeText, new Vector2(1110, 10), Color.Black);                    
                }

                if (isPromoting)
                {
                    spriteBatch.Draw(queenTexture, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(rookTexture, new Vector2(400, 400), Color.White);
                    spriteBatch.Draw(bishopTexture, new Vector2(0, 400), Color.White);
                    spriteBatch.Draw(knightTexture, new Vector2(400, 0), Color.White);
                }

                if (isCheck && !isCheckMate)
                {
                    spriteBatch.Draw(Check, new Vector2(900, 300));                   
                }

                if (isCheckMate)
                {
                    spriteBatch.Draw(CheckMate, new Vector2(900, 300));
                }
            }
            if (currentState == GameState.EndGame)
            {
                if (whiteWins)
                {
                    spriteBatch.Draw(WhiteW, new Vector2(400, 200), Color.White); // Draw White Wins image
                }
                else if (blackWins)
                {
                    spriteBatch.Draw(BlackW, new Vector2(400, 200), Color.White); // Draw Black Wins image
                }
            }
            if (currentState == GameState.Draw || currentState == GameState.EndGame)
            {
                spriteBatch.Draw(backToMainMenu, backToMainMenuRect, Color.White);
            }
            spriteBatch.End();
           

            base.Draw(gameTime);
        }
    }
}
