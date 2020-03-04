using CommandLine;

namespace Arkivverket.Arkade.CLI
{
    [Verb("metadata")]
    public class MetadataOptions : Options
    {
        [Option('g', "generate-metadata-example", HelpText = "Generate example metadata file. Argument is output file name.", Required = true)]
        public string GenerateMetadataExample { get; set; }
    }
}
