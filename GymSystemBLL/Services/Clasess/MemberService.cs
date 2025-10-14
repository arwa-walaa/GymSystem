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
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork )
        {
           _unitOfWork = unitOfWork;
        }
        //don't forget to register Iunitofwork in service in program.cs
        public bool CreateMember(CreateMemberViewModel createMemberViewModel)
        {
            //check if phone or email are uniqe
            try
            {
                if (IsEmailExist(createMemberViewModel.Email) || IsPhoneExist(createMemberViewModel.Phone))
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

                _unitOfWork.GetRepo<Member>().Add(member) ;
                return _unitOfWork.SaveChanges() > 0;
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

            var members = _unitOfWork.GetRepo<Member>().GetAll() ?? [];
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

        public MemberViewModel? GetMemberDetails(int id)
        {
           //Iplan repo

            var member = _unitOfWork.GetRepo<Member>().GetById(id);
            if (member is null) return null;
            var viewModel = new MemberViewModel()
            {
                Id = member.Id,
                Name = member.Name,
                Phone = member.Phone,
                Email = member.Email,
                Gender = member.Gender.ToString(),
                Photo = member.Photo,
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Address = $"{member.Address?.BuildingNumber} - {member.Address?.Street} - {member.Address?.City}",


            };  
            var activeMembership = _unitOfWork.GetRepo<Membership>()
                .GetAll(m => m.MemberId == id && m.Status=="Active")
                .FirstOrDefault();
            if (activeMembership is not null)
            {
                
                viewModel.MembershipStartDate = activeMembership.CreatedAt.ToShortDateString();
                viewModel.MembershipEndDate = activeMembership.EndDate.ToShortDateString();

                var plan = _unitOfWork.GetRepo<Plan>().GetById(activeMembership.PlanId);
                viewModel.PlanName = plan?.Name;
            }

            return viewModel;

        }

     

        public HealthViewModel? GetMemberRecordHealth(int MamberId)
        {
            var MemeberHealthRecord = _unitOfWork.GetRepo<HealthRecord>().GetById(MamberId);
            if (MemeberHealthRecord is null) return null;
            var healthViewModel = new HealthViewModel()
            {
                Weight = MemeberHealthRecord.Weight,
                Height = MemeberHealthRecord.Height,
                BloodType = MemeberHealthRecord.BloodType,
                Note = MemeberHealthRecord.Note,
            };
            return healthViewModel;


        }

        public bool UpdateMemberDetails(int memberId, MamberToUpdateViewModel mamberToUpdateViewModel)
        {

            try
            {
                var memberRepo= _unitOfWork.GetRepo<Member>();
                if (IsEmailExist(mamberToUpdateViewModel.Email) || IsPhoneExist(mamberToUpdateViewModel.Phone))
                {
                    return false;
                }
                var member = memberRepo.GetById(memberId);
                if (member is null) return false;

                member.Email = mamberToUpdateViewModel.Email;
                member.Phone = mamberToUpdateViewModel.Phone;
                member.Photo = mamberToUpdateViewModel.Photo;
                member.Address.BuildingNumber = mamberToUpdateViewModel.BuildingNumber;
                member.Address.Street = mamberToUpdateViewModel.Street;
                member.Address.City = mamberToUpdateViewModel.City;
                member.UpdatedAt = DateTime.Now;
                 memberRepo.Update(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public MamberToUpdateViewModel? GetMemberForUpdate(int memebrId)
        {
            var _memberRepo = _unitOfWork.GetRepo<Member>();
            var memeber = _memberRepo.GetById(memebrId);
            if(memeber is null) return null;
            return new MamberToUpdateViewModel
            {
                Name = memeber.Name,
                Photo = memeber.Photo,
                Email = memeber.Email,
                Phone = memeber.Phone,
                BuildingNumber = memeber.Address.BuildingNumber,
                Street = memeber.Address.Street,
                City = memeber.Address.City,
            };

        }

        public bool DeleteMember(int id)
        {
            var _memberRepo = _unitOfWork.GetRepo<Member>();
            var _memberSessionRepo = _unitOfWork.GetRepo<MemberSession>();
            var _membershipRepo = _unitOfWork.GetRepo<Membership>();
            var memeber = _memberRepo.GetById(id);
            if (memeber is null) return false;
            var HasActiveMemberSession = _memberSessionRepo.GetAll(m=>m.MemberId == id && m.Session.StratDate> DateTime.Now ).Any();
            if (HasActiveMemberSession) return false;
            var Membership = _membershipRepo.GetAll(m => m.MemberId == id);
            //remove

            try
            {
                if (Membership.Any())
                {
                    foreach (var item in Membership)
                    {
                        _membershipRepo.Delete(item);

                    }

                }
             
                 _memberRepo.Delete(memeber);
                return _unitOfWork.SaveChanges() > 0;


            }
            catch (Exception)
            {

                return false;
            }


        }

        #region Helper

        private bool IsEmailExist(string email)
        {
            var _memberRepo = _unitOfWork.GetRepo<Member>();
            return _memberRepo.GetAll(m => m.Email == email).Any();
        }
        private bool IsPhoneExist(string phone)
        {
            var _memberRepo = _unitOfWork.GetRepo<Member>();
            return _memberRepo.GetAll(m => m.Phone == phone).Any();
        }

    

        #endregion
    }
}
