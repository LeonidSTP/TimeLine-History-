using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace TimeLine.GamesControls
{
    /// <summary>
    /// Логика взаимодействия для Life.xaml
    /// </summary>
    public partial class Life : UserControl
    {
        public const int MAXLifeAmount = 5;

        public int CurrentAmountOfLife
        {
            get
            {
                return lifeList.Where(l => l.IsActive).Count();
            }
        }

        List<LifeControl> lifeList = new List<LifeControl>();

        public Life()
        {
            InitializeComponent();

            for(int i = 0; i < MAXLifeAmount; i++)
            {
                LifeContainer.ColumnDefinitions.Add(new ColumnDefinition());
                LifeControl control = new LifeControl();
                Grid.SetColumn(control, i);
                LifeContainer.Children.Add(control);
                lifeList.Add(control);
            }
        }

        public void Fail()
        {
            for (int i = 0; i < MAXLifeAmount; i++)
            {
                if (lifeList[i].IsActive)
                {
                    lifeList[i].IsActive = false;
                    return;
                }
            }
        }

        public void Initialize()
        {
            for (int i = MAXLifeAmount - 1; i >= 0; i--)
            {
                lifeList[i].IsActive = true;
            }
        }
    }
}
