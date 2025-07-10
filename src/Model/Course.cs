namespace aCHADemia.Model
{
    public class Course
    {
        public int SubjectId { get; set; }
        public string? Name { get; set; }
        public override string ToString() => Name ?? "";
    }
}
