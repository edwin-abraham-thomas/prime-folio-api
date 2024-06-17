using Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Interfaces.Services;
using core.Models;
using core.Utilities;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentController : ControllerBase
{
    private readonly IContentService _contentService;
    private readonly ILogger<ContentController> _logger;

    public ContentController(IContentService contentService, ILogger<ContentController> logger)
    {
        _contentService = contentService;
        _logger = logger;
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<ActionResult<Models.Content>> GetAsync([FromRoute] string userId, CancellationToken cancellationToken)
    {
        var response = await _contentService.GetContentAsync(userId, cancellationToken);
        return ResultMapper.ConvertToActionResult(response);
    }

    [HttpPut]
    public async Task<ActionResult<Models.Content>> PutAsync([FromBody] ContentUpdateRequest request, CancellationToken cancellationToken)
    {
        var response = await _contentService.UpdateContentAsync(request, cancellationToken);
        return ResultMapper.ConvertToActionResult(response);
    }
}
