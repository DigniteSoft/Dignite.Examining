using System.Security.Principal;
using System.Threading.Tasks;
using Dignite.Examining.Exams;
using Dignite.Examining.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Volo.Abp.Authorization.Permissions;

namespace Dignite.Examining.Authorization
{
    public class AnswerPaperAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, AnswerPaper>
    {
        private readonly IPermissionChecker _permissionChecker;

        public AnswerPaperAuthorizationHandler(IPermissionChecker permissionChecker
            )
        {
            _permissionChecker = permissionChecker;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            AnswerPaper resource)
        {
            if (requirement.Name == CommonOperations.Create.Name && await HasSubmitPermission(context, resource))
            {
                context.Succeed(requirement);
                return;
            }

            if (requirement.Name == CommonOperations.Delete.Name && await HasDeletePermission(context, resource))
            {
                context.Succeed(requirement);
                return;
            }
        }
        private async Task<bool> HasSubmitPermission(AuthorizationHandlerContext context, AnswerPaper resource)
        {
            if (resource.CreatorId != null && resource.CreatorId == context.User.FindUserId())
            {
                return true;
            }
            return false;
        }


        private async Task<bool> HasDeletePermission(AuthorizationHandlerContext context, AnswerPaper resource)
        {
            if (await _permissionChecker.IsGrantedAsync(ExaminingPermissions.Exams.Delete))
            {
                return true;
            }

            if (resource.CreatorId != null && resource.CreatorId == context.User.FindUserId())
            {
                return true;
            }


            return false;
        }

    }
}