namespace PhotoExhibiter.Models.Entities
{
    public class Genre
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        private Genre () {}

        private Genre (int genreId, string name)
        {
            Id = genreId;
            Name = name;
        }

        public static Genre Create (int genreId, string name)
        {
            return new Genre (genreId, name);
        }
    }
}
