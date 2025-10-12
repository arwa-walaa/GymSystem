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
     
       public IEnumerable<TrainerViewModel> GetAllTrainers();

        bool CreateTrainer(CreateTrainerViewModel createTrainerViewModel);

        TrainerViewModel? GetTrainerDetails(int id);

        //get healthrecord
       

        //get Trainer id to update view 

        TrainerToUpdateViewModel? GetTrainerForUpdate(int memebrId);

        //apply update

        bool UpdateTrainerDetails(int TrainerId, TrainerToUpdateViewModel TrainerToUpdateViewModel);

        bool DeleteTrainer(int id);

    }
}
