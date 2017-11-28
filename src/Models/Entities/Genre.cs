namespace PhotoExhibiter.Models.Entities
{
    public class Genre //Value Object?
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        private Genre () { }

        private Genre (int genreId, string name)
        {
            Id = genreId;
            Name = name;
        }

        public static Genre Create (int genreId, string name) => new Genre (genreId, name);
    }
}