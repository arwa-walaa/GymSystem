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
        int AddMember(MemberViewModel member);
        int UpdateMember(MemberViewModel member);
        int DeleteMember(int id);
        MemberViewModel? GetMemberById(int id);
        IEnumerable<MemberViewModel> GetAllMembers();
    }
}
