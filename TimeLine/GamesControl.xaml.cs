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
using TimeLine.GamesControls;

namespace TimeLine
{
    /// <summary>
    /// Interaction logic for GamesControl.xaml
    /// </summary>
    public partial class GamesControl : UserControl
    {
        private int counter;
        private readonly int numberOfQuestion;
        private const int MAXNumberOfQuestion = 30;

        private List<Question> questionList;
        private List<int> usedQuestion = new List<int>();
        private Random rand = new Random();

        public delegate void EndGameDelegate(int counter, int currentAmountOfLife, int numberOfQuestions, int maxAmountOfLife);

        public event EndGameDelegate EndGame;

        public GamesControl()
        {
            InitializeComponent();

            questionList = Question.ReadQuestions();
            numberOfQuestion = Math.Min(questionList.Count, MAXNumberOfQuestion);

            timeLineControl.CheckingAnswerResult += TimeLineControl_CheckingAnswerResult;
            timeLineControl.WrongAnswerLifeChange += TimeLineControl_WrongAnswerLifeChange;
        }

        private void TimeLineControl_WrongAnswerLifeChange()
        {
            gamesControlLife.Fail();
        }

        private void TimeLineControl_CheckingAnswerResult(bool isAnswerValid)
        {
            if (counter == numberOfQuestion)
            {
                Dispatcher.Invoke(DispatcherPriority.Render, (Action)(() => { }));
                Thread.Sleep(500);
                Dispatcher.Invoke(DispatcherPriority.Render, (Action)(() => { }));

                EndGame?.Invoke(counter, gamesControlLife.CurrentAmountOfLife, numberOfQuestion, Life.MAXLifeAmount);
            }
            else if (isAnswerValid || gamesControlLife.CurrentAmountOfLife > 0)
            {
                UpdateQuestion(questionList[GetRandom()]);
            }
            else
            {
                Dispatcher.Invoke(DispatcherPriority.Render, (Action)(() => { }));
                Thread.Sleep(500);
                Dispatcher.Invoke(DispatcherPriority.Render, (Action)(() => { }));

                EndGame?.Invoke(counter, gamesControlLife.CurrentAmountOfLife, numberOfQuestion, Life.MAXLifeAmount);
            }
        }

        public int GetRandom()
        {
            int index;
            int listSize = questionList.Count;

            do
            {
                index = rand.Next(listSize);
            } while (usedQuestion.Contains(index));

            usedQuestion.Add(index);

            return index;
        }

        public void StartGame()
        {
            counter = 0;
            usedQuestion.Clear();

            UpdateQuestion(questionList[GetRandom()]);

            timeLineControl.Initialize();
            gamesControlLife.Initialize();
        }

        void UpdateQuestion(Question currentQuestion)
        {
            counter++;
            textBlockNumberOfQuestion.Text = "Подія " + counter.ToString() + " з " + numberOfQuestion.ToString();
            textBlockQuestion.Text = currentQuestion.Name;

            timeLineControl.CurrentQuestion = currentQuestion;
        }
    }
}
