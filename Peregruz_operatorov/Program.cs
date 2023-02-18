namespace PeregruzkaOperatorov
{

    class Matrix
    {
        public enum FillType
        {
            Zero,
            Random
        }

        private double[,] matrix;
        private static Random rnd = new Random(); // static - то есть создается не два одинаковых рандома, а один и из него берутся значения

        #region Constructors

        /// cоздание матрицы(массива) столбцов-строк (забита нулями)
        public Matrix(int rows, int cols, FillType fillType)
        {
            matrix = new double[rows, cols];
            switch (fillType)
            {
                case FillType.Zero:
                    ClearMatrix();
                    break;
                case FillType.Random:
                    FillRandomValues(0, 10);
            break;

            }

        }

        /// копирование массива Matrix, для того чтобы нельзя было вносить изменения из вне
        public Matrix(double[,] matrix)
        { /*this.matrix = matrix; тут мы просто указываем ссылку на массив, и его можно
будет переписать, нужно создовать копию */
            this.matrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    this.matrix[i, j] = matrix[i, j];
                }
            }
        }

        /// конструктор копирования
        public Matrix(Matrix m)
        {
            this.matrix = new double[m.Rows, m.Cols];
            for (int i = 0; i < m.Rows; i++)
            {
                for (int j = 0; j < m.Cols; j++)
                {
                    this.matrix[i, j] = m.matrix[i, j];
                }
            }
        }

        #endregion

        #region Props

        public int Rows { get { return matrix.GetLength(0); } }
        public int Cols { get { return matrix.GetLength(1); } }

        public double this[int i, int j]
        {
            set { this.SetValue(i, j, value); }
            get { return this.GetValue(i, j); }
        }

        #endregion

        #region SupportMethods

        /// заполняем рандом случайными числами т.к. в один момент времени генерируется одна последовательность рандома. Проходимся по массиву и рандомим
        public void FillRandomValues(int min, int max)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    this.matrix[i, j] = rnd.Next(min, max);
                }
            }
        }

        /// очистка матрицы (нулевая матрица)
        public void ClearMatrix()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    matrix[i, j] = 0;
                }
            }
        }

        public double GetValue(int i, int j)
        {
            if (i < 0 || i > Rows - 1 || j < 0 || j > Cols - 1)
            {
                throw new Exception("Out of range Exception");
            }
            return matrix[i, j];
        }

        public void SetValue(int i, int j, double value)
        {
            if (i < 0 || i > Rows - 1 || j < 0 || j > Cols - 1)
            {
                throw new Exception("Out of range Exception");
            }
            matrix[i, j] = value;
        }

        #endregion


        #region NormalMethods

        /// Сложение одинаковых матриц первой и второй
        public Matrix Add(Matrix m)
        {
            if (this.Rows == m.Rows && this.Cols == m.Cols)
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        this.matrix[i, j] += m.matrix[i, j];
                    }
                }
                return this;
            }

            else
            {
                throw new Exception("Sizes are not equal");
            }
        }

        public int CompareTo(Matrix m)
        {
            if (this.Rows == m.Rows && this.Cols == m.Cols)
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        if (this.matrix[i, j] < m.matrix[i, j])
                        {
                            return -1;
                        }
                        else if (this.matrix[i, j] > m.matrix[i, j])
                        {
                            return 1;
                        }
                    }
                }
                return 0;
            }
            else
            {
                throw new Exception("Sizes are not equal");
            }
        }
        #endregion

        #region OverrideMethods

        /// Преобразования чего либо в СТРОКУ (override)-переопределили метод
        public override string ToString()
        {
            string output = "";

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    output += matrix[i, j].ToString().PadLeft(4); //СТРОКИ. Выравнивание (PadLeft) по правому краю 4 пробела слева (PadRight) наоборот
                }

                output += "\n";
            }
            return output;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            Matrix mRes = new Matrix(m1);
            mRes.Add(m2);
            return mRes;
        }

        public static bool operator ==(Matrix m1, Matrix m2)
        {
            return m1.CompareTo(m2) == 0;
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            return m1.CompareTo(m2) != 0;
        }

        public static bool operator >(Matrix m1, Matrix m2)
        {
            return m1.CompareTo(m2) == 1;
        }
        public static bool operator <(Matrix m1, Matrix m2)
        {
            return m1.CompareTo(m2) == -1;
        }

        public static bool operator >=(Matrix m1, Matrix m2)
        {
            return m1.CompareTo(m2) == 1 || m1.CompareTo(m2) == 0;
        }
        public static bool operator <=(Matrix m1, Matrix m2)
        {
            return m1.CompareTo(m2) == -1 || m1.CompareTo(m2) == 0;
        }

        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            /* Matrix m1 = new Matrix(2,3);
            Matrix m2 = new Matrix(2,3);
            Matrix m3 = new Matrix(2,3);

            Console.WriteLine(m1);
            Console.WriteLine(m2);
            Console.WriteLine(m3);

            Matrix m4 = m1 + m2 + m3;
            m1.Add(m2).Add(m3); */

            /* m1.Add(m2);
            m1.Add(m3); */

            /* Console.WriteLine(m1);
            Console.WriteLine(m4); */

            Matrix m1 = new Matrix(1, 5, Matrix.FillType.Random);
            Matrix m2 = new Matrix(1, 5, Matrix.FillType.Random);

            Console.WriteLine(m1);
            Console.WriteLine(m2);

            // m1.SetValue(0,0,15);
            m1[0, 0] = 15;
            Console.WriteLine(m1);

            //Console.WriteLine(m2.GetValue(1,1));
            Console.WriteLine(m2[1, 1]);


            /* try
            {
            if (m1 == m2)
            {
            Console.WriteLine("ok");
            }
            else
            {
            Console.WriteLine("no");
            }
            }
            catch (Exception e)
            {
            Console.WriteLine(e.Message);
            }*/

            /*try
            {
            m1.Add(m2);
            Console.WriteLine(m1);
            }
            catch (Exception e)
            {
            Console.WriteLine(e.Message);
            }*/

            Console.ReadKey();

        }
    }

}