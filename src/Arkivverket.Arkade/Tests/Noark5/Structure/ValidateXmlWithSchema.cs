using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Arkivverket.Arkade.Core;

namespace Arkivverket.Arkade.Tests.Noark5.Structure
{
    /// <summary>
    ///     Validates that the XML is valid with regards to the XML schema. In this case the ADDML schema.
    /// </summary>
    public class ValidateXmlWithSchema : BaseTest
    {
        public ValidateXmlWithSchema(IArchiveContentReader archiveReader) : base(TestType.Structure, archiveReader)
        {
        }

        public override string GetName()
        {
            return GetType().Name;
        }

        protected override void Test(Archive archive)
        {
            try
            {
                using (var validationReader = XmlReader.Create(ArchiveReader.GetStructureContentAsStream(archive), SetupXmlValidation()))
                {
                    while (validationReader.Read())
                    {
                    }
                }
                TestSuccess($"Validated XML file {archive.GetStructureDescriptionFileName()} with ADDML schema.");
            }
            catch (Exception e)
            {
                TestError($"Error while validating xml [{archive.GetStructureDescriptionFileName()}] with ADDML schema: {e.Message}");
            }
        }

        private static XmlReaderSettings SetupXmlValidation()
        {
            var settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += delegate(object sender, ValidationEventArgs vargs) { throw vargs.Exception; };
            settings.Schemas.Add("http://www.arkivverket.no/standarder/addml", GetPathToAddmlSchema());
            return settings;
        }

        private static string GetPathToAddmlSchema()
        {
            return AppDomain.CurrentDomain.BaseDirectory
                   + Path.DirectorySeparatorChar
                   + "ExternalModels"
                   + Path.DirectorySeparatorChar
                   + "xsd"
                   + Path.DirectorySeparatorChar
                   + "addml.xsd";
        }

    }
}