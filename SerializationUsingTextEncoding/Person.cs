namespace SerializationUsingTextEncoding
{
    public class Person
    {
        [SerializationInfo("name")]
        public string Name { get; set; }
        [SerializationInfo("age")]
        public int Age { get; set; }
    }
}
