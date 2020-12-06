using Microsoft.VisualStudio.TestTools.UnitTesting;
using VivaLaDama.Models;
using System;
using System.Collections.Generic;
using System.Text;
using VivaLaDama.Controllers;
using Microsoft.EntityFrameworkCore;

namespace VivaLaDama.Models.Tests
{
    [TestClass()]
    public class GameSessionContextTests
    {
        private readonly GameSessionContext context;
        private GameSessionController controller;
        public GameSessionContextTests()
        {
            this.context = new GameSessionContext(new DbContextOptionsBuilder<GameSessionContext>()
                                                    .UseInMemoryDatabase(databaseName: "VivaLaDamaContextTestDb")
                                                    .Options);
            this.controller = new GameSessionController(context);
        }
        [TestMethod()]
        public void GameSessionContextTest_CheckingIfTargetNullAfterTwoMoves()
        {
            Pawn blackPawn = new Pawn { Color = Pawn.ColorPawn.BLACK, PawnId = 8 };
            Pawn whitePawn = new Pawn { Color = Pawn.ColorPawn.WHITE, PawnId = 0 };
            Move moveBlack1 = new Move { Target = blackPawn, To = new Coordinate { Row = 3, Column = 2 } };
            Move moveBlack2 = new Move { Target = blackPawn, To = new Coordinate { Row = 5, Column = 0 } };
            Move moveWhite = new Move { Target = whitePawn, To = new Coordinate { Row = 4, Column = 1 } };
            GameSession game = new GameSession { Game = new Chessboard(), Moves = new List<Move>(), NamePlayer1 = "Luca", NamePlayer2 = "Marco" };
            bool result;

            context.Set<GameSession>().Add(game);
            context.SaveChanges();

            game = this.controller.RetrieveGameSession(game.GameSessionId);
            result = game.ExecuteMove(moveBlack1, true);
            Assert.IsTrue(result, "The first black move should be valid!");
            context.SaveChanges();

            game = this.controller.RetrieveGameSession(game.GameSessionId);
            result = game.ExecuteMove(moveWhite, true);
            Assert.IsTrue(result, "The first white move should be valid!");
            context.SaveChanges();

            game = this.controller.RetrieveGameSession(game.GameSessionId);
            result = game.ExecuteMove(moveBlack2, true);
            Assert.IsTrue(result, "The second black move should be valid!");
            Assert.IsNotNull(moveBlack2.Target, "Should not be null moveBlack2.Target");
            context.SaveChanges();

            Assert.IsNotNull(moveBlack2.Target, "This target should not be null");
        }
    }
}