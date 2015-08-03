using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class SymmetricalMatrix<T> : SquareMatrix<T> where T : struct, IEquatable<T>
    {
        public SymmetricalMatrix(int order, T value)
        {
            if (order < 0)
            {
                throw new ArgumentException("Order.");
            }
            _order = order;
            int length = GetLengthArrayByOrder(order);
            _array = new T[length];

            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = value;
            }
        }

        public SymmetricalMatrix(int order)
            : this(order, default(T))
        { }

        public SymmetricalMatrix(T[,] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }

            if (array.GetLength(0) != array.GetLength(1))
            {
                throw new ArgumentOutOfRangeException();
            }

            _order = array.GetLength(0);
            int length = GetLengthArrayByOrder(_order);
            _array = new T[length];
            int index = 0;

            for (int i = 0; i < _order; i++)
            {
                for (int j = i; j < _order; j++)
                {
                    if (j == i)
                    {
                        _array[index] = array[i, j];
                        index++;
                    }
                    else
                    {
                        if (!array[i, j].Equals(array[j, i]))
                        {
                            throw new ArgumentException();
                        }
                        _array[index] = array[i, j];
                        index++;
                    }
                }
            }
            index++;
        }

        public override T this[int i, int j]
        {
            get
            {
                if (i > _order || j > _order || j < 0 || i < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                int index;

                if (j >= i)
                {
                    index = GetIndex(i, j);
                }
                else
                {
                    index = GetIndex(j, i);
                }
                return _array[index];
            }
            set
            {
                if (i > _order || j > _order || j < 0 || i < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                int index;

                if (j >= i)
                {
                    index = GetIndex(i, j);
                }
                else
                {
                    index = GetIndex(j, i);
                }
                _array[index] = value;
                ElementChangeEventArgs e = new ElementChangeEventArgs(i, j);
                base.OnElementChange(e);
            }
        }

        private int GetLengthArrayByOrder(int order)
        {
            if (order == 0)
            {
                return 0;
            }
            else
            {
                return order + GetLengthArrayByOrder(order - 1);
            }
        }

        private int GetIndex(int i, int j)
        {
            int index = 0;
            int temp = 0 ;
            while (i > 0)
            {
                index += _order - temp;
                temp++;
                i--;
                j--;
            }

            return index + j;
        }
    }
}
