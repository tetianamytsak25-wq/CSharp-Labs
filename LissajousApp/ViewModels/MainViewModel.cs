using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using LissajousApp.Models;

namespace LissajousApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private double amplitudeX = 1;
        private double amplitudeY = 1;
        private double frequencyX = 1;
        private double frequencyY = 1;
        private double phaseX = 0;
        private double phaseY = 0;
        private int pointCount = 500;

        private readonly LissajousModel model;

        public PointCollection PlotPoints { get; private set; }
            = new PointCollection();

        public double AmplitudeX
        {
            get { return amplitudeX; }
            set
            {
                amplitudeX = value;
                OnPropertyChanged(nameof(AmplitudeX));
            }
        }

        public double AmplitudeY
        {
            get { return amplitudeY; }
            set
            {
                amplitudeY = value;
                OnPropertyChanged(nameof(AmplitudeY));
            }
        }

        public double FrequencyX
        {
            get { return frequencyX; }
            set
            {
                frequencyX = value;
                OnPropertyChanged(nameof(FrequencyX));
            }
        }

        public double FrequencyY
        {
            get { return frequencyY; }
            set
            {
                frequencyY = value;
                OnPropertyChanged(nameof(FrequencyY));
            }
        }

        public double PhaseX
        {
            get { return phaseX; }
            set
            {
                phaseX = value;
                OnPropertyChanged(nameof(PhaseX));
            }
        }

        public double PhaseY
        {
            get { return phaseY; }
            set
            {
                phaseY = value;
                OnPropertyChanged(nameof(PhaseY));
            }
        }

        public int PointCount
        {
            get { return pointCount; }
            set
            {
                pointCount = value;
                OnPropertyChanged(nameof(PointCount));
            }
        }

        public ICommand BuildCommand { get; }

        public MainViewModel()
        {
            model = new LissajousModel();
            BuildCommand = new RelayCommand(Build);
        }

        private void Build()
        {
            if (AmplitudeX <= 0 || AmplitudeY <= 0)
            {
                MessageBox.Show("Амплітуди повинні бути більшими за 0.");
                return;
            }

            if (FrequencyX <= 0 || FrequencyY <= 0)
            {
                MessageBox.Show("Частоти повинні бути більшими за 0.");
                return;
            }

            if (PointCount < 20)
            {
                MessageBox.Show("Кількість точок повинна бути не менше 20.");
                return;
            }

            var mathematicalPoints = model.CalculatePoints(
                AmplitudeX,
                AmplitudeY,
                FrequencyX,
                FrequencyY,
                PhaseX,
                PhaseY,
                PointCount);

            PointCollection screenPoints = new PointCollection();

            double centerX = 250;
            double centerY = 250;

            double scale = 220 / Math.Max(AmplitudeX, AmplitudeY);

            foreach (Point point in mathematicalPoints)
            {
                double screenX = centerX + point.X * scale;
                double screenY = centerY - point.Y * scale;

                screenPoints.Add(
                    new Point(screenX, screenY));
            }

            PlotPoints = screenPoints;

            OnPropertyChanged(nameof(PlotPoints));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}