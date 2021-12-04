using DevGames.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevGames.API.Persistence.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DevGamesContext context;

        public PostRepository(DevGamesContext context)
        {
            this.context = context;
        }

        public void Add(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
        }

        public void AddComment(Comment comment)
        {
            context.Comments.Add(comment);
            context.SaveChanges();
        }

        public IEnumerable<Post> GetAllByBoard(int boardId)
        {
            return context.Posts.Where(post => post.BoardId == boardId);
        }

        public Post GetById(int id)
        {
            return context.Posts.Include(post => post.Comments).SingleOrDefault(post => post.Id == id);
        }

        public bool PostExists(int id)
        {
            return context.Posts.Any(post => post.Id == id);
        }
    }
}
