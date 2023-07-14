using MVCArchitecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCArchitecture.Models;
using MVCArchitecture.Views;

namespace MVCArchitecture.Controllers;

public class EmployeeController
{
    private Employee _employeeModel;
    private VEmployee _employeeView;

    public EmployeeController(Employee employeeModel, VEmployee employeeView)
    {
        _employeeModel = employeeModel;
        _employeeView = employeeView;
    }

    public void GetAll()
    {
        var result = _employeeModel.GetAll();
        if (result.Count is 0)
        {
            _employeeView.DataEmpty();
        }
        else
        {
            _employeeView.GetAll(result);
        }
    }

    public void Insert()
    {
        var employee = _employeeView.Insert();
        var result = _employeeModel.Insert(employee);
        switch(result)
        {
            case -1:
                _employeeView.Error();
                break;

            case 0:
                _employeeView.Failure();
                break;
            default:
                _employeeView.Success();
                break;
        }
    }

    public void Update() 
    {
        var employee = _employeeView.Update();
        var result = _employeeModel.Update(employee);
        switch (result)
        {
            case -1:
                _employeeView.Error();
                break;

            case 0:
                _employeeView.Failure();
                break;
            default:
                _employeeView.Success();
                break;
        }
    }

    public void Delete()
    {
        var employee = _employeeView.Delete();
        var result = _employeeModel.Delete(employee);
        switch (result)
        {
            case -1:
                _employeeView.Error();
                break;

            case 0:
                _employeeView.Failure();
                break;
            default:
                _employeeView.Success();
                break;
        }
    }

    public void GetByID()
    {
        int id = _employeeView.GetByID();
        Employee employee = _employeeModel.GetByID(id);
        if (employee != null)
        {
            _employeeView.DisplayEmployee(employee);
        }
        else
        {
            _employeeView.DataEmpty();
        }
    }
}
