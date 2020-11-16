namespace VivaLaDama.Models
{
    public class Chessboard
    {
        private const int DEFAULT_LENGTH = 8;
        private const int MAX_MOVE_DISTANCE = 2;
        public Pawn[,] Grid { get; }
        public bool Turn { get; set; }//'false' when is the turn of the black pawns, 'true' otherwise
        public Chessboard()
        {
            this.Turn = false;
            this.Grid = new Pawn[DEFAULT_LENGTH, DEFAULT_LENGTH];
            this.ResetGrid();
        }
        private void ResetGrid()
        {
            int numWhitePawns = 0, numBlackPawns = 0;

            for (int i=0; i<DEFAULT_LENGTH; i++)
            {
                for (int j=0; j<DEFAULT_LENGTH; j++)
                {
                    this.Grid[i, j] = null;//Empty box

                    if ((i+j)%2 != 0)
                    {
                        if (j<=2)//Putting in the first 3 rows black pawns
                        {
                            this.Grid[i, j] = new Pawn(Pawn.ColorPawn.BLACK, numWhitePawns++);
                        }
                        else if (j>=DEFAULT_LENGTH-3)//Putting in the last three rows white pawns
                        {
                            this.Grid[i, j] = new Pawn(Pawn.ColorPawn.WHITE, numBlackPawns++);
                        }
                    }
                }
            }
        }
        private void FindPossibleDestination(Move move, out Coordinate dest1, out Coordinate dest2)
        {
            Coordinate from = move.From, to = move.To;
            Coordinate difference = to - from;
            Pawn pawn = move.Target;

            dest1 = dest2 = null;
            if((difference.Row==difference.Column || difference.Row==-difference.Column) && 
               difference.Row>=-MAX_MOVE_DISTANCE && difference.Row<=MAX_MOVE_DISTANCE && difference.Row!=0 &&
               difference.Column>=-MAX_MOVE_DISTANCE && difference.Column<=MAX_MOVE_DISTANCE && difference.Column!=0)
            {
                if(difference.Row > 0 && difference.Column > 0 && (pawn.Color == Pawn.ColorPawn.WHITE || pawn.Upgraded == true))
                {
                    dest1 = from.GetUpRight();
                    dest2 = dest1.GetUpRight();
                }
                else if(difference.Row < 0 && difference.Column > 0 && (pawn.Color == Pawn.ColorPawn.WHITE || pawn.Upgraded == true))
                {
                    dest1 = from.GetUpLeft();
                    dest2 = dest1.GetUpLeft();
                }
                else if(difference.Row < 0 && difference.Column < 0 && (pawn.Color == Pawn.ColorPawn.BLACK || pawn.Upgraded == true))
                {
                    dest1 = from.GetDownLeft();
                    dest2 = dest1.GetDownLeft();
                }
                else if(difference.Row > 0 && difference.Column < 0 && (pawn.Color == Pawn.ColorPawn.BLACK || pawn.Upgraded == true))
                {
                    dest1 = from.GetDownRight();
                    dest2 = dest1.GetDownRight();
                }
            }
        }
        private bool EvaluateMove(Move move)
        {
            Coordinate start = this.GetCoordinateFromPawn(move.Target);
            Coordinate difference = move.To - move.From;
            Coordinate dest1, dest2;
            bool ret = false;

            if(start!=null && //Checking if the pawn is on the grid
               move.From.IsValid(DEFAULT_LENGTH) &&
               move.To.IsValid(DEFAULT_LENGTH) &&
               move.Target.IsColorValid() &&
               start.Equals(move.From) && //Checking if the position of the pawn on the grid is the same as the one defined in the object 'move'
               ((this.Turn==false && move.Target.Color==Pawn.ColorPawn.BLACK) || 
                (this.Turn==true && move.Target.Color==Pawn.ColorPawn.WHITE))) //Checking if the turn is respected
            {
                this.FindPossibleDestination(move, out dest1, out dest2);

                if(dest1!=null && dest2!=null)
                {
                    ret = this.CheckMove(move.Target, dest1, dest2, move.To);
                }
            }

            return ret;
        }
        private Coordinate GetCoordinateFromPawn(Pawn pawn)
        {
            Coordinate ret = null;

            if(pawn!=null)
            {
                for (var i=0; i<DEFAULT_LENGTH && ret==null; i++)
                {
                    for (var j=0; j<DEFAULT_LENGTH && ret==null; j++)
                    {
                        if (this.Grid[i, j].Equals(pawn))
                        {
                            ret = new Coordinate(i, j);
                        }
                    }
                }
            }

            return ret;
        }
        private bool IsBoxEmpty(Coordinate coordinate)
        {
            bool ret = false;

            if(coordinate.IsValid(DEFAULT_LENGTH))
            {
                ret = this.Grid[coordinate.Row, coordinate.Column] == null;
            }
            return ret;
        }
        private bool IsBoxNotEmpty(Coordinate coordinate)
        {
            bool ret = false;

            if(coordinate.IsValid(DEFAULT_LENGTH))
            {
                ret = !this.IsBoxEmpty(coordinate);
            }
            return ret;
        }
        private bool CheckMove(Pawn pawn, Coordinate dest1, Coordinate dest2, Coordinate finalDest)
        {
            bool ret;

            ret = dest1.Equals(finalDest) && 
                  this.IsBoxEmpty(dest1);//Just a move of 1 step

            ret = ret || (this.IsBoxNotEmpty(dest1) && 
                          this.Grid[dest1.Row, dest1.Column].GetOpponentColor()==pawn.Color &&
                          (this.Grid[dest1.Row, dest1.Column].Upgraded==pawn.Upgraded || pawn.Upgraded==true) &&
                          dest2.Equals(finalDest) &&
                          this.IsBoxEmpty(dest2));//A pawn attacked another one

            return ret;
        }
        public bool ExecuteMove(Move move)
        {
            bool ret = this.EvaluateMove(move);

            if(ret==true)
            {
                this.UpdateGrid(move);
            }
            else if(move.Target==null && move.From.Equals(move.To)) //Skip
            {
                this.Turn = !this.Turn;
                ret = true;
            }

            return ret;
        }
        private void UpdateGrid(Move move)
        {
            Coordinate dest1, dest2;

            this.FindPossibleDestination(move, out dest1, out dest2);

            if(dest1.Equals(move.To))
            {
                this.Grid[dest1.Row, dest1.Column] = this.Grid[move.From.Row, move.From.Column];
                this.Turn = !this.Turn;
            }
            else if(dest2.Equals(move.To))
            {
                this.Grid[dest1.Row, dest1.Column] = null;
                this.Grid[dest2.Row, dest2.Column] = this.Grid[move.From.Row, move.From.Column];
            }

            if ((move.To.Row==0 && move.Target.Color==Pawn.ColorPawn.WHITE) ||
                (move.To.Row==DEFAULT_LENGTH-1 && move.Target.Color==Pawn.ColorPawn.BLACK))//Upgrading the pawn
            {
                this.Grid[move.To.Row, move.To.Column].Upgraded = true;
            }

            this.Grid[move.From.Row, move.From.Column] = null;
        }
        public int GetNumberOfPawnsOfColor(Pawn.ColorPawn color)
        {
            Coordinate coordinate = new Coordinate(-1,-1);
            int numPawns = 0;

            for(int i=0; i<DEFAULT_LENGTH; i++)
            {
                for(int j=0; j<DEFAULT_LENGTH; j++)
                {
                    coordinate.Row = i;
                    coordinate.Column = j;

                    if(this.IsBoxNotEmpty(coordinate) && this.Grid[i, j].Color==color)
                    {
                        numPawns++;
                    }
                }
            }

            return numPawns;
        }
    }
}
