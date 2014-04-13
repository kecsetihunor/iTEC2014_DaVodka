using iTEC.BL.Interfaces.DTOs;
using iTEC.BL.Interfaces.Managers;
using iTEC.ErrorHandling.Exceptions;
using iTEC.Model.Entities;
using iTEC.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Managers
{
    public class SessionManager : ISessionManager
    {
        private IUnitOfWork _unitOfWork;

        public SessionManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void StartSession()
        {
            var session = _unitOfWork.SessionRepository.Read().Where(x => x.EndDate == DateTime.MinValue).ToList().FirstOrDefault();
            session.EndDate = DateTime.Now;
            _unitOfWork.SessionRepository.Update(session);

            _unitOfWork.SessionRepository.Create(new Session()
            {
                StartDate = DateTime.Now
            });

            _unitOfWork.SaveChanges();
        }

        public Int32 GetActiveSessionId()
        {
            return _unitOfWork.SessionRepository.Read()
                .Where(x => x.EndDate == DateTime.MinValue)
                .ToList()
                .FirstOrDefault().Id;
        }
    }
}
