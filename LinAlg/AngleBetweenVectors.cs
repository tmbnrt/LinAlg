using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LinAlg
{
    public static class AngleBetweenVectors
    {
        public static double Getangle(Vector<double> v1, Vector<double> v2)
        {
            double v1_length = Math.Sqrt(v1[0] * v1[0] + v1[1] * v1[1] + v1[2] * v1[2]);
            double v2_length = Math.Sqrt(v2[0] * v2[0] + v2[1] * v2[1] + v2[2] * v2[2]);
            double scalar = v1[0] * v2[0] + v1[1] * v2[1] + v1[2] * v2[2];

            double rhs = scalar / (v1_length * v2_length);

            double angle = Math.Acos(rhs);

            return angle;
        }

        public static double GetangleArray(double[] v1, double[] v2)
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