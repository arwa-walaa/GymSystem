using GymSystemBLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Interfaces
{
    internal interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel createTrainer);
        TrainerViewModel? GetTrainerDetails(int trainerId);
        TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId);
        bool UpdateTrainerDetails(TrainerToUpdateViewModel updatedTrainer, int trainerId);
        bool RemoveTrainer(int trainerId);

    }
}
