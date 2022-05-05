namespace Common.AppService.WindowService
{
    public interface IManagable
    {
        void Close(bool? result = null);

        void CloseWithArg(bool? result, object? arg);
    }
}