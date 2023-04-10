using BucHelp.Data;

namespace BucHelp.DatabaseServices
{
    private async Task<FAQ[]> GetFAQ()
    {
        using (HttpClient client = new HttpClient())
        {
            return await client.GetFromJsonAsync("http://192.168.1.3/getfaq", FAQ);
        }
    }
}
