// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.AzureStack.Management.Storage.Admin.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// The result of the container migration.
    /// </summary>
    public partial class MigrationResult
    {
        /// <summary>
        /// Initializes a new instance of the MigrationResult class.
        /// </summary>
        public MigrationResult()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the MigrationResult class.
        /// </summary>
        /// <param name="jobId">The migration job ID.</param>
        /// <param name="sourceShareName">The name of the source storage
        /// share.</param>
        /// <param name="storageAccountName">The storage account name.</param>
        /// <param name="containerName">The name of the container to be
        /// migrated.</param>
        /// <param name="destinationShareName">The name of the destination
        /// storage share.</param>
        /// <param name="migrationStatus">The migration status. Possible values
        /// include: 'Active', 'Paused', 'Deleted', 'Rollback', 'Complete',
        /// 'Canceled', 'Failed', 'All'</param>
        /// <param name="subEntitiesCompleted">The number of entities which
        /// have been migrated.</param>
        /// <param name="subEntitiesFailed">The number of entities which failed
        /// in migration.</param>
        /// <param name="failureReason">The migration failure reason.</param>
        public MigrationResult(string jobId = default(string), string sourceShareName = default(string), string storageAccountName = default(string), string containerName = default(string), string destinationShareName = default(string), string migrationStatus = default(string), long? subEntitiesCompleted = default(long?), long? subEntitiesFailed = default(long?), string failureReason = default(string))
        {
            JobId = jobId;
            SourceShareName = sourceShareName;
            StorageAccountName = storageAccountName;
            ContainerName = containerName;
            DestinationShareName = destinationShareName;
            MigrationStatus = migrationStatus;
            SubEntitiesCompleted = subEntitiesCompleted;
            SubEntitiesFailed = subEntitiesFailed;
            FailureReason = failureReason;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets the migration job ID.
        /// </summary>
        [JsonProperty(PropertyName = "jobId")]
        public string JobId { get; private set; }

        /// <summary>
        /// Gets the name of the source storage share.
        /// </summary>
        [JsonProperty(PropertyName = "sourceShareName")]
        public string SourceShareName { get; private set; }

        /// <summary>
        /// Gets the storage account name.
        /// </summary>
        [JsonProperty(PropertyName = "storageAccountName")]
        public string StorageAccountName { get; private set; }

        /// <summary>
        /// Gets the name of the container to be migrated.
        /// </summary>
        [JsonProperty(PropertyName = "containerName")]
        public string ContainerName { get; private set; }

        /// <summary>
        /// Gets the name of the destination storage share.
        /// </summary>
        [JsonProperty(PropertyName = "destinationShareName")]
        public string DestinationShareName { get; private set; }

        /// <summary>
        /// Gets the migration status. Possible values include: 'Active',
        /// 'Paused', 'Deleted', 'Rollback', 'Complete', 'Canceled', 'Failed',
        /// 'All'
        /// </summary>
        [JsonProperty(PropertyName = "migrationStatus")]
        public string MigrationStatus { get; private set; }

        /// <summary>
        /// Gets the number of entities which have been migrated.
        /// </summary>
        [JsonProperty(PropertyName = "subEntitiesCompleted")]
        public long? SubEntitiesCompleted { get; private set; }

        /// <summary>
        /// Gets the number of entities which failed in migration.
        /// </summary>
        [JsonProperty(PropertyName = "subEntitiesFailed")]
        public long? SubEntitiesFailed { get; private set; }

        /// <summary>
        /// Gets the migration failure reason.
        /// </summary>
        [JsonProperty(PropertyName = "failureReason")]
        public string FailureReason { get; private set; }

    }
}