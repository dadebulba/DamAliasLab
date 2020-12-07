using System;
using System.Collections.Generic;

namespace VivaLaDama.Models
{
    public class Chessboard
    {
        private const int DEFAULT_LENGTH = 8;
        private const int ROWS_FILLED_OF_PAWNS = 3;
        private const int MAX_MOVE_DISTANCE = 2;
        public Pawn[,] Grid { get; set; }
        public bool Turn { get; set; }//'false' when is the turn of the black pawns, 'true' otherwise
        public int PointsWhite { get; set; }
        public int PointsBlack { get; set; }
        private Pawn LastPawnMoved { get; set; }
        private bool MustEat { get; set; }
        public Chessboard()
        {
            this.Grid = new Pawn[DEFAULT_LENGTH, DEFAULT_LENGTH];
            this.ResetGrid();
            this.ResetValue();
        }
        private void ResetGrid()
        {
            int numWhitePawns = 0, numBlackPawns = 0;

            for (int i = 0; i < DEFAULT_LENGTH; i++)
            {
                for (int j = 0; j < DEFAULT_LENGTH; j++)
                {
                    this.Grid[i, j] = null;//Empty box

                    if ((i + j) % 2 != 0)
                    {
                        if (i < ROWS_FILLED_OF_PAWNS)//Putting in the first 3 rows black pawns
                        {
                            this.Grid[i, j] = new Pawn { Color = Pawn.ColorPawn.BLACK, PawnId = numBlackPawns++, Upgraded = false };
                        }
                        else if (i >= DEFAULT_LENGTH - ROWS_FILLED_OF_PAWNS)//Putting in the last three rows white pawns
                        {
                            this.Grid[i, j] = new Pawn { Color = Pawn.ColorPawn.WHITE, PawnId = numWhitePawns++, Upgraded = false };
                        }
                    }
                }
            }
        }
        private void ResetValue()
        {
            this.PointsBlack = this.PointsWhite = 0;
            this.MustEat = false;
            this.LastPawnMoved = null;
            this.Turn = false;
        }
        public bool IsTurnRespected(Pawn pawn)
        {
            return pawn != null &&
                   (this.Turn == false && pawn.Color == Pawn.ColorPawn.BLACK) ||
                   (this.Turn == true && pawn.Color == Pawn.ColorPawn.WHITE);
        }
        public bool IsMovementValid(Coordinate from, Coordinate to)
        {
            Coordinate difference = to - from;
            int row = Math.Abs(difference.Row);
            int column = Math.Abs(difference.Column);

            return row == column &&
                   row > 0 && column > 0 &&
                   row <= MAX_MOVE_DISTANCE && column <= MAX_MOVE_DISTANCE;
        }
        public void FindPossibleDestination(Move move, out Coordinate dest1, out Coordinate dest2)
        {
            Coordinate from = move.From, to = move.To;
            Coordinate difference = to - from;
            Pawn pawn = move.Target;

            dest1 = dest2 = null;

            if (this.IsMovementValid(from, to))
            {
                if (difference.Row < 0 && difference.Column > 0 && (pawn.Color == Pawn.ColorPawn.WHITE || pawn.Upgraded == true))
                {
                    dest1 = from.GetUpRight();
                    dest2 = dest1.GetUpRight();
                }
                else if (difference.Row < 0 && difference.Column < 0 && (pawn.Color == Pawn.ColorPawn.WHITE || pawn.Upgraded == true))
                {
                    dest1 = from.GetUpLeft();
                    dest2 = dest1.GetUpLeft();
                }
                else if (difference.Row > 0 && difference.Column < 0 && (pawn.Color == Pawn.ColorPawn.BLACK || pawn.Upgraded == true))
                {
                    dest1 = from.GetDownLeft();
                    dest2 = dest1.GetDownLeft();
                }
                else if (difference.Row > 0 && difference.Column > 0 && (pawn.Color == Pawn.ColorPawn.BLACK || pawn.Upgraded == true))
                {
                    dest1 = from.GetDownRight();
                    dest2 = dest1.GetDownRight();
                }
            }
        }
        private bool EvaluateMove(Move move, bool changeTarget)
        {
            Coordinate dest1, dest2;
            bool ret = false;

            if (move != null && this.DoesThisPawnExist(move.Target) && this.IsTurnRespected(move.Target) && move.To.IsValid(DEFAULT_LENGTH))
            {
                if(changeTarget == true)
                {
                    move.From = this.GetCoordinateFromPawn(move.Target);
                    move.Target = this.Grid[move.From.Row, move.From.Column];
                }
                this.FindPossibleDestination(move, out dest1, out dest2);

                if (dest1 != null && dest2 != null)
                {
                    ret = this.CheckMove(move.Target, dest1, dest2, move.To);
                }
            }

            return ret;
        }
        public Coordinate GetCoordinateFromPawn(Pawn pawn)
        {
            Coordinate ret = null;

            if (pawn != null)
            {
                for (var i = 0; i < DEFAULT_LENGTH && ret == null; i++)
                {
                    for (var j = 0; j < DEFAULT_LENGTH && ret == null; j++)
                    {
                        if (this.Grid[i, j] != null && this.Grid[i, j].Equals(pawn))
                        {
                            ret = new Coordinate { Row = i, Column = j };
                        }
                    }
                }
            }

            return ret;
        }
        public bool DoesThisPawnExist(Pawn pawn)
        {
            bool ret = false;

            if (pawn != null)
            {
                for (var i = 0; i < DEFAULT_LENGTH && ret == false; i++)
                {
                    for (var j = 0; j < DEFAULT_LENGTH && ret == false; j++)
                    {
                        if (this.Grid[i, j] != null && this.Grid[i, j].Equals(pawn))
                        {
                            ret = true;
                        }
                    }
                }
            }

            return ret;
        }
        private bool IsBoxEmpty(Coordinate coordinate)
        {
            bool ret = false;

            if (coordinate.IsValid(DEFAULT_LENGTH))
            {
                ret = this.Grid[coordinate.Row, coordinate.Column] == null;
            }
            return ret;
        }
        private bool IsBoxNotEmpty(Coordinate coordinate)
        {
            bool ret = false;

            if (coordinate.IsValid(DEFAULT_LENGTH))
            {
                ret = !this.IsBoxEmpty(coordinate);
            }
            return ret;
        }
        public bool CheckMove(Pawn pawn, Coordinate dest1, Coordinate dest2, Coordinate finalDest)
        {
            bool ret;

            ret = dest1.Equals(finalDest) &&
                  this.IsBoxEmpty(dest1) &&
                  this.MustEat == false;//Just a move of 1 step

            ret = ret || this.CanEat(pawn, dest1, dest2) &&
                         dest2.Equals(finalDest);//A pawn attacked another one

            return ret;
        }
        private bool CanEat(Pawn pawn, Coordinate dest1, Coordinate dest2)
        {
            return (this.IsBoxNotEmpty(dest1) &&
                    this.Grid[dest1.Row, dest1.Column].GetOpponentColor() == pawn.Color &&
                    (this.Grid[dest1.Row, dest1.Column].Upgraded == pawn.Upgraded || pawn.Upgraded == true) &&
                    this.IsBoxEmpty(dest2) &&
                    ((this.MustEat == true && this.LastPawnMoved.Equals(pawn)) || this.MustEat == false));
        }
        public bool ExecuteMove(Move move, bool changeTarget)
        {
            bool ret = true;

            if (move == null)
            {
                this.FlipTurn();
            }
            else if ((ret = this.EvaluateMove(move, changeTarget)))
            {
                this.UpdateGrid(move);
            }

            return ret;
        }
        private void UpdateGrid(Move move)
        {
            Coordinate dest1, dest2;

            this.FindPossibleDestination(move, out dest1, out dest2);

            if (dest1.Equals(move.To))
            {
                this.Grid[dest1.Row, dest1.Column] = this.Grid[move.From.Row, move.From.Column];
                this.FlipTurn();
            }
            else if (dest2.Equals(move.To))
            {
                this.UpdatePoints();

                this.Grid[dest1.Row, dest1.Column] = null;
                this.Grid[dest2.Row, dest2.Column] = this.Grid[move.From.Row, move.From.Column];
                this.MustEat = false;
                this.LastPawnMoved = null;

                if (this.CanEatMorePawns(move.Target, move.From))
                {
                    this.LastPawnMoved = move.Target;
                    this.MustEat = true;
                }
                else
                {
                    this.FlipTurn();
                }
            }

            if (this.CanUpgradePawn(move.Target, move.To))//Upgrading the pawn
            {
                this.Grid[move.To.Row, move.To.Column].Upgraded = true;
            }

            this.Grid[move.From.Row, move.From.Column] = null;
        }
        public int GetNumberOfPawnsOfColor(Pawn.ColorPawn color)
        {
            Coordinate coordinate = new Coordinate();
            int numPawns = 0;

            for (int i = 0; i < DEFAULT_LENGTH; i++)
            {
                coordinate.Row = i;
                for (int j = 0; j < DEFAULT_LENGTH; j++)
                {
                    coordinate.Column = j;

                    if (this.IsBoxNotEmpty(coordinate) && this.Grid[i, j].Color == color)
                    {
                        numPawns++;
                    }
                }
            }

            return numPawns;
        }
        public void SetTurnForWhite()
        {
            this.Turn = true;
        }
        public void SetTurnForBlack()
        {
            this.Turn = false;
        }
        private List<PawnPositioned> GetPawnByColor(Pawn.ColorPawn color)
        {
            List<PawnPositioned> ret = new List<PawnPositioned>();
            Coordinate position = new Coordinate();

            for (int i = 0; i < DEFAULT_LENGTH; i++)
            {
                position.Row = i;
                for (int j = 0; j < DEFAULT_LENGTH; j++)
                {
                    position.Column = j;
                    if (this.IsBoxNotEmpty(position) && this.Grid[i, j].Color == color)
                    {
                        ret.Add(new PawnPositioned(this.Grid[i, j], position));
                    }
                }
            }

            return ret;
        }
        public List<PawnPositioned> GetBlack()
        {
            return GetPawnByColor(Pawn.ColorPawn.BLACK);
        }
        public List<PawnPositioned> GetWhite()
        {
            return GetPawnByColor(Pawn.ColorPawn.WHITE);
        }
        public Pawn.ColorPawn GetTurn()
        {
            return (this.Turn) ? Pawn.ColorPawn.WHITE : Pawn.ColorPawn.BLACK;
        }
        private bool CanEatMorePawns(Pawn pawn, Coordinate from)
        {
            Coordinate[] dests1, dests2;
            bool ret = false;

            if (pawn.Upgraded)
            {
                dests1 = new Coordinate[] { from.GetDownLeft(), from.GetDownRight(), from.GetUpLeft(), from.GetUpRight() };
                dests2 = new Coordinate[] { from.GetDownLeft().GetDownLeft(), from.GetDownRight().GetDownRight(), from.GetUpLeft().GetUpLeft(), from.GetUpRight().GetUpRight() };
            }
            else if (pawn.Color == Pawn.ColorPawn.WHITE)
            {
                dests1 = new Coordinate[] { from.GetUpLeft(), from.GetUpRight() };
                dests2 = new Coordinate[] { from.GetUpLeft().GetUpLeft(), from.GetUpRight().GetUpRight() };
            }
            else
            {
                dests1 = new Coordinate[] { from.GetDownLeft(), from.GetDownRight() };
                dests2 = new Coordinate[] { from.GetDownLeft().GetDownLeft(), from.GetDownRight().GetDownRight() };
            }

            for (int i = 0; i < dests2.Length && ret == false; i++)
            {
                ret |= this.CanEat(pawn, dests1[i], dests2[i]);
            }

            return ret;
        }
        private bool CanUpgradePawn(Pawn pawn, Coordinate destination)
        {
            return (destination.Row == 0 && pawn.Color == Pawn.ColorPawn.WHITE) ||
                   (destination.Row == DEFAULT_LENGTH - 1 && pawn.Color == Pawn.ColorPawn.BLACK);
        }
        private void FlipTurn()
        {
            this.Turn = !this.Turn;
        }
        private void UpdatePoints()
        {
            if(this.GetTurn()==Pawn.ColorPawn.BLACK)
            {
                this.PointsBlack++;
            } 
            else
            {
                this.PointsWhite++;
            }
        }
    }
}
