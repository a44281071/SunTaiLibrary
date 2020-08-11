using System.Windows;
using System.Windows.Markup;

[assembly: XmlnsPrefix("http://www.suntai.org/library", "t3")]
[assembly: XmlnsDefinition("http://www.suntai.org/library", "SunTaiLibrary.Controls")]
[assembly: XmlnsDefinition("http://www.suntai.org/library", "SunTaiLibrary.Converters")]
[assembly: XmlnsDefinition("http://www.suntai.org/library", "SunTaiLibrary.Dependencies")]
[assembly: XmlnsDefinition("http://www.suntai.org/library", "SunTaiLibrary.Attached")]
[assembly: XmlnsDefinition("http://www.suntai.org/library", "SunTaiLibrary.Commands")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]
