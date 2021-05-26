using QuizPrototype.Domain.Entities;
using QuizPrototype.Domain.Interfaces;
using QuizPrototype.Infrastructure.Data.Context;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace QuizPrototype.Infrastructure.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly QuizPrototypeDbContext context;

        public GameRepository(QuizPrototypeDbContext context)
        {
            this.context = context;
        }

        public async Task CreateGame(Game game)
        {
            await context.Game.AddAsync(game);
            await context.SaveChangesAsync();
        }

        public async Task<List<Game>> GetAll()
        {
            return await context.Game.OrderByDescending(x => x.Score).ToListAsync();
        }

        public async Task<Game> GetByGuid(string guid)
        {
            return await context.Game.Where(g => g.Guid.Equals(guid)).SingleOrDefaultAsync();
        }

        public async Task UpdateGame(Game game)
        {
            context.Update(game);
            await context.SaveChangesAsync();
        }
    }
}
