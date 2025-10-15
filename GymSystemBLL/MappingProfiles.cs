using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            // Create your mapping configurations here
            // Example:
            // CreateMap<Source, Destination>();
            // CreateMap<PlanViewModel, Plan>();
            // CreateMap<CreateMemberViewModel, Member>();
            CreateMap<GymSystemDAL.Entities.Session, GymSystemBLL.ViewModels.SessionsViewModel.SessionViewModel>()
                .ForMember(dest => dest.CategoryName, 
                opt => opt.MapFrom(src => src.SessionCategory.CategoryName))
                 .ForMember(dest => dest.TrainerName,
                opt => opt.MapFrom(src => src.SessionTrainer.Name))
                .ForMember(dest => dest.AvailableSlot,
                opt => opt.Ignore());

            CreateMap<GymSystemBLL.ViewModels.SessionsViewModel.CreateSessionViewModel, GymSystemDAL.Entities.Session>();

        }
    }
}
