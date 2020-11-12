using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

       public Polygon GeneratePolygon(int maxNumberOfEdges, int maxStrokeThickness, int minX, int maxX, int minY, int maxY, bool isFilled, int moveX, int moveY)
        {
            int numberOfEdges = rand.Next(3, maxNumberOfEdges);
            Polygon myPolygon = new Polygon
            {
                Stroke = Brushes.Black,
                StrokeThickness = rand.Next(1, maxStrokeThickness),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };

            if(isFilled)
            {
                myPolygon.Fill = Brushes.Black;
            }

            PointCollection myPointCollection = new PointCollection{};
            for(int i = 0; i < numberOfEdges; i++)
            {
                myPointCollection.Add(new Point(rand.Next(minX, maxX), rand.Next(minY, maxY)));
            }
            myPolygon.Points = myPointCollection;

            myPolygon.Measure(new Size(bitmapWidth, bitmapHeight));
            myPolygon.Arrange(new Rect(moveX, moveY, bitmapWidth, bitmapHeight));

            return myPolygon;
        }
       
        public Ellipse GenerateEllipse(int maxStrokeThickness, int minWidth, int maxWidth, int minHeight, int maxHeight, bool isFilled, int moveX, int moveY)
        {
            Ellipse ellipse =  new Ellipse
            {
                Stroke = Brushes.Black,
                StrokeThickness = rand.Next(1, maxStrokeThickness),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Width = rand.Next(minWidth, maxWidth),
                Height = rand.Next(minHeight, maxHeight)
            };

            if (isFilled)
            {
                ellipse.Fill = Brushes.Black;
            }

            ellipse.Measure(new Size(bitmapWidth, bitmapHeight));
            ellipse.Arrange(new Rect(moveX, moveY, bitmapWidth, bitmapHeight));

            return ellipse;
        }

        public Polyline GeneratePolyline(int maxNumberOfEdges, int maxStrokeThickness, int minX, int maxX, int minY, int maxY, int moveX, int moveY)
        {
            int numberOfEdges = rand.Next(1, maxNumberOfEdges);
            Polyline myPolyline = new Polyline
            {
                Stroke = Brushes.Black,
                StrokeThickness = rand.Next(1, maxStrokeThickness),
                FillRule = FillRule.EvenOdd
            };

            PointCollection myPointCollection = new PointCollection {};
            for (int i = 0; i < numberOfEdges; i++)
            {
                myPointCollection.Add(new Point(rand.Next(minX, maxX), rand.Next(minY, maxY)));
            }
            myPolyline.Points = myPointCollection;

            myPolyline.Measure(new Size(bitmapWidth, bitmapHeight));
            myPolyline.Arrange(new Rect(moveX, moveY, bitmapWidth, bitmapHeight));

            return myPolyline;
        }

        public RenderTargetBitmap GeneratePicture(int numberOfShapes, int maxNumberOfEdges, int maxStrokeThickness)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap(bitmapWidth, bitmapHeight, 96, 96, PixelFormats.Default);
            
            for(int i = 0; i <numberOfShapes; i++)
            {
                int minX = rand.Next(bitmapWidth / 10, bitmapWidth / 2);
                int maxX = rand.Next(minX, bitmapWidth);
                int minY = rand.Next(bitmapHeight / 10, bitmapHeight / 2);
                int maxY = rand.Next(minY, bitmapHeight);
                int moveX = rand.Next(0, bitmapWidth - maxX);
                int moveY = rand.Next(0, bitmapHeight - maxY);
                bool isFilled = true;
                if (rand.Next(0, 2) == 0)
                    isFilled = false;

                switch (rand.Next(0, 3))
                {
                    case 0:
                        bitmap.Render(GenerateEllipse(maxStrokeThickness, minX, maxX, minY, maxY, isFilled, moveX, moveY));
                        break;
                    case 1:
                        bitmap.Render(GeneratePolygon(maxNumberOfEdges, maxStrokeThickness, minX, maxX, minY, maxY, isFilled, moveX, moveY));
                        break;
                    case 2:
                        bitmap.Render(GeneratePolyline(maxNumberOfEdges, maxStrokeThickness, minX, maxX, minY, maxY, moveX, moveY));
                        break;
                }
            }

            return bitmap;
        }
    }
}
