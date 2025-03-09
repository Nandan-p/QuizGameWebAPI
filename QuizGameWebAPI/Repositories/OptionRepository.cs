using Microsoft.EntityFrameworkCore;
using QuizGameWebAPI.Data;
using QuizGameWebAPI.Models;
using QuizGameWebAPI.Models.ResponseModels;

namespace QuizGameWebAPI.Repositories
{
    public class OptionRepository
    {
        private readonly QuizDbContext _context;

        public OptionRepository(QuizDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OptionModel>> GetAllAsync() => await _context.Options.ToListAsync();
        public async Task<OptionModel> GetByIdAsync(long id) => await _context.Options.FindAsync(id);
        public async Task<IEnumerable<OptionModel>> GetByQuestioIdAsync(long id) 
        {
            var getAll = await GetAllAsync();
            if (getAll != null) 
                return getAll.Where(w => w.QuestionId == id).ToList();

            return default;
        }
        public async Task AddRangeAsync(List<OptionModel> option) 
        {
            await _context.Options.AddRangeAsync(option);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateRangeAsync(List<OptionModel> option) 
        {
            _context.Options.UpdateRange(option);
            await _context.SaveChangesAsync();
        }
    }

}
