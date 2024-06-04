﻿using HomeBankingV9.DTOs;
using HomeBankingV9.Models;
using HomeBankingV9.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HomeBankingV9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        public AuthController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                Client user = _clientRepository.FindByEmail(loginDTO.Email);
                if(user == null)
                    return StatusCode(403, "User not found");
                if(!user.Password.Equals(loginDTO.Password))
                    return StatusCode(403, "Invalid credentials");
                var claims = new List<Claim>();
                if (user.Email.Equals("edu@gmail.com"))
                {
                    claims.Add(new Claim("Admin", user.Email));
                    claims.Add(new Claim("Client", user.Email));
                } else
                {
                    claims.Add(new Claim("Client", user.Email));
                }
                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)
                );
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}