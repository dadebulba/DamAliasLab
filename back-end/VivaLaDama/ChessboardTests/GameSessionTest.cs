using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VivaLaDama.Controllers;
using VivaLaDama.Models;

namespace VivaLaDamaTests
{
    [TestClass]
    public class GameSessionTest
    {
        private readonly GameSessionContext _context;
        private GameSessionController _controller;
        public GameSessionTest()
        {
            var options = new DbContextOptionsBuilder<GameSessionContext>()
            .UseInMemoryDatabase(databaseName: "GameSessionTestingDB")
            .Options;

            this._context = new GameSessionContext(options);
            this._controller = new GameSessionController(_context);
        }

        [TestMethod]
        public void RemoveLastMoveTest()
        {
            GameSession game = new GameSession("gino", "pino");
            Pawn blackPawn = new Pawn { Color = Pawn.ColorPawn.BLACK, Id = 8 };
            Move moveBlack = new Move { Target = blackPawn, To = new Coordinate { Row = 3, Column = 0 } };
            game.ExecuteMove(moveBlack);
            _context.Add(game);
            _context.SaveChanges();
            int performedMoves = game.Moves.Count;
            _controller.DeleteLastMoveFromDb(game);
            GameSession gameToCheck = _context.GameSessions.Find(game.IdGame);
            Assert.IsTrue(gameToCheck.Moves.Count == performedMoves - 1, "Should remove last move");
        }
    }
}
