using System.Threading.Tasks;

namespace Zoe.Sim
{
    public interface ISimulation
    {
        Task Run(SimulationOptions options);
    }
}