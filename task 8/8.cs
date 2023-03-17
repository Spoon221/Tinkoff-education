using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;


namespace Task8_Tinkoff
{
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Length
        {
            get { return Math.Sqrt(X * X + Y * Y); }
        }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector(Point A, Point B)
        {
            X = B.X - A.X;
            Y = B.Y - A.Y;
        }

        public override string ToString()
        {
            return X + " " + Y;
        }

        public static double ScalarProduct(Vector v1, Vector v2) => v1.X * v2.X + v1.Y * v2.Y;

        public static double AngleBetween(Vector v1, Vector v2)
        {
            var scalarP = ScalarProduct(v1, v2);
            var temp = (v1.Length * v2.Length);
            var temp1 = scalarP / temp;
            return Math.Acos(temp1);
        }

        public Vector Rotate(double rotationAngle)
        {
            var x = X * Math.Cos(rotationAngle) - Y * Math.Sin(rotationAngle);
            var y = X * Math.Sin(rotationAngle) + Y * Math.Cos(rotationAngle);
            return new Vector(x, y);
        }
    }

    public struct Point
    {
        public double X;
        public double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public class Program
    {
        public static (double x, double y) Task8(double x, double y, double[] corners)
        {

            (double x, double y)[,] planCornersCoordinates = new (double x, double y)[2, 2];
            planCornersCoordinates[0, 0] = (corners[0], corners[1]);
            planCornersCoordinates[1, 0] = (corners[2], corners[3]);
            planCornersCoordinates[1, 1] = (corners[4], corners[5]);
            planCornersCoordinates[0, 1] = (corners[6], corners[7]);

            double planXLenght = Math.Abs(planCornersCoordinates[0, 0].x - planCornersCoordinates[1, 1].x);
            double planYLenght = Math.Abs(planCornersCoordinates[0, 0].y - planCornersCoordinates[1, 1].y);
            double planSquare = planXLenght * planYLenght;
            double square = x * y;
            double scale = Math.Sqrt(square / planSquare);


            if (planCornersCoordinates[0, 0].y == planCornersCoordinates[1, 0].y)
            {
                var signX = -Math.Sign(planCornersCoordinates[1, 0].x - planCornersCoordinates[0, 0].x);
                var signY = -Math.Sign(planCornersCoordinates[0, 1].y - planCornersCoordinates[0, 0].y);
                x = (planCornersCoordinates[0, 0].x * scale) / (scale + signX);
                y = (planCornersCoordinates[0, 0].y * scale) / (scale + signY);
            }

            if (planCornersCoordinates[0, 0].x == planCornersCoordinates[1, 0].x)
            {
                var signX = Math.Sign(planCornersCoordinates[1, 0].y - planCornersCoordinates[0, 0].y);
                var signY = Math.Sign(planCornersCoordinates[0, 1].x - planCornersCoordinates[0, 0].x);
                var determinant = 1 / scale * signX * signY * 1 / scale - 1;
                var determinantX = -planCornersCoordinates[0, 0].y * 1 / scale * signY - planCornersCoordinates[0, 0].x;
                x = determinantX / determinant;
                var determinantY = -planCornersCoordinates[0, 0].x * 1 / scale * signX - planCornersCoordinates[0, 1].y;
                y = determinantY / determinant;
            }
            return (x, y);
        }


        public static (double x, double y) Task8_Version2(double x, double y, double[] corners)
        {

            Point[,] planCornersCoordinates = new Point[2, 2];
            planCornersCoordinates[0, 0] = new Point(corners[0], corners[1]);
            planCornersCoordinates[1, 0] = new Point(corners[2], corners[3]);
            planCornersCoordinates[1, 1] = new Point(corners[4], corners[5]);
            planCornersCoordinates[0, 1] = new Point(corners[6], corners[7]);


            Vector planXAxis = new Vector(planCornersCoordinates[0, 0], planCornersCoordinates[1, 0]);
            double rotationAngle = -Vector.AngleBetween(planXAxis, new Vector(1, 0)) * Math.Sign(planXAxis.Y);
            Vector planYAxis = new Vector(planCornersCoordinates[0, 0], planCornersCoordinates[0, 1]);


            double planXLenght = planXAxis.Length;
            double planYLenght = planYAxis.Length;
            double planSquare = planXLenght * planYLenght;
            double square = x * y;
            double scale = Math.Sqrt(square / planSquare);


            if (planCornersCoordinates[0, 0].X == planCornersCoordinates[1, 0].X)
            {
                var signX = Math.Sign(planCornersCoordinates[1, 0].Y - planCornersCoordinates[0, 0].Y);
                var signY = Math.Sign(planCornersCoordinates[0, 1].X - planCornersCoordinates[0, 0].X);
                var determinant = 1 / scale * signX * signY * 1 / scale - 1;
                var determinantX = -planCornersCoordinates[0, 0].Y * 1 / scale * signY - planCornersCoordinates[0, 0].X;
                x = determinantX / determinant;
                var determinantY = -planCornersCoordinates[0, 0].X * 1 / scale * signX - planCornersCoordinates[0, 1].Y;
                y = determinantY / determinant;
                return (x, y);
            }
            else
            {

                var signX = -Math.Sign(planCornersCoordinates[1, 0].X - planCornersCoordinates[0, 0].X);
                var signY = -Math.Sign(planCornersCoordinates[0, 1].Y - planCornersCoordinates[0, 0].Y);
                x = (planCornersCoordinates[0, 0].X * scale) / (scale + signX * Math.Abs(Math.Cos(rotationAngle)));
                y = (planCornersCoordinates[0, 0].Y * scale) / (scale + signY * Math.Abs(Math.Cos(rotationAngle)));
            }

            return (x, y);
        }

        public static double[] GetNumericalInput()
        {
            var parametrs = Console.ReadLine().Split(' ').Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
            return parametrs;
        }

        static void Main(string[] args)
        {
            var firstLine = GetNumericalInput();
            var secondLine = GetNumericalInput();
            var result = Task8(firstLine[0], firstLine[1], secondLine);
            Console.WriteLine(result.x + " " + result.y);
        }
    }
}