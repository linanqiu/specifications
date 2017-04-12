using System;
using System.Linq.Expressions;

namespace Specifications
{
	public interface ISpecification<T>
	{
		bool That(T expected, T actual);
		ISpecification<T> And(ISpecification<T> other);
		ISpecification<T> Or(ISpecification<T> other);
	}

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

		public abstract bool That(T expected, T actual);
	}

	public class ConditionSpecification<T> : CompositeSpecification<T>
	{
		private  Expression<Func<T, T, bool>> _condition;

		public ConditionSpecification(Expression<Func<T, T, bool>> condition)
		{
			_condition = condition;
		}

		public override bool That(T expected, T actual)
		{
			var func = _condition.Compile();
			var result = func(expected, actual);
			if (!result)
			{
				Console.WriteLine("Failed condition:");
				Console.WriteLine(_condition.Type);
				Console.WriteLine(_condition.ToString());
			}
			return result;
		}
	}

	public class AndSpecification<T> : CompositeSpecification<T>
	{
		private readonly ISpecification<T> _left;
		private readonly ISpecification<T> _right;

		public AndSpecification(ISpecification<T> left, ISpecification<T> right)
		{
			_left = left;
			_right = right;
		}

		public override bool That(T expected, T actual)
		{
			return _left.That(expected, actual) && _right.That(expected, actual);
		}
	}

	public class OrSpecification<T> : CompositeSpecification<T>
	{
		private readonly ISpecification<T> _left;
		private readonly ISpecification<T> _right;

		public OrSpecification(ISpecification<T> left, ISpecification<T> right)
		{
			_left = left;
			_right = right;
		} 

		public override bool That(T expected, T actual)
		{
			return _left.That(expected, actual) || _right.That(expected, actual);
		}
	}
}
