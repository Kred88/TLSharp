using System;
using System.Collections.Generic;
using System.IO;
using TLSharp.Core.MTProto;
using TLSharp.Core.Utils;

namespace TLSharp.Core.Requests
{
	public class CreateChatRequest : MTProtoRequest
	{
		private List<InputUser> _users;
        private string _title;

        public messages_StatedMessage statedmessage;


		public CreateChatRequest(List<InputUser> users, string title)
		{
			_users = users;
            _title = title;
		}

		public override void OnSend(BinaryWriter writer)
		{
			writer.Write(0x419d9aee); // CreateChat

            writer.Write(0x1cb5c415); // Vector
			writer.Write(_users.Count);
			foreach (InputUser user in _users)
			{
				user.Write(writer);
			}

            Serializers.String.write(writer, _title);
		}

		public override void OnResponse(BinaryReader reader)
		{
            var code = reader.ReadUInt32(); // 0xd07ae726

            statedmessage = TL.Parse<messages_StatedMessage>(reader);
		}

		public override void OnException(Exception exception)
		{
			throw new NotImplementedException();
		}

		public override bool Confirmed => true;
		public override bool Responded { get; }
	}
}
