using Recon.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recon.Util
{
    public static class Convert<T>
    {
        public static mega<T> ToMega(List<T> values)
        {
            mega<T> mega = new mega<T>();
            foreach (T element in values)
                mega.Add(element);
            return mega;
        }
    }
}
