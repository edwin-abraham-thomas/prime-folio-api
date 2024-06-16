using core.Models;
using Microsoft.AspNetCore.Mvc;

namespace core.Utilities
{
    public static class ResultMapper
    {
        public static ActionResult<T> ConvertToActionResult<T>(Response<T> response)
        {
            ActionResult<T> actionResult = null;

            if(response.IsSuccess && !response.HasError)
            {
                return new OkObjectResult(response.Result);
            }

            switch (response.ErrorCode)
            {
                case Constants.CommonErrorCodes.INTERNAL:
                    actionResult = new StatusCodeResult(500);
                    break;

                case Constants.CommonErrorCodes.NOT_FOUND:
                    actionResult = new NotFoundResult();
                    break;

                default:
                    actionResult = new StatusCodeResult(500);
                    break;
            }

            return actionResult;
        }
    }
}
