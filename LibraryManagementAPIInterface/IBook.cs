using System;
using System.Collections;
using LibraryManagementAPIModel.DTO;

namespace LibraryManagementAPIInterface
{
	public interface IBook
	{
        Task<ResponseModel> GetAllAsync();
        Task<ResponseModel> GetByIdAsync(int id);
        Task<ResponseModel> AddAsync(BookViewModel model);
        Task<ResponseModel> UpdateAsync(BookViewModel model);
        Task<ResponseModel> DeleteAsync(int id);

    }
}

