using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TimeLine.GamesControls
{
    /// <summary>
    /// Interaction logic for TimeIntervalControl.xaml
    /// </summary>
    public partial class TimeIntervalControl : UserControl
    {
        private const int MAXLineNumber = 30;

        private const int NormalLineNumber = 15;

        private SolidColorBrush normalBrush = new SolidColorBrush(Color.FromRgb(255, 206, 57));

        private SolidColorBrush rightAnswerBrush = new SolidColorBrush(Color.FromRgb(57, 255, 57));

        private SolidColorBrush wrongAnswerBrush = new SolidColorBrush(Color.FromRgb(255, 57, 57));

        public int Index;

        public int IndexQuestionBefore;

        public int IndexQuestionAfter;

        //public delegate void ControlMouseEnterDelegate(int indexBefore, int indexAfter);

        //public event ControlMouseEnterDelegate ControlMouseEnter;

        //public delegate void ControlMouseLeaveDelegate(int indexBefore, int indexAfter);

        //public event ControlMouseLeaveDelegate ControlMouseLeave;

        public delegate void ControlMouseDownDelegate(int controlIndex);

        public event ControlMouseDownDelegate ControlMouseDown;

        public TimeIntervalControl(int position)
        {
            InitializeComponent();

            ShowAsNormal();

            Index = position;
        }

        private void timeIntervalContainer_MouseEnter(object sender, MouseEventArgs e)
        {
            timeIntervalContainer.Background.Opacity = 0.9;
            //this.Height = 100;
            //this.Margin = new Thickness(0, -20, 0, -20);
            //ControlMouseEnter?.Invoke(IndexQuestionBefore, IndexQuestionAfter);
        }

        private void timeIntervalContainer_MouseLeave(object sender, MouseEventArgs e)
        {
            timeIntervalContainer.Background.Opacity = 0.5;
            //this.Height = 75;
            //this.Margin = new Thickness(0, -15, 0, -15);
            //ControlMouseLeave?.Invoke(IndexQuestionBefore, IndexQuestionAfter);
        }

        public void ShowAsWrongAnswer()
        {
            timeIntervalContainer.Background = wrongAnswerBrush;
            timeIntervalContainer.Background.Opacity = 1;
        }

        public void ExpandControl()
        {
            timeIntervalContainer.Background = rightAnswerBrush;
            timeIntervalContainer.Background.Opacity = 1;

            for (int i = NormalLineNumber; i < MAXLineNumber; i++)
            {
                timeIntervalContainer.RowDefinitions.Add(new RowDefinition());
                Line line = new Line();
                line.Height = 1;
                line.StrokeThickness = 1;
                line.Width = this.Width / 6;
                line.X2 = this.Width / 6;
                line.Stroke = Brushes.White;
                Grid.SetRow(line, i);
                timeIntervalContainer.Children.Add(line);

                Height += 5;

                Dispatcher.Invoke(DispatcherPriority.Render, (Action)(() => { }));
                Thread.Sleep(25);
                Dispatcher.Invoke(DispatcherPriority.Render, (Action)(() => { }));
            }
        }

        private void timeIntervalContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ControlMouseDown?.Invoke(Index);
        }

        public void ShowAsNormal()
        {
            this.Height = 75;

            timeIntervalContainer.Children.Clear();
            timeIntervalContainer.RowDefinitions.Clear();
            timeIntervalContainer.Background = normalBrush;
            timeIntervalContainer.Background.Opacity = 0.5;

            for (int i = 0; i < NormalLineNumber; i++)
            {
                timeIntervalContainer.RowDefinitions.Add(new RowDefinition());
                Line line = new Line();
                line.Height = 1;
                line.StrokeThickness = 1;
                line.Width = this.Width / 6;
                line.X2 = this.Width / 6;
                line.Stroke = Brushes.White;
                Grid.SetRow(line, i);
                timeIntervalContainer.Children.Add(line);
            }
        }
    }
}
