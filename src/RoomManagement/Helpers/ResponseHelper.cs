namespace RoomManagement.Helpers;

public static class ResponseHelper
{
    public static void SetHeaderResponse(HttpContext context, string ResponseStatusCode, string ResponseMessage)
    {

        if (!context.Response.Headers.ContainsKey("responsecode"))
        {
            context.Response.Headers.Add("responsecode", ResponseStatusCode);
        }

        if (!context.Response.Headers.ContainsKey("responsemessage"))
        {
            context.Response.Headers.Add("responsemessage", ResponseMessage.Replace(Environment.NewLine, string.Empty));
        }

        if (!context.Response.Headers.ContainsKey("responsedatasource"))
        {
            context.Response.Headers.Add("responsedatasource", "N/A");
        }

    }
}
