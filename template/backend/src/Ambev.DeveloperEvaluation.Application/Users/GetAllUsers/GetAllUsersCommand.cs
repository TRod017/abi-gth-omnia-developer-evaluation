using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers
{
    /// <summary>
    /// Command to retrieve a paginated list of users.
    /// </summary>
    /// <remarks>
    /// This command carries pagination parameters to retrieve users in pages.
    /// Returns a <see cref="PaginatedList{GetAllUsersResult}"/> upon execution.
    /// </remarks>
    public class GetAllUsersCommand : IRequest<PaginatedList<GetAllUsersResult>>
    {
        /// <summary>
        /// Gets or sets the current page number. Default is 1.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of items per page. Default is 10.
        /// </summary>
        public int Size { get; set; } = 10;
    }
}
