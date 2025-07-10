namespace aCHADemia.Model
{
    public class Subject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

        public override string ToString() => Name ?? "";
    }
}
