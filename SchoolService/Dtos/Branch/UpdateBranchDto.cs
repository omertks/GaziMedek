namespace SchoolService.Dtos.Branch
{
    public class UpdateBranchDto
    {
        public string BranchName { get; set; }

        public int DepartmentId { get; set; } // Bu olmayadabilir
    }
}
