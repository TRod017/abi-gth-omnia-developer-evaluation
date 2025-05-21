using Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetAllUsers
{
    /// <summary>
    /// Represents the request used to retrieve a paginated list of users.
    /// </summary>
    /// <remarks>
    /// Supports pagination parameters via query string.
    /// </remarks>
    public class GetAllUsersRequest : IRequest<PaginatedResponse<GetAllUsersResult>>
    {
        /// <summary>
        /// Gets or sets the current page number. Default value is 1.
        /// </summary>
        [FromQuery(Name = "_page")]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of items per page. Default value is 10.
        /// </summary>
        [FromQuery(Name = "_size")]
        public int Size { get; set; } = 10;
    }
}
