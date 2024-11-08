﻿using MAUI.Word.Finder.src.Shared.Framework.NewFolder;
using Microsoft.Extensions.Logging;

namespace MAUI.Word.Finder
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()                
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
               

#if DEBUG
            builder.Logging.AddDebug();
#endif
            //->TODO: Convert this into an extension or
            //      Create de Dependency injection using the NuGet package.
            Bootstrap.Startup(builder);

            return builder.Build();
        }
    }
}
