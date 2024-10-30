namespace OnlineLearningPlatform.Models
{
    public class StudentProgress
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
