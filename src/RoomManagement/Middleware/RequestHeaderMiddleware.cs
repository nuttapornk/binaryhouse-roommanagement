using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RoomManagement.Helpers;
using Application.Common.Models;

namespace RoomManagement.Middleware;

public class RequestHeaderMiddleware : IMiddleware
{
    private readonly List<string> _whiteLists = new();
    public RequestHeaderMiddleware()
    {
        _whiteLists = new List<string> { "/alive", "/health", "/swagger" };
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string message = string.Empty;
        try
        {

            foreach (var whiteList in _whiteLists)
            {
                if (context.Request.Path.StartsWithSegments(whiteList))
                {
                    await next(context);
                    return;
                }
            }


            if (!context.Request.Headers.ContainsKey("refer"))
            {
                message = "Missing request header - refer";
            }
            else if (!context.Request.Headers.ContainsKey("sender"))
            {
                message = "Missing request header - sender";
            }
            else if (!context.Request.Headers.ContainsKey("forward"))
            {
                message = "Missing request header - forward";
            }
            else
            {
                if (String.IsNullOrEmpty(context.Request.Headers["refer"].ToString()))
                {
                    message = "Missing value request header - refer";
                }
                else if (String.IsNullOrEmpty(context.Request.Headers["sender"].ToString()))
                {
                    message = "Missing value request header - sender";
                }
                else if (String.IsNullOrEmpty(context.Request.Headers["forward"].ToString()))
                {
                    message = "Missing value request header - forward";
                }
            }


        }
        catch (Exception ex)
        {
            message = ex.Message;
        }

        if (!String.IsNullOrEmpty(message))
        {
            context.Response.ContentType = "application/json";
            ResponseHelper.SetHeaderResponse(context, ((int)HttpStatusCode.InternalServerError).ToString(), HttpStatusCode.InternalServerError.ToString());
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            BaseResponse? response = BaseResponse.Error500(devErrorMessage: message);
            await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(response), Encoding.UTF8);

            return;
        }

        await next.Invoke(context);
    }
}
