namespace FootHubWebApp
{
    internal class FootHubViewModel
    {
        public string Heading { get; set; }
        public string Body { get; set; }

        public FootHubViewModel(string heading, string body)
        {
            this.Heading = heading;
            this.Body = body;
        }
    }
}