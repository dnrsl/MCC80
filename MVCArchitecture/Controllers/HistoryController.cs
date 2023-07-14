using MVCArchitecture.Views;
using MVCArchitecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Controllers;

public class HistoryController
{
    private History _historyModel;
    private VHistory _historyView;

    public HistoryController (History historyModel, VHistory historyView)
    {
        _historyModel = historyModel;
        _historyView = historyView;
    }

    public void GetAll()
    {
        var result = _historyModel.GetAll();
        if (result.Count == 0)
        {
            _historyView.DataEmpty();
        }
        else
        {
            _historyView.GetAll(result);
        }
    }

    public void Insert()
    {
        var history = _historyView.Insert();
        var result = _historyModel.Insert(history);

        switch (result)
        {
            case -1:
                _historyView.Error();
                break;
            case 0:
                _historyView.Failure();
                break;
            default:
                _historyView.Success();
                break;
        }
    }

    public void Update()
    {
        var history = _historyView.Update();
        var result = _historyModel.Update(history);

        switch (result)
        {
            case -1:
                _historyView.Error();
                break;
            case 0:
                _historyView.Failure();
                break;
            default:
                _historyView.Success();
                break;
        }
    }

    public void Delete()
    {
        var history = _historyView.Delete();
        var result = _historyModel.Delete(history);

        switch (result)
        {
            case -1:
                _historyView.Error();
                break;
            case 0:
                _historyView.Failure();
                break;
            default:
                _historyView.Success();
                break;
        }
    }

    public void GetByID()
    {
        int empID = _historyView.GetByID();
        History history = _historyModel.GetByID(empID);
        if (history != null)
        {
            _historyView.DisplayHistory(history);
        }
        else
        {
            _historyView.DataEmpty();
        }
    }
}
