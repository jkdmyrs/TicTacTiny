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
        public const string ROOM_DOES_NOT_EXIST = "Room does not exist.";
        public const string ROOM_IN_USE = "Room name is use. Pick a different name.";
    }

    public static class MaskConstants
    {
        public const UInt32 MASK_WIN_ROW1 = 1032192u;
        public const UInt32 MASK_WIN_ROW2 = 16128u;
        public const UInt32 MASK_WIN_ROW3 = 252u;

        public const UInt32 MASK_WIN_COL1 = 798912u;
        public const UInt32 MASK_WIN_COL2 = 199728u;
        public const UInt32 MASK_WIN_COL3 = 49932u;

        public const uint MASK_WIN_DIAG1 = 52416u;
        public const uint MASK_WIN_DIAG2 = 789516u;

        public const UInt32 MASK_FLIP_GAME = 349524u;
    }
}

