using System;
using System.Collections.Generic;
using System.Text;

namespace LinAlg
{
    public static class DistancePointToLine3D
    {
        public static double DistancePointLine3D(double[] linePoint1, double[] linePoint2, double[] point)
        {
            double distance = 0.0;

            double[] vectorPA = new double[3] { point[0] - linePoint1[0], point[1] - linePoint1[1], point[2] - linePoint1[2] };
            double[] vectorU = new double[3] { linePoint1[0] - linePoint2[0], linePoint1[1] - linePoint2[1], linePoint1[2] - linePoint2[2] };

            double[] crossProduct_pa_u = new double[] { vectorPA[1] * vectorU[2] - vectorPA[2] * vectorU[1], vectorPA[2] * vectorU[0] - vectorPA[0] * vectorU[2], vectorPA[0] * vectorU[1] - vectorPA[1] * vectorU[0] };

            double upperLength = Math.Sqrt(crossProduct_pa_u[0] * crossProduct_pa_u[0] + crossProduct_pa_u[1] * crossProduct_pa_u[1] + crossProduct_pa_u[2] * crossProduct_pa_u[2]);

            double lowerLength = Math.Sqrt(vectorU[0] * vectorU[0] + vectorU[1] * vectorU[1] + vectorU[2] * vectorU[2]);

            distance = upperLength / lowerLength;

            return distance;
        }
    }
}