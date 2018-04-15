using System.Collections.Generic;
using System.Linq;

namespace QuizService.Model.DataContract
{
    /// <summary>
    /// Represents operation result.
    /// </summary>
    public class OperationResultContract
    {
        public OperationResultContract()
        {

        }

        public OperationResultContract(IEnumerable<LocalizableErrorContract> errors)
        {
            this.Errors = errors;
        }

        /// <summary>
        /// Gets value indication whether operation completed successfully.
        /// </summary>
        public bool IsSuccessful { get { return !Errors.Any(); } }

        /// <summary>
        /// Gets or sets list of operation errors.
        /// </summary>
        public IEnumerable<LocalizableErrorContract> Errors { get; set; }
    }
}
