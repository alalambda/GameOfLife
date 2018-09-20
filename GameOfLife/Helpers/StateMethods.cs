using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Helpers
{
    public static class StateMethods
    {
        public static T GetRandomState<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T) v.GetValue(new Random().Next(v.Length));
        }
    }
}
