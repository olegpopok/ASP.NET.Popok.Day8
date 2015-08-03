using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class ElementChangeEventArgs : EventArgs
    {
        private readonly int _leftIndex, _rightIndex;

        public ElementChangeEventArgs(int i, int j)
        {
            _leftIndex = i;
            _rightIndex = j;
        }

        public int LeftIndex { get { return _leftIndex; } }
        public int RightIndex { get { return _rightIndex; } }
    }
}
