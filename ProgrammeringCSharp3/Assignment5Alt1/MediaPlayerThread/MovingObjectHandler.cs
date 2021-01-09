using MediaPlayerThread.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MediaPlayerThread
{
    public class MovingObjectHandler
    {
        private Button startSpinnButton;
        private Button stopSpinnButton;
        private Canvas spinningObjectCanvas;
        Polygon polygonOfClass;

        private bool IsRunning;
        private FigureMoveDirection direction;


        public MovingObjectHandler(Button StartSpinnButtonFromGui, Button StopSpinnButtonFromGui, Canvas spinningObjectCanvasFromGui)
        {
            startSpinnButton = StartSpinnButtonFromGui;
            stopSpinnButton = StopSpinnButtonFromGui;
            spinningObjectCanvas = spinningObjectCanvasFromGui;

            DisableSpinningObjectButtons(ButtonBeingDisabled.Stop);

            DrawFigure();
        }

        public void StartSpinning(CancellationToken cancelationToken)
        {
            DrawFigure();
            spinningObjectCanvas.Dispatcher.Invoke(() => spinningObjectCanvas.Children.Add(polygonOfClass));
            MoveObject(cancelationToken);
        }

        private void DrawFigure()
        {
            DrawFigureIfNotExist();
            SetFigurePosition(FigureDrawMode.StartUp);
        }

        private void DrawFigureIfNotExist()
        {
            if (polygonOfClass == null)
            {
                polygonOfClass = new Polygon();
                polygonOfClass.Points = GetPointsOfPollygon();

                polygonOfClass.Fill = Brushes.Aqua;
                polygonOfClass.Stroke = Brushes.Blue;
                polygonOfClass.StrokeThickness = 1;
            }
        }

        private void SetFigurePosition(FigureDrawMode drawMode)
        {
            Thickness marginOfShape = polygonOfClass.Dispatcher.Invoke(() => polygonOfClass.Margin);

            if (drawMode == FigureDrawMode.StartUp)
            {
                marginOfShape.Left = 20;
            }
            else
            {
                if (direction == FigureMoveDirection.Left)
                {
                    marginOfShape.Left += 20;
                }
                else
                {
                    marginOfShape.Left -= 20;
                }
                direction = GetDirection(marginOfShape.Left, direction == FigureMoveDirection.Left ? FigureMoveDirection.Left : direction);
            }
            polygonOfClass.Dispatcher.Invoke(() => polygonOfClass.Margin = marginOfShape);
            spinningObjectCanvas.Dispatcher.Invoke(() => spinningObjectCanvas.Children.Remove(polygonOfClass));
            spinningObjectCanvas.Dispatcher.Invoke(() => spinningObjectCanvas.Children.Add(polygonOfClass));
        }

        private FigureMoveDirection GetDirection(double left, FigureMoveDirection previousDirection)
        {
            if ((previousDirection == FigureMoveDirection.Left) && (left < (spinningObjectCanvas.Dispatcher.Invoke(() => spinningObjectCanvas.ActualWidth))))
                return FigureMoveDirection.Left;
            else if (left >= 0)
                return FigureMoveDirection.Right;

            return FigureMoveDirection.Left;
        }

        private PointCollection GetPointsOfPollygon()
        {
            PointCollection pointCollection = new PointCollection();
            pointCollection.Add(new System.Windows.Point(1, 50));
            pointCollection.Add(new System.Windows.Point(10, 80));
            pointCollection.Add(new System.Windows.Point(50, 50));

            return pointCollection;
        }

        private void DisableSpinningObjectButtons(ButtonBeingDisabled buttonToDisable)
        {
            switch (buttonToDisable)
            {
                case ButtonBeingDisabled.Play:
                    startSpinnButton.Dispatcher.Invoke(() => startSpinnButton.IsEnabled = false);
                    stopSpinnButton.Dispatcher.Invoke(() => stopSpinnButton.IsEnabled = true);
                    break;
                case ButtonBeingDisabled.Stop:
                    stopSpinnButton.Dispatcher.Invoke(() => stopSpinnButton.IsEnabled = false);
                    startSpinnButton.Dispatcher.Invoke(() => startSpinnButton.IsEnabled = true);
                    break;
            }
        }

        private void MoveObject(CancellationToken cancelationToken)
        {
            if (cancelationToken.IsCancellationRequested)
            {
                cancelationToken.ThrowIfCancellationRequested();
            }
            while (IsRunning)
            {
                SetFigurePosition(FigureDrawMode.Move);
                Thread.Sleep(1000);
            }
        }


        public void StartPlay(CancellationToken cancelationToken)
        {
            IsRunning = true;
            DisableSpinningObjectButtons(ButtonBeingDisabled.Play);
            MoveObject(cancelationToken);
        }

        public void StopPlay(CancellationToken cancelationToken)
        {
            IsRunning = false;
            DisableSpinningObjectButtons(ButtonBeingDisabled.Stop);
            MoveObject(cancelationToken);
        }
    }
}
