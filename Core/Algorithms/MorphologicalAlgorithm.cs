using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Color = System.Drawing.Color;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    public interface IAlgorithm
    {
        String GetName();

        Bitmap Apply(Bitmap image, IMorphologicalAlgorithmConfiguration configuration);

        Type GetConfigurationClass();

        public String Name
        {
            get
            {
                return GetName();
            }
        }
    }

    abstract class MorphologicalAlgorithm<T> : IAlgorithm where T : IMorphologicalAlgorithmConfiguration
    {
        protected abstract Bitmap Apply(Bitmap image, T configuration);

        public abstract String GetName();

        public Bitmap Apply(Bitmap image, IMorphologicalAlgorithmConfiguration configuration)
        {
            return Apply(image, (T)configuration);
        }

        public Type GetConfigurationClass() {
            return typeof(T);
        }

        protected List<Tuple<int, int>> CalculatePointsToCheck(ISet<Tuple<int, int>> points, Tuple<int, int> centralPoint)
        {
            List<Tuple<int, int>> newPoints = new List<Tuple<int, int>>();

            foreach (Tuple<int, int> point in points)
            {
                newPoints.Add(Tuple.Create(point.Item1 - centralPoint.Item1, point.Item2 - centralPoint.Item2));
            }

            return newPoints;
        }

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

                            bX += (double) (SourceBuffer[currOffset]) * xMatrix[filterY + filterOffset, filterX + filterOffset];

                            gX += (double) (SourceBuffer[currOffset + 1]) * xMatrix[filterY + filterOffset, filterX + filterOffset];

                            rX += (double) (SourceBuffer[currOffset + 2]) * xMatrix[filterY + filterOffset, filterX + filterOffset];
                            
                            bY += (double) (SourceBuffer[currOffset]) * yMatrix[filterY + filterOffset, filterX + filterOffset];

                            gY += (double) (SourceBuffer[currOffset + 1]) * yMatrix[filterY + filterOffset, filterX + filterOffset];

                            rY += (double) (SourceBuffer[currOffset + 2]) * yMatrix[filterY + filterOffset, filterX + filterOffset];
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

    public interface IMorphologicalAlgorithmConfiguration { }

    class DefaultMorphologicalAlgorithmConfiguration : IMorphologicalAlgorithmConfiguration
    {
        public double BrightnessThreshold { get; set; } = 0.02;

        public Color LineColor { get; set; } = Color.Red;

        private readonly ISet<Tuple<int, int>> _structuralElementShape = new HashSet<Tuple<int, int>>()
        {
            Tuple.Create(0, 0), Tuple.Create(0, 1), Tuple.Create(0, 2),
            Tuple.Create(1, 0), Tuple.Create(1, 1), Tuple.Create(1, 2),
            Tuple.Create(2, 0), Tuple.Create(2, 1), Tuple.Create(2, 2)
        };

        public ISet<Tuple<int, int>> StructuralElementPoints
        {
            get
            {
                return _structuralElementShape;
            }
            set
            {
                _structuralElementShape.Clear();
                _structuralElementShape.UnionWith(value);
            }
        }

        public Tuple<int, int> Center { get; set; } = Tuple.Create(1, 1);
    }

    class EmptyMorphologicalAlgorithmConfiguration: IMorphologicalAlgorithmConfiguration
    {

    }
}
