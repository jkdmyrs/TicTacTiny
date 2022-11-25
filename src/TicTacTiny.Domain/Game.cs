using System;
using System.Collections;
using System.Numerics;
using jkdmyrs.TicTacTiny.Domain.Exceptions;
using jkdmyrs.TicTacTiny.Domain.Extensions;

namespace jkdmyrs.TicTacTiny.Domain
{
    public class Game
    {
        internal UInt32 Board { get; private set; }

        public bool HasWinner => (Board & MaskConstants.MASK_WINNER) == MaskConstants.MASK_WINNER;
        public bool Winner
        {
            get
            {
                if (!HasWinner)
                {
                    throw new InvalidOperationException("Game not finished.");
                }
                return (Board & MaskConstants.MASK_WINNER_X) == MaskConstants.MASK_WINNER_X;
            }
        }
        public bool CurrentMove
        {
            get
            {
                if (HasWinner)
                {
                    throw new InvalidOperationException("Game finished.");
                }
                return (Board & MaskConstants.MASK_NEXT_MOVE_X) == MaskConstants.MASK_NEXT_MOVE_X;
            }
        }

        public Game(string hexStr)
        {
            Board = Convert.ToUInt32(hexStr, 16);
        }

        public Game Move(int player, int position)
        {
            bool move = player == 1;
            if (!(player == 1 || player == 0))
            {
                throw new InvalidMoveException(string.Format(CopyTextConstants.INVALID_PLAYER_FMT, player));
            }
            if (move != CurrentMove)
            {
                throw new InvalidMoveException(string.Format(CopyTextConstants.WRONG_PLAYER_FMT, move.ToPlayerCopyText(), CurrentMove.ToPlayerCopyText()));
            }
            if (position < 0 || position > 8)
            {
                throw new InvalidMoveException(string.Format(CopyTextConstants.INVALID_POSITION_FMT, position));
            }

            void Move()
            {
                if ((Board & MaskConstants.MASK_MOVE_POSITIONS[position]) == MaskConstants.MASK_MOVE_POSITIONS[position])
                {
                    throw new InvalidMoveException(string.Format(CopyTextConstants.DUPLICATE_MOVE_FMT, position));
                }
                Board = Board | MaskConstants.MASK_MOVE_POSITIONS[position];
                Board = move ? Board | MaskConstants.MASK_MOVE_X_POSITIONS[position] : Board;
            }

            Move();
            bool? winner = CheckWinner(Board);
            if (winner is null)
            {
                Board = Board & MaskConstants.MASK_NO_WINNER;
                if (move)
                {
                    Board = Board & MaskConstants.MASK_NEXT_MOVE_0;
                }
                else
                {
                    Board = Board | MaskConstants.MASK_NEXT_MOVE_X;
                }
            }
            else
            {
                Board = Board | MaskConstants.MASK_WINNER;
                if (winner.Value)
                {
                    Board = Board | MaskConstants.MASK_WINNER_X;
                }
                else
                {
                    Board = Board & MaskConstants.MASK_WINNER_O;
                }
            }
            return this;
        }

        private static bool? CheckWinner(UInt32 hexBoard)
        {
            bool? RunCheck(Func<uint, bool> check, uint board)
            {
                if (check(hexBoard))
                {
                    return true;
                }
                if (check(hexBoard ^ MaskConstants.MASK_FLIP_GAME))
                {
                    return false;
                }
                return null;
            }

            // check rows
            bool RowCheck(uint board)
            {
                return (board & MaskConstants.MASK_WIN_ROW1) == MaskConstants.MASK_WIN_ROW1
                || (board & MaskConstants.MASK_WIN_ROW2) == MaskConstants.MASK_WIN_ROW2
                || (board & MaskConstants.MASK_WIN_ROW3) == MaskConstants.MASK_WIN_ROW3;
            }
            var check = RunCheck(RowCheck, hexBoard);
            if (check is not null)
            {
                return check;
            }

            // check columns
            bool ColumnCheck(uint board)
            {
                return (board & MaskConstants.MASK_WIN_COL1) == MaskConstants.MASK_WIN_COL1
                || (board & MaskConstants.MASK_WIN_COL2) == MaskConstants.MASK_WIN_COL2
                || (board & MaskConstants.MASK_WIN_COL3) == MaskConstants.MASK_WIN_COL3;
            }
            check = RunCheck(ColumnCheck, hexBoard);
            if (check is not null)
            {
                return check;
            }

            // check diagonals
            bool DiagonalCheck(uint board)
            {
                return (board & MaskConstants.MASK_WIN_DIAG1) == MaskConstants.MASK_WIN_DIAG1
                    || (board & MaskConstants.MASK_WIN_DIAG2) == MaskConstants.MASK_WIN_DIAG2;
            }
            return RunCheck(DiagonalCheck, hexBoard);
        }

        public override string ToString()
        {
            return Board.ToString("X").PadLeft(5, '0');
        }
    }
}