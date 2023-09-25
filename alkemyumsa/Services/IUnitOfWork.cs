using alkemyumsa.DataAccess.Repositories.Interfaces;
using alkemyumsa.DataAccess.Repositories;

namespace alkemyumsa.Services
{
    public interface IUnitOfWork
    {
        public UserRepository UserRepository { get; }
        public ProjectRepository ProjectRepository { get; }
        public WorkRepository WorkRepository { get; }
        public ServiceRepository ServiceRepository { get; }
        Task<int> Complete();
    }
}
