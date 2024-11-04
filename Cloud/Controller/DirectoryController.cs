using Cloud.Domain.Http.Request.Directory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controller;

public class DirectoryController : MainController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get([FromQuery] GetDirectoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var directory = await DirectoryService.Get(request.Id);
        return Ok(directory);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByPath([FromQuery] GetByPathDirectoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var directory = await DirectoryService.GetByPath(request.Path);
        return Ok(directory);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByName([FromQuery] GetByNameDirectoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var directory = await
            DirectoryService.GetByName(request.NameDirectory, request.UserId);

        return Ok(directory);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllUserDirectory([FromQuery] GetAllUserDirectoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var directory = await
            DirectoryService.GetAllUserDirectories(request.userId);

        return Ok(directory);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetSubDirectories([FromQuery] GetSubDirectoriesRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var directory = await DirectoryService.GetSubDirectories(request.Id);

        return Ok(directory);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateDirectoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var directory = await DirectoryService.Create(request);

        return Ok(directory);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Rename([FromBody] UpdateDirectoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var directory = await DirectoryService.Rename(request);

        return Ok(directory);
    }

    [HttpPut]
    [Authorize]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateIcon([FromForm] UpdateIconDirectoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var directory = await DirectoryService.UpdateIcon(request);

        return Ok(directory);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Move([FromBody] UpdatePathDirectoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var directory = await DirectoryService.Move(request);

        return Ok(directory);
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete([FromQuery] DeleteDirectoryRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var directory = await DirectoryService.Delete(request.Id);

        return Ok(directory);
    }
}