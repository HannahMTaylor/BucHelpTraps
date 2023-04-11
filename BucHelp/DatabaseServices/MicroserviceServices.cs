using BucHelp.Data;

namespace BucHelp.DatabaseServices
{
    class MicroserviceServices
    {
        private static Task<string> location = DetermineLocation();

        private static async Task<string> DetermineLocation()
        {
            // Check if the appropriate environment variable is set
            // Whoever runs Docker should set this to the appropriate value
            string env = System.Environment.GetEnvironmentVariable("APILOCATION");
            if (env != null)
            {
                // if the value exists, trust it unconditionally
                return env;
            }
            // else there's no environment variable
            // try to guess based on the environment
            
            // Does /usr/share/dotnet/dotnet exist at this *exact* location? then it's probably Docker
            if (IsDockerDotNetExecutableExists())
            {
                // Give up
                return null;
            }
            else
            {
                // .NET is running elsewhere, probably Windows in Visual Studio
                // Bind to localhost:8080, but test it first
                bool test = await TestHelloWorld("http://localhost:8080");
                if (!test)
                {
                    // not running, so fail
                    return null;
                }
                // success
                return "http://localhost:8080";
            }
        }

        public static bool IsDockerDotNetExecutableExists()
        {
            return File.Exists("/usr/share/dotnet/dotnet");
        }

        public static async Task<string> GetAPILocation(string what)
        {
            return (await location) + what;
        }
        public static async Task<bool> TestHelloWorld(string location)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if ((await client.GetStringAsync(location + "/helloworld")).Equals("Hello World!"))
                    {
                        // successful get and string is what we expected to see
                        return true;
                    }
                }
                catch (HttpRequestException ex)
                {
                    // get failed
                    return false;
                }
            }
            // successful get, but the string was not right
            return false;
        }
        public static async Task<FAQ[]> GetFAQAsync()
        {
            // if there's no known client, return an error FAQ
            if (await location == null)
            {
                FAQ faq = new FAQ();
                faq.Question = "How do I set up the microservice API to load FAQs?";
                faq.Answer = "To set up the microservice API, make sure the API is running on a known location. " +
                    "Set the APILOCATION to an HTTP address such as \"http://172.17.0.1:80\" being sure not to add a trailing slash. " +
                    "Restart the BucHelp application, as the location is determined on startup. " +
                    "If you are not running BucHelp in Docker, the API will try to be found at http://localhost:8080 so make sure the port is mapped " +
                    "to that. If you are running BucHelp in Docker, you must specify APILOCATION explicitly." +
                    "Since you are seeing this message, autodetection did not work. Fix it!";
                if (IsDockerDotNetExecutableExists())
                {
                    faq.Answer += " The Docker .NET runtime was found, so BucHelp is running in Docker.";
                }
                else
                {
                    faq.Answer += " The Docker .NET runtime was not found, so BucHelp is probably running in Windows.";
                }
                return new FAQ[] { faq };
            }
            // else contact the microservice
            using (HttpClient client = new HttpClient())
            {
                return (FAQ[]) await client.GetFromJsonAsync(await GetAPILocation("/getfaq"), typeof(FAQ[]));
            }
        }
    }
}
