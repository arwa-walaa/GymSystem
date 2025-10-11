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

        public bool CreateMember(CreateMemberViewModel createMemberViewModel)
        {
            //check if phone or email are uniqe
            try
            {
                var EmailExist = _memberRepo.GetAll(m => m.Email == createMemberViewModel.Email).Any();
                var PhoneExist = _memberRepo.GetAll(m => m.Phone == createMemberViewModel.Phone).Any();
                if (EmailExist || PhoneExist)
                {
                    return false;
                }

                var member = new Member()
                {
                    Name = createMemberViewModel.Name,
                    Email = createMemberViewModel.Email,
                    Phone = createMemberViewModel.Phone,
                    DateOfBirth = createMemberViewModel.DateOfBirth,
                    Gender = createMemberViewModel.Gender,
                    Address = new Address()
                    {
                        BuildingNumber = createMemberViewModel.BuildingNumber,
                        Street = createMemberViewModel.Street,
                        City = createMemberViewModel.City,
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Weight = createMemberViewModel.HealthViewModel.Weight,
                        Height = createMemberViewModel.HealthViewModel.Height,
                        BloodType = createMemberViewModel.HealthViewModel.BloodType,
                        Note = createMemberViewModel.HealthViewModel.Note,

                    }

                };

               return _memberRepo.Add(member) >0;
            }
            catch (Exception)
            {

                return false;
            }
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

     
       
    }
}
