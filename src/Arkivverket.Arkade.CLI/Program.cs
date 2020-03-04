using System;
using System.Collections.Generic;
using System.IO;
using Arkivverket.Arkade.Core.Base;
using Arkivverket.Arkade.Core.Util;
using CommandLine;
using RestSharp.Extensions;
using Serilog;

namespace Arkivverket.Arkade.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ArkadeProcessingArea.SetupTemporaryLogsDirectory();

            ConfigureLogging(); // Configured with temporary log directory

            ParseArguments(args)
                .WithParsed<ProcessOptions>(RunOptionsAndReturnExitCode)
                .WithParsed<MetadataOptions>(RunMetadataOptionsAndReturnExitCode)
                .WithNotParsed(HandleParseError);
        }

        public static ParserResult<object> ParseArguments(IEnumerable<string> args)
        {
            return Parser.Default.ParseArguments<ProcessOptions, MetadataOptions>(args);
        }

        private static void RunOptionsAndReturnExitCode(ProcessOptions options)
        {
            ArkadeProcessingArea.Establish(options.ProcessingArea); // Removes temporary log directory

            ConfigureLogging(); // Re-configured with log directory within processing area

                if (ValidArgumentsForTesting(options))
                {
                    new CommandLineRunner().Run(options);
                }
                else
                {
                    Console.WriteLine(options.GetUsage());
                }
        }

        private static void RunMetadataOptionsAndReturnExitCode(MetadataOptions options)
        {
            ArkadeProcessingArea.Establish(options.ProcessingArea); // Removes temporary log directory

            ConfigureLogging(); // Re-configured with log directory within processing area

            if (ValidArgumentsForMetadataCreation(options))
            {
                new MetadataExampleGenerator().Generate(options.GenerateMetadataExample);
            }
        }

        private static void HandleParseError(IEnumerable<Error> errors)
        {
            foreach (Error error in errors)
                Log.Error(error.ToString());
        }

        private static bool ValidArgumentsForMetadataCreation(MetadataOptions options)
        {
            return !string.IsNullOrWhiteSpace(options.GenerateMetadataExample);
        }

        private static bool ValidArgumentsForTesting(ProcessOptions options)
        {
            return !string.IsNullOrWhiteSpace(options.Archive)
                   && !string.IsNullOrWhiteSpace(options.ArchiveType)
                   && (
                       !string.IsNullOrWhiteSpace(options.Skip) && options.Skip.Equals("packing")
                       || !string.IsNullOrWhiteSpace(options.MetadataFile)
                   )
                   && !string.IsNullOrWhiteSpace(options.ProcessingArea)
                   && !string.IsNullOrWhiteSpace(options.OutputDirectory);
        }

        private static void ConfigureLogging()
        {
            string systemLogFilePath = Path.Combine(
                ArkadeProcessingArea.LogsDirectory.ToString(),
                ArkadeConstants.SystemLogFileNameFormat
            );

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: OutputStrings.SystemLogOutputTemplateForConsole)
                .WriteTo.RollingFile(systemLogFilePath, outputTemplate: OutputStrings.SystemLogOutputTemplateForFile)
                .CreateLogger();
        }
    }
}