namespace QuizService.Model
{
    public class Score
    {
        public int Id { get; set; }

        public int QuizId { get; set; }

        public decimal ScoresAmount { get; set; }
    }
}
