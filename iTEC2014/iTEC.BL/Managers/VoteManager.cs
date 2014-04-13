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
    public class VoteManager : IVoteManager
    {
        private IUnitOfWork _unitOfWork;

        public VoteManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<VotedProductDTO> GetVotes(Int32 sessionId)
        {
            var result = new List<VotedProductDTO>();
            var query = _unitOfWork.VoteRepository.Read()
                .Where(x => x.Session.Id == sessionId)
                .GroupBy(x => x.Product).ToList();
            foreach (var item in query)
            {
                result.Add(new VotedProductDTO()
                {
                    Name = item.Key.Name,
                    Points = item.Select(x => x.Points).Sum(),
                    Price = item.Select(x => x.Product.Price).Sum()
                });
            }

            return result;
        }

        public VoteDTO GetVote(Int32 id)
        {
            return _unitOfWork.VoteRepository.Read()
                .Where(x => x.Id == id).Select(x => new VoteDTO
                {
                    Id = x.Id
                }).FirstOrDefault();
        }

        public void DeleteVote(Int32 id)
        {
            var vote = _unitOfWork.VoteRepository.Read().FirstOrDefault(x => x.Id == id);
            _unitOfWork.VoteRepository.Delete(vote);
            _unitOfWork.SaveChanges();
        }

        public Int32 SaveVote(VoteDTO vote)
        {
            Int32 id;
            if (vote.Id == 0)
            {
                id = _unitOfWork.VoteRepository.Create(new Vote()
                {
                    Points = vote.Points,
                    Account = new Account()
                    {
                        Id = vote.Account.Id
                    },
                    Product = new Product()
                    {
                        Id = vote.Product.Id
                    }
                }).Id;
            }
            else
            {
                var vte = _unitOfWork.VoteRepository.Read().Where(x => x.Id == vote.Id).FirstOrDefault();
                vte.Points = vote.Points;
                vte.Account.Id = vote.Account.Id;
                vte.Product.Id = vote.Product.Id;
                id = vte.Id;

                _unitOfWork.VoteRepository.Update(vte);
            }
            _unitOfWork.SaveChanges();
            return id;
        }

        public Int32 Vote(String username, Int32 productId, Int32 points, Int32 sessionId)
        {
            var account = _unitOfWork.AccountRepository.Read().Where(x => x.Username == username).FirstOrDefault();
            if (account == null)
            {
                throw new Exception("User not found");
            }

            var product = _unitOfWork.ProductRepository.Read().Where(x => x.Id == productId).FirstOrDefault();
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var session = _unitOfWork.SessionRepository.Read().Where(x => x.Id == sessionId).FirstOrDefault();
            if (session == null)
            {
                throw new Exception("Sesssion not found");
            }

            var vote = _unitOfWork.VoteRepository.Read()
                .Where(x => x.Account.Id == account.Id && x.Product.Id == product.Id && x.Session.Id == sessionId)
                .FirstOrDefault();

            if (vote == null)
            {
                _unitOfWork.VoteRepository.Create(new Vote()
                {
                    Account = account,
                    Product = product,
                    Points = points,
                    Session = session
                });
            }
            else
            {
                vote.Points = points;
                _unitOfWork.VoteRepository.Update(vote);
            }

            _unitOfWork.SaveChanges();

            return GetAvailablePoints(username, sessionId);
        }
     
        public Int32 GetAllVotedPoints()
        {
            return _unitOfWork.VoteRepository.Read().Sum(x => x.Points);
        }
        
        public Int32 GetAvailablePoints(String username, Int32 sessionId)
        {
            var account = _unitOfWork.AccountRepository.Read().Where(x => x.Username == username).FirstOrDefault();
            if (account == null)
            {
                throw new Exception("User not found!");
            }

            int votedPoints = _unitOfWork.VoteRepository.Read()
                .Where(x => x.Account.Id == account.Id && x.Session.Id == sessionId)
                .ToList()
                .Sum(x => x.Points);
            int groupPoints = account.UserGroup.Points;

            return groupPoints - votedPoints;
        }

        public Int32 GetAllPointsFromCategory(String username, Int32 categoryId, Int32 sessionId)
        {
            return _unitOfWork.VoteRepository.Read().Where(x => x.Account.Username.Equals(username) && x.Product.Category.Id == categoryId && x.Session.Id == sessionId)
                .ToList()
                .Sum(x => x.Points);
        }

        public Int32 GetAllPointsFromProduct(String username, Int32 productId, Int32 sessionId)
        {
            return _unitOfWork.VoteRepository.Read().Where(x => x.Account.Username.Equals(username) && x.Product.Id == productId && x.Session.Id == sessionId)
                .ToList()
                .Sum(x => x.Points);
        }

        public IEnumerable<BudgetDTO> CalculateBudget(Int32 budget, Int32 sessionId)
        {
            var result = new List<BudgetDTO>();
            var list = _unitOfWork.VoteRepository.Read().Where(x => x.Session.Id == sessionId).GroupBy(x => x.Product).ToList();
            foreach (var item in list)
            {
                result.Add(new BudgetDTO() { Name = item.Key.Name, Points = item.Select(x => x.Points).Sum() });
            }

            var total = result.Select(x => x.Points).Sum();

            foreach (var item in result)
            {
                item.Percentage = (double)((double)item.Points / total);
                item.Value = (double)(budget * (double)item.Percentage);
            }

            return result;
        }
    }
}
