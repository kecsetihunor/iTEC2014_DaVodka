using iTEC.BL.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.Managers
{
    public interface IVoteManager
    {
        IEnumerable<VotedProductDTO> GetVotes(Int32 sessionId);
        VoteDTO GetVote(int id);
        void DeleteVote(int id);
        Int32 SaveVote(VoteDTO vote);
        Int32 Vote(String username, Int32 productId, Int32 points, Int32 sessionId);
        Int32 GetAllVotedPoints();
        Int32 GetAvailablePoints(String username, Int32 sessionId);
        Int32 GetAllPointsFromCategory(String username, Int32 categoryId, Int32 sessionId);
        Int32 GetAllPointsFromProduct(String username, Int32 categoryId, Int32 sessionId);
        IEnumerable<BudgetDTO> CalculateBudget(Int32 budget, Int32 sessionId);
    }
}
