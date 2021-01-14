using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace morphological_image_processing_wpf.Core.Algorithms.Advanced.Convolutions
{
    class CompassConvolution : IConvolution
    {
        public Bitmap Apply(Bitmap originalBitmap, double[,,] filterMatrix, bool convertToGrayScale = false)
        {
            BitmapData sourceData = originalBitmap.LockBits(new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height),
                                                            ImageLockMode.ReadOnly,
                                                            PixelFormat.Format32bppArgb);

            byte[] sourceBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, sourceBuffer.Length);

            originalBitmap.UnlockBits(sourceData);

            if (convertToGrayScale)
            {
                ConvertToGrayScale(sourceBuffer);
            }

            double r, g, b;
            double rC, gC, bC;

            int filterHeight = filterMatrix.GetLength(0);
            int filterWidth = filterMatrix.GetLength(1);

            int filterOffset = (filterWidth - 1) / 2;
            for (int offsetY = filterOffset; offsetY < originalBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX < originalBitmap.Width - filterOffset; offsetX++)
                {
                    r = g = b = 0;

                    int byteOffset = offsetY * sourceData.Stride + offsetX * 4;

                    for (int compass = 0; compass < filterHeight; compass++)
                    {
                        rC = gC = bC = 0;

                        for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                        {
                            for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                            {
                                int currOffset = byteOffset + (filterX * 4) + (filterY * sourceData.Stride);

                                bC += (double)(sourceBuffer[currOffset]) * filterMatrix[compass, filterY + filterOffset, filterX + filterOffset];

                                gC += (double)(sourceBuffer[currOffset + 1]) * filterMatrix[compass, filterY + filterOffset, filterX + filterOffset];

                                rC += (double)(sourceBuffer[currOffset + 2]) * filterMatrix[compass, filterY + filterOffset, filterX + filterOffset];

                            }
                        }

                        b = bC > b ? bC : b;
                        g = gC > g ? gC : g;
                        r = rC > r ? rC : r;
                    }

                    r = GetBoundedInt(r, 0, 255);
                    g = GetBoundedInt(g, 0, 255);
                    b = GetBoundedInt(b, 0, 255);

                    resultBuffer[byteOffset] = (byte)(b);
                    resultBuffer[byteOffset + 1] = (byte)(g);
                    resultBuffer[byteOffset + 2] = (byte)(r);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height),
                                                          ImageLockMode.WriteOnly,
                                                          PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        private void ConvertToGrayScale(byte[] sourceBuffer)
        {
            for (int i = 0; i < sourceBuffer.Length; i += 4)
            {
                float rgb = sourceBuffer[i] * 0.11f;
                rgb += sourceBuffer[i + 1] * 0.59f;
                rgb += sourceBuffer[i + 2] * 0.3f;

                sourceBuffer[i] = (byte)rgb;
                sourceBuffer[i + 1] = (byte)rgb;
                sourceBuffer[i + 2] = (byte)rgb;
                sourceBuffer[i + 3] = 255;
            }
        }

        private double GetBoundedInt(double value, double minimum, double maximum)
        {
            return Math.Min(Math.Max(minimum, value), maximum);
        }
    }
}
