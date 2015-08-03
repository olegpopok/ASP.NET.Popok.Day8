using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;

namespace Tests
{
    [TestClass]
    public class SquareMatrixTests
    {
        [TestMethod]
        public void Equals_MatrixEqulasMatrix_TrueReturned()
        {
            var a = new SquareMatrix<int>(new int[,]{
                {1,2},
                {1,2}});
            var b = new SquareMatrix<int>(new int[,]{
                {1,2},
                {1,2}});


            bool result = a.Equals(b);


            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Equals_MatrixEqulasMatrix_FalseReturned()
        {
            var a = new SquareMatrix<int>(new int[,]{
                {1,2},
                {1,2}});
            var b = new SquareMatrix<int>(new int[,]{
                {1,3},
                {1,2}});


            bool result = a.Equals(b);


            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Equals_DiagonalMatrixEqualSquareMatrix_TrueReturned()
        {
            var a = new DiagonalMatrix<double>(new double[] {1,2});
            var b = new SquareMatrix<double>(new double[,]{
                {1,0},
                {0,2}});

            bool resul = a.Equals(b);

            Assert.AreEqual(true, resul);
        }

        [TestMethod]
        public void Equals_SymmetricalMatrixEqualSquareMatrix_TrueReturned()
        {
            var a = new SymmetricalMatrix<int>(new int[,]{
                {1,1},
                {1,2}});
            var b = new SquareMatrix<int>(new int[,]{
                {1,1},
                {1,2}});

            bool result = a.Equals(b);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Add_SquareMatrixAddSquareMatrix_MatrixReturned()
        {
            SquareMatrix<double> a = new SquareMatrix<double>(new double[,] {
                {1,2},
                {1,2}});
            SquareMatrix<double> b = new SquareMatrix<double>(new double[,] {
                {1,2},
                {1,2}});

            SquareMatrix<double> result = a.Add(b, (x,y) => x+y);
           SquareMatrix<double> trueResult = new SquareMatrix<double>( new double[,]{{2,4},{2,4}});

            Assert.AreEqual(true, result.Equals(trueResult));
        }
    }
}
