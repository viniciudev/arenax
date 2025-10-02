using AutoMapper;
using Infrastructure.Authenticate;
using Infrastructure.Repositories;

namespace Services
{

    public class SportsCourtCategoryService : ISportsCourtCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTManager _jWTManager;
        public SportsCourtCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IJWTManager jWTManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jWTManager = jWTManager;
        }
    }
    public interface ISportsCourtCategoryService
    {

    }

}
