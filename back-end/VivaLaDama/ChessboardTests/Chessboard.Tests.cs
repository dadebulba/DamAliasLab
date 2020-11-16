using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
