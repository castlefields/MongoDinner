using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDinner.Models;
using System.Web.Mvc;

namespace MongoDinner.Helpers {

    public static class ModelStateHelpers {

        public static void AddModelErrors(this ModelStateDictionary modelState, IEnumerable<RuleViolation> errors) {

            foreach (RuleViolation issue in errors) {
                modelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
            }
        }
    }
}
