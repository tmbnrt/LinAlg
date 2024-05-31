using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LinAlg
{
    /// <summary>
    /// Linear Algebra, (c) Tim Beinert, 2017
    /// </summary>
    public static class CauchyR
    {
        public static Matrix<double> TensorTransform(float[] CauchyStressFloatVector, Matrix<double> rotT)
        {
            Matrix<float> CauchyStressFloat = Matrix<float>.Build.Random(3, 3);
            CauchyStressFloat[0, 0] = CauchyStressFloatVector[0];
            CauchyStressFloat[1, 1] = CauchyStressFloatVector[1];
            CauchyStressFloat[2, 2] = CauchyStressFloatVector[2];
            CauchyStressFloat[0, 1] = CauchyStressFloatVector[3];
            CauchyStressFloat[0, 2] = CauchyStressFloatVector[4];
            CauchyStressFloat[1, 2] = CauchyStressFloatVector[5];
            CauchyStressFloat[1, 0] = CauchyStressFloatVector[3];
            CauchyStressFloat[2, 0] = CauchyStressFloatVector[4];
            CauchyStressFloat[2, 1] = CauchyStressFloatVector[5];

            Matrix<double> CauchyStress = Matrix<double>.Build.Random(3, 3);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    float stressVal = CauchyStressFloat[i, j];
                    float multiplier = 10000.0f;
                    int stressValInt = (int)(stressVal * multiplier);
                    CauchyStress[i, j] = Convert.ToDouble(stressValInt) / 10000;
                }
            }

            return ChangeBase(CauchyStress, rotT);
        }

        public static Matrix<double> ChangeBase(Matrix<double> Sigma, Matrix<double> R)
        {
            Matrix<double> SigmaRot = Matrix<double>.Build.Dense(3, 3);

            int v_i = 3;

            for (int i = 0; i < v_i; i++)
            {
                for (int j = 0; j < v_i; j++)
                {
                    for (int k = 0; k < v_i; k++)
                    {
                        for (int l = 0; l < v_i; l++)
                        {
                            SigmaRot[i, j] = SigmaRot[i, j] + R[k, i] * Sigma[k, l] * R[l, j];
                        }
                    }
                }
            }

            return SigmaRot;
        }
    }
}