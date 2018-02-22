using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Core
{
    public interface ILocalStorage
    {
        Task<string> Load();
        Task Save(string str);
    }
}
