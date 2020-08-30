using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApusAnimalMotel.Interfaces
{
    /// <summary>
    /// The generic interface of ListManager, ensures structure and that all ListManagers behave the same
    /// </summary>
    /// <typeparam name="T">as this interface is generic, it needs a type</typeparam>
    public interface IListManager<T>
    {
        int Count { get; }
        bool Add(T itemToAdd);
        bool ChangeAt(T itemToAdd, int indexToAddAt);
        bool CheckIndex(int index);
        void DeleteAll();
        bool DeleteAt(int indexToDeleteAt);
        T GetAt(int indexToGetAt);
        List<string> ToStringList();
        string[] ToStringArray();


    }
}
