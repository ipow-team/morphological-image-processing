using morphological_image_processing_wpf.Core.Algorithms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace morphological_image_processing_wpf.Core.ChangesDetection
{
    class ImageSplit 
    {
        readonly DirectBitmap image;
        bool[,] visitedPixels;
        bool[,] pixelsInQueue;
        bool[,] pixelsInHelperQueue;
        readonly int imageWidth;
        readonly int imageHeight;
        readonly List<DirectBitmap> figures;
        bool isFigureFound;

        public ImageSplit(Bitmap image)
        {
            this.image = new DirectBitmap(image);
            imageWidth = image.Width;
            imageHeight = image.Height;
            this.visitedPixels = new bool[imageWidth, imageHeight];
            this.pixelsInQueue = new bool[imageWidth, imageHeight];
            this.pixelsInHelperQueue = new bool[imageWidth, imageHeight];
            isFigureFound = false;
            figures = new List<DirectBitmap>();
        }

        public List<DirectBitmap> LookForShapes()
        {
            for (int i = 0; i < imageWidth; i++)
            {
                for (int j = 0; j < imageHeight; j++)
                {
                    visitedPixels[i, j] = false;
                    pixelsInQueue[i, j] = false;
                    pixelsInHelperQueue[i, j] = false;
                }
            }

            int xx = 0;
            int yy = 0;

            Queue<Tuple<int, int>> pixelsToVisit = new Queue<Tuple<int, int>>();
            Queue<Tuple<int, int>> helperQueue = new Queue<Tuple<int, int>>();

            pixelsToVisit.Enqueue(Tuple.Create(0, 0));
            
            while (pixelsToVisit.Count > 0 || helperQueue.Count > 0)
            {
                Tuple<int, int> currentPixel;

                if (helperQueue.Count > 0)
                {
                    currentPixel = helperQueue.Dequeue();
                } 
                else
                {
                    isFigureFound = false;
                    currentPixel = pixelsToVisit.Dequeue();
                }
               
                if (IsPartOfFigure(currentPixel.Item1, currentPixel.Item2))
                {
                    helperQueue = AddNeighbourhoodToCheck(helperQueue, currentPixel.Item1, currentPixel.Item2, true);
                    AddPixelToFigure(currentPixel);
                } 
                else
                {
                    pixelsToVisit = AddNeighbourhoodToCheck(pixelsToVisit, currentPixel.Item1, currentPixel.Item2, false);
                }

                while (pixelsToVisit.Count == 0 && helperQueue.Count == 0 && xx < imageWidth && yy < imageHeight)
                {
                    if(!visitedPixels[xx, yy])
                    {
                        pixelsToVisit.Enqueue(Tuple.Create(xx, yy));
                        pixelsInQueue[xx, yy] = true;
                    }

                    xx += 1;

                    if (xx == imageWidth)
                    {
                        xx = 0;
                        yy += 1;
                    }
                }
            }
            return figures;
        }

        private bool IsPartOfFigure(int x, int y)
        {
            bool result = false;

            if (visitedPixels[x, y] == false && image.GetPixel(x, y).GetBrightness() < 0.5)
            {
                result = true;
            }

            visitedPixels[x, y] = true;

            return result;
        }

        private Queue<Tuple<int, int>> AddNeighbourhoodToCheck(Queue<Tuple<int, int>> queue, int coorX, int coorY, bool isHelperQueue)
        {
            for (int i = Math.Max(coorX - 1, 0); i <= Math.Min(coorX + 1, imageWidth - 1); i++)
            {
                for (int j = Math.Max(coorY - 1, 0); j <= Math.Min(coorY + 1, imageHeight - 1); j++)
                {
                    var tuple = Tuple.Create(i, j);
                    if (visitedPixels[i, j] == false)
                    {
                        if (isHelperQueue && pixelsInHelperQueue[i, j] == false)
                        {
                            queue.Enqueue(tuple);
                            pixelsInHelperQueue[i, j] = true;
                        }
                        else if (!isHelperQueue && pixelsInQueue[i, j] == false)
                        {
                            queue.Enqueue(tuple);
                            pixelsInQueue[i, j] = true;
                        }
                    }
                }
            }
            
            return queue;
        }

        private void AddPixelToFigure(Tuple<int, int> pixel)
        {
            if (!isFigureFound)
            {
                isFigureFound = true;
                figures.Add(new DirectBitmap(imageWidth, imageHeight));
            }

            figures[^1].SetPixel(pixel.Item1, pixel.Item2, Color.Black);
        }
    }
}
