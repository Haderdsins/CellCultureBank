using CellCultureBank.BLL.Models.BankSecond.Create;
using CellCultureBank.BLL.Models.BankSecond.Update;
using CellCultureBank.BLL.Models.Update;
using CellCultureBank.DAL.Models;

namespace CellCultureBank.BLL.Profile;

public class BankSecondProfile : AutoMapper.Profile
{
    public BankSecondProfile()
    {
        CreateMap<CreateItemOfSecondBank, BankSecond>();
        CreateMap<UpdateItemOfSecondBank, BankSecond>();
    }
}