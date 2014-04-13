using iTEC.Services.AccessLayer.DelightService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Services.AccessLayer
{
    public interface IDelightServices
    {
        #region AccountServices
        IEnumerable<AccountAvatarDTO> GetAccounts();
        AccountAvatarDTO GetUserStateInfo(Int32 id);
        Int32 Authenticate(String username, String password, Boolean remember);
        void RegisterUser(String username, String password, String email);
        void RegisterOwner(String username, String password, String email);
        Int32 GetAccountId(String username);
        #endregion

        #region Category
        IEnumerable<CategoryDTO> GetCategories();
        CategoryDTO GetCategory(int id);
        IEnumerable<VotedProductDTO> GetProductsFromCategory(String username, int id);
        IEnumerable<VotedCategoryDTO> GetVotedCategories(String username);
        void DeleteCategory(int id);
        void AddProductToCategory(int categoryId, int productId);
        void RemoveProductFromCategory(int productId);
        Int32 SaveCategory(CategoryDTO category);
        #endregion

        #region Product
        IEnumerable<ProductDTO> GetProducts();
        PricedProductDTO GetProduct(int id);
        IEnumerable<ProductFromCategoryDTO> GetAllProductsCompareToSpecificCategory(int categoryId);
        void DeleteProduct(int id);
        Int32 SaveProduct(PricedProductDTO product);
        #endregion

        #region UserGroups
        IEnumerable<UserGroupDTO> GetUserGroups();
        UserGroupDTO GetUserGroup(int id);
        IEnumerable<UserDTO> GetUsersFromGroup(int id);
        void AddUserToGroup(int userGroupId, int userId);
        void RemoveUserFromGroup(int userId);
        void DeleteUserGroup(int id);
        Int32 SaveUserGroup(UserGroupDTO userGroup);
        #endregion

        #region User
        IEnumerable<UserDTO> GetUsers();
        UserDTO GetUser(Int32 id);
        IEnumerable<UserFromGroupDTO> GetAllUsersCompareToSpecificGroup(int id);
        void DeleteUser(Int32 id);
        Int32 SaveUser(UserDTO user);
        #endregion

        #region Votes
        IEnumerable<VotedProductDTO> GetVotes();
        VoteDTO GetVote(int id);
        void DeleteVote(int id);
        Int32 SaveVote(VoteDTO vote);
        Int32 Vote(String username, Int32 productId, Int32 points);
        Int32 GetAllVotedPoints();
        Int32 GetAvailablePoints(String username);
        IEnumerable<BudgetDTO> GetBudget(Int32 budget);
        #endregion

        void StartSession();

        #region Report
        ProductVoteReportChart GetProductVoteReport();
        #endregion

        #region Message
        MessageDTO GetMessage();
        void SaveMessage(MessageDTO message);
        #endregion
    }
}
