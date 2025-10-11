using GymSystemBLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Interfaces
{
    internal interface IMemberService 
    {
     
       public IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel createMemberViewModel);

        MemberViewModel? GetMemberDetails(int id);

        //get healthrecord
        HealthViewModel? GetMemberRecordHealth(int MamberId);

        //get member id to update view 

        MamberToUpdateViewModel? GetMemberForUpdate(int memebrId);

        //apply update

        bool UpdateMemberDetails(int memberId, MamberToUpdateViewModel mamberToUpdateViewModel);

    }
}
