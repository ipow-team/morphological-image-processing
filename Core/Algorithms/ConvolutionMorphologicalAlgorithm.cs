using MorphologicalImageProcessing.Core.Algorithms;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace morphological_image_processing_wpf.Core.Algorithms
{
    abstract class ConvolutionMorphologicalAlgorithm<T> : MorphologicalAlgorithm<T> where T : IMorphologicalAlgorithmConfiguration
    {
        protected Bitmap Convolution(Bitmap originalBitmap,
                                      double[,] xMatrix,
                                      double[,] yMatrix,
                                      double factor = 1,
                                      int bias = 0,
                                      bool convertToGrayScale = false)
        {
            BitmapData sourceData = originalBitmap.LockBits(new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height),
                                                            ImageLockMode.ReadOnly,
                                                            PixelFormat.Format32bppArgb);

            byte[] SourceBuffer = new byte[sourceData.Stride * sourceData.Height];

            byte[] ResultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, SourceBuffer, 0, SourceBuffer.Length);

            originalBitmap.UnlockBits(sourceData);

            if (convertToGrayScale == true)
            {
                for (int i = 0; i < SourceBuffer.Length; i += 4)
                {
                    float rgb = SourceBuffer[i] * 0.11f;
                    rgb += SourceBuffer[i + 1] * 0.59f;
                    rgb += SourceBuffer[i + 2] * 0.3f;

                    SourceBuffer[i] = (byte)rgb;
                    SourceBuffer[i + 1] = (byte)rgb;
                    SourceBuffer[i + 2] = (byte)rgb;
                    SourceBuffer[i + 3] = 255;
                }
            }

            int filterOffset = 1;
            for (int offsetY = filterOffset; offsetY < originalBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX < originalBitmap.Width - filterOffset; offsetX++)
                {
                    double rX, gX, bX;
                    rX = gX = bX = 0;

                    double rY, gY, bY;
                    rY = gY = bY = 0;

                    double rT, gT, bT;

                    int byteOffset = offsetY * sourceData.Stride + offsetX * 4;
                    for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            int currOffset = byteOffset + (filterX * 4) + (filterY * sourceData.Stride);

                            bX += (double)(SourceBuffer[currOffset]) * xMatrix[filterY + filterOffset, filterX + filterOffset];

                            gX += (double)(SourceBuffer[currOffset + 1]) * xMatrix[filterY + filterOffset, filterX + filterOffset];

                            rX += (double)(SourceBuffer[currOffset + 2]) * xMatrix[filterY + filterOffset, filterX + filterOffset];

                            bY += (double)(SourceBuffer[currOffset]) * yMatrix[filterY + filterOffset, filterX + filterOffset];

                            gY += (double)(SourceBuffer[currOffset + 1]) * yMatrix[filterY + filterOffset, filterX + filterOffset];

                            rY += (double)(SourceBuffer[currOffset + 2]) * yMatrix[filterY + filterOffset, filterX + filterOffset];
                        }
                    }

                    bT = Math.Sqrt((bX * bX) + (bY * bY));

                    gT = Math.Sqrt((gX * gX) + (gY * gY));

                    rT = Math.Sqrt((rX * rX) + (rY * rY));

                    if (bT > 255)
                    {
                        bT = 255;
                    }
                    else if (bT < 0)
                    {
                        bT = 0;
                    }

                    if (gT > 255)
                    {
                        gT = 255;
                    }
                    else if (gT < 0)
                    {
                        gT = 0;
                    }

                    if (rT > 255)
                    {
                        rT = 255;
                    }
                    else if (rT < 0)
                    {
                        rT = 0;
                    }

                    ResultBuffer[byteOffset] = (byte)(bT);
                    ResultBuffer[byteOffset + 1] = (byte)(gT);
                    ResultBuffer[byteOffset + 2] = (byte)(rT);
                    ResultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height),
                                                          ImageLockMode.WriteOnly,
                                                          PixelFormat.Format32bppArgb);

            Marshal.Copy(ResultBuffer, 0, resultData.Scan0, ResultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }
    }
}
