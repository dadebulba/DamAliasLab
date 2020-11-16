using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VivaLaDama.Models;

namespace VivaLaDama.UnitTests.Models
{
    [TestClass]
    public class ChessboardTests
    {
        [TestMethod]
        public void ChessboardConstructor_CheckingIfTheNumberOfWhiteAndBlackPawnsAreEqual_ReturnTrue()
        {
            Chessboard chessboard = new Chessboard();
            Pawn.ColorPawn white = Pawn.ColorPawn.WHITE;
            Pawn.ColorPawn black = Pawn.ColorPawn.BLACK;
            int numWhitePawns, numBlackPawns;
            bool result;

            numWhitePawns = chessboard.GetNumberOfPawnsOfColor(white);
            numBlackPawns = chessboard.GetNumberOfPawnsOfColor(black);

            result = (numWhitePawns == numBlackPawns);

            Assert.IsTrue(result, "The number of white and black pawns should be equal after the initialization!");
        }
        [TestMethod]
        public void ChessboardExecuteMove_CheckingIfPlayerCanSkip_ReturnTrue()
        {
            Chessboard chessboard = new Chessboard();
            Coordinate from = new Coordinate(0, 0);
            Coordinate to = new Coordinate(0, 0);
            Move move = new Move(null, from, to);
            bool turnBeforeMove = chessboard.Turn, turnAfterMove;
            bool result;

            Assert.IsTrue(chessboard.ExecuteMove(move), "A player should be able to skip!");

            turnAfterMove = chessboard.Turn;
            result = (turnBeforeMove != turnAfterMove);

            Assert.IsTrue(result, "If a player skipped, the turn should go to the other player!");
        }
        [TestMethod]
        public void ChessboardExecuteMove_CheckingMovementWhiteLeft_ReturnTrue()
        {
            Chessboard chessboard = new Chessboard();
            Pawn pawn = new Pawn(Pawn.ColorPawn.WHITE, 1);
            Coordinate from = chessboard.GetCoordinateFromPawn(pawn);
            Coordinate to, dest1, dest2;
            Move move;
            bool result;

            chessboard.SetTurnForWhite();
            to = from.GetUpLeft();
            move = new Move(pawn, from, to);

            Assert.IsTrue(chessboard.IsTurnRespected(pawn), "Should be the turn of white!");
            Assert.IsTrue(chessboard.IsPawnPositionedAsDeclared(pawn, move.From), "Should be positioned as declared!");
            Assert.IsTrue(chessboard.AreCoordinatesInRange(move.From, move.To), "Coordinates should be in range!");
            Assert.IsTrue(chessboard.IsMovementValid(move.From, move.To), "Should be a valid movement!");
            chessboard.FindPossibleDestination(move, out dest1, out dest2);

            Assert.IsTrue(chessboard.CheckMove(pawn, dest1, dest2, move.To), "Should be valid!");

            result = chessboard.ExecuteMove(move);
            Assert.IsTrue(result, "A white pawn should have the possibility to move up-left");
        }
    }
}
