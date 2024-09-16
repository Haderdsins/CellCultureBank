using CellCultureBank.BLL.Models.Create;
using CellCultureBank.BLL.Models.Update;
using CellCultureBank.DAL.Models;

namespace CellCultureBank.BLL.Profile;
/// <summary>
/// Настройка профиля automapper первого банка
/// </summary>
public class BankFirstProfile : AutoMapper.Profile
{
    public BankFirstProfile()
    {
        CreateMap<CreateItemOfBankModel, BankFirst>();
        CreateMap<UpdateItemOfBankModel, BankFirst>();
    }
}