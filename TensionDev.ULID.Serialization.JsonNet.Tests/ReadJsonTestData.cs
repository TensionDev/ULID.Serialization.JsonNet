using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TensionDev.ULID.Serialization.JsonNet.Tests
{
    public class ReadJsonTestData : TheoryData<JsonToken, Object, Boolean>
    {
        public ReadJsonTestData()
        {
            Add(JsonToken.String, "00000000000000000000000000", false);
            Add(JsonToken.String, "7ZZZZZZZZZZZZZZZZZZZZZZZZZ", false);
            Add(JsonToken.String, "01ARZ3NDEKTSV4RRFFQ69G5FAV", false);

            Add(JsonToken.Boolean, "01ARZ3NDEKTSV4RRFFQ69G5FAV", true);
            Add(JsonToken.Bytes, "01ARZ3NDEKTSV4RRFFQ69G5FAV", true);
            Add(JsonToken.Date, "01ARZ3NDEKTSV4RRFFQ69G5FAV", true);
            Add(JsonToken.Float, "01ARZ3NDEKTSV4RRFFQ69G5FAV", true);
            Add(JsonToken.Integer, "01ARZ3NDEKTSV4RRFFQ69G5FAV", true);
            Add(JsonToken.Null, "01ARZ3NDEKTSV4RRFFQ69G5FAV", true);
            Add(JsonToken.Raw, "01ARZ3NDEKTSV4RRFFQ69G5FAV", true);
            Add(JsonToken.Undefined, "01ARZ3NDEKTSV4RRFFQ69G5FAV", true);

            Add(JsonToken.String, Int16.MaxValue, true);
            Add(JsonToken.String, Int32.MaxValue, true);
            Add(JsonToken.String, Int64.MaxValue, true);
            Add(JsonToken.String, Single.MaxValue, true);
            Add(JsonToken.String, Double.MaxValue, true);
            Add(JsonToken.String, DateTime.MinValue, true);
            Add(JsonToken.String, DateTime.MaxValue, true);
            Add(JsonToken.String, Guid.Empty, true);

            Add(JsonToken.Raw, new Object(), true);
        }
    }
}
