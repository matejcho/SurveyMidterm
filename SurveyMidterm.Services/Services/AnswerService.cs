using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SurveyMidterm.Data;
using SurveyMidterm.Data.Entities;
using SurveyMidterm.Models.Models.Answer;
using SurveyMidterm.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMidterm.Services.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IMapper _mapper;
        private readonly SurveyDbContext _context;
        public AnswerService(IMapper mapper, SurveyDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IEnumerable<AnswerModelBase>> Get()
        {
            var answers = await _context.Answers.ToListAsync();
            return _mapper.Map<IEnumerable<AnswerModelBase>>(answers);
        }

        public async Task<AnswerModelBase> Get(int id)
        {
            var answer = await _context.Answers.FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<AnswerModelBase>(answer);
        }

        public async Task<AnswerModelBase> Insert(AnswerCreateModel model)
        {
            var entity = _mapper.Map<Answer>(model);

            await _context.Answers.AddAsync(entity);
            await SaveAsync();

            return _mapper.Map<AnswerModelBase>(entity);
        }

        public async Task<AnswerModelBase> Update(AnswerUpdateModel model)
        {
            var entity = _mapper.Map<Answer>(model);

            _context.Answers.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await SaveAsync();

            return _mapper.Map<AnswerModelBase>(entity);
        }
        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(entity);
            return await SaveAsync() > 0;
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
