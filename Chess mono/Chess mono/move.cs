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
    class Move
    {
        public Piece piece { get; set; }
        public Vector2 destination { get; set; }

        public Move(Piece piece_, Vector2 destination_)
        {
            piece = piece_;
            destination = destination_;
        }

        // Method to check if a move is a capture
        public bool IsCapture(List<Piece> pieces)
        {
            Piece targetPiece = pieces.FirstOrDefault(p => p.position == destination);
            return targetPiece != null && targetPiece.isWhite != piece.isWhite;
        }

        public static List<Move> getLegalMoves(bool isWhite, List<Piece> pieces)
        {
            List<Move> legalMoves = new List<Move>();
            foreach (Piece piece in pieces)
            {
                if (piece.isWhite == isWhite)
                {
                    Vector2 startPos = piece.position;

                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            Vector2 endPos = new Vector2((i * 100) + 10, (j * 100) + 10);

                            if (MoveMethods.legalMove(startPos, piece.name, endPos, isWhite, isWhite, pieces, piece) == true)
                            {
                                legalMoves.Add(new Move(piece, endPos));
                            }

                        }
                    }
                }
            }
            return legalMoves;
        }

        public static List<Vector2> getLegalMovesForPiece(Piece piece, List<Piece> pieces, bool isWhiteTurn)
        {
            List<Vector2> legalMoves = new List<Vector2>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Vector2 endPos = new Vector2(i * 100 + 10, j * 100 + 10);
                    if (MoveMethods.legalMove(piece.position, piece.name, endPos, piece.isWhite, isWhiteTurn, pieces, piece))
                    {
                        legalMoves.Add(endPos);
                    }
                }
            }
            return legalMoves;
        }

        //orders the moves by placing capture moves at the front of the returned list of all legal moves, this make the pruning much more effective
        public static List<Move> orderedMoves(List<Move> moves, List<Piece> pieces)
        {
            return moves.OrderBy(move => move.IsCapture(pieces) && move.piece.name == "Pawn") // Pawn captures first
                        .ThenBy(move => move.IsCapture(pieces) && move.piece.name == "Bishop") // Bishop captures next
                        .ThenBy(move => move.IsCapture(pieces) && move.piece.name == "Knight") // Knight captures next
                        .ThenBy(move => move.IsCapture(pieces) && move.piece.name == "Rook") // Rook captures next
                        .ThenBy(move => move.IsCapture(pieces) && move.piece.name == "Queen") // Queen captures next
                        .ThenBy(move => move.IsCapture(pieces) && move.piece.name == "King") // King captures last
                        .ThenBy(move => !move.IsCapture(pieces) && move.piece.name != "King") // Non-capture: other moves
                        .ThenBy(move => !move.IsCapture(pieces) && move.piece.name == "King") // Non-capture: king moves last
                        .ToList();
        }



        //returns all capture moves in a given position, this can be used to make sure the bot only evaluates 'balanced' positions
        public static List<Move> getCaptureMoves(bool isWhite, List<Piece> pieces)
        {
            return getLegalMoves(isWhite, pieces).Where(move => move.IsCapture(pieces)).ToList();
        }

    }
}
