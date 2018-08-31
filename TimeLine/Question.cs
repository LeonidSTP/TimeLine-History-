using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TimeLine
{
    public class Question
    {
        public string Name { get; set; }
        public int Index { get; set; }

        public static List<Question> ReadQuestions()
        {
            int i = 0;
            List<Question> Qst = new List<Question>();
            string[] readText = File.ReadAllLines("QuestionBefore.txt");
            int size = readText.Count();
            foreach (string s in readText)
            {
                if (!string.IsNullOrWhiteSpace(s))
                {
                    Qst.Add(new Question() { Name = s, Index = i - size });
                    i++;
                }
            }

            i = 1;
            readText = File.ReadAllLines("QuestionAfter.txt");
            foreach (string s in readText)
            {
                if (!string.IsNullOrWhiteSpace(s))
                {
                    Qst.Add(new Question() { Name = s, Index = i });
                    i++;
                }
            }

            return Qst;
        }
    }
}
