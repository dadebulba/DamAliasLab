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
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 1 }, To = new Coordinate { Row = 4, Column = 1 } }, true };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 1 }, To = new Coordinate { Row = 4, Column = 3 } }, true };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 1 }, To = new Coordinate { Row = 6, Column = 1 } }, false };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 1 }, To = new Coordinate { Row = 6, Column = 3 } }, false };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 1 }, To = new Coordinate { Row = 4, Column = 2 } }, false };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 1 }, To = new Coordinate { Row = 3, Column = 2 } }, false };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 1 }, To = new Coordinate { Row = 3, Column = 0 } }, false };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 1 }, To = new Coordinate { Row = 3, Column = 4 } }, false };
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
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 0 }, To = new Coordinate { Row = 4, Column = -1 } } };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 0 }, To = new Coordinate { Row = 5, Column = -2 } } };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 3 }, To = new Coordinate { Row = 5, Column = 8 } } };
        }
        [DataTestMethod]
        [DynamicData(nameof(GetData_ChessboardExecuteMove_CheckingMovementBlack), DynamicDataSourceType.Method)]
        public void ChessboardExecuteMove_CheckingMovementBlack(Move move, bool resultExpected)
        {
            Chessboard chessboard = new Chessboard();
            bool result;

            chessboard.SetTurnForBlack();
            result = chessboard.ExecuteMove(move);

            Assert.AreEqual(resultExpected, result);
        }
        public static IEnumerable<object[]> GetData_ChessboardExecuteMove_CheckingMovementBlack()
        {
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.BLACK, Id = 9 }, To = new Coordinate { Row = 3, Column = 2 } }, true };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.BLACK, Id = 9 }, To = new Coordinate { Row = 3, Column = 4 } }, true };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.BLACK, Id = 9 }, To = new Coordinate { Row = 1, Column = 2 } }, false };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.BLACK, Id = 9 }, To = new Coordinate { Row = 1, Column = 4 } }, false };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.BLACK, Id = 9 }, To = new Coordinate { Row = 4, Column = 1 } }, false };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.BLACK, Id = 9 }, To = new Coordinate { Row = 4, Column = 5 } }, false };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.BLACK, Id = 9 }, To = new Coordinate { Row = 4, Column = 3 } }, false };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.BLACK, Id = 9 }, To = new Coordinate { Row = 3, Column = 3 } }, false };
        }
        [DataTestMethod]
        [DynamicData(nameof(GetData_ChessboardExecuteMove_CheckingMovementBlackOutOfGrid), DynamicDataSourceType.Method)]
        public void ChessboardExecuteMove_CheckingMovementBlackOutOfGrid_ReturnFalse(Move move)
        {
            Chessboard chessboard = new Chessboard();
            bool result;

            chessboard.SetTurnForWhite();
            result = chessboard.ExecuteMove(move);

            Assert.IsFalse(result, "A white pawn should not be able to go out of the grid!");
        }
        public static IEnumerable<object[]> GetData_ChessboardExecuteMove_CheckingMovementBlackOutOfGrid()
        {
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 11 }, To = new Coordinate { Row = 4, Column = -1 } } };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 11 }, To = new Coordinate { Row = 5, Column = -2 } } };
            yield return new object[] { new Move { Target = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 8 }, To = new Coordinate { Row = 4, Column = -1 } } };
        }
        [TestMethod]
        public void ChessboardExecute_CheckingIfBlackCanAttack()
        {
            Chessboard chessboard = new Chessboard();
            Pawn blackPawn = new Pawn { Color = Pawn.ColorPawn.BLACK, Id = 8 };
            Pawn whitePawn = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 1 };
            Move moveBlack1 = new Move { Target = blackPawn, To = new Coordinate { Row = 3, Column = 0 } };
            Move moveBlack2 = new Move { Target = blackPawn, To = new Coordinate { Row = 5, Column = 2 } };
            Move moveWhite = new Move { Target = whitePawn, To = new Coordinate { Row = 4, Column = 1 } };
            int numWhitePawns, numBlackPawns;
            bool result;

            result = chessboard.ExecuteMove(moveBlack1);
            Assert.IsTrue(result, "The first black move should be valid!");

            result = chessboard.ExecuteMove(moveWhite);
            Assert.IsTrue(result, "The first white move should be valid!");

            result = chessboard.ExecuteMove(moveBlack2);
            Assert.IsTrue(result, "The second black move should be valid!");

            numWhitePawns = chessboard.GetNumberOfPawnsOfColor(Pawn.ColorPawn.WHITE);
            numBlackPawns = chessboard.GetNumberOfPawnsOfColor(Pawn.ColorPawn.BLACK);
            result = numBlackPawns > numWhitePawns;
            Assert.IsTrue(result, "The number of white pawns should be lower then the number of black pawns!");

            result = chessboard.DoesThisPawnExist(whitePawn);
            Assert.IsFalse(result, "This pawn should not exist anymore");
        }
        [TestMethod]
        public void ChessboardExecute_CheckingIfWhiteCanAttack()
        {
            Chessboard chessboard = new Chessboard();
            Pawn blackPawn = new Pawn { Color = Pawn.ColorPawn.BLACK, Id = 8 };
            Pawn whitePawn = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 1 };
            Move moveBlack1 = new Move { Target = blackPawn, To = new Coordinate { Row = 3, Column = 2 } };
            Move moveBlack2 = new Move { Target = blackPawn, To = new Coordinate { Row = 4, Column = 3 } };
            Move moveWhite1 = null;
            Move moveWhite2 = new Move { Target = whitePawn, To = new Coordinate { Row = 3, Column = 4 } };
            int numWhitePawns, numBlackPawns;
            bool result;

            result = chessboard.ExecuteMove(moveBlack1);
            Assert.IsTrue(result, "The first black move should be valid!");

            result = chessboard.ExecuteMove(moveWhite1);
            Assert.IsTrue(result, "The first white move should be valid!");

            result = chessboard.ExecuteMove(moveBlack2);
            Assert.IsTrue(result, "The second black move should be valid!");

            result = chessboard.ExecuteMove(moveWhite2);
            Assert.IsTrue(result, "The second white move should be valid!");

            numWhitePawns = chessboard.GetNumberOfPawnsOfColor(Pawn.ColorPawn.WHITE);
            numBlackPawns = chessboard.GetNumberOfPawnsOfColor(Pawn.ColorPawn.BLACK);
            result = numBlackPawns < numWhitePawns;
            Assert.IsTrue(result, "The number of black pawns should be lower then the number of white pawns!");

            result = chessboard.DoesThisPawnExist(blackPawn);
            Assert.IsFalse(result, "This pawn should not exist anymore");
        }
        [TestMethod]
        public void ChessboardExecute_CheckingThatWhiteUpgradedPawnCanMoveAllAround()
        {
            Chessboard chessboard = new Chessboard();
            Pawn pawn = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 1 };
            Move firstMove = new Move { Target = pawn, To = new Coordinate { Row = 4, Column = 1 } };
            Move secondAndLastMove = new Move { Target = pawn, To = new Coordinate { Row = 5, Column = 2 } };
            Move thirdMove = new Move { Target = pawn, To = new Coordinate { Row = 4, Column = 3 } };
            bool result;

            chessboard.Grid[5, 2].Upgraded = true;

            chessboard.SetTurnForWhite();
            result = chessboard.ExecuteMove(firstMove);
            Assert.IsTrue(result, "A white upgraded pawn should be able to do this first move");

            chessboard.SetTurnForWhite();
            result = chessboard.ExecuteMove(secondAndLastMove);
            Assert.IsTrue(result, "A white upgraded pawn should be able to do this second move");

            chessboard.SetTurnForWhite();
            result = chessboard.ExecuteMove(thirdMove);
            Assert.IsTrue(result, "A white upgraded pawn should be able to do this third move");

            chessboard.SetTurnForWhite();
            result = chessboard.ExecuteMove(secondAndLastMove);
            Assert.IsTrue(result, "A white upgraded pawn should be able to do this last move");
        }
        [TestMethod]
        public void ChessboardExecute_CheckingThatBlackUpgradedPawnCanMoveAllAround()
        {
            Chessboard chessboard = new Chessboard();
            Pawn pawn = new Pawn { Color = Pawn.ColorPawn.BLACK, Id = 8 };
            Move firstMove = new Move { Target = pawn, To = new Coordinate { Row = 3, Column = 0 } };
            Move secondAndLastMove = new Move { Target = pawn, To = new Coordinate { Row = 2, Column = 1 } };
            Move thirdMove = new Move { Target = pawn, To = new Coordinate { Row = 3, Column = 2 } };
            bool result;

            chessboard.Grid[2, 1].Upgraded = true;

            chessboard.SetTurnForBlack();
            result = chessboard.ExecuteMove(firstMove);
            Assert.IsTrue(result, "A black upgraded pawn should be able to do this first move");

            chessboard.SetTurnForBlack();
            result = chessboard.ExecuteMove(secondAndLastMove);
            Assert.IsTrue(result, "A black upgraded pawn should be able to do this second move");

            chessboard.SetTurnForBlack();
            result = chessboard.ExecuteMove(thirdMove);
            Assert.IsTrue(result, "A black upgraded pawn should be able to do this third move");

            chessboard.SetTurnForBlack();
            result = chessboard.ExecuteMove(secondAndLastMove);
            Assert.IsTrue(result, "A black upgraded pawn should be able to do this last move");
        }
        [TestMethod]
        public void ChessboardExecute_CheckingIfNormalPawnsCanEatUpgradedPawns_ReturnFalse()
        {
            Chessboard chessboard = new Chessboard();
            Pawn blackPawn = new Pawn { Color = Pawn.ColorPawn.BLACK, Id = 8 };
            Pawn whitePawn = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 1 };
            Move moveBlack1 = new Move { Target = blackPawn, To = new Coordinate { Row = 3, Column = 2 } };
            Move moveBlack2 = new Move { Target = blackPawn, To = new Coordinate { Row = 4, Column = 3 } };
            Move moveWhite1 = null;
            Move moveWhite2 = new Move { Target = whitePawn, To = new Coordinate { Row = 3, Column = 4 } };
            int numWhitePawns, numBlackPawns;
            bool result;

            chessboard.Grid[2, 1].Upgraded = true;

            result = chessboard.ExecuteMove(moveBlack1);
            Assert.IsTrue(result, "The first black move should be valid!");

            result = chessboard.ExecuteMove(moveWhite1);
            Assert.IsTrue(result, "The first white move should be valid!");

            result = chessboard.ExecuteMove(moveBlack2);
            Assert.IsTrue(result, "The second black move should be valid!");

            result = chessboard.ExecuteMove(moveWhite2);
            Assert.IsFalse(result, "The second white move should not be valid!");//Mossa non valida

            numWhitePawns = chessboard.GetNumberOfPawnsOfColor(Pawn.ColorPawn.WHITE);
            numBlackPawns = chessboard.GetNumberOfPawnsOfColor(Pawn.ColorPawn.BLACK);
            result = numBlackPawns == numWhitePawns;
            Assert.IsTrue(result, "The number of black pawns should be equal to the number of white pawns!");

            result = chessboard.DoesThisPawnExist(blackPawn);
            Assert.IsTrue(result, "This pawn should exist");
        }
        [TestMethod]
        public void ChessboardExecute_CheckingIfPawnCanMoveOnAnotherPawnWithoutEatingIt_ReturnFalse()
        {
            Chessboard chessboard = new Chessboard();
            Pawn blackPawn = new Pawn { Color = Pawn.ColorPawn.BLACK, Id = 8 };
            Pawn whitePawn = new Pawn { Color = Pawn.ColorPawn.WHITE, Id = 1 };
            Move moveBlack1 = new Move { Target = blackPawn, To = new Coordinate { Row = 3, Column = 2 } };
            Move moveBlack2 = new Move { Target = blackPawn, To = new Coordinate { Row = 4, Column = 3 } };
            Move moveWhite = new Move { Target = whitePawn, To = new Coordinate { Row = 4, Column = 3 } };
            bool result;

            result = chessboard.ExecuteMove(moveBlack1);
            Assert.IsTrue(result, "The first black move should be valid!");

            result = chessboard.ExecuteMove(moveWhite);
            Assert.IsTrue(result, "The first white move should be valid!");

            result = chessboard.ExecuteMove(moveBlack2);
            Assert.IsFalse(result, "The second black move should not be valid!");
        }
    }
}
