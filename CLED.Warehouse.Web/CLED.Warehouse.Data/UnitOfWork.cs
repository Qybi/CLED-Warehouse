using CLED.Warehouse.Data.Abstractions;
using CLED.Warehouse.Web;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ISTES.Appointment.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WarehouseContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        private readonly ILoggerFactory _loggerFactory;

        public UnitOfWork(WarehouseContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<UnitOfWork>();
        }

        private ITutorRepository _tutorRepo;
        private IEventRepository _bookingRepo;
        private IResourceRepository _resourceRepo;
        private IParentRepository _parentRepo;
        private IHistoryRepository _historyRepo;
        private IPackageRepository _packageRepo;
        private IPackageSaleRepository _packSalesRepo;
        private IStudentRepository _studentRepo;
        private IMailTemplateRepository _mailTemplateRepo;

        public IMailTemplateRepository MailTemplates => _mailTemplateRepo ??= new MailTemplateRepository(_context, _loggerFactory.CreateLogger<MailTemplateRepository>());
        public IStudentRepository Students => _studentRepo ??= new StudentRepository(_context, _loggerFactory.CreateLogger<StudentRepository>());
        public IPackageSaleRepository PackageSales => _packSalesRepo ??= new PackageSaleRepository(_context, _loggerFactory.CreateLogger<PackageSaleRepository>());
        public IPackageRepository Packages => _packageRepo ??= new PackageRepository(_context, _loggerFactory.CreateLogger<PackageRepository>());
        public IHistoryRepository History => _historyRepo ??= new HistoryRepository(_context, _loggerFactory.CreateLogger<HistoryRepository>());
        public IParentRepository Parents => _parentRepo ??= new ParentRepository(_context, _loggerFactory.CreateLogger<ParentRepository>());
        public IResourceRepository Resources => _resourceRepo ??= new ResourceRepository(_context, _loggerFactory.CreateLogger<ResourceRepository>());
        public ITutorRepository Tutors => _tutorRepo ??= new TutorRepository(_context, _loggerFactory.CreateLogger<TutorRepository>());
        public IEventRepository Events => _bookingRepo ??= new EventRepository(_context, _loggerFactory.CreateLogger<EventRepository>());

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while saving the changes on the db.");
                throw;
            }
        }
    }
}
