// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.Network.Models
{
    public partial class ConnectionMonitorSuccessThreshold : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (ChecksFailedPercent != null)
            {
                writer.WritePropertyName("checksFailedPercent");
                writer.WriteNumberValue(ChecksFailedPercent.Value);
            }
            if (RoundTripTimeMs != null)
            {
                writer.WritePropertyName("roundTripTimeMs");
                writer.WriteNumberValue(RoundTripTimeMs.Value);
            }
            writer.WriteEndObject();
        }

        internal static ConnectionMonitorSuccessThreshold DeserializeConnectionMonitorSuccessThreshold(JsonElement element)
        {
            int? checksFailedPercent = default;
            float? roundTripTimeMs = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("checksFailedPercent"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    checksFailedPercent = property.Value.GetInt32();
                    continue;
                }
                if (property.NameEquals("roundTripTimeMs"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    roundTripTimeMs = property.Value.GetSingle();
                    continue;
                }
            }
            return new ConnectionMonitorSuccessThreshold(checksFailedPercent, roundTripTimeMs);
        }
    }
}
