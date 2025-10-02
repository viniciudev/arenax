using AutoMapper;
using Core;
using Core.DTOs;
using Infrastructure.Authenticate;
using Infrastructure.Repositories;
using System.Text.Json;

namespace Services
{


    public class SportsCenterService : ISportsCenterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTManager _jWTManager;
        public SportsCenterService(IUnitOfWork unitOfWork, IMapper mapper, IJWTManager jWTManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jWTManager = jWTManager;
        }

        public async Task<int> CreateSportsCenter(SportsCenterDto modelDto)
        {
            SportsCenter model = _mapper.Map<SportsCenter>(modelDto);
            //criar ligação SportCenterUser
            if (modelDto?.Amenities?.Count > 0)
                model.Amenities = JsonSerializer.Serialize(modelDto.Amenities, Default);

            await _unitOfWork.SportsCenter.Add(model);
            _unitOfWork.Save();

            await _unitOfWork.SportsCenterUsers.Add(new SportsCenterUsers
            {
                IdUser = modelDto.IdUser,
                IdSportsCenter = model.Id
            });
            _unitOfWork.Save();
            return model.Id;
        }

        public async Task<bool> DeleteSportsCenter(int SportsCenterId)
        {
            if (SportsCenterId > 0)
            {
                var SportsCenterDetail = await _unitOfWork.SportsCenter.GetById(SportsCenterId);
                if (SportsCenterDetail != null)
                {
                    _unitOfWork.SportsCenter.Delete(SportsCenterDetail);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }

        public async Task<SportsCenterDto?> GetSportsCenterById(int SportsCenterId)
        {
            if (SportsCenterId > 0)
            {
                var resp = await _unitOfWork.SportsCenter.GetByIdAsync(SportsCenterId);
                return resp;
            }
            else
            {
                return null;
            }
        }
        public static readonly JsonSerializerOptions Default = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        public async Task<bool> UpdateSportsCenter(SportsCenterDto sportsCenterParam)

        {
            if (sportsCenterParam == null)
                return false;

            try
            {
                await _unitOfWork.SportsCenter.UpdatePartialAsync(sportsCenterParam.Id, entity =>
                {
                    // Atualizar propriedades simples
                    entity.Name = sportsCenterParam.Name;
                    entity.Cnpj = sportsCenterParam.Cnpj;
                    entity.Phone = sportsCenterParam.Phone;
                    if (sportsCenterParam?.Amenities?.Count > 0)
                        entity.Amenities = JsonSerializer.Serialize(sportsCenterParam.Amenities, Default); 


                    // Atualizar Address
                    if (sportsCenterParam.Address != null)
                    {
                        if (entity.Address == null)
                        {
                            entity.Address = _mapper.Map<Address>(sportsCenterParam.Address);
                        }
                        else
                        {
                            _mapper.Map(sportsCenterParam.Address, entity.Address);
                        }
                    }

                    // Atualizar OpeningHours
                    if (sportsCenterParam.OpeningHours != null)
                    {
                        if (entity.OpeningHours == null)
                        {
                            entity.OpeningHours = _mapper.Map<OpeningHours>(sportsCenterParam.OpeningHours);
                        }
                        else
                        {
                            _mapper.Map(sportsCenterParam.OpeningHours, entity.OpeningHours);
                        }
                    }
                });

                var result = _unitOfWork.Save();
                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task UpdateSportsCenterLogo(int id, string uniquFileName)
        {
            var sportsCenter = await _unitOfWork.SportsCenter.GetById(id);
            if (sportsCenter == null)
            {
                throw new ArgumentException("Centro esportivo não encontrado");
            }

            // Atualize o logo no objeto
            sportsCenter.UniqueFileName = uniquFileName;

            _unitOfWork.SportsCenter.Update(sportsCenter);
            _unitOfWork.Save();
        }
        public async Task<List<SportsCenterDto>> GetAllByFilter(SportCenterFilter value)
        {
            return await _unitOfWork.SportsCenter.GetAllByFilter(value);
        }
    }

    public interface ISportsCenterService
    {
        Task<int> CreateSportsCenter(SportsCenterDto SportsCenter);
        Task<SportsCenterDto?> GetSportsCenterById(int SportsCenterId);
        Task<bool> UpdateSportsCenter(SportsCenterDto SportsCenterParam);
        Task<bool> DeleteSportsCenter(int SportsCenterId);
        Task UpdateSportsCenterLogo(int id, string uniquFileName);
        Task<List<SportsCenterDto>> GetAllByFilter(SportCenterFilter value);

    }
}
