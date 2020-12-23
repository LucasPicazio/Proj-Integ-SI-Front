using System.Threading.Tasks;

namespace PSI_FRONT.Helpers
{
    public interface IDisplayMessage
    {
        ValueTask DisplayErrorMessage(string message);
        ValueTask DisplaySuccessMessage(string message);
    }
}
