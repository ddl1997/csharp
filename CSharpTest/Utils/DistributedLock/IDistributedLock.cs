
using System.Threading.Tasks;

namespace CSharpTest.FuncTest.Utils
{
    interface IDistributedLock
    {
        bool TryLockScript(string key, string value);
        bool TryLockTransaction(string key, string value);
        void UnLock(string key);
    }
}
