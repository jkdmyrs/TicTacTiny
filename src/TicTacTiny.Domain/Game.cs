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
            bool? winner = CheckWinner(newBoard, hexBoard);
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

        // todo - improve this with bitwise maths
        private static bool? CheckWinner(bool[] board, UInt32 hexBoard)
        {
            // check rows
            if ((hexBoard & MaskConstants.MASK_WIN_ROW1) == MaskConstants.MASK_WIN_ROW1
                || (hexBoard & MaskConstants.MASK_WIN_ROW2) == MaskConstants.MASK_WIN_ROW2
                || (hexBoard & MaskConstants.MASK_WIN_ROW3) == MaskConstants.MASK_WIN_ROW3)
            {
                return true;
            }
            UInt32 invertedBoard = hexBoard ^ MaskConstants.MASK_FLIP_GAME;
            if ((invertedBoard & MaskConstants.MASK_WIN_ROW1) == MaskConstants.MASK_WIN_ROW1
                || (invertedBoard & MaskConstants.MASK_WIN_ROW2) == MaskConstants.MASK_WIN_ROW2
                || (invertedBoard & MaskConstants.MASK_WIN_ROW3) == MaskConstants.MASK_WIN_ROW3)
            {
                return false;
            }

            // check columns
            List<(bool, bool)> spots = new();
            var workingBoard = (bool[])board.Clone();
            for (int i = 0; i < 9; i++)
            {
                var spot = workingBoard.Take(2).ToArray();
                spots.Add((spot[0], spot[1]));
                workingBoard = workingBoard.Skip(2).ToArray();
            }
            for (int i = 0; i < 3; i++)
            {
                List<(bool, bool)> col = new();
                col.Add(spots[i]);
                col.Add(spots[i + 3]);
                col.Add(spots[i + 6]);
                if (col.All(x => x.Item1 == true && x.Item2 == true))
                {
                    return true;
                }
                if (col.All(x => x.Item1 == true && x.Item2 == false))
                {
                    return false;
                }
            }

            // check diagonals
            List<(bool, bool)> diag1 = new();
            diag1.Add(spots[0]);
            diag1.Add(spots[4]);
            diag1.Add(spots[8]);
            if (diag1.All(x => x.Item1 == true && x.Item2 == true))
            {
                return true;
            }
            if (diag1.All(x => x.Item1 == true && x.Item2 == false))
            {
                return false;
            }

            List<(bool, bool)> diag2 = new();
            diag2.Add(spots[2]);
            diag2.Add(spots[4]);
            diag2.Add(spots[6]);
            if (diag2.All(x => x.Item1 == true && x.Item2 == true))
            {
                return true;
            }
            if (diag2.All(x => x.Item1 == true && x.Item2 == false))
            {
                return false;
            }

            return null;
        }
    }
}