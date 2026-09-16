using System;
using System.Collections.Generic;
using System.Windows;

namespace LissajousApp.Models
{
    public class LissajousModel
    {
        public List<Point>CalculatePoints(double amplitudeX,  double amplitudeY, double frequencyX, double frequencyY, double phaseX, double phaseY, int pointCount)
        {
            List<Point> points = new List<Point>();

            double phaseXRad = phaseX * Math.PI / 180;
            double phaseYRad = phaseY * Math.PI / 180;
            double period = 1;
            double timeStep = period / (pointCount - 1);

            for (int i = 0; i < pointCount; i++)
            {
                double t = i * timeStep;
                double x = amplitudeX * Math.Sin(2 * Math.PI * frequencyX * t + phaseXRad);
                double y = amplitudeY * Math.Sin(2 * Math.PI * frequencyY * t + phaseYRad);
                points.Add(new Point(x, y));
            }
            return points;
        }
    }
}