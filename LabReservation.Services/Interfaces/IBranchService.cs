using LabReservation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabReservation.Services.Interfaces
{

    public interface IBranchService
    {
        public Task<List<Branch>> getAllBranchs();

        public Task<List<Branch>> getAllBranchsQuerable(int? cityId, int? labId, int? areaId);
        public Task<List<Branch>> getAllBranchsWithHome();

        public Task<List<Branch>> getAllBranchsWithOutHome();
        public Task<Branch> addBranch(Branch branch);
        public Task<Branch> getBranchById(int id);
        public Task<Branch> updateBranch(Branch branch);
        public Task<bool> removeBranch(Branch branch);
    }

}
