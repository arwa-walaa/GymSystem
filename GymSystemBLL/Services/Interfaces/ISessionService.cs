using GymSystemBLL.ViewModels.SessionsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionByID(int SessionId);

        bool CreateSession(CreateSessionViewModel createSessionViewModel);

        UpdateSessionViewModel? GetSessionToUpdate(int sessionId);
        bool UpdateSession(int sessionId, UpdateSessionViewModel updateSessionViewModel);
        bool RemoveSession(int sessionId);
    }
}
