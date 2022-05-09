using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.Enums;

namespace Insomnia.Portal.API.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        internal new OkObjectResult Ok() => base.Ok(new
        {
            Done = true
        });

        internal new OkObjectResult Ok(object model) => base.Ok(new
        {
            Done = true,
            Model = model
        });

        internal new BadRequestObjectResult BadRequest() => base.BadRequest(new
        {
            Done = false
        });

        internal BadRequestObjectResult BadRequest(string errorMessage) => base.BadRequest(new
        {
            Done = false,
            Message = errorMessage
        });

        internal NotFoundObjectResult NotFound(string errorMessage) => base.NotFound(new
        {
            Done = false,
            Message = errorMessage
        });

        internal IActionResult Result(BaseReturn model)
        {
            if (!model.Success)
                return GetError(model.Code, model.Message);

            return Ok(model.Model);
        }

        private IActionResult GetError(CodeRequest code, string errorMessage) =>
            code switch
            {
                CodeRequest.NotFound => NotFound(errorMessage),
                CodeRequest.BadRequest => BadRequest(errorMessage),
                _ => BadRequest("Непредвиденная ошимбка!")
            };
    }
}
