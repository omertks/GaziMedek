using SchoolService.Dtos.Branch;
using SchoolService.Models;

namespace SchoolService.Services.Interfaces
{
    public interface IBranchService
    {
        public Task<List<Branch>> GetAllBranchByDepartment(int id);

        public Task<Branch> GetBranchById(int id);

        public Task CreateBranch(CreateBranchDto createBranchDto);

        public Task UpdateBranch(UpdateBranchDto updateBranchDto, int id);

        public Task DeleteBranch(int id);

    }
}
