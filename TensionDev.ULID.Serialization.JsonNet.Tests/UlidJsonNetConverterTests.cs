using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace TensionDev.ULID.Serialization.JsonNet.Tests
{
    public class UlidJsonNetConverterTests : IDisposable
    {
        private bool disposedValue;

        private readonly UlidJsonNetConverter _converter;

        public UlidJsonNetConverterTests()
        {
            _converter = new UlidJsonNetConverter();
        }

        [Theory]
        [InlineData("00000000000000000000000000")]
        [InlineData("7ZZZZZZZZZZZZZZZZZZZZZZZZZ")]
        [InlineData("01ARZ3NDEKTSV4RRFFQ69G5FAV")]
        public void TestReadJson(string validUlidString)
        {
            // Arrange
            var readerMock = new Mock<JsonReader>(MockBehavior.Strict);
            readerMock.SetupGet(r => r.TokenType).Returns(JsonToken.String);
            readerMock.SetupGet(r => r.Value).Returns((object)validUlidString);

            var converter = new UlidJsonNetConverter();

            // existingValue must be non-nullable; obtain an instance via Parse to satisfy parameter constraints.
            Ulid existingValue = Ulid.Parse(validUlidString);
            var serializer = new JsonSerializer();

            // Act
            Ulid result = converter.ReadJson(readerMock.Object, typeof(Ulid), existingValue, hasExistingValue: false, serializer: serializer);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(validUlidString, result.ToString());
        }

        [Theory]
        [InlineData("00000000000000000000000000")]
        [InlineData("7ZZZZZZZZZZZZZZZZZZZZZZZZZ")]
        [InlineData("01ARZ3NDEKTSV4RRFFQ69G5FAV")]
        public void TestWriteJson(string validUlidString)
        {
            var writerMock = new Mock<JsonWriter>(MockBehavior.Strict);
            object? capturedValue = null;
            // Setup to accept any object and capture it
            writerMock.Setup(w => w.WriteValue(It.IsAny<string?>()))
                      .Callback<string?>(v => capturedValue = v)
                      .Verifiable();

            var serializerMock = new Mock<JsonSerializer>(MockBehavior.Loose);

            TensionDev.ULID.Ulid value = TensionDev.ULID.Ulid.Parse(validUlidString);

            // Act
            _converter.WriteJson(writerMock.Object, value, serializerMock.Object);

            // Assert
            writerMock.Verify(w => w.WriteValue(It.IsAny<string?>()), Times.Once, "WriteValue should be called exactly once.");
            // Ensure the captured value matches the value.ToString() result
            var expected = value.ToString();
            Assert.NotNull(capturedValue);
            Assert.Equal(expected, capturedValue?.ToString());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UlidJsonNetConverterTests()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}