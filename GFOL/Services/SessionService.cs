using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFOL.Data;
using GFOL.Helpers;
using GFOL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace GFOL.Services
{
    public interface ISessionService
    {
        bool HasUserComeFromCheck();
        void SetUserComeFromCheck();
        void ClearUserComeFromCheck();
        void SetUserSessionVars(UserSessionVM vm);
        UserSessionVM GetUserSessionVars();
        void ClearSession();
    }
    public class SessionService : ISessionService
    {
        private const string KeySubmissionVm = "SubmissionVm";
        private const string KeyHasUserComeFromCheck = "UserComeFromCheck";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public bool HasUserComeFromCheck()
        {
            var userSessionVm = GetUserSessionVars();
            if (!userSessionVm.HasUserComeFromCheck.HasValue)
            {
                userSessionVm.HasUserComeFromCheck = false;

                SetUserSessionVars(userSessionVm);
            }

            return (bool)userSessionVm.HasUserComeFromCheck;
        }

        public void SetUserComeFromCheck()
        {
            var context = _httpContextAccessor.HttpContext;
            context.Session.SetString(KeyHasUserComeFromCheck, true.ToString());
        }

        public void ClearUserComeFromCheck()
        {
            var context = _httpContextAccessor.HttpContext;
            context.Session.Remove(KeyHasUserComeFromCheck);
        }

        public void SetUserSessionVars(UserSessionVM vm)
        {
            var context = _httpContextAccessor.HttpContext;
            if (vm.SubmissionVm != null)
            {
                context.Session.SetString(KeySubmissionVm, JsonConvert.SerializeObject(vm.SubmissionVm));
                context.Session.SetString(KeyHasUserComeFromCheck, vm.HasUserComeFromCheck.ToString());
            }
        }
        public void ClearSession()
        {
            var context = _httpContextAccessor.HttpContext;
            context.Session.Remove(KeySubmissionVm);
            context.Session.Remove(KeyHasUserComeFromCheck);
        }

        public UserSessionVM GetUserSessionVars()
        {
            var returnVm = new UserSessionVM
            {
                SubmissionVm = GetObject<SubmissionVM>(KeySubmissionVm),
                HasUserComeFromCheck = GetBoolValueFromSession(KeyHasUserComeFromCheck)
            };

            return returnVm;
        }
        private T GetObject<T>(string key)
        {
            var context = _httpContextAccessor.HttpContext;
            var value = context.Session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
        private bool? GetBoolValueFromSession(string key)
        {
            var context = _httpContextAccessor.HttpContext;
            var stringValue = context.Session.GetString(key);
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return null;
            }
            else
            {
                return bool.Parse(stringValue);
            }
        }

    }
}
