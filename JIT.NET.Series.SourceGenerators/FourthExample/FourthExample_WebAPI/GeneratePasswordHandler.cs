using FourthExample_WebAPI.Models;
using FourthExample_WebAPI.Providers;
using Microsoft.AspNetCore.Mvc;

namespace FourthExample_WebAPI;

public class GeneratePasswordHandler
{
    private readonly IPasswordGenerator _passwordGenerator;

    public GeneratePasswordHandler(IPasswordGenerator passwordGenerator)
    {
        _passwordGenerator = passwordGenerator;
    }

    [HttpGet("password")]
    public IActionResult GetGeneratedPassword()
    {
        return new OkObjectResult(_passwordGenerator.GenerateRandomPassword());
    }
}