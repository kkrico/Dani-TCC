using System;
using System.Collections.Generic;
using System.Linq;
using Dani_TCC.Core.Extensions;
using Dani_TCC.Core.GuardClause;
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
            beginSurvey.SurveyCommand = _context.Question.AsNoTracking().First().Questiondescription;

            return beginSurvey;
        }

        public void EndSurvey(IEnumerable<EndSurveyViewModel> endSurvey)
        {
            Guard.IsNotNull(endSurvey, nameof(endSurvey));

            List<Valueanswer> selectedValueAnswers = GetSelectedValueAnswers(endSurvey);
            List<Answer> answers = GetSelectedAnswers(selectedValueAnswers);
            List<Valueanswer> nonSelectedValueAnswers = GetNonSelectedValueAnswers(endSurvey, answers);
            List<Survey> selectedSurveis = GetSurveys(answers);

            foreach (Valueanswer selectedValueAnswer in selectedValueAnswers)
            {
                selectedValueAnswer.Haschoosen = Convert.ToByte(true);
                selectedValueAnswer.Selectiontime =
                    endSurvey.First(d => d.ValueAnswerId == selectedValueAnswer.Idvalueanswer).InterVal;
            }

            foreach (Valueanswer nonSelectedValueAnswer in nonSelectedValueAnswers)
            {
                nonSelectedValueAnswer.Haschoosen = Convert.ToByte(false);
                nonSelectedValueAnswer.Selectiontime = null;
            }

            foreach (Survey survey in selectedSurveis)
            {
                survey.Finalfilldate = DateTime.Now;
            }

            _context.SaveChanges();
        }

        private List<Survey> GetSurveys(List<Answer> selectedAnswers)
        {
            IEnumerable<int> idsSelectedSurveys = selectedAnswers.Select(e => e.Idsurvey);
            return _context.Survey.Where(s => idsSelectedSurveys.Contains(s.Idsurvey)).ToList();
        }

        private List<Valueanswer> GetNonSelectedValueAnswers(IEnumerable<EndSurveyViewModel> endSurvey, List<Answer> selectedAnswers)
        {
            IEnumerable<int> selectedsValueAnswersId = endSurvey.Select(e => e.ValueAnswerId);
            IEnumerable<int> selectedIsAnswers = selectedAnswers.Select(e => e.Idanswer);

            return _context.Valueanswer.Where(valueanswer => !selectedsValueAnswersId.Contains(valueanswer.Idvalueanswer) && selectedIsAnswers.Contains(valueanswer.Idanswer)).ToList();
        }

        private List<Answer> GetSelectedAnswers(List<Valueanswer> selectedValueAnswers)
        {
            IEnumerable<int> selectedIsAnswers = selectedValueAnswers.Select(e => e.Idanswer);
            return _context.Answer.Where(a => selectedIsAnswers.Contains(a.Idanswer)).ToList();
        }

        private List<Valueanswer> GetSelectedValueAnswers(IEnumerable<EndSurveyViewModel> endSurvey)
        {
            IEnumerable<int> selectedsValueAnswersId = endSurvey.Select(e => e.ValueAnswerId).ToList();
            return _context.Valueanswer.Where(valueanswer => selectedsValueAnswersId.Contains(valueanswer.Idvalueanswer)).ToList();
        }

        private BeginSurveyViewModel GenerateBeginSurveyModel(IEnumerable<Answer> answers)
        {
            var options = new List<OptionViewModel>();
            List<Photo> photos = _photoService.ListValidSurveyPhotos().ToList();

            do
            {
                int randomIndex = RandomNumber.Between(0, photos.Count() - 1);

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

            var savedValueAnswers = new List<Valueanswer>();
            var optionVIewModelList = new List<OptionViewModel>();
            var questionViewModelList = new List<QuestionViewModel>();

            foreach (Answer answer in answers)
            {
                var question = new QuestionViewModel { AnswerId = answer.Idanswer };
                questionViewModelList.Add(question);

                for (var i = 0; i < Constants.TotalOptions; i++)
                {

                    OptionViewModel optionsViewModel = options.PopAt(0);

                    Valueanswer valueAnswer = GenerateValueAnswer(optionsViewModel, answer);

                    _context.Valueanswer.Add(valueAnswer);
                    question.Options.Add(optionsViewModel);
                    savedValueAnswers.Add(valueAnswer);
                    optionVIewModelList.Add(optionsViewModel);
                    // optionsViewModel.ValueAnswerId = valueAnswer.Idvalueanswer;
                }
            }

            _context.SaveChanges();

            foreach (QuestionViewModel question in questionViewModelList)
            {
                var firstOption = optionVIewModelList.PopAt(0);
                var secondOption = optionVIewModelList.PopAt(0);

                var firstValueAnswer = savedValueAnswers.PopAt(0);
                var secondValueAnswer = savedValueAnswers.PopAt(0);

                firstOption.ValueAnswerId = firstValueAnswer.Idvalueanswer;
                secondOption.ValueAnswerId = secondValueAnswer.Idvalueanswer;

                question.Options = new List<OptionViewModel> { firstOption, secondOption };

                beginSurvey.Questions.Add(question);
            }


            beginSurvey.Questions = beginSurvey.Questions.OrderBy(d => d.AnswerId).ToList();
            return beginSurvey;
        }

        private Valueanswer GenerateValueAnswer(OptionViewModel optionsViewModel, Answer answer)
        {
            var valueAnswer = new Valueanswer()
            {
                Idphoto = optionsViewModel.PhotoId,
                IdanswerNavigation = answer,
            };


            //_context.Valueanswer.Add(valueAnswer);
            //_context.SaveChanges();
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