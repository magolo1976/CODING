namespace ConsoleApp.StudyCase.Models
{
    public class StudyData(double studyHours, double sleepingHours, int expected)
    {
        public double StudyHours => studyHours;
        public double SleepingHours => sleepingHours;
        public int Expected => expected;
    }
}
