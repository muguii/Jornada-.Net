using DevGames.API.Entities;

namespace DevGames.API.Persistence
{
    public class DevGamesContext
    {
        public List<Board> Boards { get; private set; }

        public DevGamesContext()
        {
            Boards = new List<Board>();
        }
    }
}
