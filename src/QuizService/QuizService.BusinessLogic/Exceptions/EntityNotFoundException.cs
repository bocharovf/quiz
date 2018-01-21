using QuizService.Model.Exceptions;
using System;

namespace QuizService.BusinessLogic.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception, IEntityNotFoundException
    {
        private const string ErrorCodeVaue = "EntityNotFound";

        public string EntityType { get; }

        public object EntityId { get; }

        public string ErrorCode => ErrorCodeVaue;

        public object Extension => new { this.EntityType, this.EntityId };

        public EntityNotFoundException(Type entityType) : this(entityType, null)
        {
        }

        public EntityNotFoundException(Type entityType, object entityId) : base(ErrorCodeVaue)
        {
            this.EntityId = entityId;
            this.EntityType = entityType.Name;
        }

        public override string Message
        {
            get
            {
                return EntityId == null 
                        ? $"{EntityType} was not found."
                        : $"{EntityType} with identifier {EntityId} was not found.";
            }

        }
    }
}
