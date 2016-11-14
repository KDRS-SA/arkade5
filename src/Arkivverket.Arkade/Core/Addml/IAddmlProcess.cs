﻿namespace Arkivverket.Arkade.Core.Addml
{
    /// <summary>
    ///     All ADDML processes must implement this interface.
    ///     The different Run-methods allows the process to do various tasks during parsing of a file, a record or a field.
    /// </summary>
    public interface IAddmlProcess
    {
        /// <summary>
        ///     Returns the process name as used in the ADDML xml file.
        /// </summary>
        /// <returns></returns>
        string GetName();

        /// <summary>
        ///     Returns the description of the process.
        /// </summary>
        /// <returns></returns>
        string GetDescription();

        /// <summary>
        ///     Invoked on the process when starting to read data from a new file
        /// </summary>
        /// <param name="flatFile">the file to process</param>
        void Run(FlatFile flatFile);

        /// <summary>
        ///     Invoked on the process when a new record is read from the file
        /// </summary>
        /// <param name="record">the record to process</param>
        void Run(Record record);

        /// <summary>
        ///     Invoked on the process when a new field is read from a record
        /// </summary>
        /// <param name="field">the field to process</param>
        void Run(Field field);

        /// <summary>
        ///     Let the process clean up when last line of a file has been read.
        /// </summary>
        void EndOfFile();

        /// <summary>
        ///     Return results from the process.
        /// </summary>
        /// <returns></returns>
        TestRun GetTestRun();
    }
}