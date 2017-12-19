using Autofac;
using Autofac.Features.Variance;

namespace Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterServices<DatabaseHandler.DatabaseHandlerAssembly>();
            builder.RegisterServices<HistoryHandler.HistoryHandlerAssembly>();
            builder.RegisterServices<AuthorService.AuthorServiceAssembly>();
            builder.RegisterServices<BookService.BookServiceAssembly>();

            builder.RegisterMediator<DatabaseHandler.DatabaseHandlerAssembly>();
            builder.RegisterMediator<HistoryHandler.HistoryHandlerAssembly>();
            builder.RegisterMediator<AuthorService.AuthorServiceAssembly>();
            builder.RegisterMediator<BookService.BookServiceAssembly>();
        }
    }
}