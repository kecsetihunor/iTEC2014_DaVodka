using iTEC.BL.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace iTEC.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDelightService" in both code and config file together.
    [ServiceContract]
    public interface IDelightService
    {
        #region AccountContracts
        [OperationContract]
        IEnumerable<AccountAvatarDTO> GetAccounts();

        [OperationContract]
        AccountAvatarDTO GetUserStateInfo(Int32 id);

        [OperationContract]
        Int32 Authenticate(String username, String password, Boolean remember);

        [OperationContract]
        void RegisterUser(String username, String password, String email);

        [OperationContract]
        void RegisterOwner(String username, String password, String email);

        [OperationContract]
        Int32 GetAccountId(String username);
        #endregion

        #region CategoryContracts
        [OperationContract]
        IEnumerable<CategoryDTO> GetCategories();

        [OperationContract]
        CategoryDTO GetCategory(int id);

        [OperationContract]
        IEnumerable<VotedCategoryDTO> GetVotedCategories(String username);

        [OperationContract]
        IEnumerable<VotedProductDTO> GetProductsFromCategory(String username, int id);

        [OperationContract]
        void AddProductToCategory(int categoryId, int productId);
        
        [OperationContract]
        void RemoveProductFromCategory(int productId);

        [OperationContract]
        void DeleteCategory(int id);

        [OperationContract]
        Int32 SaveCategory(CategoryDTO userGroup);
        #endregion

        #region ProductContracts
        [OperationContract]
        IEnumerable<PricedProductDTO> GetProducts();

        [OperationContract]
        PricedProductDTO GetProduct(int id);

        [OperationContract]
        IEnumerable<ProductFromCategoryDTO> GetAllProductsCompareToSpecificCategory(int categoryId);

        [OperationContract]
        void DeleteProduct(int id);

        [OperationContract]
        Int32 SaveProduct(PricedProductDTO userGroup);
        #endregion

        #region UserGroupsContract
        [OperationContract]
        IEnumerable<UserGroupDTO> GetUserGroups();

        [OperationContract]
        UserGroupDTO GetUserGroup(int id);

        [OperationContract]
        IEnumerable<UserDTO> GetUsersFromGroup(int id);
        
        [OperationContract]
        void AddUserToGroup(int userGroupId, int userId);

        [OperationContract]
        void RemoveUserFromGroup(int userId);

        [OperationContract]
        void DeleteUserGroup(int id);

        [OperationContract]
        Int32 SaveUserGroup(UserGroupDTO userGroup);
        #endregion

        #region User
        [OperationContract]
        IEnumerable<UserDTO> GetUsers();

        [OperationContract]
        UserDTO GetUser(Int32 id);

        [OperationContract]
        IEnumerable<UserFromGroupDTO> GetAllUsersCompareToSpecificGroup(int id);

        [OperationContract]
        void DeleteUser(Int32 id);

        [OperationContract]
        Int32 SaveUser(UserDTO user); 
        #endregion

        #region VotesContract
        [OperationContract]
        IEnumerable<VotedProductDTO> GetVotes();

        [OperationContract]
        VoteDTO GetVote(int id);

        [OperationContract]
        void DeleteVote(int id);

        [OperationContract]
        Int32 SaveVote(VoteDTO userGroup);

        [OperationContract]
        Int32 Vote(String username, Int32 productId, Int32 points);

        [OperationContract]
        Int32 GetAllVotedPoints();

        [OperationContract]
        Int32 GetAvailablePoints(String username);

        [OperationContract]
        IEnumerable<BudgetDTO> GetBudget(Int32 budget);
        #endregion

        #region ReportContract
        [OperationContract]
        ProductVoteReportChart GetProductVoteReport();
        #endregion

        [OperationContract]
        void StartSession();

        #region MessageContract
        [OperationContract]
        MessageDTO GetMessage();
        [OperationContract]
        void SaveMessage(MessageDTO message);
        #endregion
    }
}
