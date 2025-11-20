using LibraryManagementAPIInterface;
using LibraryManagementAPIModel;
using LibraryManagementAPIModel.DTO;
using Microsoft.EntityFrameworkCore;

public class BookConcrete : IBook
{
    private readonly LibraryDbContext _context;

    public BookConcrete(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel> GetAllAsync()
    {
        var response = new ResponseModel();
        try
        {
            var data = await _context.Books.ToListAsync();

            response.IsSuccess = true;
            response.StatusCode = 200;
            response.Message = "Fetched all books";
            response.Data = data;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.StatusCode = 500;
            response.Message = ex.Message;
        }
        return response;
    }

    public async Task<ResponseModel> GetByIdAsync(int id)
    {
        var response = new ResponseModel();
        try
        {
            var data = await _context.Books.FindAsync(id);

            if (data == null)
            {
                response.IsSuccess = false;
                response.StatusCode = 404;
                response.Message = "Book not found";
                return response;
            }

            response.IsSuccess = true;
            response.StatusCode = 200;
            response.Message = "Book found";
            response.Data = data;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.StatusCode = 500;
            response.Message = ex.Message;
        }
        return response;
    }

    public async Task<ResponseModel> AddAsync(BookViewModel model)
    {
        var response = new ResponseModel();
        try
        {
            var entity = new BookModel
            {
                Title = model.Title,
                Author = model.Author,
                PublishedYear = model.Year
            };

            await _context.Books.AddAsync(entity);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.StatusCode = 201;
            response.Message = "Book added successfully";
            response.Data = entity;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.StatusCode = 500;
            response.Message = ex.Message;
        }
        return response;
    }

    public async Task<ResponseModel> UpdateAsync(BookViewModel model)
    {
        var response = new ResponseModel();
        try
        {
            var entity = await _context.Books.FindAsync(model.Id);

            if (entity == null)
            {
                response.IsSuccess = false;
                response.StatusCode = 404;
                response.Message = "Book not found";
                return response;
            }

            entity.Title = model.Title;
            entity.Author = model.Author;
            entity.PublishedYear = model.Year;

            _context.Books.Update(entity);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.StatusCode = 200;
            response.Message = "Book updated";
            response.Data = entity;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.StatusCode = 500;
            response.Message = ex.Message;
        }
        return response;
    }

    public async Task<ResponseModel> DeleteAsync(int id)
    {
        var response = new ResponseModel();
        try
        {
            var entity = await _context.Books.FindAsync(id);

            if (entity == null)
            {
                response.IsSuccess = false;
                response.StatusCode = 404;
                response.Message = "Book not found";
                return response;
            }

            _context.Books.Remove(entity);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.StatusCode = 200;
            response.Message = "Book deleted";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.StatusCode = 500;
            response.Message = ex.Message;
        }
        return response;
    }
}
