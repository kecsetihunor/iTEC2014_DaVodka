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
    public class MessageManager : IMessageManager
    {
        private IUnitOfWork _unitOfWork;

        public MessageManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public MessageDTO GetMessage()
        {
            return _unitOfWork.MessageRepository.Read()
                .Select(x => new MessageDTO()
                {
                    Id = x.Id,
                    MessageBody = x.MessageBody
                }).FirstOrDefault();
        }

        public void SaveMessage(MessageDTO message)
        {
            var msg = _unitOfWork.MessageRepository.Read().FirstOrDefault();
            if (msg == null)
            {
                _unitOfWork.MessageRepository.Create(new Message()
                {
                    MessageBody = message.MessageBody
                });
            }
            else
            {
                message.MessageBody = message.MessageBody;
                _unitOfWork.MessageRepository.Update(msg);
            }
            _unitOfWork.SaveChanges();
        }
    }
}
