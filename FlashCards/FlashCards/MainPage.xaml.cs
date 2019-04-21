using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashCards
{
   
    public partial class MainPage : ContentPage
    {
        QuestionDB questiondb = App.Database;
        
        List<Question> questions = new List<Question>()
        {
            //new Question("How are you doing?", "You are okay."),
            //new Question("How many jumpers does the queen have", "Between 9 and 10.")
        };

    

        int currentQuestionIndex = 0;
        int correctAnswers = 0;
        int wrongAnswers = 0;
        int tries = 1;


        StackLayout parent;

        //bool isFirstWrongAnswer = false;
        Button correct;
        Button wrong;
        Label displayCorrect;
        Label displayWrong;
        Label triesToGetAllRight;

        public MainPage()
        {
            InitializeComponent();


            //questiondb.ClearQuestions();
            //write to dabase
            //If new instance run following
            //questiondb.SaveItemAsync(new Question("How are you doing?", "You are okay."));
            //questiondb.SaveItemAsync(new Question("How many jumpers does the queen have", "Between 9 and 10."));

            //read from database
            questions.Clear();
            var qList = questiondb.GetItemsNotDoneAsync();
            questions = qList.Result;

            questionLabel.BindingContext = questions[currentQuestionIndex];
           
        }

        public void DisplayAnswer(object sender, EventArgs e)
        {
            
           
            EraseStatsOnStateChange();

            if (questions[currentQuestionIndex].Answer == questionLabel.Text)
            {
                questionLabel.Text = questions[currentQuestionIndex].BaseQuestion;

                parent.Children.Remove(correct);
                parent.Children.Remove(wrong);

                /*if (currentQuestionIndex == questions.Count)
                {
                    currentQuestionIndex = 0;
                }
                else
                {
                    currentQuestionIndex++;
                }*/

            } else {
                questionLabel.Text = questions[currentQuestionIndex].Answer;

                correct = new Button { Text = "I got it right" };

                wrong = new Button { Text = "I got it wrong" };

                parent = layout;

                parent.Children.Add(correct);
                parent.Children.Add(wrong);

                correct.Clicked += CorrectAnswer;
                wrong.Clicked += WrongAnswer;

            }

        }

        public void CorrectAnswer(object sender, EventArgs e)
        {
            correctAnswers++;
            parent.Children.Remove(correct);
            parent.Children.Remove(wrong);

            if ((currentQuestionIndex + 1) == questions.Count)
            {
                currentQuestionIndex = 0;
                if(wrongAnswers > 0)
                {
                    tries++;
                }

                DisplayStatsOnListEnd();
            }
            else
            {
                currentQuestionIndex++;
            }

            questionLabel.Text = questions[currentQuestionIndex].BaseQuestion;
        }

        public void WrongAnswer(object sender, EventArgs e)
        {
            wrongAnswers++;
            parent.Children.Remove(correct);
            parent.Children.Remove(wrong);
            
            if ((currentQuestionIndex+1) == questions.Count)
            {
                currentQuestionIndex = 0;
                if (wrongAnswers > 0)
                {
                    tries++;
                }
                DisplayStatsOnListEnd();
            }
            else
            {
                currentQuestionIndex++;
            }

            questionLabel.Text = questions[currentQuestionIndex].BaseQuestion;
        }

        public void DisplayStatsOnListEnd()
        {
            displayCorrect = new Label
            {
                Text = "Correct Answers: " + correctAnswers
            };

            displayWrong = new Label
            {
                Text = "Wrong Answers: " + wrongAnswers
            };

            if (wrongAnswers == 0)
            {
                triesToGetAllRight = new Label
                {
                    Text = "You got all the cards right after " + tries + " tries."
                };

                
            }

            correctAnswers = 0;
            wrongAnswers = 0;
            parent.Children.Add(displayCorrect);
            parent.Children.Add(displayWrong);


            if (triesToGetAllRight != null)
            {
                parent.Children.Add(triesToGetAllRight);
                //tries = 0;
                tries = 1;
                
            }

           

        }
        
        public void EraseStatsOnStateChange()
        {
            try
            {
                parent.Children.Remove(displayCorrect);
                parent.Children.Remove(displayWrong);
                parent.Children.Remove(triesToGetAllRight);

                correctAnswers = 0;
                wrongAnswers = 0;
                triesToGetAllRight = null;
            } catch (Exception e)
            {

            }
        }
    }
}
