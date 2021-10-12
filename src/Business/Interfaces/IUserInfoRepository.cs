using Business.Models;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserInfoRepository : IRepository<UserInfo>
    {
        public Task<UserInfo> ObterUserInfoPorUserId(string userId);
    }
}
