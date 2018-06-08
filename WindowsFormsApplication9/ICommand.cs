namespace BlackJack
{
    public interface ICommand<T>
    {
        void execute(T t);
    }
    public interface CommandHandler<T>
    {
        void Handle(T command);
    }
}