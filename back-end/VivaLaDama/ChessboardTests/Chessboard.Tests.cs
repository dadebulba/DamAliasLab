using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
            Move move = null;
            bool turnBeforeMove = chessboard.Turn, turnAfterMove;
            bool result;

            Assert.IsTrue(chessboard.ExecuteMove(move), "A player should be able to skip!");

            turnAfterMove = chessboard.Turn;
            result = (turnBeforeMove != turnAfterMove);

            Assert.IsTrue(result, "If a player skipped, the turn should go to the other player!");
        }
        [DataTestMethod]
        [DynamicData(nameof(GetData_ChessboardExecuteMove_CheckingMovementWhite), DynamicDataSourceType.Method)]
        public void ChessboardExecuteMove_CheckingMovementWhite(Move move, bool resultExpected)
        {
            Chessboard chessboard = new Chessboard();
            bool result;

            chessboard.SetTurnForWhite();
            result = chessboard.ExecuteMove(move);

            Assert.AreEqual(resultExpected, result);
        }
        public static IEnumerable<object[]> GetData_ChessboardExecuteMove_CheckingMovementWhite()
        {
            yield return new object[] { new Move(new Pawn(Pawn.ColorPawn.WHITE, 1), new Coordinate(4, 1)), true};
            yield return new object[] { new Move(new Pawn(Pawn.ColorPawn.WHITE, 1), new Coordinate(4, 3)), true};
            yield return new object[] { new Move(new Pawn(Pawn.ColorPawn.WHITE, 1), new Coordinate(6, 1)), false};
            yield return new object[] { new Move(new Pawn(Pawn.ColorPawn.WHITE, 1), new Coordinate(6, 3)), false};
            yield return new object[] { new Move(new Pawn(Pawn.ColorPawn.WHITE, 1), new Coordinate(4, 2)), false};
            yield return new object[] { new Move(new Pawn(Pawn.ColorPawn.WHITE, 1), new Coordinate(3, 2)), false};
            yield return new object[] { new Move(new Pawn(Pawn.ColorPawn.WHITE, 1), new Coordinate(3, 0)), false};
            yield return new object[] { new Move(new Pawn(Pawn.ColorPawn.WHITE, 1), new Coordinate(3, 4)), false};
        }
        [DataTestMethod]
        [DynamicData(nameof(GetData_ChessboardExecuteMove_CheckingMovementWhiteOutOfGrid), DynamicDataSourceType.Method)]
        public void ChessboardExecuteMove_CheckingMovementWhiteOutOfGrid_ReturnFalse(Move move)
        {
            Chessboard chessboard = new Chessboard();
            bool result;

            chessboard.SetTurnForWhite();
            result = chessboard.ExecuteMove(move);

            Assert.IsFalse(result, "A white pawn should not be able to go out of the grid!");
        }
        public static IEnumerable<object[]> GetData_ChessboardExecuteMove_CheckingMovementWhiteOutOfGrid()
        {
            yield return new object[] { new Move(new Pawn(Pawn.ColorPawn.WHITE, 0), new Coordinate(4, -1))};
            yield return new object[] { new Move(new Pawn(Pawn.ColorPawn.WHITE, 0), new Coordinate(5, -2))};
            yield return new object[] { new Move(new Pawn(Pawn.ColorPawn.WHITE, 3), new Coordinate(4, 8))};
            yield return new object[] { new Move(new Pawn(Pawn.ColorPawn.WHITE, 3), new Coordinate(5, 9))};
        }
    }
}
