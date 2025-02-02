using Microsoft.AspNetCore.Mvc;

namespace MovieAnalytics.API.Controllers;

// [ServiceFilter(typeof(LogUserActivity))]
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{

}