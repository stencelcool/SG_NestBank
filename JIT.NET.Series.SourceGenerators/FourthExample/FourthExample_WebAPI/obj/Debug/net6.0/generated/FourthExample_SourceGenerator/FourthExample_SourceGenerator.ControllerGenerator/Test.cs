﻿using FourthExample_WebAPI.Models;
using FourthExample_WebAPI.Providers;
using Microsoft.AspNetCore.Mvc;
namespace GeneratedController
{
    [ApiController]
    public class GeneratePasswordHandlerController : ControllerBase
    {

    private readonly IPasswordGenerator _passwordGenerator;

    public GeneratePasswordHandlerController(IPasswordGenerator passwordGenerator)
    {
        _passwordGenerator = passwordGenerator;
    }

    [HttpGet("password")]
    public IActionResult GetGeneratedPassword()
    {
        return new OkObjectResult(_passwordGenerator.GenerateRandomPassword());
    }
}
}
