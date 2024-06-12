namespace RPGManager.WarstwaDomenowa.Models
{

    public class Result<T>
    {
        public bool IsSuccessful;
        public string Message;
        public T obj;
    }
}
