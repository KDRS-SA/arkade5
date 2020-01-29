using System.Collections.Generic;
using Arkivverket.Arkade.Core.Base;
using Arkivverket.Arkade.Core.Testing.Noark5.Structure;

namespace Arkivverket.Arkade.Core.Testing.Noark5
{
    public class Noark5TestProvider : ITestProvider
    {
        public static List<uint> GetStructureTestIds()
        {
            return new List<uint>
            {
                N5_01_ValidateStructureFileExists._id.Number,
                N5_02_ValidateAddmlDataobjectsChecksums._id.Number,
                N5_03_ValidateXmlWithSchema._id.Number,
                N5_28_ValidateNumberOfDocumentfiles._id.Number,
            };
        }

        public List<IArkadeStructureTest> GetStructureTests()
        {
            return new List<IArkadeStructureTest>
            {
                new N5_01_ValidateStructureFileExists(),
                new N5_02_ValidateAddmlDataobjectsChecksums(),
                new N5_03_ValidateXmlWithSchema(),
                new N5_28_ValidateNumberOfDocumentfiles(),
            };
        }

        public List<IArkadeStructureTest> GetStructureTests(List<uint> testsToDo)
        {
            List<IArkadeStructureTest> applicableTests = new List<IArkadeStructureTest>();

            if (testsToDo.Contains(N5_01_ValidateStructureFileExists._id.Number)) applicableTests.Add(new N5_01_ValidateStructureFileExists());
            if (testsToDo.Contains(N5_02_ValidateAddmlDataobjectsChecksums._id.Number)) applicableTests.Add(new N5_02_ValidateAddmlDataobjectsChecksums());
            if (testsToDo.Contains(N5_03_ValidateXmlWithSchema._id.Number)) applicableTests.Add(new N5_03_ValidateXmlWithSchema());
            if (testsToDo.Contains(N5_28_ValidateNumberOfDocumentfiles._id.Number)) applicableTests.Add(new N5_28_ValidateNumberOfDocumentfiles());

            return applicableTests;
        }

        public List<INoark5Test> GetContentTests(Archive archive)
        {
            return new List<INoark5Test>
            {
                new N5_04_NumberOfArchives(),
                new N5_05_NumberOfArchiveParts(),
                new N5_06_StatusOfArchiveParts(),
                new N5_08_NumberOfClasses(),
                new N5_10_NumberOfFolders(archive),
                new N5_07_NumberOfClassificationSystems(),
                new N5_16_NumberOfRegistrations(),
                new N5_18_NumberOfRegistrationsPerYear(),
                new N5_21_NumberOfRegistrationsWithoutDocumentDescription(),
                new N5_23_NumberOfDocumentDescriptions(),
                new N5_24_NumberOfDocumentDescriptionsWithoutDocumentObject(),
                new N5_26_NumberOfDocumentObjects(),
                new N5_09_NumberOfClassesInMainClassificationSystemWithoutSubClassesFoldersOrRegistrations(),
                new N5_35_NumberOfCaseParts(archive),
                new N5_32_ControlDocumentFilesExists(archive),
                new N5_36_NumberOfComments(),
                new N5_37_NumberOfCrossReferences(),
                new N5_38_NumberOfPrecedents(),
                new N5_39_NumberOfCorrespondenceParts(),
                new N5_40_NumberOfDepreciations(),
                new N5_41_NumberOfDocumentFlows(),
                new N5_11_NumberOfFoldersPerYear(),
                new N5_14_NumberOfFoldersWithoutRegistrationsOrSubfolders(),
                new N5_27_FirstAndLastRegistrationCreationDates(),
                new N5_59_NumberOfJournalPosts(archive),
                new N5_13_NumberOfFoldersPerClass(),
                new N5_33_DocumentfilesReferenceControl(archive),
                new N5_17_NumberOfEachJournalPostType(),
                new N5_20_NumberOfRegistrationsPerClass(),
                new N5_22_NumberOfEachJournalStatus(),
                new N5_15_NumberOfEachCaseFolderStatus(),
                new N5_25_NumberOfEachDocumentStatus(),
                new N5_29_NumberOfEachDocumentFormat(),
                new N5_34_NumberOfMultiReferencedDocumentFiles(),
                new N5_12_ControlNoSuperclassesHasFolders(),
                new N5_43_NumberOfClassifications(),
                new N5_44_NumberOfDisposalResolutions(archive),
                new N5_45_NumberOfDisposalsExecuted(archive),
                new N5_42_NumberOfRestrictions(archive),
                new N5_46_NumberOfConversions(),
                new N5_60_ArchiveStartAndEndDateControl(archive),
                new N5_61_NumberOfChangesLogged(archive),
                new N5_62_ChangeLogArchiveReferenceControl(archive),
                new N5_48_ArchivepartReferenceControl(),
                new N5_47_SystemIdUniqueControl(),
                new N5_19_ControlNoSuperclassesHasRegistrations(),
                new N5_51_ClassReferenceControl(),
                new N5_30_DocumentFilesChecksumControl(archive),
                new N5_63_ControlElementsHasContent(),
            };
        }

        public List<INoark5Test> GetContentTests(Archive archive, List<uint> testsToDo)
        {
            List<INoark5Test> applicableTests = new List<INoark5Test>();

            if (testsToDo.Contains(N5_04_NumberOfArchives._id.Number)) applicableTests.Add(new N5_04_NumberOfArchives());
            if (testsToDo.Contains(N5_05_NumberOfArchiveParts._id.Number)) applicableTests.Add(new N5_05_NumberOfArchiveParts());
            if (testsToDo.Contains(N5_06_StatusOfArchiveParts._id.Number)) applicableTests.Add(new N5_06_StatusOfArchiveParts());
            if (testsToDo.Contains(N5_08_NumberOfClasses._id.Number)) applicableTests.Add(new N5_08_NumberOfClasses());
            if (testsToDo.Contains(N5_10_NumberOfFolders._id.Number)) applicableTests.Add(new N5_10_NumberOfFolders(archive));
            if (testsToDo.Contains(N5_07_NumberOfClassificationSystems._id.Number)) applicableTests.Add(new N5_07_NumberOfClassificationSystems());
            if (testsToDo.Contains(N5_16_NumberOfRegistrations._id.Number)) applicableTests.Add(new N5_16_NumberOfRegistrations());
            if (testsToDo.Contains(N5_18_NumberOfRegistrationsPerYear._id.Number)) applicableTests.Add(new N5_18_NumberOfRegistrationsPerYear());
            if (testsToDo.Contains(N5_21_NumberOfRegistrationsWithoutDocumentDescription._id.Number)) applicableTests.Add(new N5_21_NumberOfRegistrationsWithoutDocumentDescription());
            if (testsToDo.Contains(N5_23_NumberOfDocumentDescriptions._id.Number)) applicableTests.Add(new N5_23_NumberOfDocumentDescriptions());
            if (testsToDo.Contains(N5_24_NumberOfDocumentDescriptionsWithoutDocumentObject._id.Number)) applicableTests.Add(new N5_24_NumberOfDocumentDescriptionsWithoutDocumentObject());
            if (testsToDo.Contains(N5_26_NumberOfDocumentObjects._id.Number)) applicableTests.Add(new N5_26_NumberOfDocumentObjects());
            if (testsToDo.Contains(N5_09_NumberOfClassesInMainClassificationSystemWithoutSubClassesFoldersOrRegistrations._id.Number)) applicableTests.Add(new N5_09_NumberOfClassesInMainClassificationSystemWithoutSubClassesFoldersOrRegistrations());
            if (testsToDo.Contains(new N5_35_NumberOfCaseParts(archive).GetId().Number)) applicableTests.Add(new N5_35_NumberOfCaseParts(archive));
            if (testsToDo.Contains(N5_32_ControlDocumentFilesExists._id.Number)) applicableTests.Add(new N5_32_ControlDocumentFilesExists(archive));
            if (testsToDo.Contains(N5_36_NumberOfComments._id.Number)) applicableTests.Add(new N5_36_NumberOfComments());
            if (testsToDo.Contains(N5_37_NumberOfCrossReferences._id.Number)) applicableTests.Add(new N5_37_NumberOfCrossReferences());
            if (testsToDo.Contains(N5_38_NumberOfPrecedents._id.Number)) applicableTests.Add(new N5_38_NumberOfPrecedents());
            if (testsToDo.Contains(N5_39_NumberOfCorrespondenceParts._id.Number)) applicableTests.Add(new N5_39_NumberOfCorrespondenceParts());
            if (testsToDo.Contains(N5_40_NumberOfDepreciations._id.Number)) applicableTests.Add(new N5_40_NumberOfDepreciations());
            if (testsToDo.Contains(N5_41_NumberOfDocumentFlows._id.Number)) applicableTests.Add(new N5_41_NumberOfDocumentFlows());
            if (testsToDo.Contains(N5_11_NumberOfFoldersPerYear._id.Number)) applicableTests.Add(new N5_11_NumberOfFoldersPerYear());
            if (testsToDo.Contains(N5_14_NumberOfFoldersWithoutRegistrationsOrSubfolders._id.Number)) applicableTests.Add(new N5_14_NumberOfFoldersWithoutRegistrationsOrSubfolders());
            if (testsToDo.Contains(N5_27_FirstAndLastRegistrationCreationDates._id.Number)) applicableTests.Add(new N5_27_FirstAndLastRegistrationCreationDates());
            if (testsToDo.Contains(N5_59_NumberOfJournalPosts._id.Number)) applicableTests.Add(new N5_59_NumberOfJournalPosts(archive));
            if (testsToDo.Contains(N5_13_NumberOfFoldersPerClass._id.Number)) applicableTests.Add(new N5_13_NumberOfFoldersPerClass());
            if (testsToDo.Contains(N5_33_DocumentfilesReferenceControl._id.Number)) applicableTests.Add(new N5_33_DocumentfilesReferenceControl(archive));
            if (testsToDo.Contains(N5_17_NumberOfEachJournalPostType._id.Number)) applicableTests.Add(new N5_17_NumberOfEachJournalPostType());
            if (testsToDo.Contains(N5_20_NumberOfRegistrationsPerClass._id.Number)) applicableTests.Add(new N5_20_NumberOfRegistrationsPerClass());
            if (testsToDo.Contains(N5_22_NumberOfEachJournalStatus._id.Number)) applicableTests.Add(new N5_22_NumberOfEachJournalStatus());
            if (testsToDo.Contains(N5_15_NumberOfEachCaseFolderStatus._id.Number)) applicableTests.Add(new N5_15_NumberOfEachCaseFolderStatus());
            if (testsToDo.Contains(N5_25_NumberOfEachDocumentStatus._id.Number)) applicableTests.Add(new N5_25_NumberOfEachDocumentStatus());
            if (testsToDo.Contains(N5_29_NumberOfEachDocumentFormat._id.Number)) applicableTests.Add(new N5_29_NumberOfEachDocumentFormat());
            if (testsToDo.Contains(N5_34_NumberOfMultiReferencedDocumentFiles._id.Number)) applicableTests.Add(new N5_34_NumberOfMultiReferencedDocumentFiles());
            if (testsToDo.Contains(N5_12_ControlNoSuperclassesHasFolders._id.Number)) applicableTests.Add(new N5_12_ControlNoSuperclassesHasFolders());
            if (testsToDo.Contains(N5_43_NumberOfClassifications._id.Number)) applicableTests.Add(new N5_43_NumberOfClassifications());
            if (testsToDo.Contains(N5_44_NumberOfDisposalResolutions._id.Number)) applicableTests.Add(new N5_44_NumberOfDisposalResolutions(archive));
            if (testsToDo.Contains(N5_45_NumberOfDisposalsExecuted._id.Number)) applicableTests.Add(new N5_45_NumberOfDisposalsExecuted(archive));
            if (testsToDo.Contains(N5_42_NumberOfRestrictions._id.Number)) applicableTests.Add(new N5_42_NumberOfRestrictions(archive));
            if (testsToDo.Contains(N5_46_NumberOfConversions._id.Number)) applicableTests.Add(new N5_46_NumberOfConversions());
            if (testsToDo.Contains(N5_60_ArchiveStartAndEndDateControl._id.Number)) applicableTests.Add(new N5_60_ArchiveStartAndEndDateControl(archive));
            if (testsToDo.Contains(N5_61_NumberOfChangesLogged._id.Number)) applicableTests.Add(new N5_61_NumberOfChangesLogged(archive));
            if (testsToDo.Contains(N5_62_ChangeLogArchiveReferenceControl._id.Number)) applicableTests.Add(new N5_62_ChangeLogArchiveReferenceControl(archive));
            if (testsToDo.Contains(N5_48_ArchivepartReferenceControl._id.Number)) applicableTests.Add(new N5_48_ArchivepartReferenceControl());
            if (testsToDo.Contains(N5_47_SystemIdUniqueControl._id.Number)) applicableTests.Add(new N5_47_SystemIdUniqueControl());
            if (testsToDo.Contains(N5_19_ControlNoSuperclassesHasRegistrations._id.Number)) applicableTests.Add(new N5_19_ControlNoSuperclassesHasRegistrations());
            if (testsToDo.Contains(N5_51_ClassReferenceControl._id.Number)) applicableTests.Add(new N5_51_ClassReferenceControl());
            if (testsToDo.Contains(N5_30_DocumentFilesChecksumControl._id.Number)) applicableTests.Add(new N5_30_DocumentFilesChecksumControl(archive));
            if (testsToDo.Contains(N5_63_ControlElementsHasContent._id.Number)) applicableTests.Add(new N5_63_ControlElementsHasContent());

            return applicableTests;
        }

        public static List<uint> GetContentTestIds(Archive archive)
        {
            return new List<uint>
            {
                N5_04_NumberOfArchives._id.Number,
                N5_05_NumberOfArchiveParts._id.Number,
                N5_06_StatusOfArchiveParts._id.Number,
                N5_08_NumberOfClasses._id.Number,
                N5_10_NumberOfFolders._id.Number,
                N5_07_NumberOfClassificationSystems._id.Number,
                N5_16_NumberOfRegistrations._id.Number,
                N5_18_NumberOfRegistrationsPerYear._id.Number,
                N5_21_NumberOfRegistrationsWithoutDocumentDescription._id.Number,
                N5_23_NumberOfDocumentDescriptions._id.Number,
                N5_24_NumberOfDocumentDescriptionsWithoutDocumentObject._id.Number,
                N5_26_NumberOfDocumentObjects._id.Number,
                N5_09_NumberOfClassesInMainClassificationSystemWithoutSubClassesFoldersOrRegistrations._id.Number,
                new N5_35_NumberOfCaseParts(archive).GetId().Number,
                N5_32_ControlDocumentFilesExists._id.Number,
                N5_36_NumberOfComments._id.Number,
                N5_37_NumberOfCrossReferences._id.Number,
                N5_38_NumberOfPrecedents._id.Number,
                N5_39_NumberOfCorrespondenceParts._id.Number,
                N5_40_NumberOfDepreciations._id.Number,
                N5_41_NumberOfDocumentFlows._id.Number,
                N5_11_NumberOfFoldersPerYear._id.Number,
                N5_14_NumberOfFoldersWithoutRegistrationsOrSubfolders._id.Number,
                N5_27_FirstAndLastRegistrationCreationDates._id.Number,
                N5_59_NumberOfJournalPosts._id.Number,
                N5_13_NumberOfFoldersPerClass._id.Number,
                N5_33_DocumentfilesReferenceControl._id.Number,
                N5_17_NumberOfEachJournalPostType._id.Number,
                N5_20_NumberOfRegistrationsPerClass._id.Number,
                N5_22_NumberOfEachJournalStatus._id.Number,
                N5_15_NumberOfEachCaseFolderStatus._id.Number,
                N5_25_NumberOfEachDocumentStatus._id.Number,
                N5_29_NumberOfEachDocumentFormat._id.Number,
                N5_34_NumberOfMultiReferencedDocumentFiles._id.Number,
                N5_12_ControlNoSuperclassesHasFolders._id.Number,
                N5_43_NumberOfClassifications._id.Number,
                N5_44_NumberOfDisposalResolutions._id.Number,
                N5_45_NumberOfDisposalsExecuted._id.Number,
                N5_42_NumberOfRestrictions._id.Number,
                N5_46_NumberOfConversions._id.Number,
                N5_60_ArchiveStartAndEndDateControl._id.Number,
                N5_61_NumberOfChangesLogged._id.Number,
                N5_62_ChangeLogArchiveReferenceControl._id.Number,
                N5_48_ArchivepartReferenceControl._id.Number,
                N5_47_SystemIdUniqueControl._id.Number,
                N5_19_ControlNoSuperclassesHasRegistrations._id.Number,
                N5_51_ClassReferenceControl._id.Number,
                N5_30_DocumentFilesChecksumControl._id.Number,
                N5_63_ControlElementsHasContent._id.Number,
            };
        }
    }
}