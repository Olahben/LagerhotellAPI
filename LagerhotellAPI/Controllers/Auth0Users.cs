﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LagerhotellAPI.Controllers;

[Route("auth0-users")]
[ApiController]
public class Auth0UsersController : ControllerBase
{
    private readonly Auth0UserService _auth0UserService;
    public Auth0UsersController(Auth0UserService auth0UserService)
    {
        _auth0UserService = auth0UserService;
    }

    // Endpoints are going to be protected with authorization after auth0 tokens are integrated
    [HttpGet]
    [Route("get-user")]
    public async Task<IActionResult> GetUser([FromQuery] string auth0UserId)
    {
        try
        {
            var user = await _auth0UserService.GetCompleteUser(auth0UserId);
            return Ok(user);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
        catch (TooManyRequestsException e)
        {
            return StatusCode(StatusCodes.Status429TooManyRequests, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete]
    [Route("delete-user")]
    public async Task<IActionResult> DeleteUser([FromQuery] string auth0UserId)
    {
        try
        {
            await _auth0UserService.DeleteUser(auth0UserId);
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
        catch (TooManyRequestsException e)
        {
            return StatusCode(StatusCodes.Status429TooManyRequests, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}