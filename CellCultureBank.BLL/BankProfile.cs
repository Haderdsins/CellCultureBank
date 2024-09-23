using CellCultureBank.BLL.Models.BankSecond;
using CellCultureBank.DAL.Models;

namespace CellCultureBank.BLL.Profile;
/// <summary>
/// Настройка профиля automapper банка
/// </summary>
public class BankProfile : AutoMapper.Profile
{
    public BankProfile()
    {
        CreateMap<CreateItemOfBank, BankOfCell>();
        CreateMap<UpdateItemOfBank, BankOfCell>();
        CreateMap<UpdateCellModel, BankOfCell>();
    }
}