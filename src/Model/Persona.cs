namespace aCHADemia.Model
{
    public class Persona
    {
        public int Id { get; set; } // 1 - professor. 2 - student. could delete this
        public string? IdNumber { get; set; } // Cedula

        public string? Name { get; set; } 

        public override string ToString() => Name ?? "";
    }
}
