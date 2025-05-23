using Microsoft.AspNetCore.Mvc;
using System.Net;
using UnivAuth.Api.Helpers;
using UnivAuth.Application.DTOs;
using UnivAuth.Application.UseCases;
using UnivAuth.Domain.Entities;

namespace UnivAuth.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]

    public class AuthController : ControllerBase
    {        
        private readonly LoginService _loginService;
        private readonly AppAuth _apptAuth;

        public AuthController(LoginService loginService, AppAuth appAuth)
        {
            _loginService = loginService;
            _apptAuth = appAuth;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (!_apptAuth.AppSecretKeyValidation(Request.Headers["x-api-app-key"].ToString()))
                {
                    return Unauthorized(new { status = HttpStatusCode.Unauthorized, mensaje = "No autorizado" });
                }

                if (string.IsNullOrWhiteSpace(request.Usuario) || string.IsNullOrWhiteSpace(request.Pwd))
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, mensaje = "Usuario o contraseña incorrectos" });
                }

                User? user = await _loginService.LoginAsync(request);

                if (user == null)
                {
                    return Unauthorized(new { status = HttpStatusCode.Unauthorized, mensaje = "Usuario o contraseña incorrectos" });
                }

                if (user.status != HttpStatusCode.OK)
                {
                    return StatusCode((int)user.status, new
                    {
                        user.status,
                        user.mensaje
                    });
                }

                LoginResult result = new LoginResult();
                
                result.usr = user.usr;
                result.secreto = user.secreto;
                result.tfa = user.tfa;

                result.status = HttpStatusCode.OK;
                result.mensaje = "";

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = HttpStatusCode.InternalServerError,
                    mensaje = ex.Message
                });
            }
        }

        [HttpPost("login2fa")]
        public async Task<IActionResult> LoginTFA([FromBody] TFaRequest request)
        {
            if (!_apptAuth.AppSecretKeyValidation(Request.Headers["x-api-app-key"].ToString()))
            {
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, mensaje = "No autorizado" });
            }

            if (string.IsNullOrWhiteSpace(request.Usuario) || string.IsNullOrWhiteSpace(request.Secreto))
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, mensaje = "Usuario o código de autenticación incorrectos" });
            }

            User? user = await _loginService.Login2faAsync(request);

            if (user == null)
            {
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, mensaje = "Usuario o código de autenticación incorrectos" });
            }

            user.status = HttpStatusCode.OK;
            user.mensaje = "";

            return Ok(user);
        }
    }
}
