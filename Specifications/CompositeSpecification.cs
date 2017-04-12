using System;
namespace Specifications
{

	public abstract class CompositeSpecification<T> : ISpecification<T>
	{
		public ISpecification<T> And(ISpecification<T> other)
		{
			return new AndSpecification<T>(this, other);
		}

		public ISpecification<T> Or(ISpecification<T> other)
		{
			return new OrSpecification<T>(this, other);
		}

		public abstract bool That(T entity);
	}
	
}
