using Daisy.Core.Entities;
using Daisy.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service.ServiceContracts
{
    public interface IUrlRecordService
    {
        void DeleteUrlRecord(UrlRecord urlRecord);

        void InsertUrlRecord(UrlRecord urlRecord);

        void UpdateUrlRecord(UrlRecord urlRecord);

        UrlRecord GetUrlRecordBy(string entityName, string slug);

        UrlRecord GetUrlRecordBy(string entityName, string slug, int languageId);

        string GetActiveSlug(int entityId, string entityName, int languageId);

        string SaveSlug<T>(T entity, string slug, int languageId) where T : BaseEntity;
    }
}
