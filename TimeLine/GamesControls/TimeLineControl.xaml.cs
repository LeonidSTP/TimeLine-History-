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
    /// Interaction logic for TimeLineControl.xaml
    /// </summary>
    public partial class TimeLineControl : UserControl
    {
        private List<QuestionControl> questionControlList = new List<QuestionControl>();
        private List<TimeIntervalControl> timeIntervalControlList = new List<TimeIntervalControl>();

        public Question CurrentQuestion { get; set; }

        public delegate void CheckingAnswerResultDelegate(bool isAnswerValid);

        public event CheckingAnswerResultDelegate CheckingAnswerResult;

        public delegate void WrongAnswerLifeChangeDelegate();

        public event WrongAnswerLifeChangeDelegate WrongAnswerLifeChange;

        public TimeLineControl()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            timeLineControlContainer.Children.Clear();
            timeLineControlContainer.RowDefinitions.Clear();

            timeIntervalControlList.Clear();
            questionControlList.Clear();

            for (int i = 0; i < 100; i++)
            {
                timeLineControlContainer.RowDefinitions.Add(new RowDefinition());
            }

            AddNewQuestionLine(0, 0, new Question() { Name = "Заснування Київа", Index = int.MinValue });

            AddNewTimeInterval(1, 0);
            timeIntervalControlList[0].IndexQuestionBefore = 0;
            timeIntervalControlList[0].IndexQuestionAfter = 1;

            AddNewQuestionLine(2, 1, new Question() { Name = "Початок Національно-визвольної війни", Index = 0 });

            AddNewTimeInterval(3, 1);
            timeIntervalControlList[1].IndexQuestionBefore = 1;
            timeIntervalControlList[1].IndexQuestionAfter = 2;

            AddNewQuestionLine(4, 2, new Question() { Name = "Початок Евромайдану", Index = int.MaxValue });
        }

        private void AddNewQuestionLine(int rowNumber, int position, Question question)
        {
            QuestionControl questionControl = new QuestionControl(question);

            Grid.SetRow(questionControl, rowNumber);
            timeLineControlContainer.Children.Add(questionControl);

            questionControlList.Insert(position, questionControl);
        }

        private TimeIntervalControl AddNewTimeInterval(int rowNumber, int position)
        {
            TimeIntervalControl timeInterval = new TimeIntervalControl(position);

            //timeInterval.ControlMouseEnter += TimeInterval_ControlMouseEnter;
            //timeInterval.ControlMouseLeave += TimeInterval_ControlMouseLeave;
            timeInterval.ControlMouseDown += TimeInterval_ControlMouseDown;

            Grid.SetRow(timeInterval, rowNumber);
            timeLineControlContainer.Children.Add(timeInterval);

            timeIntervalControlList.Insert(position, timeInterval);

            return timeInterval;
        }

        //private void TimeInterval_ControlMouseEnter(int indexBefore, int indexAfter)
        //{
        //    questionControlList[indexBefore].textBlockQuestion.FontSize = 30;
        //    questionControlList[indexAfter].textBlockQuestion.FontSize = 30;
        //}

        //private void TimeInterval_ControlMouseLeave(int indexBefore, int indexAfter)
        //{
        //    questionControlList[indexBefore].textBlockQuestion.FontSize = 24;
        //    questionControlList[indexAfter].textBlockQuestion.FontSize = 24;
        //}

        private async void TimeInterval_ControlMouseDown(int controlIndex)
        {
            TimeIntervalControl clickedTimeIntervalControl = timeIntervalControlList[controlIndex];
            TimeIntervalControl validTimeIntervalControl = null;

            QuestionControl questionBefore = questionControlList[clickedTimeIntervalControl.IndexQuestionBefore];
            QuestionControl questionAfter = questionControlList[clickedTimeIntervalControl.IndexQuestionAfter];

            bool isAnswerValid;

            if (CurrentQuestion.Index > questionBefore.Question.Index
                && CurrentQuestion.Index < questionAfter.Question.Index)
            {
                clickedTimeIntervalControl.ExpandControl();

                await Task.Delay(500);

                InsertQuestion(controlIndex);

                clickedTimeIntervalControl.ShowAsNormal();

                isAnswerValid = true;
            }
            else
            {
                clickedTimeIntervalControl.ShowAsWrongAnswer();

                WrongAnswerLifeChange?.Invoke();

                await Task.Delay(1000);

                for (int i = 0; i < timeIntervalControlList.Count; i++)
                {
                    questionBefore = questionControlList[timeIntervalControlList[i].IndexQuestionBefore];
                    questionAfter = questionControlList[timeIntervalControlList[i].IndexQuestionAfter];

                    if (CurrentQuestion.Index > questionBefore.Question.Index
                        && CurrentQuestion.Index < questionAfter.Question.Index)
                    {
                        validTimeIntervalControl = timeIntervalControlList[i];
                        break;
                    }
                }

                if (clickedTimeIntervalControl.Index > validTimeIntervalControl.Index)
                {
                    questionBefore.BringIntoView();
                }
                else
                {
                    questionAfter.BringIntoView();
                }
                validTimeIntervalControl.ExpandControl();

                await Task.Delay(500);

                InsertQuestion(validTimeIntervalControl.Index);

                clickedTimeIntervalControl.ShowAsNormal();
                validTimeIntervalControl.ShowAsNormal();

                isAnswerValid = false;
            }

            await Task.Delay(500);

            CheckingAnswerResult?.Invoke(isAnswerValid);
        }

        private void InsertQuestion(int position)
        {
            TimeIntervalControl validControl = timeIntervalControlList[position];
            int rowIndex = Grid.GetRow(validControl);
            int index = timeIntervalControlList.IndexOf(validControl);

            for (int i = index + 1; i < timeIntervalControlList.Count; i++)
            {
                timeIntervalControlList[i].Index++;
                timeIntervalControlList[i].IndexQuestionBefore++;
                timeIntervalControlList[i].IndexQuestionAfter++;
                Grid.SetRow(timeIntervalControlList[i], Grid.GetRow(timeIntervalControlList[i]) + 2);
            }

            for (int i = validControl.IndexQuestionAfter; i < questionControlList.Count; i++)
            {
                Grid.SetRow(questionControlList[i], Grid.GetRow(questionControlList[i]) + 2);
            }

            TimeIntervalControl newTimeInterval = AddNewTimeInterval(rowIndex + 2, index + 1);
            AddNewQuestionLine(rowIndex + 1, validControl.IndexQuestionAfter, CurrentQuestion);

            newTimeInterval.IndexQuestionBefore = validControl.IndexQuestionAfter;
            newTimeInterval.IndexQuestionAfter = newTimeInterval.IndexQuestionBefore + 1;
        } 
    }
}
