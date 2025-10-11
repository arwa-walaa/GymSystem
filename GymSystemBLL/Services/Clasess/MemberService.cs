using GymSystemBLL.ViewModels;
using GymSystemDAL.Entities;
using GymSystemDAL.Repositroies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Clasess
{
    internal class MemberService : Interfaces.IMemberService
    {
        private readonly IGenericRepo<Member> _memberRepo;

        public MemberService( IGenericRepo<Member>  memberRepo)
        {
            _memberRepo = memberRepo;
        }
        public int AddMember(MemberViewModel member)
        {
            throw new NotImplementedException();
        }

        public int DeleteMember(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {

            //first way of mapping 
            //    var members = _memberRepo.GetAll() ?? [];
            //    if (members is null || members.Any())
            //    {
            //        return [];
            //    }
            //    var memberViewModels = new List<MemberViewModel>();
            //    foreach (var member in members)
            //    {
            //        var memberViewModel = new MemberViewModel()
            //        {
            //            Id = member.Id,
            //            Name = member.Name,
            //            Phone = member.Phone,
            //            Email = member.Email,
            //            Gender= member.Gender.ToString(),
            //        };
            //        memberViewModels.Add(memberViewModel);
            //    }
            //    return memberViewModels;

            var members = _memberRepo.GetAll() ?? [];
            if (members is null || members.Any())
            {
                return [];
            }
            var memberViewModels = members.Select(member => new MemberViewModel
            {
                Id = member.Id,
                Name = member.Name,
                Phone = member.Phone,
                Email = member.Email,
                Gender = member.Gender.ToString(),
                Photo = member.Photo

            });
            return memberViewModels;
        }

        public MemberViewModel? GetMemberById(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateMember(MemberViewModel member)
        {
            throw new NotImplementedException();
        }
    }
}
