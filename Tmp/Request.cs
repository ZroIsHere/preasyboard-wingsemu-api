
namespace noswebapp.Tmp
{
    public class Request
    {
        private int _id;
        private string _challenge;
        private string _timestamp;
        private Request req;

        public int id { get;  set; }
        public string challenge { get;  set; }
        public string timestamp { get;  set; }
        public Request()
        {
        }

        public Request(int id, string challenge, string timestamp)
        {
            _id = id;
            _challenge = challenge;
            _timestamp = timestamp;
        }

        public Request(Request req)
        {
            this.req = req;
        }
    }
}

