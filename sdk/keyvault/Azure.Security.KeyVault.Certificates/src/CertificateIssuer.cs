﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;

namespace Azure.Security.KeyVault.Certificates
{
    /// <summary>
    /// A certificate issuer used to sign certificates managed by Azure Key Vault
    /// </summary>
    public class CertificateIssuer : IJsonDeserializable, IJsonSerializable
    {
        private const string CredentialsPropertyName = "credentials";
        private const string OrgDetailsPropertyName = "org_details";
        private const string AttributesPropertyName = "attributes";
        private const string AccountIdPropertyName = "account_id";
        private const string PasswordPropertyName = "pwd";
        private const string OrganizationIdPropertyName = "id";
        private const string AdminDetailsPropertyName = "admin_details";
        private const string CreatedPropertyName = "created";
        private const string UpdatedPropertyName = "updated";
        private const string EnabledPropertyName = "enabled";

        private static readonly JsonEncodedText s_credentialsPropertyNameBytes = JsonEncodedText.Encode(CredentialsPropertyName);
        private static readonly JsonEncodedText s_orgDetailsPropertyNameBytes = JsonEncodedText.Encode(OrgDetailsPropertyName);
        private static readonly JsonEncodedText s_attributesPropertyNameBytes = JsonEncodedText.Encode(AttributesPropertyName);
        private static readonly JsonEncodedText s_enabledPropertyNameBytes = JsonEncodedText.Encode(EnabledPropertyName);
        private static readonly JsonEncodedText s_accountIdPropertyNameBytes = JsonEncodedText.Encode(AccountIdPropertyName);
        private static readonly JsonEncodedText s_passwordPropertyNameBytes = JsonEncodedText.Encode(PasswordPropertyName);
        private static readonly JsonEncodedText s_organizationIdPropertyNameBytes = JsonEncodedText.Encode(OrganizationIdPropertyName);
        private static readonly JsonEncodedText s_adminDetailsPropertyNameBytes = JsonEncodedText.Encode(AdminDetailsPropertyName);

        private List<AdministratorContact> _administrators;

        internal CertificateIssuer(IssuerProperties properties = null)
        {
            Properties = properties ?? new IssuerProperties();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateContact"/> class with the given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the issuer, including values from <see cref="WellKnownIssuerNames"/>.</param>
        public CertificateIssuer(string name)
        {
            Properties = new IssuerProperties(name);
        }

        /// <summary>
        /// The unique identifier of the certificate issuer
        /// </summary>
        public Uri Id => Properties.Id;

        /// <summary>
        /// The name of the certificate issuer
        /// </summary>
        public string Name => Properties.Name;

        /// <summary>
        /// The account identifier or username used to authenticate to the certificate issuer
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// The password or key used to authenticate to the certificate issuer
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The organizational identifier for the issuer
        /// </summary>
        public string OrganizationId { get; set; }

        /// <summary>
        /// A list of contacts who administrate the certificate issuer account
        /// </summary>
        public IList<AdministratorContact> Administrators => LazyInitializer.EnsureInitialized(ref _administrators);

        /// <summary>
        /// The time the issuer was created in UTC
        /// </summary>
        public DateTimeOffset? CreatedOn { get; internal set; }

        /// <summary>
        /// The last modified time of the issuer in UTC
        /// </summary>
        public DateTimeOffset? UpdatedOn { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether the issuer can currently be used to issue certificates. If null, the server default will be used.
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// Gets or sets the attributes of the <see cref="CertificateIssuer"/>.
        /// </summary>
        public IssuerProperties Properties { get; }

        internal virtual void ReadProperty(JsonProperty prop)
        {
            switch (prop.Name)
            {
                case CredentialsPropertyName:
                    ReadCredentialsProperties(prop.Value);
                    break;

                case OrgDetailsPropertyName:
                    ReadOrgDetailsProperties(prop.Value);
                    break;

                case AttributesPropertyName:
                    ReadAttributeProperties(prop.Value);
                    break;

                default:
                    Properties.ReadProperty(prop);
                    break;
            }
        }

        private void ReadCredentialsProperties(JsonElement json)
        {
            foreach (JsonProperty prop in json.EnumerateObject())
            {
                switch (prop.Name)
                {
                    case AccountIdPropertyName:
                        AccountId = prop.Value.GetString();
                        break;

                    case PasswordPropertyName:
                        Password = prop.Value.GetString();
                        break;
                }
            }
        }

        private void ReadOrgDetailsProperties(JsonElement json)
        {
            foreach (JsonProperty prop in json.EnumerateObject())
            {
                switch (prop.Name)
                {
                    case OrganizationIdPropertyName:
                        OrganizationId = prop.Value.GetString();
                        break;

                    case AdminDetailsPropertyName:
                        foreach (JsonElement elem in prop.Value.EnumerateArray())
                        {
                            var admin = new AdministratorContact();
                            admin.ReadProperties(elem);
                            Administrators.Add(admin);
                        }
                        Password = prop.Value.GetString();
                        break;
                }
            }
        }

        private void ReadAttributeProperties(JsonElement json)
        {
            foreach (JsonProperty prop in json.EnumerateObject())
            {
                switch (prop.Name)
                {
                    case EnabledPropertyName:
                        Enabled = prop.Value.GetBoolean();
                        break;

                    case CreatedPropertyName:
                        CreatedOn = DateTimeOffset.FromUnixTimeSeconds(prop.Value.GetInt64());
                        break;

                    case UpdatedPropertyName:
                        UpdatedOn = DateTimeOffset.FromUnixTimeSeconds(prop.Value.GetInt64());
                        break;
                }
            }
        }

        internal virtual void WriteProperties(Utf8JsonWriter json)
        {
            Properties.WriteProperties(json);

            if (!string.IsNullOrEmpty(AccountId) || !string.IsNullOrEmpty(Password))
            {
                json.WriteStartObject(s_credentialsPropertyNameBytes);

                WriteCredentialsProperties(json);

                json.WriteEndObject();
            }

            if (!string.IsNullOrEmpty(OrganizationId) || !_administrators.IsNullOrEmpty())
            {
                json.WriteStartObject(s_orgDetailsPropertyNameBytes);

                WriteOrgDetailsProperties(json);

                json.WriteEndObject();
            }

            if (Enabled.HasValue)
            {
                json.WriteStartObject(s_attributesPropertyNameBytes);

                json.WriteBoolean(s_enabledPropertyNameBytes, Enabled.Value);

                json.WriteEndObject();
            }
        }

        private void WriteCredentialsProperties(Utf8JsonWriter json)
        {
            if (!string.IsNullOrEmpty(AccountId))
            {
                json.WriteString(s_accountIdPropertyNameBytes, AccountId);
            }

            if (!string.IsNullOrEmpty(Password))
            {
                json.WriteString(s_passwordPropertyNameBytes, Password);
            }
        }

        private void WriteOrgDetailsProperties(Utf8JsonWriter json)
        {
            if (!string.IsNullOrEmpty(OrganizationId))
            {
                json.WriteString(s_organizationIdPropertyNameBytes, AccountId);
            }

            if (!_administrators.IsNullOrEmpty())
            {
                json.WriteStartArray(s_adminDetailsPropertyNameBytes);

                foreach (AdministratorContact admin in _administrators)
                {
                    json.WriteStartObject();

                    admin.WriteProperties(json);

                    json.WriteEndObject();
                }

                json.WriteEndArray();
            }
        }

        void IJsonDeserializable.ReadProperties(JsonElement json)
        {
            foreach (JsonProperty prop in json.EnumerateObject())
            {
                ReadProperty(prop);
            }
        }

        void IJsonSerializable.WriteProperties(Utf8JsonWriter json) => WriteProperties(json);
    }
}