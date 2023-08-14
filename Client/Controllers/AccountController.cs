using API.DTOs.Accounts;
using Client.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Client.Controllers;

public class AccountController : Controller
{
    private readonly IAccountRepository _accountRepository;
    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
        {
            // Pengguna sudah login, lakukan tindakan sesuai kebijakan Anda
            return RedirectToAction("Index", "Home");
        }

        // Jika belum login, tampilkan halaman login
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await _accountRepository.Login(loginDto);
        if (result is null)
        {
            return RedirectToAction("Error", "Home");
        }
        else if (result.Code == 409)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }
        else if (result.Code == 200)
        {
            HttpContext.Session.SetString("JWToken", result.Data.Token);
            return RedirectToAction("Index", "Home");
        }
        
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        // Clear authentication cookies
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
