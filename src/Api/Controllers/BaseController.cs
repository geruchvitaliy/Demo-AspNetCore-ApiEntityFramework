using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers
{
    /// <summary>
    /// Base Controller
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Gets id of authenticated user
        /// </summary>
        protected Guid UserId => Guid.NewGuid();
    }
}