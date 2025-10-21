using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chess_mono
{
    public static class MoveMethods
    {
        public static bool isLegalMoveWithoutCheck(Vector2 startPos, string chessPiece, Vector2 endPos, bool isWhite, bool isWhiteTurn, List<Piece> pieces)
        {
            Vector2 changePos = new Vector2(endPos.X - startPos.X, endPos.Y - startPos.Y);           
            switch (chessPiece)
            {
                case ("Knight"):
                    {
                        if (changePos == new Vector2(-100, -200) || changePos == new Vector2(-100, 200) || changePos == new Vector2(100, 200) || changePos == new Vector2(100, -200) || changePos == new Vector2(-200, -100) || changePos == new Vector2(-200, 100) || changePos == new Vector2(200, 100) || changePos == new Vector2(200, -100))
                        {
                            if (isWhite == isWhiteTurn && !IsPathBlocked(startPos, endPos, pieces, "Knight"))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                case ("Queen"):
                    {
                        // Get absolute values
                        int absX = (int)Math.Abs(changePos.X);
                        int absY = (int)Math.Abs(changePos.Y);

                        // Check if within range (<=800) and is a valid diagonal or straight move
                        if ((absX <= 800 && absY <= 800) && (absX == absY || absX == 0 || absY == 0))
                        {
                            // Check turn
                            if (isWhite == isWhiteTurn && !IsPathBlocked(startPos, endPos, pieces, ""))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                case ("Bishop"):
                    {
                        if (changePos == new Vector2(100, 100) || changePos == new Vector2(200, 200) || changePos == new Vector2(300, 300) || changePos == new Vector2(400, 400) || changePos == new Vector2(500, 500) || changePos == new Vector2(600, 600) || changePos == new Vector2(700, 700) || changePos == new Vector2(800, 800) || changePos == new Vector2(-100, 100) || changePos == new Vector2(-200, 200) || changePos == new Vector2(-300, 300) || changePos == new Vector2(-400, 400) || changePos == new Vector2(-500, 500) || changePos == new Vector2(-600, 600) || changePos == new Vector2(-700, 700) || changePos == new Vector2(-800, 800) || changePos == new Vector2(100, -100) || changePos == new Vector2(200, -200) || changePos == new Vector2(300, -300) || changePos == new Vector2(400, -400) || changePos == new Vector2(500, -500) || changePos == new Vector2(600, -600) || changePos == new Vector2(700, -700) || changePos == new Vector2(800, -800) || changePos == new Vector2(-100, -100) || changePos == new Vector2(-200, -200) || changePos == new Vector2(-300, -300) || changePos == new Vector2(-400, -400) || changePos == new Vector2(-500, -500) || changePos == new Vector2(-600, -600) || changePos == new Vector2(-700, -700) || changePos == new Vector2(-800, -800))
                        {
                            if (isWhite == isWhiteTurn && !IsPathBlocked(startPos, endPos, pieces, ""))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                            // bishop code
                        }
                        else
                        {
                            return false;
                        }
                    }
                case ("Rook"):
                    {
                        if (changePos == new Vector2(0, 100) || changePos == new Vector2(0, 200) || changePos == new Vector2(0, 300) || changePos == new Vector2(0, 400) || changePos == new Vector2(0, 500) || changePos == new Vector2(0, 600) || changePos == new Vector2(0, 700) || changePos == new Vector2(0, 800) || changePos == new Vector2(0, -100) || changePos == new Vector2(0, -200) || changePos == new Vector2(0, -300) || changePos == new Vector2(0, -400) || changePos == new Vector2(0, -500) || changePos == new Vector2(0, -600) || changePos == new Vector2(0, -700) || changePos == new Vector2(0, -800) || changePos == new Vector2(100, 0) || changePos == new Vector2(200, 0) || changePos == new Vector2(300, 0) || changePos == new Vector2(400, 0) || changePos == new Vector2(500, 0) || changePos == new Vector2(600, 0) || changePos == new Vector2(700, 0) || changePos == new Vector2(800, 0) || changePos == new Vector2(-100, 0) || changePos == new Vector2(-200, 0) || changePos == new Vector2(-300, 0) || changePos == new Vector2(-400, 0) || changePos == new Vector2(-500, 0) || changePos == new Vector2(-600, 0) || changePos == new Vector2(-700, 0) || changePos == new Vector2(-800, 0))
                        {
                            if (isWhite == isWhiteTurn && !IsPathBlocked(startPos, endPos, pieces, "Rook"))
                            {
                                return true;
                            }
                            {
                                return false;
                            }
                            // rook code
                        }
                        else
                        {
                            return false;
                        }
                    }
                case ("King"):
                    {
                        int absX = (int)Math.Abs(changePos.X);
                        int absY = (int)Math.Abs(changePos.Y);

                        // Check if within range (<=100) and is a valid diagonal or straight move
                        if ((absX <= 100 && absY <= 100) && (absX == absY || absX == 0 || absY == 0))
                        {
                            // Check turn
                            if (isWhite == isWhiteTurn && !IsPathBlocked(startPos, endPos, pieces, ""))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                case ("Pawn"):
                    {
                        //double move on first move
                        if (isWhite != isWhiteTurn)
                        {
                            return false;
                        }
                        if (isWhite && isWhiteTurn)
                        {
                            if (startPos.Y == 610 && changePos.Y == -200 && changePos.X == 0 && !IsPathBlocked(startPos, endPos, pieces, "Pawn") && GetPieceAtPosition(endPos, pieces) == null)
                            {
                                return true;
                            }
                        }
                        if (!isWhite && !isWhiteTurn)
                        {
                            if (startPos.Y == 110 && changePos.Y == 200 && changePos.X == 0 && !IsPathBlocked(startPos, endPos, pieces, "Pawn") && GetPieceAtPosition(endPos, pieces) == null)
                            {
                                return true;
                            }
                        }
                        // Move forward
                        if (changePos.X == 0 && ((changePos.Y == -100 && isWhite) || (changePos.Y == 100 && !isWhite)))
                        {
                            // Check if the path is blocked or if there is a piece at the end position (pawns cannot capture forward)
                            if (!IsPathBlocked(startPos, endPos, pieces, "Pawn") && !isPieceAtPosition(endPos, pieces))
                            {
                                return true;
                            }
                        }
                        // Capture diagonally
                        else if (Math.Abs(changePos.X) == 100 && ((changePos.Y == -100 && isWhite) || (changePos.Y == 100 && !isWhite)))
                        {
                            // Check if there is an opposing piece at the end position
                            Piece pieceAtEndPos = GetPieceAtPosition(endPos, pieces);
                            if (pieceAtEndPos != null && pieceAtEndPos.isWhite != isWhite)
                            {
                                return true;
                            }
                        }


                        return false;
                    }
                default:
                    {
                        return false;
                    }
            }
        }        

        public static bool IsPathBlocked(Vector2 startPos, Vector2 endPos, List<Piece> pieces, string chessPiece)
        {
            if (chessPiece == "Knight")
            {
                return false;
            }
            Vector2 direction = endPos - startPos;

            if (chessPiece == "Pawn")
            {
                if (isPieceAtPosition(endPos, pieces) && Math.Abs(direction.Y) == 100 && direction.X == 0)
                {
                    return true;
                }
            }

            // Our step sizes
            Vector2 x = new Vector2(100, 0);
            Vector2 y = new Vector2(0, 100);
            Vector2 xy = new Vector2(100, 100);

            // Getting the direction vector
            Vector2 directionUnit = new Vector2(Math.Sign(direction.X), Math.Sign(direction.Y));

            if (Math.Abs(direction.Y) < float.Epsilon) // Checks for horizontal movement
            {
                int countX = (int)(Math.Abs(direction.X) / x.X); // Now we're checking the absolute value
                for (int i = 1; i < countX; i++)
                {
                    Vector2 intermediatePosition = startPos + (x * i * directionUnit.X); // Multiplying by the direction
                    if (isPieceAtPosition(intermediatePosition, pieces))
                    {
                        return true; // A piece is blocking the path
                    }
                }
            }
            else if (Math.Abs(direction.X) < float.Epsilon) // Checks for vertical movement
            {
                int countY = (int)(Math.Abs(direction.Y) / y.Y); // Now we're checking the absolute value
                for (int i = 1; i < countY; i++)
                {
                    Vector2 intermediatePosition = startPos + (y * i * directionUnit.Y); // Multiplying by the direction
                    if (isPieceAtPosition(intermediatePosition, pieces))
                    {
                        return true; // A piece is blocking the path
                    }
                }
            }
            else // Diagonal movement
            {
                int countXY = (int)(Math.Abs(direction.X) / xy.X); // As we're dealing with a square board and diagonal movements, X or Y would do here.
                for (int i = 1; i < countXY; i++)
                {
                    Vector2 intermediatePosition = startPos + (xy * i * directionUnit); // Multiplying by the direction
                    if (isPieceAtPosition(intermediatePosition, pieces))
                    {
                        return true; // A piece is blocking the path
                    }
                }
            }

            return false;

        }

        public static bool isPieceAtPosition(Vector2 position, List<Piece> pieces)
        {
            foreach (Piece piece in pieces)
            {
                if (piece.position == position)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool detCheck(List<Piece> pieces, bool isWhite)
        {
            
            Piece Wking = null;
            Piece Bking = null;
            foreach (Piece piece in pieces)
            {
                if (piece.name == "King")
                {
                    if (piece.isWhite)
                    {
                        Wking = piece;
                    }
                    else
                    {
                        Bking = piece;
                    }
                }
            }

            foreach (Piece piece in pieces)
            {
                if (piece.isWhite && !isWhite && Bking!=null)
                {
                    if (isLegalMoveWithoutCheck(piece.position, piece.name, Bking.position, true, true, pieces))
                    {
                        return true;
                    }
                }
                else if (!piece.isWhite && isWhite && Wking!=null)
                {
                    if (isLegalMoveWithoutCheck(piece.position, piece.name, Wking.position, false, false, pieces))
                    {
                        return true;
                    }
                }                                                             
            }


            return false;
        }

        public static bool selfCheckChecker(Vector2 startPos, Vector2 endPos, List<Piece> pieces, Piece currentPiece)
        {
            // Simulate the move
            Piece pieceAtStartPos = currentPiece;
            Piece pieceAtEndPos = GetPieceAtPosition(endPos, pieces);

            List<Piece> newPieces = new List<Piece>(pieces);

            if (pieceAtStartPos == null || startPos == null)
            {
                return false;
            }

            // If the piece at endPos is not null, it means we're about to capture it,
            // so we remove it temporarily

            //hereeeeeeeeeeeeeeeeeeeeeeeeeeeee
            if (pieceAtEndPos != null)
            {
                newPieces.Remove(pieceAtEndPos);
            }

            // Move the piece at startPos to endPos
            pieceAtStartPos.position = endPos;

            // Check if the king is now in check
            bool isInCheck = false; 
             
            if (detCheck(newPieces, currentPiece.isWhite))
            {
                isInCheck = true;
            }





            // Undo the move
            pieceAtStartPos.position = startPos;

            // If we removed a piece, put it back
            if (pieceAtEndPos != null)
            {
                newPieces.Add(pieceAtEndPos);
            }

            return isInCheck;
        }

        public static Piece GetPieceAtPosition(Vector2 position, List<Piece> pieces)
        {
            foreach (Piece piece in pieces)
            {
                if (piece.position == position)
                {
                    return piece;
                }
            }

            return null;
        }

        public static bool castling(Vector2 startPos, Vector2 endPos, bool isWhiteTurn, List<Piece> pieces, Piece currentPiece)
        {
            currentPiece.position = startPos;
            
            if (detCheck(pieces, isWhiteTurn))
            {
                return false;
            }
            //cannot castle if in check


            Vector2 changePos = new Vector2(endPos.X - startPos.X, endPos.Y - startPos.Y);
            Vector2 increment = startPos;
            
            

            if (currentPiece is King && currentPiece.isWhite == isWhiteTurn && Math.Abs(changePos.X) == 200 && changePos.Y == 0)
            {
                // checking if move is on a valid turn
                // checking if king is moving to a castle square


                King king = (King)currentPiece;
                if(king.hasMoved)
                {
                    return false;
                }

                //checking if the king has moved


                Vector2 positionCheck = endPos;
                //get the position of the king

                if (changePos.X == -200)
                {
                    //if move left then 3 squares between king a rook
                    positionCheck.X -= new Vector2(100).X;
                    if (IsPathBlocked(startPos, positionCheck, pieces, currentPiece.name))
                    {
                        return false;
                    }
                    //checking if path is blocked for the rook

                    for (int i = 0; i < 3; i++)
                    {
                        increment.X = increment.X - new Vector2(100).X;
                        if (selfCheckChecker(startPos, increment, pieces, currentPiece))
                        {
                            return false;
                        }
                        

                        //placing king in each position of intermediate castle squares to check if it is in check
                        //middle square and end square as already checked current square at the start 
                    }
                    Vector2 rookPos = endPos;
                    rookPos.X += new Vector2(-200).X;

                    //find the rook on left side
                    Piece rook = MoveMethods.GetPieceAtPosition(rookPos, pieces);

                    if (rook is Rook && rook.isWhite == currentPiece.isWhite)
                    {
                        Rook newRook = (Rook)rook;

                        if (newRook.hasMoved)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    // move to the right
                    positionCheck.X += new Vector2(100).X;
                    //checking if path is blocked when moving to the right
                    if (IsPathBlocked(startPos, positionCheck, pieces, currentPiece.name))
                    {
                        return false;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        increment.X += new Vector2(100).X;
                        if (selfCheckChecker(startPos, increment, pieces, currentPiece))
                        {
                            return false;
                        }
                        

                    }

                    Vector2 rookPos = endPos;
                    rookPos.X += new Vector2(100).X;

                    Piece rook = MoveMethods.GetPieceAtPosition(rookPos, pieces);

                    if (rook is Rook && rook.isWhite == currentPiece.isWhite)
                    {
                        Rook newRook = (Rook)rook;

                        if (newRook.hasMoved)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public static void castle(Vector2 startPos, Vector2 endPos, bool isWhiteTurn, List<Piece> pieces, Piece currentPiece)
        {
            if (castling(startPos, endPos, isWhiteTurn, pieces, currentPiece))
            {
                if(currentPiece.isWhite)
                {
                    if (endPos-startPos == new Vector2(-200,0))
                    {
                        currentPiece.position.X = new Vector2(210).X;
                        GetPieceAtPosition(new Vector2(10, 710), pieces).position.X = new Vector2(310).X;
                    }
                    else
                    {
                        currentPiece.position.X = new Vector2(610).X;
                        GetPieceAtPosition(new Vector2(710, 710), pieces).position.X = new Vector2(510).X;
                    }
                }
                else
                {
                    if (endPos-startPos == new Vector2(-200, 0))
                    {
                        currentPiece.position.X = new Vector2(210).X;
                        GetPieceAtPosition(new Vector2(10, 10), pieces).position.X = new Vector2(310).X;
                    }
                    else
                    {
                        currentPiece.position.X = new Vector2(610).X;
                        GetPieceAtPosition(new Vector2(710, 10), pieces).position.X = new Vector2(510).X;
                    }
                }

            }               
        }

        public static bool checkMate(bool isWhiteTurn, List<Piece> pieces)
        {

            // if not in check return false

            if (!detCheck(pieces, isWhiteTurn))
            {
                return false;
            }


            foreach (Piece piece in pieces)
            {
                // checkmate is always on losers turn

                // check for every piece of loser if there is any move that takes out of check


                if (piece.isWhite == isWhiteTurn)
                {
                    Vector2 startPos = piece.position;
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            Vector2 endPos = new Vector2((i * 100) + 10, (j * 100) + 10);

                            if (legalMove(startPos, piece.name, endPos, piece.isWhite, isWhiteTurn, pieces, piece))
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public static bool canPromote(Piece piece)
        {
            if(piece is Pawn)
            {
                if(piece.position.Y == 10 || piece.position.Y == 710)
                {
                    return true;
                }
            }
            return false;
        }
       

        public static bool legalMove(Vector2 startPos, string chessPiece, Vector2 endPos, bool isWhite, bool isWhiteTurn, List<Piece> pieces, Piece currentPiece)
        {
            //something wrong with isLegalMoveWithoutCheck
            if (isLegalMoveWithoutCheck(startPos, chessPiece, endPos, isWhite, isWhiteTurn, pieces) && (startPos != endPos))               
            {
                if (isPieceAtPosition(endPos, pieces) == false || (GetPieceAtPosition(endPos, pieces).isWhite != currentPiece.isWhite))
                {
                    if (!selfCheckChecker(startPos, endPos, pieces, currentPiece))
                    {
                        if(endPos.X > -1 && endPos.X < 801 && endPos.Y > -1 && endPos.Y < 801)
                        {
                            return true;
                        }
                    }
                }
                // Check if the move puts the player's own king in check
                
            }
            if(castling(startPos, endPos, isWhiteTurn, pieces, currentPiece))
            {
                return true;
            }

            return false;
        }
    }
}