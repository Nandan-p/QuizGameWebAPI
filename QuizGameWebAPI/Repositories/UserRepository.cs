using Microsoft.EntityFrameworkCore;
using QuizGameWebAPI.Data;
using QuizGameWebAPI.Models;

namespace QuizGameWebAPI.Repositories
{
    public class UserRepository
    {
        private readonly QuizDbContext _context;

        public UserRepository(QuizDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync() => await _context.Users.AsNoTracking().ToListAsync();
        public async Task<IEnumerable<UserQuizModel>> GetAllUserQuizAsync() => await _context.UserQuiz.ToListAsync();
        public async Task<UserModel> GetByIdAsync(long id) => await _context.Users.AsNoTracking().FirstOrDefaultAsync(f => f.UserId == id);
        public async Task<UserQuizModel> GetUserQuizByIdAsync(long id) => await _context.UserQuiz.FindAsync(id);
        public async Task<UserModel> AddAsync(UserModel user) 
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<UserQuizModel> AddUserQuizAsync(UserQuizModel userQuiz) 
        {
            await _context.UserQuiz.AddAsync(userQuiz); 
            await _context.SaveChangesAsync();
            return userQuiz; 
        }
        public async Task<UserModel> UpdateAsync(UserModel user) 
        {
            if (user != null) 
                _context.Users.Update(user); await _context.SaveChangesAsync();
            return user;
        }
    }

}
