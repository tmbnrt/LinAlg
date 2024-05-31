using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LinAlg
{
    public static class StandardGeometry
    {
        public static double CalcRadius3Points(double[] pointA, double[] pointB, double[] pointC)
        {
            double radius = 999;

            Vector<double> pointA_3D = Vector<double>.Build.Dense(3);
            Vector<double> pointB_3D = Vector<double>.Build.Dense(3);
            Vector<double> pointC_3D = Vector<double>.Build.Dense(3);
            pointA_3D[0] = pointA[0]; pointA_3D[1] = pointA[1]; pointA_3D[2] = pointA[2];
            pointB_3D[0] = pointB[0]; pointB_3D[1] = pointB[1]; pointB_3D[2] = pointB[2];
            pointC_3D[0] = pointC[0]; pointC_3D[1] = pointC[1]; pointC_3D[2] = pointC[2];

            double[] vec_AB = new double[3] { pointB[0] - pointA[0], pointB[1] - pointA[1], pointB[2] - pointA[2] };
            double[] vec_AC = new double[3] { pointC[0] - pointA[0], pointC[1] - pointA[1], pointC[2] - pointA[2] };
            Vector<double> vec_AB_3D = Vector<double>.Build.Dense(3);
            Vector<double> vec_AC_3D = Vector<double>.Build.Dense(3);
            vec_AB_3D[0] = vec_AB[0]; vec_AB_3D[1] = vec_AB[1]; vec_AB_3D[2] = vec_AB[2];
            vec_AC_3D[0] = vec_AC[0]; vec_AC_3D[1] = vec_AC[1]; vec_AC_3D[2] = vec_AC[2];

            double[] crossV = new double[] { vec_AC[1] * vec_AB[2] - vec_AC[2] * vec_AB[1], vec_AC[2] * vec_AB[0] - vec_AC[0] * vec_AB[2], vec_AC[0] * vec_AB[1] - vec_AC[1] * vec_AB[0] };

            Vector<double> x_new = Vector<double>.Build.Dense(3);
            Vector<double> y_new = Vector<double>.Build.Dense(3);
            Vector<double> z_new = Vector<double>.Build.Dense(3);

            x_new[0] = vec_AB[0];
            x_new[1] = vec_AB[1];
            x_new[2] = vec_AB[2];

            z_new[0] = crossV[0];
            z_new[1] = crossV[1];
            z_new[2] = crossV[2];

            y_new[0] = (z_new[1] * x_new[2] - z_new[2] * x_new[1]) * (-1);
            y_new[1] = (z_new[2] * x_new[0] - z_new[0] * x_new[2]) * (-1);
            y_new[2] = (z_new[0] * x_new[1] - z_new[1] * x_new[0]) * (-1);

            Matrix<double> R = GetCauchyR.GetRreverse(x_new, y_new, z_new);

            Vector<double> pointA_2D = VecTran.ChangeBase(pointA_3D, R);
            Vector<double> pointB_2D = VecTran.ChangeBase(pointB_3D, R);
            Vector<double> pointC_2D = VecTran.ChangeBase(pointC_3D, R);

            Vector<double> midPoint_AB = pointA_2D + (pointB_2D - pointA_2D) / 2;
            Vector<double> midPoint_AC = pointA_2D + (pointC_2D - pointA_2D) / 2;
            Vector<double> direction_AB = Vector<double>.Build.Dense(3);
            direction_AB[0] = (pointB_2D - pointA_2D)[1] * 1 - (pointB_2D - pointA_2D)[2] * 0;
            direction_AB[1] = (pointB_2D - pointA_2D)[2] * 0 - (pointB_2D - pointA_2D)[0] * 1;
            direction_AB[2] = (pointB_2D - pointA_2D)[0] * 0 - (pointB_2D - pointA_2D)[1] * 0;
            Vector<double> direction_AC = Vector<double>.Build.Dense(3);
            direction_AC[0] = (pointC_2D - pointA_2D)[1] * 1 - (pointC_2D - pointA_2D)[2] * 0;
            direction_AC[1] = (pointC_2D - pointA_2D)[2] * 0 - (pointC_2D - pointA_2D)[0] * 1;
            direction_AC[2] = (pointC_2D - pointA_2D)[0] * 0 - (pointC_2D - pointA_2D)[1] * 0;

            Vector<double> center = Intersection2Lines3D(midPoint_AB, direction_AB, midPoint_AC, direction_AC);

            Vector<double> radVec = Vector<double>.Build.Dense(3);
            radVec[0] = pointA_2D[0] - center[0];
            radVec[1] = pointA_2D[1] - center[1];
            radVec[2] = 0.0;
            radius = Math.Sqrt(radVec[0] * radVec[0] + radVec[1] * radVec[1] + radVec[2] * radVec[2]);

            if (double.IsNaN(radius) || radius > 1000)
                radius = 1001;

            return radius;
        }

        public static Vector<double> Intersection2Lines3D(Vector<double> pointA, Vector<double> directionA, Vector<double> pointB, Vector<double> directionB)
        {
            Vector<double> intersectionPoint = Vector<double>.Build.Dense(3);

            double lambda;
            double mu;

            lambda = (pointB[0] / directionA[0] - pointA[0] / directionA[0] + directionB[0] / directionB[1] / directionA[0] * pointA[1] - directionB[0] / directionB[1] / directionA[0] * pointB[1])
                   / (1 - directionB[0] / directionB[1] / directionA[0] * directionA[1]);

            mu = 1 / directionB[1] * (pointA[1] + lambda * directionA[1] - pointB[1]);

            intersectionPoint[0] = pointA[0] + lambda * directionA[0];
            intersectionPoint[1] = pointA[1] + lambda * directionA[1];
            intersectionPoint[2] = 0.0;

            if (double.IsNaN(lambda))
            {
                lambda = (pointA[0] + pointB[1] / directionA[1] * directionA[0] - pointA[1] / directionA[1] * directionA[0] - pointB[0])
                        / (directionB[0] - directionB[1] / directionA[1] * directionA[0]);

                intersectionPoint[0] = pointB[0] + lambda * directionB[0];
                intersectionPoint[1] = pointB[1] + lambda * directionB[1];
                intersectionPoint[2] = 0.0;
            }

            return intersectionPoint;
        }
    }
}