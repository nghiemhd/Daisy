using Daisy.Core.Caching;
using Daisy.Core.Entities;
using Daisy.Core.Infrastructure;
using Daisy.Logging;
using Daisy.Service.Seo;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service
{
    public class UrlRecordService : HandleErrorService, IUrlRecordService
    {
        private IUnitOfWork unitOfWork;
        private IRepository<UrlRecord> urlRecordRepository;
        private IRepository<Language> languageRepository;
        private ILogger logger;
        private ICacheManager cacheManager;

        private const string URLRECORD_ACTIVE_BY_ID_NAME_LANGUAGE_KEY = "Daisy.urlrecord.active.id-name-language-{0}-{1}-{2}";
        private const string URLRECORD_PATTERN_KEY = "Daisy.urlrecord.";
        private const string URLRECORD_ALL_KEY = "Nop.urlrecord.all";

        public UrlRecordService(IUnitOfWork unitOfWork, ILogger logger, ICacheManager cacheManager)
            : base(logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.cacheManager = cacheManager;
            this.urlRecordRepository = this.unitOfWork.GetRepository<UrlRecord>();
            this.languageRepository = this.unitOfWork.GetRepository<Language>();
        }

        public void DeleteUrlRecord(UrlRecord urlRecord)
        {
            Process(() =>
            {
                this.urlRecordRepository.Delete(urlRecord);
                this.unitOfWork.Commit();

                cacheManager.RemoveByPattern(URLRECORD_PATTERN_KEY);
            });
        }

        public void InsertUrlRecord(UrlRecord urlRecord)
        {
            Process(() =>
            {
                this.urlRecordRepository.Insert(urlRecord);
                this.unitOfWork.Commit();

                cacheManager.RemoveByPattern(URLRECORD_PATTERN_KEY);
            });
        }

        public void UpdateUrlRecord(UrlRecord urlRecord)
        {
            Process(() =>
            {
                this.urlRecordRepository.Update(urlRecord);
                this.unitOfWork.Commit();

                cacheManager.RemoveByPattern(URLRECORD_PATTERN_KEY);
            });
        }

        public UrlRecord GetUrlRecordBy(string entityName, string slug)
        {
            int languageId = this.languageRepository.Query()
                .Where(x => x.IsPublished)
                .Select(x => x.Id)
                .FirstOrDefault();
            return GetUrlRecordBy(entityName, slug, languageId);
        }

        public UrlRecord GetUrlRecordBy(string entityName, string slug, int languageId)
        {
            var result = Process(() =>
            {
                var urlRecord = this.urlRecordRepository.Query().Where(x =>
                    x.LanguageId == languageId &&
                    x.Slug == slug &&
                    x.EntityName == entityName).FirstOrDefault();

                return urlRecord;
            });

            return result;
        }

        public string GetActiveSlug(int entityId, string entityName, int languageId)
        {
            string key = string.Format(URLRECORD_ACTIVE_BY_ID_NAME_LANGUAGE_KEY, entityId, entityName, languageId);
            return cacheManager.Get(key, () =>
            {
                var query = from ur in urlRecordRepository.Query()
                            where ur.EntityId == entityId &&
                            ur.EntityName == entityName &&
                            ur.LanguageId == languageId &&
                            ur.IsActive
                            orderby ur.Id descending
                            select ur.Slug;
                var slug = query.FirstOrDefault();
                //little hack here. nulls aren't cacheable so set it to ""
                if (slug == null)
                    slug = "";
                return slug;
            });
        }

        public string SaveSlug<T>(T entity, string slug, int languageId) where T : BaseEntity
        {
            var entityName = typeof(T).Name;
            slug = SeoExtensions.GetSeName(slug, true, false);

            UrlRecord urlRecord = this.urlRecordRepository.Query().Where(x =>
                x.EntityId == entity.Id &&
                x.EntityName == entityName &&
                x.LanguageId == languageId).FirstOrDefault();
           
            if (urlRecord == null)
            {
                urlRecord = new UrlRecord { 
                    EntityId = entity.Id,
                    EntityName = entityName,
                    LanguageId = languageId,
                    Slug = slug,
                    IsActive = true
                };

                this.urlRecordRepository.Insert(urlRecord);
            }
            else
            {
                urlRecord.Slug = slug;
            }

            this.unitOfWork.Commit();
            string key = string.Format(URLRECORD_ACTIVE_BY_ID_NAME_LANGUAGE_KEY, entity.Id, entityName, languageId);
            cacheManager.Remove(key);
            return urlRecord.Slug;
        }

        #region Utilities

        protected virtual IList<UrlRecordForCaching> GetAllUrlRecordsCached()
        {
            //cache
            string key = string.Format(URLRECORD_ALL_KEY);
            return cacheManager.Get(key, () =>
            {
                var urlRecords = this.urlRecordRepository.Query().ToList();
                var list = new List<UrlRecordForCaching>();
                foreach (var item in urlRecords)
                {
                    var localizedPropertyForCaching = new UrlRecordForCaching()
                    {
                        Id = item.Id,
                        EntityId = item.EntityId,
                        EntityName = item.EntityName,
                        Slug = item.Slug,
                        IsActive = item.IsActive,
                        LanguageId = item.LanguageId
                    };
                    list.Add(localizedPropertyForCaching);
                }
                return list;
            });
        }

        #endregion

        #region Nested classes

        [Serializable]
        public class UrlRecordForCaching
        {
            public int Id { get; set; }
            public int EntityId { get; set; }
            public string EntityName { get; set; }
            public string Slug { get; set; }
            public bool IsActive { get; set; }
            public int LanguageId { get; set; }
        }

        #endregion
    }
}
