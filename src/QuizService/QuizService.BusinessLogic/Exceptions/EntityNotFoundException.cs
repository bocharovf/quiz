using QuizService.Model.Exceptions;
using System;

namespace QuizService.BusinessLogic.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : BusinessLogicException
    {
        private const string ErrorCodeVaue = "EntityNotFound";

        public string EntityType { get; }

        public object EntityId { get; }

        public override string ErrorCode => ErrorCodeVaue;

        public override object Extension => new { this.EntityType, this.EntityId };

        public EntityNotFoundException(Type entityType, object entityId) : base(ErrorCodeVaue)
        {
            this.EntityId = entityId;
            this.EntityType = entityType.Name;
        }

        public override string Message
        {
            get
            {
                return $"{EntityType} with identifier {EntityId} was not found.";
            }

        }
    }
}
