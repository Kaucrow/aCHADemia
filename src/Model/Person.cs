namespace aCHADemia.Model

{
    public class Person
    {
        public string CI { get; set; }
        public string? Name { get; set; }
        public int PersonTypeId { get; set; }

        // optional, show the type of person

        public string PersonType { get; set; }

        public Person() { }

        // constructor
        public Person(string ci, string name, int personTypeId, string personType = null)
        {
            CI = ci;
            Name = name;
            PersonTypeId = personTypeId;
            //optional
            PersonType = personType;
        }

        // show in ui
        public override string ToString() => $"{CI} - {Name}";

    }
}
