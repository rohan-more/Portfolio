namespace AISandbox
{
    public static class BitManipulator
    {
        public static bool IsBitSet(byte number, byte bitNumber)
        {
            byte bit_mask = 0x80;
            return (number & (bit_mask >> bitNumber)) != 0;
        }
        public static bool IsBitClear(byte number, byte bitNumber)
        {
            return !IsBitSet(number, bitNumber);
        }
        public static void SetBit(ref byte number, byte bitNumber)
        {
            byte bit_mask = 0x80;
            number = (byte)(number | (bit_mask >> bitNumber));
        }
        public static void ClearBit(ref byte number, byte bitNumber)
        {
            byte bit_mask = 0x80;
            number = (byte)(number & ~(bit_mask >> bitNumber));
        }
    }
}

