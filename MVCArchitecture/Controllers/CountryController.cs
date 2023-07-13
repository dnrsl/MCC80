using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCArchitecture.Models;
using MVCArchitecture.Views;

namespace MVCArchitecture.Controllers;

public class CountryController
{
    private Country _countryModel;
    private VCountry _countryView;

    public CountryController(Country countryModel, VCountry countryView)
    {
        _countryModel = countryModel;
        _countryView = countryView;
    }

    public void GetAll()
    {
        var result = _countryModel.GetAll();
        if (result.Count is 0)
        {
            _countryView.DataEmpty();
        }
        else
        {
            _countryView.GetAll(result);
        }
    }

    public void Insert()
    {
        var country = _countryView.Insert();
        var result = _countryModel.Insert(country);

        switch (result)
        {
            case -1:
                _countryView.Error();
                break;
            case 0:
                _countryView.Failure();
                break;
            default:
                _countryView.Success();
                break;
        }
    }

    public void Update()
    {
        var country = _countryView.Update();
        var result = _countryModel.Update(country);
        switch (result)
        {
            case -1:
                _countryView.Error();
                break;
            case 0:
                _countryView.Failure();
                break;
            default:
                _countryView.Success();
                break;
        }
    }

    public void Delete()
    {
        var country = _countryView.Delete();
        var result = _countryModel.Delete(country);

        switch (result)
        {
            case -1:
                _countryView.Error();
                break;
            case 0:
                _countryView.Failure();
                break;
            default:
                _countryView.Success();
                break;
        }
    }

    public void GetByID()
    {
        int id = _countryView.GetByID();
        Country country = _countryModel.GetByID(id);
        if (country != null)
        {
            _countryView.DisplayCountry(country);
        }
        else
        {
            _countryView.DataEmpty();
        }
    }
}
