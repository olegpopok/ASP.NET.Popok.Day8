using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class DiagonalMatrix<T> : SquareMatrix<T> where T : struct, IEquatable<T> 
    {
        public DiagonalMatrix(int order)
            :this(order,default(T))
        {
        }

        public DiagonalMatrix(int order, T[] array)
        {
            if(array == null)
            {
                throw new ArgumentNullException("Array.");
            }
            if (order != array.Length)
            {
                throw new ArgumentException("Length of array.");
            }
            this._order = order;
            this._array = new T[order];
            Array.Copy(array,0,_array,0,order);
        }

        public DiagonalMatrix(int order, T value)
        { 
            if(order < 0)
            {
                throw new ArgumentOutOfRangeException("Order");
            }
            _order = order;
            _array = new T[order];
            for (int i = 0; i < order; i++)
            {
                _array[i] = value;
            }
        }

        public override T this[int i, int j]
        {
            get
            {
                if(i >= _order || j >= _order || i < 0 || j < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                if(i == j)
                {
                    return _array[i];
                }

                return default(T);
            }
            set
            {
                if(j != i)
                {
                    throw new InvalidOperationException("Cannot set an off-diagonal element in a diagonal matrix");
                }
                _array[i] = value;
                ElementChangeEventArgs e = new ElementChangeEventArgs(i, j);
                base.OnElementChange(e);
            }
        }

    }
}
