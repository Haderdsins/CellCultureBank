using CellCultureBank.BLL.Models;
using CellCultureBank.DAL.Models;

namespace CellCultureBank.BLL;
/// <summary>
/// Настройка профиля automapper банка
/// </summary>
public class BankProfile : AutoMapper.Profile
{
    public BankProfile()
    {
        CreateMap<CreateItemModel, BankOfCell>();
        CreateMap<UpdateItemModel, BankOfCell>();
        CreateMap<FreezeCellModel, BankOfCell>();
        CreateMap<DefrostCellModel, BankOfCell>();
    }
}