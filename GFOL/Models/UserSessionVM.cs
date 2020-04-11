using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFOL.Helpers;

namespace GFOL.Models
{
    public class UserSessionVM
    {
        public SubmissionVM SubmissionVm { get; set; }
        public bool? HasUserComeFromCheck { get; set; }
    }
}
