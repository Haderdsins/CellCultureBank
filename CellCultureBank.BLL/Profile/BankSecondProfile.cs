using CellCultureBank.BLL.Models.BankSecond;
using CellCultureBank.BLL.Models.Update;
using CellCultureBank.DAL.Models;

namespace CellCultureBank.BLL.Profile;
/// <summary>
/// Настройка профиля automapper второго банка
/// </summary>
public class BankSecondProfile : AutoMapper.Profile
{
    public BankSecondProfile()
    {
        CreateMap<CreateItemOfSecondBank, BankSecond>();
        CreateMap<UpdateItemOfSecondBank, BankSecond>();
        CreateMap<UpdateCellModel, BankSecond>();
    }
}