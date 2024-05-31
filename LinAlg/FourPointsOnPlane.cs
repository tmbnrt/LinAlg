using System;
using System.Collections.Generic;
using System.Text;

namespace LinAlg
{
    public static class FourPointsOnPlane
    {
        public static bool CheckPlane(List<double> node1Coords, List<double> node2Coords, List<double> node3Coords, List<double> node4Coords)
        {
            bool onPlane = false;

            double[] vector1 = new double[] { node2Coords[0] - node1Coords[0], node2Coords[1] - node1Coords[1], node2Coords[2] - node1Coords[2] };
            double[] vector2 = new double[] { node3Coords[0] - node1Coords[0], node3Coords[1] - node1Coords[1], node3Coords[2] - node1Coords[2] };
            double[] vector3 = new double[] { node4Coords[0] - node1Coords[0], node4Coords[1] - node1Coords[1], node4Coords[2] - node1Coords[2] };

            double[] crossProductV1V2 = new double[] { vector1[1] * vector2[2] - vector1[2] * vector2[1], vector1[2] * vector2[0] - vector1[0] * vector2[2], vector1[0] * vector2[1] - vector1[1] * vector2[0] };

            double vol = crossProductV1V2[0] * vector3[0] + crossProductV1V2[1] * vector3[1] + crossProductV1V2[2] * vector3[2];

            if (Math.Abs(vol) <= 1.0) { onPlane = true; }

            return onPlane;
        }

        public static bool CheckPlaneArray(double[] node1Coords, double[] node2Coords, double[] node3Coords, double[] node4Coords)
        {
            bool onPlane = false;

            double[] vector1 = new double[] { node2Coords[0] - node1Coords[0], node2Coords[1] - node1Coords[1], node2Coords[2] - node1Coords[2] };
            double[] vector2 = new double[] { node3Coords[0] - node1Coords[0], node3Coords[1] - node1Coords[1], node3Coords[2] - node1Coords[2] };
            double[] vector3 = new double[] { node4Coords[0] - node1Coords[0], node4Coords[1] - node1Coords[1], node4Coords[2] - node1Coords[2] };

            double[] crossProductV1V2 = new double[] { vector1[1] * vector2[2] - vector1[2] * vector2[1], vector1[2] * vector2[0] - vector1[0] * vector2[2], vector1[0] * vector2[1] - vector1[1] * vector2[0] };

            double vol = crossProductV1V2[0] * vector3[0] + crossProductV1V2[1] * vector3[1] + crossProductV1V2[2] * vector3[2];

            if (Math.Abs(vol) <= 1.0) { onPlane = true; }

            return onPlane;
        }
    }
}