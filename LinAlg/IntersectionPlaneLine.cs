using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinAlg
{
    public static class IntersectionPlaneLine
    {
        public static double[] IntersectCoord(double[] line_P1, double[] line_P2, double[] plane_P1, double[] plane_P2, double[] plane_P3)
        {
            double[] intersect = new double[3];

            double[] line_dir = new double[3] { line_P2[0] - line_P1[0], line_P2[1] - line_P1[1], line_P2[2] - line_P1[2] };

            double[] plane_dir1 = new double[3] { plane_P2[0] - plane_P1[0], plane_P2[1] - plane_P1[1], plane_P2[2] - plane_P1[2] };
            double[] plane_dir2 = new double[3] { plane_P3[0] - plane_P1[0], plane_P3[1] - plane_P1[1], plane_P3[2] - plane_P1[2] };

            double[] plane_norm = new double[] { plane_dir1[1] * plane_dir2[2] - plane_dir1[2] * plane_dir2[1], plane_dir1[2] * plane_dir2[0] - plane_dir1[0] * plane_dir2[2], plane_dir1[0] * plane_dir2[1] - plane_dir1[1] * plane_dir2[0] };

            double c = plane_norm[0] * plane_P1[0] + plane_norm[1] * plane_P1[1] + plane_norm[2] * plane_P1[2];

            double r = (c - plane_norm[0] * line_P1[0] - plane_norm[1] * line_P1[1] - plane_norm[2] * line_P1[2])
                        / (plane_norm[0] * line_dir[0] + plane_norm[1] * line_dir[1] + plane_norm[2] * line_dir[2]);

            if (Double.IsNaN(r))
                return intersect;

            intersect[0] = line_P1[0] + r * line_dir[0];
            intersect[1] = line_P1[1] + r * line_dir[1];
            intersect[2] = line_P1[2] + r * line_dir[2];

            return intersect;
        }
    }
}