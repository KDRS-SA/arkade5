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
                ("-a archive", ErrorType.BadVerbSelectedError),
                ("wrong -a archive", ErrorType.BadVerbSelectedError),

                ("process -t type -m metadata -p directory -o directory", ErrorType.MissingRequiredOptionError),
                ("process -a archive -m metadata -p directory -o directory", ErrorType.MissingRequiredOptionError),
                ("process -a archive -t type -p directory -o directory", ErrorType.MissingRequiredOptionError),
                ("process -a archive -t type -m metadata -o directory", ErrorType.MissingRequiredOptionError),
                ("process -a archive -t type -m metadata -p directory", ErrorType.MissingRequiredOptionError),
                ("metadata -p process-dir", ErrorType.MissingRequiredOptionError),

                ("process -a -t type -m metadata -p directory -o directory", ErrorType.MissingValueOptionError),
                ("process -a archive -t -m metadata -p directory -o directory", ErrorType.MissingValueOptionError),
                ("process -a archive -t type -m -p directory -o directory", ErrorType.MissingValueOptionError),
                ("process -a archive -t type -m metadata -p -o directory", ErrorType.MissingValueOptionError),
                ("process -a archive -t type -m metadata -p process-dir -o", ErrorType.MissingValueOptionError),
                ("metadata -g -p process-dir", ErrorType.MissingValueOptionError),

                ("process -s", ErrorType.MissingValueOptionError),
                ("process -i", ErrorType.MissingValueOptionError),

                ("process -a archive -g metadata", ErrorType.UnknownOptionError),
                ("metadata -g metadata -a archive", ErrorType.UnknownOptionError),

                ("process -a archive -a archive", ErrorType.RepeatedOptionError),
                ("metadata -g metadata -g metadata", ErrorType.RepeatedOptionError),
            };

            foreach ((string command, ErrorType expectedError) in argsWithExpectedError)
            {
                IEnumerable<ErrorType> parseErrors = null;

                Program.ParseArguments(command.Split(' ')).WithNotParsed(errors =>
                {
                    parseErrors = errors.Select(e => e.Tag);
                });

                parseErrors.Should().Contain(expectedError);
            }
        }
    }
}
