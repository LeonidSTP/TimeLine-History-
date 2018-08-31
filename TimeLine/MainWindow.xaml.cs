using MahApps.Metro.Controls;
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

namespace TimeLine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            gamesRules.StartGame += StartGame;
            gamesControl.EndGame += EndGame;

            gamesResult.RestartGame += RestartGame;
            gamesResult.CloseGame += CloseGame;
        }

        private void CloseGame()
        {
            Close();
        }

        private void RestartGame()
        {
            gamesControl.StartGame();

            ShowNextControl(gamesResult, gamesControl);
        }

        private void EndGame(int counter, int currentAmountOfLife, int numberOfQuestions, int MaxAmountOfLife)
        {
            ShowNextControl(gamesControl, gamesResult);

            gamesResult.ShowResult(counter, currentAmountOfLife, numberOfQuestions, MaxAmountOfLife);
        }

        private void StartGame()
        {
            ShowNextControl(gamesRules, gamesControl);

            gamesControl.StartGame();
        }

        private void ShowNextControl(UIElement previousControl, UIElement nextControl)
        {
            previousControl.Opacity = 1;
            nextControl.Opacity = 0;
            nextControl.Visibility = Visibility.Visible;

            for (int i = 0; i < 50; i++)
            {
                previousControl.Opacity -= 0.02;
                nextControl.Opacity += 0.02;

                Dispatcher.Invoke(DispatcherPriority.Render, (Action)(() => { }));
                Thread.Sleep(25);
                Dispatcher.Invoke(DispatcherPriority.Render, (Action)(() => { }));
            }

            previousControl.Visibility = Visibility.Collapsed;
            previousControl.Opacity = 1;
        }
    }
}
