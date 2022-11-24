using System;
namespace jkdmyrs.TicTacTiny.Domain.Tests
{
	public class GameTests
    {
        [Fact]
        public void QuickGame_XWins()
        {
            var game = Game.FromCode(DomainConstants.NEW_GAME);
            game.hexBoard.Should().Be((uint)2);
            game.Move(1, 1);
            game.hexBoard.Should().Be((uint)196608);
            game.Move(0, 0);
            game.hexBoard.Should().Be((uint)720898);
            game.Move(1, 4);
            game.hexBoard.Should().Be((uint)723968);
            game.Move(0, 2);
            game.hexBoard.Should().Be((uint)756738);
            game.Move(1, 7);
            game.hexBoard.Should().Be((uint)756787);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeTrue("because X won");
        }

        [Fact]
        public void QuickGame_OWins()
        {
            var game = Game.FromCode(DomainConstants.NEW_GAME);
            game.hexBoard.Should().Be((uint)2);
            game.Move(1, 8);
            game.hexBoard.Should().Be((uint)12);
            game.Move(0, 1);
            game.hexBoard.Should().Be((uint)131086);
            game.Move(1, 0);
            game.hexBoard.Should().Be((uint)917516);
            game.Move(0, 4);
            game.hexBoard.Should().Be((uint)919566);
            game.Move(1, 2);
            game.hexBoard.Should().Be((uint)968716);
            game.Move(0, 7);
            game.hexBoard.Should().Be((uint)968749);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeFalse("because O won");
        }

        [Fact]
        public void Row1MaskCheck_X()
        {
            var game = Game.FromCode(DomainConstants.NEW_GAME);
            game.Move(1, 0);
            game.Move(0, 3);
            game.Move(1, 1);
            game.Move(0, 4);
            game.Move(1, 2);
            (game.hexBoard & MaskConstants.MASK_WIN_ROW1).Should().Be(MaskConstants.MASK_WIN_ROW1);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeTrue("because X won");
        }

        [Fact]
        public void Row2MaskCheck_X()
        {
            var game = Game.FromCode(DomainConstants.NEW_GAME);
            game.Move(1, 3);
            game.Move(0, 0);
            game.Move(1, 4);
            game.Move(0, 1);
            game.Move(1, 5);
            (game.hexBoard & MaskConstants.MASK_WIN_ROW2).Should().Be(MaskConstants.MASK_WIN_ROW2);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeTrue("because X won");
        }

        [Fact]
        public void Row3MaskCheck_X()
        {
            var game = Game.FromCode(DomainConstants.NEW_GAME);
            game.Move(1, 6);
            game.Move(0, 0);
            game.Move(1, 7);
            game.Move(0, 1);
            game.Move(1, 8);
            (game.hexBoard & MaskConstants.MASK_WIN_ROW3).Should().Be(MaskConstants.MASK_WIN_ROW3);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeTrue("because X won");
        }
    }
}