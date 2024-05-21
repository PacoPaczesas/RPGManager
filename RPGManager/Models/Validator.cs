
namespace RPGManager.Models
{

    public class ValidatorResult<T>
    {
        public bool IsCompleate;
        public string Message;
        public T obj;
    }
}
