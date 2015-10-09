using Daisy.Core.Entities;
using Daisy.Core.Infrastructure;
using Daisy.Logging;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service
{
    public class LocalizationService : HandleErrorService, ILocalizationService
    {
        private IUnitOfWork unitOfWork;
        private ILogger logger; 
        private IRepository<Language> languageRepository;

        public LocalizationService(IUnitOfWork unitOfWork, ILogger logger)
            : base(logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.languageRepository = this.unitOfWork.GetRepository<Language>();
        }

        public IEnumerable<Language> GetLanguages()
        {
            var languages = Process(() =>
            {
                return this.languageRepository.Query().OrderBy(x => x.DisplayOrder).AsEnumerable();
            });

            return languages;
        }

        public Language GetLanguageBy(int id)
        {
            var language = Process(() =>
            {
                return this.languageRepository.Query().Where(x => x.Id == id).FirstOrDefault();
            });

            return language;
        }
    }
}
