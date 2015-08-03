using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class Math
    {
        public static SquareMatrix<T> Add<T>(this SquareMatrix<T> lhs, SquareMatrix<T> rhs, Func<T,T,T> func) where T: struct, IEquatable<T>
        {
            if(ReferenceEquals(lhs, null))
            {
                throw new ArgumentNullException("lhs");
            }

            if(ReferenceEquals(rhs, null))
            {
                throw new ArgumentNullException("rhs");
            }

            if(lhs.Order != rhs.Order)
            {
                throw new InvalidOperationException();
            }

            SquareMatrix<T> result = new SquareMatrix<T>(lhs.Order);

            for(int i = 0; i < lhs.Order; i++)
            {
                for(int j = 0; j < rhs.Order; j++)
                {
                    result[i, j] = func(lhs[i,j], rhs[i,j]);
                }
            }

            return result;
        }
    }
}
