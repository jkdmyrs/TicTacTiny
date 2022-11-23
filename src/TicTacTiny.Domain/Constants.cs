using System;

namespace jkdmyrs.TicTacTiny.Domain
{
    public static class DomainConstants
    {
        public const string NEW_GAME = "00002";
    }

    public static class CopyTextConstants
    {
        public const string INVALID_PLAYER_FMT = "Invalid player: {0}. Player must be 1 (X) or 0 (O).";
        public const string WRONG_PLAYER_FMT = "Invalid player: {0}. Current player: {1}";
        public const string INVALID_POSITION_FMT = "Invalid position: {0}. Position must be 0-8.";
    }
}

