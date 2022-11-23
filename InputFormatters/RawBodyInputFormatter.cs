using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace PreasyBoard.Api.InputFormatters;

public class RawBodyInputFormatter : InputFormatter
{
    public RawBodyInputFormatter()
    {
        this.SupportedMediaTypes.Add("application/json");
        this.SupportedMediaTypes.Add("text/plain");
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
    {
        var request = context.HttpContext.Request;
        using (var reader = new StreamReader(request.Body))
        {
            var content = await reader.ReadToEndAsync();
            return await InputFormatterResult.SuccessAsync(content);
        }
    }

    protected override bool CanReadType(Type type)
    {
        return type == typeof(string);
    }
}