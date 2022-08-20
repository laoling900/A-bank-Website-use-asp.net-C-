using BankWebApp.Models;
using Newtonsoft.Json;

namespace BankWebApp.Converters;

    public class AccountTypeStringToAccountTypeEnumConverter: JsonConverter<AccountType>
    {

    public override void WriteJson(JsonWriter writer, AccountType value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override AccountType ReadJson(JsonReader reader, Type objectType, AccountType existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var type=(string)reader.Value;

        return type switch
        {
            "S" => AccountType.Saving,
            "C" => AccountType.Checking,
            _ => throw new InvalidOperationException($"Unknown AccountType:{type}")
        };
    }

}

