using jkdmyrs.TicTacTiny.Domain;

namespace jkdmyrs.TicTacTiny.Web.Components
{
    public class GameViewModel
    {
        private readonly Game _game;

        public GameViewModel(string hexStr)
        {
            _game = new(hexStr);
        }

        public char[] Board => GetBoard();

        public bool HasWinner => _game.HasWinner;

        public string GameStatus => this.HasWinner
            ? (_game.Winner ? 'X' : 'O') + " wins"
            : _game.CurrentMove ? "X" : "O";

        public (int, int, int) GetWinningPositions()
        {
            uint winningMask = FindWinningMask();
            return winningMask switch
            {
                MaskConstants.MASK_WIN_ROW1 => (0, 1, 2),
                MaskConstants.MASK_WIN_ROW2 => (3, 4, 5),
                MaskConstants.MASK_WIN_ROW3 => (6, 7, 8),
                MaskConstants.MASK_WIN_COL1 => (0, 3, 6),
                MaskConstants.MASK_WIN_COL2 => (1, 4, 7),
                MaskConstants.MASK_WIN_COL3 => (2, 5, 8),
                MaskConstants.MASK_WIN_DIAG1 => (2, 4, 6),
                MaskConstants.MASK_WIN_DIAG2 => (0, 4, 8),
                _ => throw new Exception("Unexpected"),
            };
        }

        private uint FindWinningMask()
        {
            List<uint> winningMasks = new()
            {
                MaskConstants.MASK_WIN_ROW1,
                MaskConstants.MASK_WIN_ROW2,
                MaskConstants.MASK_WIN_ROW3,
                MaskConstants.MASK_WIN_COL1,
                MaskConstants.MASK_WIN_COL2,
                MaskConstants.MASK_WIN_COL3,
                MaskConstants.MASK_WIN_DIAG1,
                MaskConstants.MASK_WIN_DIAG2,
            };
            for (int i = 0; i < Board.Length; i++)
            {
                if ((_game.Board & winningMasks[i]) == winningMasks[i])
                {
                    return winningMasks[i];
                }
            }
            for (int i = 0; i < Board.Length; i++)
            {
                if (((_game.Board ^ MaskConstants.MASK_FLIP_GAME) & winningMasks[i]) == winningMasks[i])
                {
                    return winningMasks[i];
                }
            }
            throw new Exception("Unexpected");
        }

        private char CheckPosition(int position)
        {
            var occupied = (_game.Board & MaskConstants.MASK_MOVE_POSITIONS[position]) == MaskConstants.MASK_MOVE_POSITIONS[position];
            bool? check = occupied
                ? (_game.Board & MaskConstants.MASK_MOVE_X_POSITIONS[position]) == MaskConstants.MASK_MOVE_X_POSITIONS[position]
                : null;
            return check.HasValue
                        ? check.Value ? 'X' : 'O'
                        : ' ';
        }

        private char[] GetBoard()
        {
            char[] board = new char[9];
            Enumerable.Range(0, 9).ToList().ForEach(x =>
            {
                board[x] = CheckPosition(x);
            });
            return board;
        }
    }
}
