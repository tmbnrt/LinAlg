﻿using System;
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

        public static bool IsPointInTriangle(double[] point, double[] tri_a, double[] tri_b, double[] tri_c)
        {
            Vector<double> p = Vector<double>.Build.Dense(3);
            Vector<double> a = Vector<double>.Build.Dense(3);
            Vector<double> b = Vector<double>.Build.Dense(3);
            Vector<double> c = Vector<double>.Build.Dense(3);
            p[0] = point[0];
            p[1] = point[1];
            p[2] = point[2];            
            a[0] = tri_a[0];
            a[1] = tri_a[1];
            a[2] = tri_a[2];            
            b[0] = tri_b[0];
            b[1] = tri_b[1];
            b[2] = tri_b[2];            
            c[0] = tri_c[0];
            c[1] = tri_c[1];
            c[2] = tri_c[2];

            Vector<double> v0 = c - a;
            Vector<double> v1 = b - a;
            Vector<double> v2 = p - a;

            double dot00 = v0.DotProduct(v0);
            double dot01 = v0.DotProduct(v1);
            double dot02 = v0.DotProduct(v2);
            double dot11 = v1.DotProduct(v1);
            double dot12 = v1.DotProduct(v2);

            // barycentric coords
            double inv = 1.0 / (dot00 * dot11 - dot01 * dot01);
            double u = (dot11 * dot02 - dot01 * dot12) * inv;
            double v = (dot00 * dot12 - dot01 * dot02) * inv;

            // Check if point is in triangle
            return (u >= 0.0) && (v >= 0.0) && (u + v < 1.0);
        }

        public static bool LineIntersectsTriangle(double[] line_a, double[] line_b, double[] tri_a, double[] tri_b, double[] tri_c, out double[] intersection)
        {
            intersection = new double[3];

            Vector<float> p0 = Vector<float>.Build.Dense(3);
            Vector<float> p1 = Vector<float>.Build.Dense(3);
            Vector<float> a = Vector<float>.Build.Dense(3);
            Vector<float> b = Vector<float>.Build.Dense(3);
            Vector<float> c = Vector<float>.Build.Dense(3);
            p0[0] = (float)line_a[0];
            p0[1] = (float)line_a[1];
            p0[2] = (float)line_a[2];
            p1[0] = (float)line_b[0];
            p1[1] = (float)line_b[1];
            p1[2] = (float)line_b[2];
            a[0] = (float)tri_a[0];
            a[1] = (float)tri_a[1];
            a[2] = (float)tri_a[2];
            b[0] = (float)tri_b[0];
            b[1] = (float)tri_b[1];
            b[2] = (float)tri_b[2];
            c[0] = (float)tri_c[0];
            c[1] = (float)tri_c[1];
            c[2] = (float)tri_c[2];

            Vector<float> e1 = b - a;
            Vector<float> e2 = c - a;
            Vector<float> crossP = Vector<float>.Build.Dense(3);
            crossP[0] = (p1 - p0)[1] * e2[2] - (p1 - p0)[2] * e2[1];
            crossP[1] = (p1 - p0)[2] * e2[0] - (p1 - p0)[0] * e2[2];
            crossP[2] = (p1 - p0)[0] * e2[1] - (p1 - p0)[1] * e2[0];

            float det = e1.DotProduct(crossP);

            // If det is almost zero --> line is in the plane of triangle
            if (det > -float.Epsilon && det < float.Epsilon)
                return false;

            float inv = 1.0f / det;

            // Calculate distance from a to line origin
            Vector<float> s = p0 - a;
            float u = s.DotProduct(crossP) * inv;

            // inside triangle
            if (u < 0.0f || u > 1.0f)
                return false;

            // Prepare to test V parameter
            Vector<float> cross_q = Vector<float>.Build.Dense(3);
            cross_q[0] = s[1] * e1[2] - s[2] * e1[1];
            cross_q[1] = s[2] * e1[0] - s[0] * e1[2];
            cross_q[2] = s[0] * e1[1] - s[1] * e1[0];

            float v = (p1 - p0).DotProduct(cross_q) * inv;

            // intersection outside of the triangle
            if (v < 0.0f || u + v > 1.0f)
                return false;

            // distance p0 to intersection
            float t = e2.DotProduct(cross_q) * inv;

            // intersection along line segment
            if (t > float.Epsilon)
            {
                intersection[0] = (double)p0[0] + (double)t * (double)(p1[0] - p0[0]);
                intersection[1] = (double)p0[1] + (double)t * (double)(p1[1] - p0[1]);
                intersection[2] = (double)p0[2] + (double)t * (double)(p1[2] - p0[2]);
                return true;
            }

            return false;
        }
    }
}