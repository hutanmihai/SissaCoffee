using Microsoft.EntityFrameworkCore;
using SissaCoffee.Data;
using SissaCoffee.Models;

namespace SissaCoffee.Repositories.TagRepository;
using SissaCoffee.Repositories.GenericRepository;

public class TagRepository: GenericRepository<Tag>, ITagRepository
{
    public TagRepository(AppDbContext context) : base(context)
    {
    }
    
    public Tag? FindByName(string name)
    {
        return _table.FirstOrDefault(e => e.Name == name);
    }
}