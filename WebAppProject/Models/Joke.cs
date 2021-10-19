namespace WebAppProject.Models
{
    public class Joke
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string JokeQuestion { get; set; }
        public string JokeAnswer { get; set; }

        public Joke()
        {

        }
    }
}