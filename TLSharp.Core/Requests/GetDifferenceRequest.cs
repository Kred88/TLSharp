using System;
using System.Collections.Generic;
using System.IO;
using TLSharp.Core.MTProto;
using TLSharp.Core.Utils;

namespace TLSharp.Core.Requests
{
	public class GetDifferenceRequest : MTProtoRequest
	{

        //updates.getDifference#a041495 pts:int date:int qts:int = updates.Difference;
        private int _pts;
        private int _date;
        private int _qts;

        public updates_Difference differences;


		public GetDifferenceRequest(int pts, int date, int qts)
		{
			_pts = pts;
            _date = date;
            _qts = qts;
        }

		public override void OnSend(BinaryWriter writer)
		{
			writer.Write(0xa041495); // getDifference

            writer.Write(_pts);
            writer.Write(_date);
            writer.Write(_qts);
        }

		public override void OnResponse(BinaryReader reader)
		{
            var code = reader.ReadUInt32(); // 0x00f49ca0

            differences = TL.Parse<updates_Difference>(reader);
		}

		public override void OnException(Exception exception)
		{
			throw new NotImplementedException();
		}

		public override bool Confirmed => true;
		public override bool Responded { get; }
	}
}
