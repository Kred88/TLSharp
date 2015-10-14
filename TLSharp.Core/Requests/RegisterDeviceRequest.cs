using System;
using System.IO;
using TLSharp.Core.MTProto;
using TLSharp.Core.Utils;

namespace TLSharp.Core.Requests
{
	public class RegisterDeviceRequest : MTProtoRequest
	{
		private int _token_type;
        private string _token;
        private string _device_model;
        private string _system_version;
        private string _app_version;
        private bool _app_sandbox;
        private string _lang_code;

        public bool result;
        
		public RegisterDeviceRequest(int token_type, string token, string device_model, string system_version, string app_version, bool app_sandbox, string lang_code)
		{
            _token_type = token_type;
			_token = token;
            _device_model = device_model;
            _system_version = system_version;
            _app_version = app_version;
            _app_sandbox = app_sandbox;
            _lang_code = lang_code;
		}

		public override void OnSend(BinaryWriter writer)
		{
			writer.Write(0x446c712c); // registerDevice
            writer.Write(_token_type); 
            Serializers.String.write(writer, _token);
            Serializers.String.write(writer, _device_model);
            Serializers.String.write(writer, _system_version);
            Serializers.String.write(writer, _app_version);
            writer.Write(_app_sandbox ? 0x997275b5 : 0xbc799737);
            Serializers.String.write(writer, _lang_code);
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
