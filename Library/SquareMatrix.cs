using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Library
{
    public class SquareMatrix<T> : IEquatable<SquareMatrix<T>> where T : struct, IEquatable<T> 
    {
        protected T[] _array;
        protected int _order;

        protected SquareMatrix() { }

        public SquareMatrix (int order)
            :this(order, default(T)) 
        {

        }

        public SquareMatrix (int order, T value)
        {
            if(order < 0)
            {
                throw new ArgumentException("Order.");
            }

            _order = order;

            _array = new T[order*order];

            for(int i = 0; i < _array.Length; i++)
            {
                _array[i] = value;
            }
        }
        
        public SquareMatrix (int order, T[] array)
        {
            if(array == null)
            {
                throw new ArgumentNullException("Array.");
            }

            if(order*order != array.Length )
            {
                throw new ArgumentException("Length of array.");
            }

            _array = new T[array.Length];
            Array.Copy(array, 0, _array, 0, array.Length);
            _order = order;
        }

        public SquareMatrix (T[,] array)
        {
            if(array == null)
            {
                throw new ArgumentNullException("Array.");
            }

            if(array.GetLength(0) != array.GetLength(1))
            {
                throw new ArgumentException("Array.");

            }
            _order = array.GetLength(0);
            _array = new T[_order*_order];
            int index = 0;
            foreach(T item in array)
            {
                _array[index] = item;
                index++;
            }
        }

        public int Order 
        {
            get { return _order; }

        }

        public bool Equals(SquareMatrix<T> other)
        {
            if(ReferenceEquals(other, null))
            {
                return false;
            }

            if(ReferenceEquals(this, other))
            {
                return true;
            }

            if(this._order != other._order)
            {
                return false;
            }

            for(int i = 0; i < this._order; i++)
            {
                for(int j = 0; j < this._order; j++)
                {
                    if(!this[i,j].Equals(other[i,j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator == (SquareMatrix<T> lhs, SquareMatrix<T> rhs)
        {
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
            {
                return ReferenceEquals(lhs, rhs);
            }
            else
            {
                return lhs.Equals(rhs);
            }
        }

        public static bool operator != (SquareMatrix<T> lhs, SquareMatrix<T> rhs)
        {
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
            {
                return ReferenceEquals(lhs, rhs);
            }
            else
            {
                return !lhs.Equals(rhs);
            }
        }

        public event EventHandler<ElementChangeEventArgs> ElementChange;

        public virtual T this[int i, int j]
        {
            get
            {
                if (i >= _order || j >= _order || i < 0 || j < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                return _array[j * _order + i];
            }
            set
            {
                if (i >= _order || j >= _order || i < 0 || j < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                _array[j * _order + i] = value;
                ElementChangeEventArgs e = new ElementChangeEventArgs(i,j);
                OnElementChange(e);
            }
        }

        public override bool Equals(object other)
        {
            if(other == null)
            {
                return false;
            }

            if(this == other)
            {
                return true;
            }

            SquareMatrix<T> matrix = other as SquareMatrix<T>;
            if(matrix == null)
            {
                return false;
            }

            return this.Equals(matrix);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int sumGhc = 0;
                for (int i = 0; i < _order; i++)
                {
                    for(int j = 0; j < _order; j++)
                    {
                        sumGhc += this[i, j].GetHashCode();
                    }
                }
                return sumGhc;
            }

        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for(int i = 0; i < _order; i++)
            {
                for(int j = 0; j < _order; j++)
                {
                    result.Append(this[i,j].ToString() + " ");
                }
                result.Append('\n');
            }
            return result.ToString();
        }

        protected virtual void OnElementChange(ElementChangeEventArgs e)
        {
            EventHandler<ElementChangeEventArgs> temp = Interlocked.CompareExchange(ref ElementChange, null, null);

            if(temp != null)
            {
                temp(this, e);
            }
        }
    }
}
