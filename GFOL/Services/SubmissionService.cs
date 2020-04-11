using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GFOL.Helpers;
using GFOL.Models;
using GFOL.Repository;
using Newtonsoft.Json;

namespace GFOL.Services
{
    public interface ISubmissionService
    {
        Task<List<SubmissionVM>> GetAllSubmissionsAsync();
        Task<SubmissionVM> GetSubmissionAsync(int id);
        SubmissionVM GetSubmissionVm(bool useFile = false);
        void SaveSubmissionIntoSession(SubmissionVM submissionVm);
        Task<int> PostSubmissionAsync(SubmissionVM submissionVm);
        SubmissionVM GetSubmissionFromSession();
        void SaveUserAnswerIntoSession(Question question);
        string GetUserAnswerFromSession(string questionId);
    }
    public class SubmissionService : ISubmissionService
    {
        private readonly IGenericRepository<Submission> _submissionRepository;
        private readonly IGenericRepository<Schema> _schemaRepository;
        private readonly ISessionService _sessionService;

        public SubmissionService(IGenericRepository<Submission> submissionRepository, IGenericRepository<Schema> schemaRepository, ISessionService sessionService)
        {
            _submissionRepository = submissionRepository;
            _schemaRepository = schemaRepository;
            _sessionService = sessionService;
        }

        public async Task<List<SubmissionVM>> GetAllSubmissionsAsync()
        {
            var returnList = new List<SubmissionVM>();
            var submissions = await _submissionRepository.FindByAsync(x => x.SavedDate<DateTime.Now);
            foreach (var submission in submissions.OrderBy(x => x.Id))
            {
                var sub = JsonConvert.DeserializeObject<SubmissionVM>(submission.SubmissionJson);
                sub.DateCreated = submission.SavedDate.ToLongDateString();
                sub.Id = submission.Id.ToString();
                returnList.Add(sub);
            }
            return returnList;
        }

        public async Task<SubmissionVM> GetSubmissionAsync(int id)
        {
            var sub = await _submissionRepository.GetByIdAsync(id);
            var submission = JsonConvert.DeserializeObject<SubmissionVM>(sub.SubmissionJson);
            submission.DateCreated = sub.SavedDate.ToLongDateString();
            submission.Id = sub.Id.ToString();

            return submission;
        }

        public SubmissionVM GetSubmissionVm(bool useFile = false)
        {
            SubmissionVM submissionVm = null;
            if (useFile)
            {
                using (var r = new StreamReader("Content/submission-schema.json"))
                {
                    var file = r.ReadToEnd();

                    submissionVm = JsonConvert.DeserializeObject<SubmissionVM>(file);
                }
            }
            else
            {
                var schema = _schemaRepository.GetByIdAsync(2).Result;
                if (schema != null)
                {
                    submissionVm = JsonConvert.DeserializeObject<SubmissionVM>(schema.SchemaJson);
                }
            }

            return submissionVm;
        }

        public void SaveSubmissionIntoSession(SubmissionVM submissionVm)
        {
            var sessionVm = _sessionService.GetUserSessionVars();
            sessionVm.SubmissionVm = submissionVm;
            _sessionService.SetUserSessionVars(sessionVm);
        }

        public async Task<int> PostSubmissionAsync(SubmissionVM submissionVm)
        {
            var json = JsonConvert.SerializeObject(submissionVm);
            var submission = await _submissionRepository.CreateAsync(new Submission {SavedDate = DateTime.Now, SubmissionJson = json});
            return submission.Id;
        }

        public SubmissionVM GetSubmissionFromSession()
        {
            var submissionVm =_sessionService.GetUserSessionVars().SubmissionVm;
            return submissionVm;
        }

        public void SaveUserAnswerIntoSession(Question question)
        {
            if(string.IsNullOrEmpty(question.Answer)) return;
            SubmissionVM submissionVm = GetSubmissionFromSession() ?? this.GetSubmissionVm();
            submissionVm.Answers.FirstOrDefault(x => x.QuestionId == question.QuestionId).AnswerText = question.Answer;
            
            var usesSessionVm = _sessionService.GetUserSessionVars();
            usesSessionVm.SubmissionVm = submissionVm;
            _sessionService.SetUserSessionVars(usesSessionVm);
        }

        public string GetUserAnswerFromSession(string questionId)
        {
            SubmissionVM submissionVm = GetSubmissionFromSession() ?? this.GetSubmissionVm();
            var answer = submissionVm.Answers.FirstOrDefault(x => x.QuestionId == questionId).AnswerText;
            return answer;
        }
    }
}
