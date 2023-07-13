using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCArchitecture.Models;
using MVCArchitecture.Views;

namespace MVCArchitecture.Controllers;

public class RegionController
{
    private Region _regionModel;
    private VRegion _regionView;

    public RegionController(Region regionModel, VRegion regionView)
    {
        _regionModel = regionModel;
        _regionView = regionView;
    }

    public void GetAll()
    {
        var result = _regionModel.GetAll();
        if (result.Count is 0)
        {
            _regionView.DataEmpty();
        }
        else
        {
            _regionView.GetAll(result);
        }
    }

    public void Insert()
    {
        var region = _regionView.Insert();
        var result = _regionModel.Insert(region);

        switch(result)
        {
            case -1:
                _regionView.Error();
                break;
            case 0:
                _regionView.Failure();
                break;
            default:
                _regionView.Success();
                break;
        }
    }

    public void Update()
    {
        var region = _regionView.Update();
        var result = _regionModel.Update(region);

        switch (result)
        {
            case -1:
                _regionView.Error();
                break;
            case 0:
                _regionView.Failure();
                break;
            default:
                _regionView.Success();
                break;
        }
    }

    public void Delete()
    {
        var region = _regionView.Delete();
        var result = _regionModel.Delete(region);

        switch (result)
        {
            case -1:
                _regionView.Error();
                break;
            case 0:
                _regionView.Failure();
                break;
            default:
                _regionView.Success();
                break;
        }
    }

    public void GetByID()
    {
        int id = _regionView.GetByID();
        Region region = _regionModel.GetByID(id);

        if (region != null)
        {
            _regionView.DisplayRegion(region);
        }
        else
        {
            _regionView.DataEmpty();
        }
    }
}
