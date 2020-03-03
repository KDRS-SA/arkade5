using System.Collections.Generic;
using System.Linq;
using CommandLine;
using FluentAssertions;
using Xunit;

namespace Arkivverket.Arkade.CLI.Tests
{
    public class CommandTests
    {
        [Fact]
        public void CommonInputErrorIsHandled()
        {
            var argsWithExpectedError = new List<(string, ErrorType)>
            {
                ("-a someArchive -t noark5 -m metadata.json -p tmp -o output -x value", ErrorType.UnknownOptionError),
                //("-a someArchive -t noark5 -m metadata.json -p tmp -o output", ErrorType.NoVerbSelectedError),
                //("", ErrorType.),
                //("", ErrorType.)
            };

            foreach ((string args, ErrorType expectedError) in argsWithExpectedError)
            {
                GetParseErrors<CommandLineOptions>(args).Should().Contain(expectedError);
            }
        }

        private static IEnumerable<ErrorType> GetParseErrors<T>(string arguments)
        {
            IEnumerable<string> args = arguments.Split(' ');

            IEnumerable<ErrorType> parseErrors = null;

            Parser.Default.ParseArguments<T>(args).WithNotParsed(errors =>
            {
                parseErrors = errors.Select(e => e.Tag);
            });

            return parseErrors;
        }
    }
}
