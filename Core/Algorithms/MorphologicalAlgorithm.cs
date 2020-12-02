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

        Bitmap Convolution(Bitmap originalBitmap,
                                         double[,] xFilterMatrix,
                                         double[,] yFilterMatrix,
                                         double factor = 1,
                                         int bias = 0,
                                         bool grayscale = false);
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

        public Bitmap Convolution(Bitmap originalBitmap,
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
        public int BoxSize { get; set; } = 3;
        public int MinBoxSize { get; } = 2;
        public int MaxBoxSize { get; } = 10;

        public List<DataGridRow> StructuralElementDataGrid { get; set; } = new List<DataGridRow>() { new DataGridRow(), new DataGridRow(), new DataGridRow(), new DataGridRow(), new DataGridRow()};

        public double BrightnessThreshold { get; set; } = 0.02;

        public Color LineColor { get; set; } = Color.Red;

        public List<Tuple<int, int>> getStructuralElementConfiguration()
        {
            List<Tuple<int, int>> StructuralElementPoints = new List<Tuple<int, int>>();
            for(int i = 0; i < StructuralElementDataGrid.Count; i++)
            {
                if (StructuralElementDataGrid[i].Column1)
                {
                    StructuralElementPoints.Add(Tuple.Create(i, 0));
                }
                if (StructuralElementDataGrid[i].Column2)
                {
                    StructuralElementPoints.Add(Tuple.Create(i, 1));
                }
                if (StructuralElementDataGrid[i].Column3)
                {
                    StructuralElementPoints.Add(Tuple.Create(i, 2));
                }
                if (StructuralElementDataGrid[i].Column4)
                {
                    StructuralElementPoints.Add(Tuple.Create(i, 3));
                }
                if (StructuralElementDataGrid[i].Column5)
                {
                    StructuralElementPoints.Add(Tuple.Create(i, 4));
                }
            }

            return StructuralElementPoints;
        }

    }

    class DataGridRow
    {
        public bool Column1 { get; set; }
        public bool Column2 { get; set; }
        public bool Column3 { get; set; }
        public bool Column4 { get; set; }
        public bool Column5 { get; set; }
    }

    class EmptyMorphologicalAlgorithmConfiguration: IMorphologicalAlgorithmConfiguration
    {

    }
}
