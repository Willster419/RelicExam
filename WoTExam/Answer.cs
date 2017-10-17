using System.Windows.Forms;

namespace WoTExam
{
    public class Answer
    {
        public Answer() { }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public RadioButton AnswerButton;
    }
}
