using System;
using System.Collections.Generic;
using Dani_TCC.Core.Models;
using Dani_TCC.Core.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Dani_TCC.Core.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly DB_PESQUISA_TCCContext _context;
        private readonly IAnswerService _answerService;

        public SurveyService(DB_PESQUISA_TCCContext context, IAnswerService answerService, ICacheService cacheService)
        {
            _context = context;
            _answerService = answerService;
        }
        
        public BeginSurveyViewModel RegisterSurvey(RegisterSurveyViewModel model)
        {
            Person person = AddNewPersonFrom(model);

            ICollection<Answer> answers = _answerService.GenerateAnswers();
            
            Survey survey = GenerateNewSurvey(person);
            survey.Answer = answers;

            _context.Survey.Add(survey);
            _context.SaveChanges();

            return GenerateBeginSurveyModel(answers);
        }

        private BeginSurveyViewModel GenerateBeginSurveyModel(IEnumerable<Answer> answers)
        {
            var l = new List<OptionViewModel>();
            foreach (Answer answer in answers)
            {
            }
            throw new NotImplementedException();
        }

        private Survey GenerateNewSurvey(Person person)
        {
            var survey = new Survey()
            {
                Initialfilldate = DateTime.Now,
                IdpersonNavigation = person,
            };

            return survey;
        }

        private Person AddNewPersonFrom(RegisterSurveyViewModel model)
        {
            var person = new Person()
            {
                Idgender = model.Gender,
                Idagegroup = model.AgeGroup,
                Idethnicity = model.Ethnicity,
                Idsexuality = model.Sexuality,
                Idfamilyincome = model.FamilyIncome,
            };
            _context.Add(person);

            return person;
        }
    }
}