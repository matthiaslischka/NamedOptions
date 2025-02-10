using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

class Program
{
    static void Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddOptions()
            .Configure<MySettings>(null, GetConfiguration().GetSection("MySettings"))
            .Configure<MySettings>("AnotherMySettings", GetConfiguration().GetSection("MySettings:AnotherMySettings"))
            .AddTransient<MyService>()
            .BuildServiceProvider();

        serviceProvider.GetRequiredService<MyService>().DisplaySettings();
    }

    static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }
}

public class MyService
{
    private readonly MySettings _mySettings;

    public MyService(IOptionsSnapshot<MySettings> mySettings)
    {
        _mySettings = mySettings.Get("AnotherMySettings");
    }

    public void DisplaySettings()
    {
        Console.WriteLine($"{_mySettings.SomeValue}");
        Console.WriteLine($"{_mySettings.AnotherValue}");
        Console.ReadLine();
    }
}

public class MySettings
{
    public string SomeValue { get; set; } = string.Empty;
    public string AnotherValue { get; set; } = string.Empty;
}