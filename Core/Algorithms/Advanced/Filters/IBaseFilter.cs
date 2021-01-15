using System;
using System.ComponentModel;

namespace morphological_image_processing_wpf.Core.Algorithms.Filters
{
    public interface IBaseFilter
    {
        string GetName();

        double[,,] GetKernel();

        double[,] GetBaseKernel();

        double GetRotation();

        GridSizes GetGridSize();

        public bool IsMutable()
        {
            return false;
        }

        public static double[,,] RotateMatrix(double[,] baseKernel, double degrees)
        {
            double[,,] kernel = new double[(int)(360 / degrees), baseKernel.GetLength(0), baseKernel.GetLength(1)];


            int xOffset = baseKernel.GetLength(1) / 2;
            int yOffset = baseKernel.GetLength(0) / 2;


            for (int y = 0; y < baseKernel.GetLength(0); y++)
            {
                for (int x = 0; x < baseKernel.GetLength(1); x++)
                {
                    for (int compass = 0; compass < kernel.GetLength(0); compass++)
                    {
                        double radians = compass * degrees * Math.PI / 180.0;
                        int resultX = (int)(Math.Round((x - xOffset) * Math.Cos(radians) - (y - yOffset) * Math.Sin(radians)) + xOffset);
                        int resultY = (int)(Math.Round((x - xOffset) * Math.Sin(radians) + (y - yOffset) * Math.Cos(radians)) + yOffset);
                        kernel[compass, resultY, resultX] = baseKernel[y, x];
                    }
                }
            }

            return kernel;
        }
    }

    public enum GridSizes
    {
        [Description("Small (3x3)")]
        Small = 0,
        [Description("Big (5x5)")]
        Big = 1
    }

    public static class GridSizeEnumExtension
    {
        public static string ToDescriptionString(this GridSizes val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
