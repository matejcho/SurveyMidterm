using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SurveyMidterm.Data;
using SurveyMidterm.Data.Entities;
using SurveyMidterm.Models.Models.SurveyUser;
using SurveyMidterm.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMidterm.Services.Services
{
    public class SurveyUserService : ISurveyUserService
    {
        private readonly IMapper _mapper;
        private readonly SurveyDbContext _context;
        public SurveyUserService(SurveyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SurveyUserModelBase>> Get()
        {
            var surveyUsers = await _context.SurveyUsers.ToListAsync();
            return _mapper.Map<IEnumerable<SurveyUserModelBase>>(surveyUsers);
        }

        public async Task<SurveyUserModelBase> Get(int id)
        {
            var surveyUser = await _context.SurveyUsers.FindAsync(id);
            return _mapper.Map<SurveyUserModelBase>(surveyUser);
        }

        public async Task<SurveyUserModelBase> Insert(SurveyUserCreateModel model)
        {
            var entity = _mapper.Map<SurveyUser>(model);
            await _context.SurveyUsers.AddAsync(entity);
            await SaveAsync();
            return _mapper.Map<SurveyUserModelBase>(entity);
        }

        public async Task<SurveyUserModelBase> Update(SurveyUserUpdateModel model)
        {
            var entity = _mapper.Map<SurveyUser>(model);
            _context.SurveyUsers.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await SaveAsync();

            return _mapper.Map<SurveyUserModelBase>(entity);                
        }
        public async Task<bool> Delete(int id)
        {
            var entity = await _context.SurveyUsers.FindAsync(id);
            _context.SurveyUsers.Remove(entity);
            return await SaveAsync() > 0;
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
