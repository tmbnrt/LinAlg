using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LinAlg
{
    public static class GetCauchyR
    {
        public static Matrix<double> GetR(Vector<double> v_1, Vector<double> v_2, Vector<double> v_3)
        {
            List<Vector<double>> refVectors = new List<Vector<double>>();

            List<Vector<double>> actVectors = new List<Vector<double>>();
            actVectors.Add(v_1.Normalize(2));
            actVectors.Add(v_2.Normalize(2));
            actVectors.Add(v_3.Normalize(2));

            int v_i = v_1.Count;

            Vector<double> e_1 = new DenseVector(new double[] { 1.0, 0.0, 0.0 });
            Vector<double> e_2 = new DenseVector(new double[] { 0.0, 1.0, 0.0 });
            Vector<double> e_3 = new DenseVector(new double[] { 0.0, 0.0, 1.0 });

            refVectors.Add(e_1);
            refVectors.Add(e_2);
            refVectors.Add(e_3);

            Matrix<double> R = Matrix<double>.Build.Dense(3, 3);

            for (int i = 0; i < v_i; i++)
            {
                for (int j = 0; j < v_i; j++)
                {
                    double alpha = Getangle(refVectors[i], actVectors[j]);
                    R[i, j] = Math.Cos(alpha);
                }
            }

            return R;
        }

        public static Matrix<double> GetR_VectorTrans(Vector<double> v_1, Vector<double> v_2, Vector<double> v_3)
        {
            List<Vector<double>> refVectors = new List<Vector<double>>();

            List<Vector<double>> actVectors = new List<Vector<double>>();
            actVectors.Add(v_1.Normalize(2));
            actVectors.Add(v_2.Normalize(2));
            actVectors.Add(v_3.Normalize(2));

            int v_i = v_1.Count;

            Vector<double> e_1 = new DenseVector(new double[] { 1.0, 0.0, 0.0 });
            Vector<double> e_2 = new DenseVector(new double[] { 0.0, 1.0, 0.0 });
            Vector<double> e_3 = new DenseVector(new double[] { 0.0, 0.0, 1.0 });

            refVectors.Add(e_1);
            refVectors.Add(e_2);
            refVectors.Add(e_3);

            Matrix<double> R = Matrix<double>.Build.Dense(3, 3);

            for (int i = 0; i < v_i; i++)
            {
                for (int j = 0; j < v_i; j++)
                {
                    double alpha = Getangle(refVectors[i], actVectors[j]);
                    R[j, i] = Math.Round(Math.Cos(alpha), 5);
                }
            }

            return R;
        }

        public static Matrix<double> GetRreverse(Vector<double> v_1, Vector<double> v_2, Vector<double> v_3)
        {
            List<Vector<double>> refVectors = new List<Vector<double>>();

            List<Vector<double>> actVectors = new List<Vector<double>>();
            actVectors.Add(v_1.Normalize(2));
            actVectors.Add(v_2.Normalize(2));
            actVectors.Add(v_3.Normalize(2));

            int v_i = v_1.Count;

            Vector<double> e_1 = new DenseVector(new double[] { 1.0, 0.0, 0.0 });
            Vector<double> e_2 = new DenseVector(new double[] { 0.0, 1.0, 0.0 });
            Vector<double> e_3 = new DenseVector(new double[] { 0.0, 0.0, 1.0 });

            refVectors.Add(e_1);
            refVectors.Add(e_2);
            refVectors.Add(e_3);

            Matrix<double> R = Matrix<double>.Build.Dense(3, 3);

            for (int i = 0; i < v_i; i++)
            {
                for (int j = 0; j < v_i; j++)
                {
                    double alpha = Getangle(actVectors[i], refVectors[j]);
                    R[i, j] = Math.Cos(alpha);
                }
            }

            return R;
        }

        public static double Getangle(Vector<double> v1, Vector<double> v2)
        {
            double v1_length = Math.Sqrt(v1[0] * v1[0] + v1[1] * v1[1] + v1[2] * v1[2]);
            double v2_length = Math.Sqrt(v2[0] * v2[0] + v2[1] * v2[1] + v2[2] * v2[2]);
            double scalar = v1[0] * v2[0] + v1[1] * v2[1] + v1[2] * v2[2];

            double rhs = scalar / (v1_length * v2_length);

            double angle = Math.Acos(rhs);

            return angle;
        }
    }
}