using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LinAlg
{
    public static class VecTran
    {
        public static Vector<double> ChangeBase(Vector<double> vec_in, Matrix<double> R)
        {
            Vector<double> vec_return = Vector<double>.Build.Dense(3);

            int v_i = 3;
            for (int i = 0; i < v_i; i++)
            {
                for (int j = 0; j < v_i; j++)
                {
                    vec_return[i] = vec_return[i] + R[i, j] * vec_in[j];
                }
            }

            return vec_return;
        }
    }
}