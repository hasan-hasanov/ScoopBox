using System;

namespace ScoopBox.Abstractions
{
    public class DateTimeAbstractions
    {
        public static long GetTicks()
        {
            return DateTime.Now.Ticks;
        }
    }
}
