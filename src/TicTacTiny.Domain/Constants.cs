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
        public const string DUPLICATE_MOVE_FMT = "Position {0} is alredy occupied.";
        public const string ROOM_DOES_NOT_EXIST = "Room does not exist.";
        public const string ROOM_IN_USE = "Room name is use. Pick a different name.";
    }

    public static class MaskConstants
    {
        public const UInt32 MASK_WIN_ROW1 = 0b11111100000000000000;
        public const UInt32 MASK_WIN_ROW2 = 0b00000011111100000000;
        public const UInt32 MASK_WIN_ROW3 = 0b00000000000011111100;

        public const UInt32 MASK_WIN_COL1 = 0b11000011000011000000;
        public const UInt32 MASK_WIN_COL2 = 0b00110000110000110000;
        public const UInt32 MASK_WIN_COL3 = 0b00001100001100001100;

        public const UInt32 MASK_WIN_DIAG1 = 0b00001100110011000000;
        public const UInt32 MASK_WIN_DIAG2 = 0b11000000110000001100;

        public readonly static Dictionary<int, uint> MASK_MOVE_POSITIONS = new()
        {
            {0, MASK_MOVE_POS_0},
            {1, MASK_MOVE_POS_1},
            {2, MASK_MOVE_POS_2},
            {3, MASK_MOVE_POS_3},
            {4, MASK_MOVE_POS_4},
            {5, MASK_MOVE_POS_5},
            {6, MASK_MOVE_POS_6},
            {7, MASK_MOVE_POS_7},
            {8, MASK_MOVE_POS_8},
        };
        public const UInt32 MASK_MOVE_POS_0 = 0b10000000000000000000;
        public const UInt32 MASK_MOVE_POS_1 = 0b00100000000000000000;
        public const UInt32 MASK_MOVE_POS_2 = 0b00001000000000000000;
        public const UInt32 MASK_MOVE_POS_3 = 0b00000010000000000000;
        public const UInt32 MASK_MOVE_POS_4 = 0b00000000100000000000;
        public const UInt32 MASK_MOVE_POS_5 = 0b00000000001000000000;
        public const UInt32 MASK_MOVE_POS_6 = 0b00000000000010000000;
        public const UInt32 MASK_MOVE_POS_7 = 0b00000000000000100000;
        public const UInt32 MASK_MOVE_POS_8 = 0b00000000000000001000;

        public readonly static Dictionary<int, uint> MASK_MOVE_X_POSITIONS = new()
        {
            {0, MASK_MOVE_X_POS_0},
            {1, MASK_MOVE_X_POS_1},
            {2, MASK_MOVE_X_POS_2},
            {3, MASK_MOVE_X_POS_3},
            {4, MASK_MOVE_X_POS_4},
            {5, MASK_MOVE_X_POS_5},
            {6, MASK_MOVE_X_POS_6},
            {7, MASK_MOVE_X_POS_7},
            {8, MASK_MOVE_X_POS_8},
        };
        public const UInt32 MASK_MOVE_X_POS_0 = 0b01000000000000000000;
        public const UInt32 MASK_MOVE_X_POS_1 = 0b00010000000000000000;
        public const UInt32 MASK_MOVE_X_POS_2 = 0b00000100000000000000;
        public const UInt32 MASK_MOVE_X_POS_3 = 0b00000001000000000000;
        public const UInt32 MASK_MOVE_X_POS_4 = 0b00000000010000000000;
        public const UInt32 MASK_MOVE_X_POS_5 = 0b00000000000100000000;
        public const UInt32 MASK_MOVE_X_POS_6 = 0b00000000000001000000;
        public const UInt32 MASK_MOVE_X_POS_7 = 0b00000000000000010000;
        public const UInt32 MASK_MOVE_X_POS_8 = 0b00000000000000000100;

        public const UInt32 MASK_FLIP_GAME = 0b01010101010101010100;
        public const UInt32 MASK_NO_WINNER = 0b11111111111111111110;
        public const UInt32 MASK_NEXT_MOVE_X = 0b00000000000000000010;
        public const UInt32 MASK_NEXT_MOVE_0 = 0b11111111111111111101;
        public const UInt32 MASK_WINNER = 0b00000000000000000001;
        public const UInt32 MASK_WINNER_X = 0b00000000000000000010;
        public const UInt32 MASK_WINNER_O = 0b11111111111111111101;
    }
}

