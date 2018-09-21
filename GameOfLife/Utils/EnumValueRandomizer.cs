using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Utils
{
    public static class EnumValueRandomizer
    {
        public static T GetRandomEnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T) v.GetValue(new Random().Next(v.Length));
        }
    }
}
