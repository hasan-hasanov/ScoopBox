using System;

namespace ScoopBox
{
    internal class DateTimeAbstractions
    {
        public static long GetTicks()
        {
            return DateTime.Now.Ticks;
        }
    }
}
