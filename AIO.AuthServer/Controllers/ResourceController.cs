﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIO.AuthServer.Controllers;

[ApiController]
[Route("resources")]
public class ResourceController : Controller
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetSecretResources()
    {
        var user = HttpContext.User?.Identity?.Name;
        return Ok($"user: {user}");
    }
}