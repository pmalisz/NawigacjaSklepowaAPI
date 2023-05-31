using Microsoft.AspNetCore.Mvc;
using NawigacjaSklepowaAPI.Services.Interfaces;

namespace NawigacjaSklepowaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppAdminController : ControllerBase
    {
        private readonly IAppAdminService _appAdminService;

        public AppAdminController(IAppAdminService authService)
        {
            _appAdminService = authService;
        }
    }
}
