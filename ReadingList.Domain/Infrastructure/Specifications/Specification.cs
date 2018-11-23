using System;

namespace ReadingList.Domain.Infrastructure.Specifications
{
    public class Specification<T>
    {
        private readonly Func<T, bool> _predicate;

        public Specification(Func<T, bool> predicate)
        {
            _predicate = predicate;
        }

        public bool SatisfiedBy(T entity)
        {
            return _predicate.Invoke(entity);
        }
    }
}