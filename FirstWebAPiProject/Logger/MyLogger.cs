namespace FirstWebAPiProject.Logger
{
    public class MyLogger : IMyLogger
    {
        public void Log(string erromessage)
        {
            Console.WriteLine(erromessage);
        }
    }
   
}
