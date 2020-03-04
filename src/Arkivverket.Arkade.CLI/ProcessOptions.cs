using CommandLine;
using CommandLine.Text;

namespace Arkivverket.Arkade.CLI
{
    [Verb("process")]
    public class ProcessOptions : Options
    {
        [Option('a', "archive", HelpText = "Archive directory or file (.tar) to process.", Required = true)]
        public string Archive { get; set; }

        [Option('t', "type", HelpText = "Archive type, valid values: noark3, noark5 or fagsystem", Required = true)]
        public string ArchiveType { get; set; }

        [Option('m', "metadata-file", HelpText = "File with metadata to include in package.", Required = true)]
        public string MetadataFile { get; set; }

        [Option('o', "output-directory", HelpText = "Directory to place processing results.", Required = true)]
        public string OutputDirectory { get; set; }

        [Option('s', "skip", HelpText = "Optional. Valid values: testing, packing")]
        public string Skip { get; set; }

        [Option('i', "information-package-type", HelpText = "Optional. Valid values: SIP, AIP. Default: SIP")]
        public string InformationPackageType { get; set; }

        internal string GetUsage()
        {
            var result = new Parser().ParseArguments<ProcessOptions>("".Split());

            var helptext = HelpText.AutoBuild(result, help =>
            {
                help.AddOptions(result);
                return help;
            }, example =>
            {
                return example;
            });

            return helptext;
        }
    }
}
