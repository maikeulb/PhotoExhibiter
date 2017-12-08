namespace PhotoExhibiter.Models.Entities
{
    public class Genre
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        private Genre () { }

        private Genre (string name)
        {
            Name = name;
        }

        public static Genre Create ( string name) => new Genre ( name);
    }
}
