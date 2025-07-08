namespace aCHADemia.Model
{
    public class Section
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public override string ToString() => Name ?? "";
    }
}
