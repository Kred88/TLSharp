using System;
using System.Collections.Generic;
using System.IO;
using TLSharp.Core.MTProto;
using TLSharp.Core.Utils;

namespace TLSharp.Core.Requests
{
	public class GetConfigRequest : MTProtoRequest
	{
        public Config configuration;


		public GetConfigRequest() { }
       
		public override void OnSend(BinaryWriter writer)
		{
			writer.Write(0xc4f9186b); // GetCOnfig
		}

		public override void OnResponse(BinaryReader reader)
		{
            var code = reader.ReadUInt32(); // 0x7dae33e0

            configuration = TL.Parse<Config>(reader);
		}

		public override void OnException(Exception exception)
		{
			throw new NotImplementedException();
		}

		public override bool Confirmed => true;
		public override bool Responded { get; }
	}
}
