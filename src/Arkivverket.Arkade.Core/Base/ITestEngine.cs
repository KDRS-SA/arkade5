using System;
using System.Collections.Generic;

namespace Arkivverket.Arkade.Core.Base
{
    public interface ITestEngine
    {
        TestSuite RunTestsOnArchive(TestSession testSession);

        TestSuite RunTestsOnArchive(TestSession testSession, List<uint> testsToDo);
    }
}