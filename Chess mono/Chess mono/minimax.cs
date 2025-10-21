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
    class minimax
    {                
        public static int pieceValue(Piece piece)
        {
            switch(piece.name)
            {
                case ("Pawn"):
                    {
                        return 1;
                    }
                case ("Knight"):
                    {
                        return 3;
                    }
                case ("Bishop"):
                    {
                        return 3;
                    }
                case ("Rook"):
                    {
                        return 5;
                    }
                case ("Queen"):
                    {
                        return 9;
                    }
                    
            }
            return 0;
        }
        
        
        public static int countMaterial(bool isWhite, List<Piece> pieces)
        {
            int whiteMaterial = 0;
            int blackMaterial = 0;
            foreach (Piece piece in pieces)
            {
                if (piece.name != "King" && piece.Alive)
                {
                    if (piece.isWhite)
                    {
                        whiteMaterial += pieceValue(piece);
                    }
                    else
                    {
                        blackMaterial += pieceValue(piece);
                    }
                }
            }
            if(isWhite)
            {
                return whiteMaterial;
            }
            else
            {
                return blackMaterial;
            }
        }

        public static int evaluation(List<Piece> pieces, bool maximisingColour)
        {
            int whiteTotalMat = countMaterial(true, pieces);
            int blackTotoalMat = countMaterial(false, pieces);
            int evaluation = whiteTotalMat - blackTotoalMat;

            if(MoveMethods.checkMate(maximisingColour, pieces))
            {
                if(maximisingColour)
                {
                    return -10000;
                }
                return 10000;
            }

            return evaluation;
        }

         
       public static int Minimax(int depth, List<Piece> pieces, bool isWhiteToMove, int alpha, int beta)
        {
            
            if (depth == 0)
            {
                return QuiescenceSearch(alpha, beta, pieces, isWhiteToMove);
            }

            //white is the maximising player
            if (isWhiteToMove)
            {
                //presume white best move is terrible
                int best = -10000;

                List<Move> legalMoves = Move.getLegalMoves(isWhiteToMove, pieces);
                List<Move> moves = Move.orderedMoves(legalMoves, pieces);

                if (moves.Count == 0)
                {
                    if (MoveMethods.checkMate(isWhiteToMove, pieces))
                    {
                        return -10000;
                    }
                    //if checkmate then very low score for white

                    //if no legal moves and not checkMate then staleMate (draw)
                    return 0;
                }

                foreach (Move move in moves)
                {
                    //need to simulate the move

                    //record start position of the piece


                    Vector2 startPos = new Vector2();
                    startPos = move.piece.position;

                    //make the move (transport piece to target square)
                    move.piece.position = move.destination;

                    //remove captured piece if there is one

                    Piece pieceAtTarget = MoveMethods.GetPieceAtPosition(move.destination, pieces);
                    if (pieceAtTarget != null)
                    {
                        pieces.Remove(pieceAtTarget);
                    }


                    int evaluation = Minimax(depth - 1, pieces, false, alpha, beta);

                    //unmake the move

                    move.piece.position = startPos;
                    if (pieceAtTarget != null) pieces.Add(pieceAtTarget);


                    best = Math.Max(best, evaluation);
                    alpha = Math.Max(alpha, best);


                    //alpha beta pruning
                    //if black has a move that is better than whites current best move then no point searching the rest of the tree
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return best;
            }
            else
            {
                //presume black best move is terrible
                int best = 10000;

                List<Move> legalMoves = Move.getLegalMoves(isWhiteToMove, pieces);
                List<Move> moves = Move.orderedMoves(legalMoves, pieces);

                if (moves.Count == 0)
                {
                    if (MoveMethods.checkMate(isWhiteToMove, pieces))
                    {
                        return 10000;
                    }
                    //if checkmate then very low score for black

                    //if no legal moves and not checkMate then staleMate (draw)
                    return 0;
                }

                foreach (Move move in moves)
                {
                    //need to simulate the move

                    //record start position of the piece

                    Vector2 startPos = new Vector2();
                    startPos = move.piece.position;

                    //make the move (transport piece to target square)
                    move.piece.position = move.destination;

                    //remove captured piece if there is one
                    Piece pieceAtTarget = MoveMethods.GetPieceAtPosition(move.destination, pieces);
                    if (pieceAtTarget != null)
                    {
                        pieces.Remove(pieceAtTarget);
                    }

                    int evaluation = Minimax(depth - 1, pieces, true, alpha, beta);

                    //unmake the move

                    move.piece.position = startPos;
                    if (pieceAtTarget != null) pieces.Add(pieceAtTarget);


                    best = Math.Min(best, evaluation);
                    beta = Math.Min(beta, best);

                    // Alpha Beta Pruning 
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return best;
            }
                            
        }

       public static Move findBestMove(List<Piece> pieces, int depth, bool isWhiteToMove)
       {                       
            int bestEval;

            //if less material increase depth since less moves to search

            if (countMaterial(true, pieces) + countMaterial(false, pieces) < 25)
            {
                depth = 4;
            }

            if (isWhiteToMove)
            {
                //assume that the best move is terrible
                bestEval = -10000;
            }
            else
            {
                bestEval = 10000;
            }

            Piece piece = pieces[1];
            Move bestMove = new Move(piece, new Vector2(410, 410));

            List<Move> legalMoves = Move.getLegalMoves(isWhiteToMove, pieces);
            List<Move> moves = Move.orderedMoves(legalMoves, pieces);

            foreach (Move move in moves)
            {
                //need to simulate the move

                //record start position of the piece
                Vector2 startPos = new Vector2();
                startPos = move.piece.position;

                //remove captured piece if there is one

                Piece pieceAtTarget = MoveMethods.GetPieceAtPosition(move.destination, pieces);
                if (pieceAtTarget != null)
                {
                    pieces.Remove(pieceAtTarget);
                }


                //make the move (transport piece to target square)
                move.piece.position = move.destination;


                //recusively call the minimax until depth = 0
                int Eval = Minimax(depth, pieces, !isWhiteToMove, -10000, 10000);


                //unmake the move

                
                move.piece.position = startPos;
                if (pieceAtTarget != null) pieces.Add(pieceAtTarget);


                if (isWhiteToMove && Eval > bestEval)
                {
                    bestEval = Eval;
                    bestMove = move;
                }
                else if (!isWhiteToMove && Eval < bestEval)
                {
                    bestEval = Eval;
                    bestMove = move;
                }
            }
            return bestMove;                                   
       }
       public static int QuiescenceSearch(int alpha, int beta, List<Piece> pieces, bool isWhiteToMove)
        {
            int standPat = evaluation(pieces, isWhiteToMove);

            //opponent would choose a different move automatically so ignore rest
            if (standPat >= beta) return beta;

            //position may be best for player so carry on
            if (alpha < standPat) alpha = standPat;

            List<Move> captureMoves = Move.getCaptureMoves(isWhiteToMove, pieces);
            foreach (Move move in captureMoves)
            {
                // simulate the capture move
                Vector2 originalPosition = move.piece.position;
                Piece capturedPiece = MoveMethods.GetPieceAtPosition(move.destination, pieces);
                move.piece.position = move.destination;
                pieces.Remove(capturedPiece);


                //need to flip the score as it will return how good is for opponent, want how good is for current player
                int score = -QuiescenceSearch(-beta, -alpha, pieces, !isWhiteToMove);

                // undo the move
                move.piece.position = originalPosition;
                if (capturedPiece != null) pieces.Add(capturedPiece);


                //if position is better for you than another move, opponent will just choose the other move
                if (score >= beta) return beta;
                if (score > alpha) alpha = score;
            }
            return alpha;
        }


    }
}
