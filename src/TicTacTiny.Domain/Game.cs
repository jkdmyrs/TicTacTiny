using System;
using System.Collections;
using System.Numerics;
using jkdmyrs.TicTacTiny.Domain.Exceptions;
using jkdmyrs.TicTacTiny.Domain.Extensions;

namespace jkdmyrs.TicTacTiny.Domain
{
  public class Game
  {
        public UInt32 hexBoard { get; set; }
        public bool[] Board { get; private set; }
        private bool[] _controlBits => Board.Skip(18).ToArray();

        public bool HasWinner => _hasWinnerBit;
        private bool _hasWinnerBit => _controlBits[1];

        private bool _hybridBit => _controlBits[0];
        public bool Winner
        {
            get
            {
                if (!_hasWinnerBit)
                {
                    throw new InvalidOperationException("Game not finished.");
                }
                return _hybridBit;
            }
        }
        public bool CurrentMove
        {
            get
            {
                if (_hasWinnerBit)
                {
                    throw new InvalidOperationException("Game finished.");
                }
                return _hybridBit;
            }
        }

        public static Game FromCode(string hexStr)
        {
            try
            {
                var hex = hexStr.AsSpan();
                var board = new bool[20];
                int b = 0;
                for (int i = 0; i < hex.Length; i++)
                {
                    BitArray bits = new BitArray(new int[] { int.Parse(hex.Slice(i, 1), style: System.Globalization.NumberStyles.HexNumber) });
                    var ourBits = new bool[4]
                    {
                        bits[3], bits[2], bits[1], bits[0]
                    };
                    board[b] = ourBits[0];
                    board[b + 1] = ourBits[1];
                    board[b + 2] = ourBits[2];
                    board[b + 3] = ourBits[3];
                    b += 4;
                }

                return new Game(board);
            }
            catch
            {
                throw new InvalidBoardException();
            }
        }

        private Game(bool[] board)
        {
            Board = board;
            hexBoard = Convert.ToUInt32(this.ToString(), 16);
        }

        public Game Move(int player, int position)
        {
            // todo - verify position is open
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
            if (Board is null)
            {
                // unexpected
                throw new Exception();
            }
            var newBoard = (bool[])Board.Clone();
            int i = position * 2;
            newBoard[i] = true;
            newBoard[i+1] = move;
            hexBoard = Convert.ToUInt32(new Game(newBoard).ToString(), 16);
            bool? winner = CheckWinner(hexBoard);
            var reversed = newBoard.Reverse().ToArray();
            if (winner is not null)
            {
                reversed[0] = true;
                reversed[1] = (bool)winner;
            }
            else
            {
                reversed[0] = false;
                reversed[1] = !move;
            }
            Board = reversed.Reverse().ToArray();
            hexBoard = Convert.ToUInt32(this.ToString(), 16);
            return this;
        }

        public override string ToString()
        {
            bool[] zero = new bool[] { false, false, false, false };
            var bits = (bool[])Board.Clone();
            char[] hex = new char[5];
            for (int i = 0; i < 5; i++)
            {
                BitArray arr = new BitArray(zero.Concat(bits.Take(4)).Reverse().ToArray());
                byte[] data = new byte[1];
                arr.CopyTo(data, 0);
                var str = BitConverter.ToString(data);
                hex[i] = str[1];
                bits = bits.Skip(4).ToArray();
            }
            return string.Join(string.Empty, hex);
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
    }
}