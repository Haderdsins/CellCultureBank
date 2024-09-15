using CellCultureBank.BLL.Models.Create;
using CellCultureBank.BLL.Models.Update;
using CellCultureBank.DAL.Models;

namespace CellCultureBank.BLL.Profile;
public class BankFirstProfile : AutoMapper.Profile
{
    public BankFirstProfile()
    {
        CreateMap<CreateItemOfBankModel, BankFirst>();
        CreateMap<UpdateItemOfBankModel, BankFirst>();
    }
}