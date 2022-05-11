using System;
using System.Collections.Generic;
using System.Linq;

using Autodesk.Connectivity.WebServices;
using Autodesk.DataManagement.Client.Framework.Currency;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Entities;
using VaultDocumentImporter.Objects;

using static VaultDocumentImporter.Objects.ImportResult.eImportResultType;
using static VaultDocumentImporter.Enumerations;
using Folder = Autodesk.Connectivity.WebServices.Folder;
using static VaultDocumentImporter.Managers.VaultConnectionManager;

namespace VaultDocumentImporter.Extensions
{
    public static class VaultExtensions
    {

        public static File[] GetFilesByFolder(this Folder folder, Connection conn)
        {
            try
            {
                File[] files = DocSrv.GetLatestFilesByFolderId(folder.Id, true);
                return files;
            }
            catch (Exception ex)
            {
                ex.Assess();
                return null;
            }
        }

        internal static ImportResult UpdateFileProperties(FileIteration fileIteration, List<PropInstParamArray> propInstParamArrays, FilePathAbsolute vaultPath)
        {
            ImportResult impRslt = new ImportResult() { ItemName = fileIteration.EntityName };
            File wsFile = null;
            string extMessage = $"Vault Path: {vaultPath}/{fileIteration.EntityName}";
            try
            {
                wsFile = DocSrv.CheckoutFile(fileIteration.EntityIterationId, CheckoutFileOptions.Master, Environment.MachineName, "C:\\Temp\\Vault Document Importer\\", "Vault Document Importer: Checked out file to update properties.", out ByteArray bArry);
                DocSrv.UpdateFileProperties(new long[] { fileIteration.EntityMasterId }, propInstParamArrays.ToArray());
                FileMngr.CheckinFile(fileIteration, "Vault Document Importer: Updated Properties", false, null, null, false, fileIteration.EntityName, FileClassification.None, false, vaultPath);
                impRslt.SetResult(FileUpdatedSuccessfully, extMessage);
            }
            catch (Exception ex)
            {
                /* Potential errors
                 *  3933
                 *  8000
                 */
                if (fileIteration.EntityMasterId != -1)
                {
                    DocSrv.UndoCheckoutFile(fileIteration.EntityMasterId, out ByteArray dlTicket);
                }
                impRslt.SetResult(FilePropertyUpdateFailed, extMessage);
                ex.Assess();

            }
            return impRslt;
        }

        #region NOT IMPLEMENTED


        #endregion

    }
}
