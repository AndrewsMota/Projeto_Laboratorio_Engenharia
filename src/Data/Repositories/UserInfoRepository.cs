using Business.Interfaces;
using Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserInfoRepository : Repository<UserInfo>, IUserInfoRepository
    {

        public UserInfoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<UserInfo> ObterUserInfoPorUserId(string userId)
        {
            return await Database.UsersInfo.AsNoTracking()
                .FirstOrDefaultAsync(UserInfo => UserInfo.UserId == userId);
        }
    }
}
