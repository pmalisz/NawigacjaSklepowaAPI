using AutoMapper;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Services.Interfaces;

namespace NawigacjaSklepowaAPI.Services
{
    public class AppAdminService : IAppAdminService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AppAdminService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
