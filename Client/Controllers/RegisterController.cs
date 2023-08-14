using API.DTOs.Accounts;
using API.DTOs.Employees;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class RegisterController : Controller
{
    private readonly IRegisterRepository _registerRepository;
    public RegisterController(IRegisterRepository registerRepository)
    {
        _registerRepository = registerRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {

        var result = await _registerRepository.Post(registerDto);
        if (result.Code == 200)
        {
            TempData["Success"] = "Data berhasil masuk";
            return RedirectToAction("Index","Employee");
        }
        else if (result.Code == 400)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }
        return RedirectToAction("Index","Employee");
    }
}
