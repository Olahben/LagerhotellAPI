﻿namespace LagerhotellAPI.Services;
public interface ICompanyUserService
{

    Task<CompanyUser> GetCompanyUserAsync(string id);
    Task<List<CompanyUser>> GetCompanyUsersAsync(int? take, int? skip);
    Task<CompanyUser> GetCompanyUserByPhoneNumber(string phoneNumber);
    Task<(string, string)> CreateCompanyUserAsync(CompanyUser companyUser);
    Task UpdateCompanyUserAsync(string id, CompanyUser companyUser);
    Task DeleteCompanyUserAsync(string id);

    Task<(string, string)> LoginCompanyUserByEmail(string email, string password);

}
