/*-
 * Copyright (c) 2020, 2022 Oracle and/or its affiliates. All rights reserved.
 *
 * Licensed under the Universal Permissive License v 1.0 as shown at
 *  https://oss.oracle.com/licenses/upl/
 */

namespace Oracle.NoSQL.SDK
{
    using System;
    using System.Threading;
    using static ValidateUtils;

    /// <summary>
    /// Represents options for table DDL passed to methods
    /// <see cref="M:Oracle.NoSQL.SDK.NoSQLClient.ExecuteTableDDLAsync*"/>
    /// <see cref="M:Oracle.NoSQL.SDK.NoSQLClient.ExecuteTableDDLWithCompletionAsync*"/>,
    /// <see cref="NoSQLClient.SetTableLimitsAsync"/> and
    /// <see cref="NoSQLClient.SetTableLimitsWithCompletionAsync"/>.
    /// </summary>
    /// <remarks>
    /// For properties not specified or <c>null</c>,
    /// appropriate defaults will be used as indicated below.
    /// </remarks>
    /// <example>
    /// Executing table DDL with provided <see cref="TableDDLOptions"/>.
    /// <code>
    /// var result = await client.ExecuteTableDDLWithCompletionAsync(
    ///     "CREATE TABLE MyTable(id LONG, name STRING, PRIMARY KEY(id)",
    ///     new TableDDLOptions
    ///     {
    ///         Compartment = "my_compartment",
    ///         Timeout = TimeSpan.FromSeconds(30),
    ///         TableLimits = new TableLimits(100, 200, 100)
    ///     });
    /// </code>
    /// </example>
    /// <seealso cref="M:Oracle.NoSQL.SDK.NoSQLClient.ExecuteTableDDLAsync*"/>
    /// <seealso cref="M:Oracle.NoSQL.SDK.NoSQLClient.ExecuteTableDDLWithCompletionAsync*"/>
    /// <seealso cref="NoSQLClient.SetTableLimitsAsync"/>
    /// <seealso cref="NoSQLClient.SetTableLimitsWithCompletionAsync"/>
    public class TableDDLOptions : IOptions
    {
        /// <inheritdoc cref="GetOptions.Compartment"/>
        public string Compartment { get; set; }

        /// <summary>
        /// Gets or sets the timeout for the operation.
        /// </summary>
        /// <remarks>
        /// For
        /// <see cref="M:Oracle.NoSQL.SDK.NoSQLClient.ExecuteTableDDLAsync*"/>
        /// and
        /// <see cref="NoSQLClient.SetTableLimitsAsync"/> it defaults to
        /// <see cref="NoSQLConfig.TableDDLTimeout"/>.  For
        /// <see cref="M:Oracle.NoSQL.SDK.NoSQLClient.ExecuteTableDDLWithCompletionAsync*"/>
        /// and
        /// <see cref="NoSQLClient.SetTableLimitsWithCompletionAsync"/>
        /// separate default timeouts are used for issuing the DDL operation
        /// and waiting for its completion, with
        /// values of <see cref="NoSQLConfig.TableDDLTimeout"/> and
        /// <see cref="NoSQLConfig.TablePollTimeout"/> correspondingly (the
        /// latter defaults to no timeout if
        /// <see cref="NoSQLConfig.TablePollTimeout"/> is not set).
        /// </remarks>
        /// <value>
        /// Operation timeout.  If set, must be a positive value.
        /// </value>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Cloud Service/Cloud Simulator only.  Gets or sets the table limits
        /// for the operation.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Valid only for <em>CREATE TABLE</em> operation
        /// executed via
        /// <see cref="NoSQLClient.ExecuteTableDDLAsync(string, TableDDLOptions, CancellationToken)"/>
        /// or
        /// <see cref="NoSQLClient.ExecuteTableDDLWithCompletionAsync(string, TableDDLOptions, CancellationToken)"/>.
        /// For <see cref="NoSQLClient.SetTableLimitsAsync"/> or
        /// <see cref="NoSQLClient.SetTableLimitsWithCompletionAsync"/>, supply
        /// table limits as separate parameter.
        /// </para>
        /// <para>
        /// Note that you may not specify table limits when creating a child
        /// table, because child/descendant tables share the table limits with
        /// their parent/top level ancestor table.
        /// </para>
        /// </remarks>
        /// <value>
        /// The table limits.
        /// </value>
        /// <seealso cref="NoSQLClient.SetTableLimitsAsync"/>
        public TableLimits TableLimits { get; set; }

        /// <summary>
        /// Gets or sets the poll delay for polling when asynchronously
        /// waiting for operation completion.
        /// </summary>
        /// <remarks>
        /// Applies only to
        /// <see cref="M:Oracle.NoSQL.SDK.NoSQLClient.ExecuteTableDDLWithCompletionAsync*"/>
        /// and <see cref="NoSQLClient.SetTableLimitsWithCompletionAsync"/>
        /// methods.  Defaults to <see cref="NoSQLConfig.TablePollDelay"/>
        /// </remarks>
        /// <value>
        /// Poll delay.  If set, must be a positive value and not greater than
        /// the timeout.
        /// </value>
        public TimeSpan? PollDelay { get; set; }

        void IOptions.Validate()
        {
            CheckPollParameters(Timeout, PollDelay, nameof(Timeout),
                nameof(PollDelay));
            TableLimits?.Validate();
        }
    }

}
