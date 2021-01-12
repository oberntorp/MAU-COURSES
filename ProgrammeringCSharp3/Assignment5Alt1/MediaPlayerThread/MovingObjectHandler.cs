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
    /// <summary>
    /// This class is responsible for creating and moving the object on screen
    /// </summary>
    public class MovingObjectHandler
    {
        private Button startMoveButton;
        private Button stopMoveButton;
        private Canvas spinningObjectCanvas;
        Polygon polygonOfClass;

        private bool IsRunning;
        private FigureMoveDirection direction;

        /// <summary>
        /// The constructor configures the essentials of the class
        /// </summary>
        /// <param name="StartMoveButtonFromGui">The start button</param>
        /// <param name="StopMoveButtonFromGui">The stop button</param>
        /// <param name="spinningObjectCanvasFromGui">The canvas containing to object</param>
        public MovingObjectHandler(Button StartMoveButtonFromGui, Button StopMoveButtonFromGui, Canvas spinningObjectCanvasFromGui)
        {
            startMoveButton = StartMoveButtonFromGui;
            stopMoveButton = StopMoveButtonFromGui;
            spinningObjectCanvas = spinningObjectCanvasFromGui;

            DisableSpinningObjectButtons(ButtonBeingDisabled.Stop);

            DrawFigure();
        }

        /// <summary>
        /// The entry point that creates the object, and moves it
        /// </summary>
        public void StartSpinning()
        {
            DrawFigure();
            spinningObjectCanvas.Dispatcher.Invoke(() => spinningObjectCanvas.Children.Add(polygonOfClass));
            MoveObject();
        }

        /// <summary>
        /// This method draws and moves the object
        /// </summary>
        private void DrawFigure()
        {
            DrawFigureIfNotExist();
            SetFigurePosition(FigureDrawMode.StartUp);
        }

        /// <summary>
        /// If the figure has not been created, create it
        /// </summary>
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

        /// <summary>
        /// Sets the points of the polygon
        /// </summary>
        /// <returns></returns>
        private PointCollection GetPointsOfPollygon()
        {
            PointCollection pointCollection = new PointCollection();
            pointCollection.Add(new System.Windows.Point(1, 50));
            pointCollection.Add(new System.Windows.Point(10, 80));
            pointCollection.Add(new System.Windows.Point(50, 50));

            return pointCollection;
        }

        /// <summary>
        /// Sets the position of the figure
        /// </summary>
        /// <param name="drawMode">Affects how the margin is applied</param>
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

        /// <summary>
        /// Gets what direction to use in the next spinn
        /// </summary>
        /// <param name="left">Newly set left margin</param>
        /// <param name="previousDirection">What was the previous direction</param>
        /// <returns>FigureMoveDirection</returns>
        private FigureMoveDirection GetDirection(double left, FigureMoveDirection previousDirection)
        {
            if ((previousDirection == FigureMoveDirection.Left) && (left < (spinningObjectCanvas.Dispatcher.Invoke(() => spinningObjectCanvas.ActualWidth))))
                return FigureMoveDirection.Left;
            else if (left >= 0)
                return FigureMoveDirection.Right;

            return FigureMoveDirection.Left;
        }

        /// <summary>
        /// Disable buttons according to the button being used recently
        /// </summary>
        /// <param name="buttonToDisable">The button to disable</param>
        private void DisableSpinningObjectButtons(ButtonBeingDisabled buttonToDisable)
        {
            switch (buttonToDisable)
            {
                case ButtonBeingDisabled.Play:
                    startMoveButton.Dispatcher.Invoke(() => startMoveButton.IsEnabled = false);
                    stopMoveButton.Dispatcher.Invoke(() => stopMoveButton.IsEnabled = true);
                    break;
                case ButtonBeingDisabled.Stop:
                    stopMoveButton.Dispatcher.Invoke(() => stopMoveButton.IsEnabled = false);
                    startMoveButton.Dispatcher.Invoke(() => startMoveButton.IsEnabled = true);
                    break;
            }
        }

        /// <summary>
        /// Move the object
        /// </summary>
        private void MoveObject()
        {
            while (IsRunning)
            {
                SetFigurePosition(FigureDrawMode.Move);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Starts the moving of the object, when hitting play
        /// </summary>
        public void StartPlay()
        {
            IsRunning = true;
            DisableSpinningObjectButtons(ButtonBeingDisabled.Play);
            MoveObject();
        }

        /// <summary>
        /// Stops the moving of the object when hitting Stop
        /// </summary>
        /// <param name="cancellationToken"></param>
        public void StopPlay(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
            else
            {
                IsRunning = false;
                DisableSpinningObjectButtons(ButtonBeingDisabled.Stop);
                MoveObject();
            }
        }
    }
}
