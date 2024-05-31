using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LinAlg
{
    public static class DistancePointLineByVector2D
    {
        public static double DistancePointLineVector2D(Vector<double> midpoint, Vector<double> pointA, Vector<double> pointB)
        {
            double distance = 0.0;

            pointA[2] = 0.0;
            pointB[2] = 0.0;
            midpoint[2] = 0.0;

            Vector<double> vec_AB = pointB - pointA;
            Vector<double> vec_PA = midpoint - pointA;

            double[] crossV = new double[] { vec_PA[1] * vec_AB[2] - vec_PA[2] * vec_AB[1], vec_PA[2] * vec_AB[0] - vec_PA[0] * vec_AB[2], vec_PA[0] * vec_AB[1] - vec_PA[1] * vec_AB[0] };

            double len_crossV = Math.Sqrt(crossV[0] * crossV[0] + crossV[1] * crossV[1] + crossV[2] * crossV[2]);
            double len_AB = Math.Sqrt(vec_AB[0] * vec_AB[0] + vec_AB[1] * vec_AB[1] + vec_AB[2] * vec_AB[2]);

            distance = len_crossV / len_AB;

            return distance;
        }
    }
}