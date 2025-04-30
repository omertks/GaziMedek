using SchoolService.Dtos.Branch;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;
using SchoolService.Services.Interfaces;

namespace SchoolService.Services
{
    public class BranchService : IBranchService
    {

        private readonly IBranchRepository _BranchRepository;


        public BranchService(IBranchRepository BranchRepository)
        {
            _BranchRepository = BranchRepository;
        }



        public async Task CreateBranch(CreateBranchDto createBranchDto)
        {

            Branch Branch = new Branch()
            {
                BranchName = createBranchDto.BranchName,
                DepartmentId = createBranchDto.DepartmentId
            };

            await _BranchRepository.CreateAsync(Branch);
        }

        public async Task DeleteBranch(int id)
        {
            await _BranchRepository.DeleteAsync(id);
        }

        public async Task<List<Branch>> GetAllBranchByDepartment(int id)
        {
            var Branchs = await _BranchRepository.GetFilteredListAsync(d => d.DepartmentId == id);

            return Branchs;
        }

        public async Task<Branch> GetBranchById(int id)
        {
            return await _BranchRepository.GetByIdAsync(id);
        }

        public async Task UpdateBranch(UpdateBranchDto updateBranchDto, int id)
        {
            var Branch = await _BranchRepository.GetByIdAsync(id);

            Branch.BranchName = updateBranchDto.BranchName;

            await _BranchRepository.UpdateAsync(Branch);
        }
    }
}
