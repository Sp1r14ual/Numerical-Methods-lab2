using System;

namespace Com_Methods
{
    class CONST
    {
        //точность решения
        public static double EPS = 1e-20;
    }

    class Tools
    {
        //относительная погрешность
        public static double Relative_Error(Vector X, Vector x)
        {
            double s = 0.0;
            for (int i = 0; i < X.N; i++)
            {
                s += Math.Pow(X.Elem[i] - x.Elem[i], 2);
            }
            return Math.Sqrt(s) / x.Norm();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {
                //размер системы
                int N = 200;

                //матрица СЛАУ
                var A = new Matrix(N, N);

                //истинное решение
                var X_True = new Vector(N);
                
                //заполнение матрицы и истинного решения    
                for (int i = 0; i < A.N; i++)
                {
                    for (int j = 0; j < A.N; j++)
                    {
                        A.Elem[i][j] = 1.0 / (i + j + 1.0);
                    }
                    X_True.Elem[i] = 1.0;
                }
                
                //правая часть
                var F = A * X_True;

                //вычисление числа обусловленности
                Console.WriteLine("\nCond(A) = " + A.Cond_InfinityNorm() + "\n");

                //метод Гаусса
                //var Solver = new Gauss_Method();
                //var Solver = new LU_Decomposition(A);
                var Solver = new QR_Decomposition(A, QR_Decomposition.QR_Algorithm.Householder);

                //var X = Solver.Start_Solver(A, F);
                var X = Solver.Start_Solver(F);

                //вывод результата
                Console.WriteLine("Result:");
                X.Console_Write_Vector();
                Console.WriteLine("\nError: {0}\n", Tools.Relative_Error(X, X_True));
            }
            catch (Exception E)
            {
                Console.WriteLine("\n*** Error! ***");
                Console.WriteLine("Method:  {0}",   E.TargetSite);
                Console.WriteLine("Message: {0}\n", E.Message);
            }
        }
    }
}