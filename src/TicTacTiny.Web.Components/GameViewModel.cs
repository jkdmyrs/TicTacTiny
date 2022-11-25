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
