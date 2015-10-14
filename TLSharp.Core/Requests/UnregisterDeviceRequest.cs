using System;
using System.IO;
using TLSharp.Core.MTProto;
using TLSharp.Core.Utils;

namespace TLSharp.Core.Requests
{
	public class UnregisterDeviceRequest : MTProtoRequest
	{
		private int _token_type;
        private string _token;

        public bool result;

		public UnregisterDeviceRequest(int token_type, string token)
		{
            _token_type = token_type;
			_token = token;
		}

		public override void OnSend(BinaryWriter writer)
		{
			writer.Write(0x65c55b40); // unregisterDevice
            writer.Write(_token_type); 
            Serializers.String.write(writer, _token);
		}

		public override void OnResponse(BinaryReader reader)
		{
            var boolTrue = 0x997275b5;
			//var code = reader.ReadUInt32(); 
			result = reader.ReadUInt32() == boolTrue;
		}

		public override void OnException(Exception exception)
		{
			throw new NotImplementedException();
		}

		public override bool Confirmed => true;
		public override bool Responded { get; }
	}
}
