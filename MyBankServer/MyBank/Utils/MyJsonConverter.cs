using MyBank.Application.Utils;

namespace MyBank.API.Utils;

public static class MyJsonConverter<T> where T : class
{
    public static T Convert(T? data)
    {
        return JsonConvert.DeserializeObject<T>(
            JsonConvert.SerializeObject(data, 
            settings: new JsonSerializerSettings { 
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore 
            }))!;
    }
}
