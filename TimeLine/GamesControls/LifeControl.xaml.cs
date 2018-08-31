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
    /// Логика взаимодействия для LifeControl.xaml
    /// </summary>
    public partial class LifeControl : UserControl
    {
        private bool isActive;

        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                if (value)
                {
                    ActivateControl();
                }
                else
                {
                    DisactivateControl();
                }
            }
        }

        public LifeControl()
        {
            InitializeComponent();
        }

        private async void ActivateControl()
        {
            imageDisactiveLife.Opacity = 1;
            imageActiveLife.Opacity = 0;
            imageActiveLife.Visibility = Visibility.Visible;

            for (int i = 0; i < 20; i++)
            {
                imageDisactiveLife.Opacity -= 0.05;
                imageActiveLife.Opacity += 0.05;

                await Task.Delay(20);
            }

            imageDisactiveLife.Visibility = Visibility.Collapsed;
            imageDisactiveLife.Opacity = 1;
        }

        private async void DisactivateControl()
        {
            imageActiveLife.Opacity = 1;
            imageDisactiveLife.Opacity = 0;
            imageDisactiveLife.Visibility = Visibility.Visible;

            for (int i = 0; i < 20; i++)
            {
                imageActiveLife.Opacity -= 0.05;
                imageDisactiveLife.Opacity += 0.05;

                await Task.Delay(25);
            }

            imageActiveLife.Visibility = Visibility.Collapsed;
            imageActiveLife.Opacity = 1;
        }
    }
}
