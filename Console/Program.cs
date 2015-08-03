using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;


namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
           // SquareMatrix<int> matrix = new SymmetricalMatrix<int>( new int[,] {
            //{1,0},{0,2}});
            SquareMatrix<int> matrix = new DiagonalMatrix<int>(new int[] { 1, 2 });

            System.Console.Write(matrix);
            CheckEvet check = new CheckEvet();
            check.Register(matrix);
            matrix[0, 0] = 2;
            matrix[1, 1] = 3;
            System.Console.Write(matrix);

            /*SquareMatrix<int> dmatrix = new DiagonalMatrix<int>(2, new int[] { 1, 2 });
            SquareMatrix<int> smatrix = new SymmetricalMatrix<int>(new int[,] {
            {1,2},
            {2,1}});
            SquareMatrix<int> summatrix = dmatrix.Add(smatrix, (x, y) => x + y);
            System.Console.WriteLine(smatrix.ToString());
            System.Console.WriteLine(matrix.ToString());
            System.Console.WriteLine(dmatrix.ToString());
            System.Console.WriteLine(summatrix.ToString());
            System.Console.WriteLine(matrix.Equals(dmatrix));
            System.Console.WriteLine(dmatrix.Equals(matrix));*/

            System.Console.ReadKey();
        }

        private class CheckEvet
        {
            public void Register<T>(SquareMatrix<T> matrix) where T:struct, IEquatable<T>
            {
                if(matrix == null)
                {
                    throw new ArgumentNullException("mattrix");
                }
                matrix.ElementChange += this.CheckElementChange;
            }

            private void CheckElementChange(Object sender, ElementChangeEventArgs e )
            {
                System.Console.WriteLine("Element [{0},{1}] is Change.", e.LeftIndex, e.RightIndex);
            }

            public void UnRegister<T>(SquareMatrix<T> matrix) where T : struct, IEquatable<T>
            {
                matrix.ElementChange -= this.CheckElementChange;
            }
        }

    }
}
