namespace DatabaseSimulator;

using DatabaseSimulator.Exceptions;
using DatabaseSimulator.Miscellaneous;

public interface IDatabase
{
    Table<T> CreateTable<T>(T model) where T : TableModel;
    Table<T> GetTable<T>() where T : TableModel;
    void Remove<T>() where T : TableModel;
    Table<NewModel> Update<OldModel, NewModel>()
        where OldModel : TableModel
        where NewModel : TableModel, new();
}
