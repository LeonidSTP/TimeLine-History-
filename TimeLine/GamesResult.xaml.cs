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

namespace TimeLine
{
    /// <summary>
    /// Interaction logic for GamesResult.xaml
    /// </summary>
    public partial class GamesResult : UserControl
    {
        public delegate void RestartGameDelegate();

        public event RestartGameDelegate RestartGame;

        public delegate void CloseGameDelegate();

        public event CloseGameDelegate CloseGame;

        private List<TextBlock> textBlockLevelList = new List<TextBlock>();

        private List<Ellipse> imageLevelList = new List<Ellipse>(); 

        private List<string> levelList = new List<string>
        {
            "Половець",
            "Школяр Середньовіччя",
            "Студен академії",
            "Професор Могилянки",
            "Доктор історичних наук"
        };

        private int levelValue;

        public GamesResult()
        {
            InitializeComponent();

            textBlockLevelList.Add(textBlockLevel0);
            textBlockLevelList.Add(textBlockLevel1);
            textBlockLevelList.Add(textBlockLevel2);
            textBlockLevelList.Add(textBlockLevel3);
            textBlockLevelList.Add(textBlockLevel4);

            imageLevelList.Add(imageLevel0);
            imageLevelList.Add(imageLevel1);
            imageLevelList.Add(imageLevel2);
            imageLevelList.Add(imageLevel3);
            imageLevelList.Add(imageLevel4);
        }
        private void buttonGameAgaine_Click(object sender, RoutedEventArgs e)
        {
            textBlockLevelList[levelValue].Opacity = 0.75;
            textBlockLevelList[levelValue].Foreground = Brushes.White;
            imageLevelList[levelValue].Opacity = 0.65;

            textBlockResultAnswers.Text = "";
            textBlockResultLevel.Text = "";

            RestartGame?.Invoke();
        }

        private void buttonGameOver_Click(object sender, RoutedEventArgs e)
        {
            CloseGame?.Invoke();
        }

        public void ShowResult(int counter, int currentAmountOfLife, int numberOfQuestions, int MaxAmountOfLife)
        {
            textBlockResultAnswers.Text = "Ви протримались " + counter.ToString() + " раундів";

            if (counter == numberOfQuestions && currentAmountOfLife == MaxAmountOfLife)
            {
                levelValue = 4;
            }
            else if (counter == numberOfQuestions)
            {
                levelValue = 3;
            }
            else if (counter >= 2 * numberOfQuestions / 3)
            {
                levelValue = 2;
            }
            else if (counter >= numberOfQuestions / 3)
            {
                levelValue = 1;
            }
            else
            {
                levelValue = 0;
            }

            textBlockLevelList[levelValue].Opacity = 1;
            textBlockLevelList[levelValue].Foreground = Brushes.Yellow;
            imageLevelList[levelValue].Opacity = 1;

            textBlockResultLevel.Text = "Ви знаєте Історію України на рівні: " + levelList[levelValue];
        }
    }
}
