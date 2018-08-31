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
using System.IO;

namespace TimeLine
{
    /// <summary>
    /// Interaction logic for GamesRules.xaml
    /// </summary>
    public partial class GamesRules : UserControl
    {
        public delegate void StartGameDelegate();

        public event StartGameDelegate StartGame;

        public GamesRules()
        {
            InitializeComponent();

            LoadRules();
        }

        private void LoadRules()
        {
            textBoxRules.Text = File.ReadAllText("Rules.txt");
        } 

        private void buttonStartGame_Click(object sender, RoutedEventArgs e)
        {
            StartGame?.Invoke();
        }
    }
}
