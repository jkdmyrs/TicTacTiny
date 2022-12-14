using System;
using jkdmyrs.TicTacTiny.Domain.Exceptions;

namespace jkdmyrs.TicTacTiny.Domain.Tests
{
	public class GameTests
    {
        [Fact]
        public void QuickGame_XWins()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.ToString().Should().Be(DomainConstants.NEW_GAME);
            game.Board.Should().Be((uint)2);
            game.Move(1, 1);
            game.Board.Should().Be((uint)196608);
            game.Move(0, 0);
            game.Board.Should().Be((uint)720898);
            game.Move(1, 4);
            game.Board.Should().Be((uint)723968);
            game.Move(0, 2);
            game.Board.Should().Be((uint)756738);
            game.Move(1, 7);
            game.Board.Should().Be((uint)756787);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeTrue("because X won");
        }

        [Fact]
        public void QuickGame_OWins()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Board.Should().Be((uint)2);
            game.Move(1, 8);
            game.Board.Should().Be((uint)12);
            game.Move(0, 1);
            game.Board.Should().Be((uint)131086);
            game.Move(1, 0);
            game.Board.Should().Be((uint)917516);
            game.Move(0, 4);
            game.Board.Should().Be((uint)919566);
            game.Move(1, 2);
            game.Board.Should().Be((uint)968716);
            game.Move(0, 7);
            game.Board.Should().Be((uint)968749);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeFalse("because O won");
        }

        [Fact]
        public void Row1MaskCheck_X()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 0);
            game.Move(0, 3);
            game.Move(1, 1);
            game.Move(0, 4);
            game.Move(1, 2);
            (game.Board & MaskConstants.MASK_WIN_ROW1).Should().Be(MaskConstants.MASK_WIN_ROW1);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeTrue("because X won");
        }

        [Fact]
        public void Row2MaskCheck_X()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 3);
            game.Move(0, 0);
            game.Move(1, 4);
            game.Move(0, 1);
            game.Move(1, 5);
            (game.Board & MaskConstants.MASK_WIN_ROW2).Should().Be(MaskConstants.MASK_WIN_ROW2);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeTrue("because X won");
        }

        [Fact]
        public void Row3MaskCheck_X()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 6);
            game.Move(0, 0);
            game.Move(1, 7);
            game.Move(0, 1);
            game.Move(1, 8);
            (game.Board & MaskConstants.MASK_WIN_ROW3).Should().Be(MaskConstants.MASK_WIN_ROW3);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeTrue("because X won");
        }

        [Fact]
        public void Col1MaskCheck_X()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 0);
            game.Move(0, 1);
            game.Move(1, 3);
            game.Move(0, 2);
            game.Move(1, 6);
            (game.Board & MaskConstants.MASK_WIN_COL1).Should().Be(MaskConstants.MASK_WIN_COL1);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeTrue("because X won");
        }

        [Fact]
        public void Col2MaskCheck_X()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 1);
            game.Move(0, 0);
            game.Move(1, 4);
            game.Move(0, 3);
            game.Move(1, 7);
            (game.Board & MaskConstants.MASK_WIN_COL2).Should().Be(MaskConstants.MASK_WIN_COL2);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeTrue("because X won");
        }

        [Fact]
        public void Col3MaskCheck_X()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 2);
            game.Move(0, 0);
            game.Move(1, 5);
            game.Move(0, 3);
            game.Move(1, 8);
            (game.Board & MaskConstants.MASK_WIN_COL3).Should().Be(MaskConstants.MASK_WIN_COL3);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeTrue("because X won");
        }

        [Fact]
        public void Row1MaskCheck_O()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 8);
            game.Move(0, 0);
            game.Move(1, 3);
            game.Move(0, 1);
            game.Move(1, 5);
            game.Move(0, 2);
            ((game.Board ^ MaskConstants.MASK_FLIP_GAME) & MaskConstants.MASK_WIN_ROW1).Should().Be(MaskConstants.MASK_WIN_ROW1);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeFalse("because O won");
        }

        [Fact]
        public void Row2MaskCheck_O()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 8);
            game.Move(0, 3);
            game.Move(1, 0);
            game.Move(0, 4);
            game.Move(1, 2);
            game.Move(0, 5);
            ((game.Board ^ MaskConstants.MASK_FLIP_GAME) & MaskConstants.MASK_WIN_ROW2).Should().Be(MaskConstants.MASK_WIN_ROW2);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeFalse("because O won");
        }

        [Fact]
        public void Row3MaskCheck_O()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 4);
            game.Move(0, 6);
            game.Move(1, 0);
            game.Move(0, 7);
            game.Move(1, 2);
            game.Move(0, 8);
            ((game.Board ^ MaskConstants.MASK_FLIP_GAME) & MaskConstants.MASK_WIN_ROW3).Should().Be(MaskConstants.MASK_WIN_ROW3);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeFalse("because O won");
        }

        [Fact]
        public void Col1MaskCheck_O()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 1);
            game.Move(0, 0);
            game.Move(1, 5);
            game.Move(0, 3);
            game.Move(1, 7);
            game.Move(0, 6);
            ((game.Board ^ MaskConstants.MASK_FLIP_GAME) & MaskConstants.MASK_WIN_COL1).Should().Be(MaskConstants.MASK_WIN_COL1);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeFalse("because O won");
        }

        [Fact]
        public void Col2MaskCheck_O()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 0);
            game.Move(0, 1);
            game.Move(1, 5);
            game.Move(0, 4);
            game.Move(1, 6);
            game.Move(0, 7);
            ((game.Board ^ MaskConstants.MASK_FLIP_GAME) & MaskConstants.MASK_WIN_COL2).Should().Be(MaskConstants.MASK_WIN_COL2);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeFalse("because O won");
        }

        [Fact]
        public void Col3MaskCheck_O()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 1);
            game.Move(0, 2);
            game.Move(1, 3);
            game.Move(0, 5);
            game.Move(1, 7);
            game.Move(0, 8);
            ((game.Board ^ MaskConstants.MASK_FLIP_GAME) & MaskConstants.MASK_WIN_COL3).Should().Be(MaskConstants.MASK_WIN_COL3);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeFalse("because O won");
        }

        [Fact]
        public void Diag1MaskCheck_X()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 2);
            game.Move(0, 0);
            game.Move(1, 4);
            game.Move(0, 8);
            game.Move(1, 6);
            (game.Board & MaskConstants.MASK_WIN_DIAG1).Should().Be(MaskConstants.MASK_WIN_DIAG1);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeTrue("because X won");
        }

        [Fact]
        public void Diag2MaskCheck_X()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 0);
            game.Move(0, 2);
            game.Move(1, 4);
            game.Move(0, 3);
            game.Move(1, 8);
            (game.Board & MaskConstants.MASK_WIN_DIAG2).Should().Be(MaskConstants.MASK_WIN_DIAG2);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeTrue("because X won");
        }

        [Fact]
        public void Diag1MaskCheck_O()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 3);
            game.Move(0, 2);
            game.Move(1, 5);
            game.Move(0, 4);
            game.Move(1, 7);
            game.Move(0, 6);
            ((game.Board ^ MaskConstants.MASK_FLIP_GAME) & MaskConstants.MASK_WIN_DIAG1).Should().Be(MaskConstants.MASK_WIN_DIAG1);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeFalse("because O won");
        }

        [Fact]
        public void Diag2MaskCheck_O()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 3);
            game.Move(0, 0);
            game.Move(1, 5);
            game.Move(0, 4);
            game.Move(1, 7);
            game.Move(0, 8);
            ((game.Board ^ MaskConstants.MASK_FLIP_GAME) & MaskConstants.MASK_WIN_DIAG2).Should().Be(MaskConstants.MASK_WIN_DIAG2);
            game.HasWinner.Should().BeTrue("because we have a winner");
            game.Winner.Should().BeFalse("because O won");
        }

        [Fact]
        public void CannotPlayInSamePositionTwice()
        {
            Game game = new(DomainConstants.NEW_GAME);
            game.Move(1, 3);
            Action act = () => game.Move(0, 3);
            act.Should().Throw<InvalidMoveException>();
        }
    }
}