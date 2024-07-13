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

    [HttpPost]
    public async Task<ActionResult<Models.Content>> PostAsync([FromBody] ContentCreateRequest request, CancellationToken cancellationToken)
    {
        var response = await _contentService.CreateContentAsync(request, cancellationToken);
        return ResultMapper.ConvertToActionResult(response);
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

    [HttpDelete]
    [Route("{userId}")]
    public async Task<ActionResult<bool>> DeleteAsync([FromRoute] string userId, CancellationToken cancellationToken)
    {
        var response = await _contentService.DeleteContentAsync(userId, cancellationToken);
        return ResultMapper.ConvertToActionResult(response);
    }
}
