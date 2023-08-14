using API.DTOs.Employees;
using API.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Client.Controllers;

[Authorize(Roles = "Admin")]
public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    public async Task<IActionResult> Index()
    {
        var result = await _employeeRepository.Get();
        var ListEmployee = new List<EmployeeDto>();

        if (result.Data != null)
        {
            ListEmployee = result.Data.Select(employee => (EmployeeDto)employee).ToList();
        }
        return View(ListEmployee);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(NewEmployeeDto newEmployee)
    {

        var result = await _employeeRepository.Post(newEmployee);
        if (result.Code == 200 )
        {
            TempData["Success"] = "Data berhasil masuk";
            return RedirectToAction(nameof(Index));
        }
        else if (result.Code == 400)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await _employeeRepository.Get(id);
        var ListEmployee = new UpdateEmployeeDto();
        if (result.Data != null)
        {
            ListEmployee = (UpdateEmployeeDto)result.Data;
        }
        return View(ListEmployee);
    }

    [HttpPost]
    public async Task<IActionResult>Update(UpdateEmployeeDto updateEmployee)
    {
        var result = await _employeeRepository.Put(updateEmployee.Guid, updateEmployee);
        if (result.Code == 200)
        {
            TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
            return RedirectToAction(nameof(Index));
        }

        TempData["Error"] = $"Failed to update data - {result.Message}!";
        return RedirectToAction(nameof(Edit));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var result = await _employeeRepository.Delete(guid);
        if (result.Code == 200)
        {
            TempData["Success"] = "Data Berhasil Dihapus";
        }
        else
        {
            TempData["Error"] = "Gagal Menghapus Data";
        }
        return RedirectToAction(nameof(Index));

    }

}
