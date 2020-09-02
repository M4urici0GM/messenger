using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Exceptions
{
    public class EntityValidationException : Exception
    {
        public IList<FailureItem> Failures { get; set; }

        public EntityValidationException (string name, object key, IList<ValidationFailure> failures)
            : base($"Entity \"{name}\" ({key}) have some invalid fields. {string.Join(",", failures)}")
        {
            Failures = failures
                .Select(error => new FailureItem { PropertyName = error.PropertyName, ErrorMessage = error.ErrorMessage})
                .ToList();
        }

        public class FailureItem
        {
            public string PropertyName { get; set; }
            public string ErrorMessage { get; set; }
        }
        
    }
}
