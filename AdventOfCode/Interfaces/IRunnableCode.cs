using System.Threading.Tasks;

namespace AdventOfCode.Interfaces
{
    interface IRunnableCode
    {
        Task<string> ExecuteAsync();
    }
}