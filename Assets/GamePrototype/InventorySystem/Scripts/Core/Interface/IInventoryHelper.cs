public interface IInventoryHelper
{
    IStock InitStock<T>(IStock stock, T data) where T : class;
}