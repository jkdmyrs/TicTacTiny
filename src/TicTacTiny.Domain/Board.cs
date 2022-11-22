using System;
using System.Collections;

namespace jkdmyrs.TicTacTiny.Domain
{
  public class Board
  {
        public bool[] BoardArr { get; private set; }
        public bool HasWinner { get; private set; }
        public bool Winner { get; private set; }
        public bool CurrentMove { get; private set; }

        public Board(string hexStr)
        {
            var hex = hexStr.AsSpan();
            BoardArr = new bool[20];
            int b = 0;
            for (int i = 0; i < hex.Length; i++)
            {
                BitArray bits = new BitArray(new int[] { int.Parse(hex.Slice(i, 1), style: System.Globalization.NumberStyles.HexNumber) });
                var ourBits = new bool[4]
                {
                    bits[3], bits[2], bits[1], bits[0]
                };
                BoardArr[b] = ourBits[0];
                BoardArr[b + 1] = ourBits[1];
                BoardArr[b + 2] = ourBits[2];
                BoardArr[b + 3] = ourBits[3];
                b += 4;
            }

            var reverse = BoardArr.Reverse().ToArray();
            HasWinner = reverse[0];
            Winner = reverse[1];
            CurrentMove = reverse[1];
        }

        private Board(bool[] board)
        {
            BoardArr = board;
            var reverse = board.Reverse().ToArray();
            HasWinner = reverse[0];
            Winner = reverse[1];
            CurrentMove = reverse[1];
        }

        public Board Move(bool move, int offset)
        {
            if (BoardArr is null)
            {
                throw new NullReferenceException(nameof(BoardArr));
            }
            if (move != CurrentMove)
            {
                throw new ArgumentException("Wrong player attempted a move.", nameof(move));
            }
            var newBoard = (bool[])BoardArr.Clone();
            int i = offset * 2;
            newBoard[i] = true;
            newBoard[i+1] = move;
            bool? winner = CheckWinner(newBoard);
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
            return new(reversed.Reverse().ToArray());
        }

        public override string ToString()
        {
            bool[] zero = new bool[] { false, false, false, false };
            var bits = (bool[])BoardArr.Clone();
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

        private static bool? CheckWinner(bool[] board)
        {
            // check rows
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
                var row = spots.Take(3);
                if (row.All(x => x.Item1 == true && x.Item2 == true))
                {
                    return true;
                }
                if (row.All(x => x.Item1 == true && x.Item2 == false))
                {
                    return false;
                }
                spots = spots.Skip(3).ToList();
            }

            // check columns
            spots = new();
            workingBoard = (bool[])board.Clone();
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