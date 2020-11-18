using System;
using System.Collections.Generic;
using System.Drawing;

namespace morphological_image_processing_wpf.Core.Generator
{
    class ImageGenerator
    {
        private readonly Random rand;
        private readonly int bitmapWidth;
        private readonly int bitmapHeight;

        public ImageGenerator(int bitmapHeight, int bitmapWidth)
        {
            rand = new Random();
            this.bitmapHeight = bitmapHeight;
            this.bitmapWidth = bitmapWidth;
        }

       public void DrawPolygon(Graphics g, int maxNumberOfEdges, int maxStrokeThickness, int minX, int maxX, int minY, int maxY, bool isFilled, int moveX, int moveY)
        {
            Brush brush = new SolidBrush(GetBrushColor());
            int numberOfEdges = rand.Next(3, maxNumberOfEdges);
            List<Point> pointList = new List<Point>();

            for (int i = 0; i < numberOfEdges; i++)
            {
                pointList.Add(new Point(rand.Next(minX, maxX) + moveX, rand.Next(minY, maxY) + moveY));
            }

            if (isFilled)
            {
                g.FillPolygon(brush, pointList.ToArray());
            }
            else
            {
                Pen pen = new Pen(brush, rand.Next(1, maxStrokeThickness));
                g.DrawPolygon(pen, pointList.ToArray());
            }
        }

        private Color GetBrushColor()
        {
            return Color.FromArgb(255, 30, 30, 30);
        }
       
        public void DrawEllipse(Graphics g, int maxStrokeThickness, int minWidth, int maxWidth, int minHeight, int maxHeight, bool isFilled, int moveX, int moveY)
        {
            Rectangle rect = new Rectangle
            {
                Width = rand.Next(minWidth, maxWidth),
                Height = rand.Next(minHeight, maxHeight),
                X = moveX,
                Y = moveY
            };

            Brush brush = new SolidBrush(GetBrushColor());

            if (isFilled)
            {
                g.FillEllipse(brush, rect);
            } 
            else
            {
                Pen pen = new Pen(brush, rand.Next(1, maxStrokeThickness));
                g.DrawEllipse(pen, rect);
            }
        }

        public void DrawPolyline(Graphics g, int maxNumberOfEdges, int maxStrokeThickness, int minX, int maxX, int minY, int maxY, int moveX, int moveY)
        {
            Brush brush = new SolidBrush(GetBrushColor());
            Pen pen = new Pen(brush, rand.Next(1, maxStrokeThickness));
            int numberOfEdges = rand.Next(2, maxNumberOfEdges);
            List<Point> pointList = new List<Point>();

            for (int i = 0; i < numberOfEdges; i++)
            {
                pointList.Add(new Point(rand.Next(minX, maxX) + moveX, rand.Next(minY, maxY) + moveY));
            }

            g.DrawLines(pen, pointList.ToArray());
        }

        public Bitmap GeneratePicture(int numberOfShapes, int maxNumberOfEdges, int maxStrokeThickness)
        {
            Bitmap bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            Graphics graphics = Graphics.FromImage(bitmap);
            
            for(int i = 0; i <numberOfShapes; i++)
            {
                int minX = rand.Next(100, 200);
                int maxX = rand.Next(minX, bitmapWidth);
                int minY = rand.Next(100, 200);
                int maxY = rand.Next(minY, bitmapHeight);
                int moveX = rand.Next(0, bitmapWidth - maxX);
                int moveY = rand.Next(0, bitmapHeight - maxY);
                bool isFilled = true;
                if (rand.Next(0, 2) == 0)
                    isFilled = false;

                switch (rand.Next(0, 3))
                {
                    case 0:
                        DrawEllipse(graphics, maxStrokeThickness, minX, maxX, minY, maxY, isFilled, moveX, moveY);
                        break;
                    case 1:
                        DrawPolygon(graphics, maxNumberOfEdges, maxStrokeThickness, minX, maxX, minY, maxY, isFilled, moveX, moveY);
                        break;
                    case 2:
                        DrawPolyline(graphics, maxNumberOfEdges, maxStrokeThickness, minX, maxX, minY, maxY, moveX, moveY);
                        break;
                }
            }

            return bitmap;
        }
    }
}
