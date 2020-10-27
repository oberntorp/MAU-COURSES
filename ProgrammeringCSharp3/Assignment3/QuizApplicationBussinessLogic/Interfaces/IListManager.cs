using System;
using System.Collections.Generic;

namespace QuizApplicationBussinessLogic.Interfaces
{
    // <summary>
    /// The generic interface of ListManager, ensures structure and that all ListManagers behave the same
    /// </summary>
    /// <typeparam name="T">as this interface is generic, it needs a type</typeparam>
    public interface IListManager<T>
    {
        int Count { get; }
        bool Add(T itemToAdd);
        bool ChangeAt(T changedItem, int indexToChangeAt);
        bool CheckIndex(int index);
        void DeleteAll();
        bool DeleteAt(int indexToDeleteAt);
        T GetAt(int indexToGetAt);
        void XMLSerialize(string fileName);
        void XMLDeserialize(string fileName);
        List<T> GetAllItems();


    }
}
