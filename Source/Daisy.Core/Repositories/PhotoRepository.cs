using Daisy.Core.Entities;
using Daisy.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Repositories
{
    public interface IPhotoRepository
    {
        Photo GetById(int id);
    }

    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        private readonly IDbContext context;
        private readonly IDbSet<Photo> dbSet;

        public PhotoRepository(IDbContext context) : base(context)
        {
            dbSet = context.Set<Photo>();
        }

        public Photo GetById(int id)
        {
            var photo = dbSet.FirstOrDefault(x => x.Id == id);
            return photo;
        }
    }

}
