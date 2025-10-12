using GymSystemBLL.Services.Interfaces;
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
    internal class TrainerService : Interfaces.ITrainerService
    {
        private readonly IGenericRepo<Trainer> _TrainerRepo;
    
        private readonly IPlanRepo _planRepo;
        private readonly IGenericRepo<HealthRecord> _healthRecordRepo;
      

        public TrainerService( IGenericRepo<Trainer>  TrainerRepo)
        {
            _TrainerRepo = TrainerRepo;
        }

        public bool CreateTrainer(CreateTrainerViewModel createTrainerViewModel)
        {
            //check if phone or email are uniqe
            try
            {
                if (IsEmailExist(createTrainerViewModel.Email) || IsPhoneExist(createTrainerViewModel.Phone))
                {
                    return false;
                }

                var Trainer = new Trainer()
                {
                    Name = createTrainerViewModel.Name,
                    Email = createTrainerViewModel.Email,
                    Phone = createTrainerViewModel.Phone,
                    DateOfBirth = createTrainerViewModel.DateOfBirth,
                    Gender = createTrainerViewModel.Gender,
                    Specialites = createTrainerViewModel.Specialites,
                    Address = new Address()
                    {
                        BuildingNumber = createTrainerViewModel.BuildingNumber,
                        Street = createTrainerViewModel.Street,
                        City = createTrainerViewModel.City,
                    }
                   

                };

               return _TrainerRepo.Add(Trainer) >0;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {

            var Trainers = _TrainerRepo.GetAll() ?? [];
            if (Trainers is null || Trainers.Any())
            {
                return [];
            }
            var TrainerViewModels = Trainers.Select(Trainer => new TrainerViewModel
            {
                Id = Trainer.Id,
                Name = Trainer.Name,
                Phone = Trainer.Phone,
                Email = Trainer.Email,
                Specialites = Trainer.Specialites.ToString(),
              

            });
            return TrainerViewModels;
        }

        public TrainerViewModel? GetTrainerDetails(int id)
        {


            var Trainer = _TrainerRepo.GetById(id);
            if (Trainer is null) return null;
            var viewModel = new TrainerViewModel()
            {
                Id = Trainer.Id,
                Name = Trainer.Name,
                Phone = Trainer.Phone,
                Email = Trainer.Email,
                Specialites = Trainer.Specialites.ToString(),
                DateOfBirth = Trainer.DateOfBirth.ToShortDateString(),
                Address = $"{Trainer.Address?.BuildingNumber} - {Trainer.Address?.Street} - {Trainer.Address?.City}",


            };  
 
            return viewModel;

        }

       
        TrainerToUpdateViewModel? ITrainerService.GetTrainerForUpdate(int TrainerId)
        {
            var trainer = _TrainerRepo.GetById(TrainerId);
            if(trainer is null) return null;
            return new TrainerToUpdateViewModel
            {
                Name = trainer.Name,
                Specialites = trainer.Specialites,
                Email = trainer.Email,
                Phone = trainer.Phone,
                BuildingNumber = trainer.Address.BuildingNumber,
                Street = trainer.Address.Street,
                City = trainer.Address.City,
            };

        }
        public bool UpdateTrainerDetails(int TrainerId, TrainerToUpdateViewModel TrainerToUpdateViewModel)
        {

            try
            {
                if (IsEmailExist(TrainerToUpdateViewModel.Email) || IsPhoneExist(TrainerToUpdateViewModel.Phone))
                {
                    return false;
                }
                var Trainer = _TrainerRepo.GetById(TrainerId);
                if (Trainer is null) return false;

                Trainer.Email = TrainerToUpdateViewModel.Email;
                Trainer.Phone = TrainerToUpdateViewModel.Phone;
                Trainer.Specialites = TrainerToUpdateViewModel.Specialites;
                Trainer.Address.BuildingNumber = TrainerToUpdateViewModel.BuildingNumber;
                Trainer.Address.Street = TrainerToUpdateViewModel.Street;
                Trainer.Address.City = TrainerToUpdateViewModel.City;
                Trainer.UpdatedAt = DateTime.Now;
                return _TrainerRepo.Update(Trainer) > 0;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool DeleteTrainer(int id)
        {
            var trainer = _TrainerRepo.GetById(id);
            if (trainer is null) return false;
            bool hasFutureSessions = trainer.TrainerSessions
            .Any(s => s.StratDate >= DateTime.Now);

            if (hasFutureSessions)
                return false;


            return _TrainerRepo.Delete(id) > 0;

        }

        #region Helper

        private bool IsEmailExist(string email)
        {
            return _TrainerRepo.GetAll(m => m.Email == email).Any();
        }
        private bool IsPhoneExist(string phone)
        {
            return _TrainerRepo.GetAll(m => m.Phone == phone).Any();
        }
        #endregion
    }
}
