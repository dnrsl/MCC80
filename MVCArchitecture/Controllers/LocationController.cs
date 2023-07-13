using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCArchitecture.Models;
using MVCArchitecture.Views;

namespace MVCArchitecture.Controllers;

public class LocationController
{
    private Location _locationModel;
    private VLocation _locationView;

    public LocationController(Location locationModel, VLocation locationView)
    {
        _locationModel = locationModel;
        _locationView = locationView;
    }

    public void GetAll()
    {
        var result = _locationModel.GetAll();
        if (result.Count is 0)
        {
            _locationView.DataEmpty();
        }
        else
        {
            _locationView.GetAll(result);
        }
    }

    public void Insert()
    {
        var location = _locationView.Insert();
        var result = _locationModel.Insert(location);
        switch (result)
        {
            case -1:
                _locationView.Error();
                break;
            case 0:
                _locationView.Failure();
                break;
            default:
                _locationView.Success();
                break;
        }
    }

    public void Update()
    {
        var location = _locationView.Update();
        var result = _locationModel.Update(location);
        switch (result)
        {
            case -1:
                _locationView.Error();
                break;
            case 0:
                _locationView.Failure();
                break;
            default:
                _locationView.Success();
                break;
        }
    }

    public void Delete()
    {
        var location = _locationView.Delete();
        var result = _locationModel.Delete(location);
        switch (result)
        {
            case -1:
                _locationView.Error();
                break;
            case 0:
                _locationView.Failure();
                break;
            default:
                _locationView.Success();
                break;
        }
    }

    public void GetByID()
    {
        int id = _locationView.GetByID();
        Location location = _locationModel.GetByID(id);
        if (location != null)
        {
            _locationView.DisplayLocation(location);
        }
        else
        {
            _locationView.DataEmpty();
        }
    }
}
