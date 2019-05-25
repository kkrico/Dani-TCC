using System;
using System.Collections.Generic;
using System.Linq;
using Dani_TCC.Core.Extensions;
using Dani_TCC.Core.Helper;
using Dani_TCC.Core.Models;
using Dani_TCC.Core.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Dani_TCC.Core.Services
{
    public static class Constants
    {
        public static readonly int TotalOptions = 2;
    } 
    public class SurveyService : ISurveyService
    {
        private readonly DB_PESQUISA_TCCContext _context;
        private readonly IAnswerService _answerService;
        private readonly IPhotoService _photoService;

        public SurveyService(DB_PESQUISA_TCCContext context, IAnswerService answerService, IPhotoService photoService)
        {
            _context = context;
            _answerService = answerService;
            _photoService = photoService;
        }
        
        public BeginSurveyViewModel RegisterSurvey(RegisterSurveyViewModel model)
        {
            Person person = AddNewPersonFrom(model);

            ICollection<Answer> answers = _answerService.GenerateAnswers();
            
            Survey survey = GenerateNewSurvey(person);
            survey.Answer = answers;

            _context.Survey.Add(survey);
            _context.SaveChanges();

            BeginSurveyViewModel beginSurvey = GenerateBeginSurveyModel(answers);
            beginSurvey.SurveyCommand = _context.Question.First().Questiondescription;

            return beginSurvey;
        }

        private BeginSurveyViewModel GenerateBeginSurveyModel(IEnumerable<Answer> answers)
        {
            var options = new List<OptionViewModel>();
            List<Photo> photos = _photoService.ListValidSurveyPhotos().ToList();

            do
            {
                int randomIndex = RandomNumber.Between(0, photos.Count()-1);

                Photo sortedPhoto = photos.ElementAt(randomIndex);
                
                var optionViewModel = new OptionViewModel()
                {
                    PhotoId = sortedPhoto.Idphoto,
                    Base64Photo = Convert.ToBase64String(sortedPhoto.FileContents)
                };
                
                options.Add(optionViewModel);
                photos.Remove(sortedPhoto);

            } while (photos.Any());

            var beginSurvey = new BeginSurveyViewModel();

            foreach (Answer answer in answers)
            {
                var question = new QuestionViewModel();
                question.AnswerId = answer.Idanswer;
                
                for (var i = 0; i < Constants.TotalOptions; i++)
                {
                    
                    OptionViewModel optionsViewModel = options.PopAt(0);
                        
                    Valueanswer valueAnswer = GenerateValueAnswer(optionsViewModel, answer);

                    question.Options.Add(optionsViewModel);
                    optionsViewModel.ValueAnswerId = valueAnswer.Idvalueanswer;
                }
                
                beginSurvey.Questions.Add(question);
            }


            return beginSurvey;
        }

        private Valueanswer GenerateValueAnswer(OptionViewModel optionsViewModel, Answer answer)
        {
            var valueAnswer = new Valueanswer()
            {
                Idphoto = optionsViewModel.PhotoId,
                Idanswer = answer.Idanswer,
            };


            _context.Valueanswer.Add(valueAnswer);
            _context.SaveChanges();
            return valueAnswer;
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