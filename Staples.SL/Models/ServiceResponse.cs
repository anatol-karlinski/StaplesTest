using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Staples.SL.Models
{
    public class ServiceResponse
    {
        private List<string> _errors = new List<string>();

        public ReadOnlyCollection<string> Errors { get => _errors.AsReadOnly(); }
        public bool OperationSuccessful = true;

        public void AddError(string error)
        {
            _errors.Add(error);
            OperationSuccessful = false;
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                _errors.Add(error);
            }
            OperationSuccessful = false;
        }
    }
}