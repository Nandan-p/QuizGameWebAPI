using Microsoft.EntityFrameworkCore;
using QuizGameWebAPI.Data;
using QuizGameWebAPI.Models;

namespace QuizGameWebAPI.Repositories
{
    public class QuestionRepository
    {
        private readonly QuizDbContext _context;

        public QuestionRepository(QuizDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuestionModel>> GetAllAsync() => await _context.Questions.ToListAsync();
        public async Task<IEnumerable<QuestionTypeModel>> GetAllQustionTypeAsync() => await _context.QuestionTypes.ToListAsync();
        public async Task<QuestionModel> GetByIdAsync(long id) => await _context.Questions.AsNoTracking().FirstOrDefaultAsync(w => w.QuestionId == id);
        public async Task<QuestionTypeModel> GetTypeByIdAsync(int id) => await _context.QuestionTypes.FindAsync(id);
        public async Task<long> AddAsync(QuestionModel question) 
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
            return question.QuestionId;
        }
        public async Task<long?> UpdateAsync(QuestionModel question) 
        {
            if (question != null)
                _context.Questions.Update(question); await _context.SaveChangesAsync();

            return question?.QuestionId ?? 0;
        }
    }

}
