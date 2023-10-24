using Banfsg.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL;

public class UserAddressesManager:IUserAddressesManager
{
    private readonly IUnitOfWork _unitOfWork;

    public UserAddressesManager(IUnitOfWork unitOfWork)
    {
       _unitOfWork = unitOfWork;
    }
   

    #region Get All User Addresses
    public IEnumerable<UserAddressDto> GetAllUserAddressesByUID(string uIDFromToken)
    {
        var addressesFromDb = _unitOfWork.UserAddressesRepo.GetAllUserAddressesByUID(uIDFromToken);
        var addresses=addressesFromDb.Select(address=>new UserAddressDto
        {
            City = address.City,
            DefaultAddress = address.DefaultAddress,
            Id = address.Id,
            Phone = address.Phone,
            Street = address.Street
        });
        return addresses;
    }
    #endregion

    #region Get User Address  by Id
    public UserAddressDto? GetAddressById(int id)
    {
        UserAddress? addressFromDb=_unitOfWork.UserAddressesRepo.GetBYId(id);
        if (addressFromDb == null) return null;
        UserAddressDto address = new UserAddressDto 
        { City = addressFromDb.City,
         DefaultAddress = addressFromDb.DefaultAddress,
         Id = addressFromDb.Id ,
         Phone=addressFromDb.Phone,
        Street=addressFromDb.Street
        };
        return address;


    }
    #endregion

    #region Adding NEW User Address
    public void AddNewAddress(string UIDFromToken,AddingAddressDto address) //take the UID to reset the default
    {
        UserAddress addressDb = new UserAddress
        {
            UserId = UIDFromToken,
            Phone = address.Phone,
            Street = address.Street,
            City = address.City,
            DefaultAddress = address.DefaultAddress
        };
        if (address.DefaultAddress == true)
        {
            _unitOfWork.UserAddressesRepo.resetDefaultAddress(UIDFromToken);
        }
        _unitOfWork.UserAddressesRepo.Add(addressDb);
        _unitOfWork.SaveChanges();

    }

    #endregion

    #region Edit User Address
    public void EditUserAddress(string UID, EditAddressDto addressDto)
    {
        var addressFromDb = _unitOfWork.UserAddressesRepo.GetBYId(addressDto.Id);
        if (addressFromDb == null) return;
        addressFromDb.DefaultAddress = addressDto.DefaultAddress;
        addressFromDb.Phone= addressDto.Phone;
        addressFromDb.Street= addressDto.Street;
        addressFromDb.City= addressDto.City;
        if (addressDto.DefaultAddress == true)
        {
            _unitOfWork.UserAddressesRepo.resetDefaultAddress(UID);
        }
        _unitOfWork.SaveChanges();
    }
    #endregion

    #region Delete User Address
    public void DeleteByAddressID(int id)
    {
        var addressFromDb = _unitOfWork.UserAddressesRepo.GetBYId(id);
        if (addressFromDb == null) return;
        _unitOfWork.UserAddressesRepo.Delete(addressFromDb);
        _unitOfWork.SaveChanges();

    }
    #endregion

    #region Reset Default address
    public void SetDefaultAddress(string UID,int id)
    {
        _unitOfWork.UserAddressesRepo.resetDefaultAddress(UID);
        var addressToedit=_unitOfWork.UserAddressesRepo.GetBYId(id);
        if (addressToedit == null) return;
        addressToedit.DefaultAddress = true;
        _unitOfWork.SaveChanges();
    }
    #endregion

}
