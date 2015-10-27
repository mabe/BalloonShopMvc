using System;
using StructureMap;

namespace BalloonShop.Mvc
{
	public interface ICommandHandler<T> where T : ICommand
	{
		void Execute(T command);
	}

	public interface IQuery<TModel> {
		
	} 

	public interface IQueryHandler<T, TModel> where T : IQuery<TModel>
	{
		TModel ExecuteQuery(T query);
	}

	public class CommandQueryHandler {
		private IContainer _container;

		public CommandQueryHandler (IContainer container)
		{
			_container = container;
		}

		public void ExecuteCommand<T>(T command) where T : ICommand {
			_container.GetInstance<ICommandHandler<T>> ().Execute (command);
		}

		public TModel ExecuteQuery<T, TModel>(T model) where T : IQuery<TModel> {
			return _container.GetInstance<IQueryHandler<T, TModel>> ().ExecuteQuery (model);
		}
	}
}

