public class CustomTools
{

    //*Copilot code*
    public static int FirstBitIndex(int num)
    {
        if (num == 0)
        {
            // If the number is zero, there are no non-zero bits.
            return -1; // Indicate that no non-zero bit was found.
        }

        int index = 0;
        while ((num & 1) == 0)
        {
            // Keep shifting the number to the right until we find a non-zero bit.
            num >>= 1;
            index++;
        }

        return index;
    }
}
