using System;
using System.IO;
using TLSharp.Core.MTProto;
using TLSharp.Core.Utils;

namespace TLSharp.Core.Requests
{
	public class GetFullUserRequest : MTProtoRequest
	{
		private InputUser _id;

        public UserFull userfull;

        //public User user;
        //public contacts_Link link;
        //public Photo profile_photo;
        //public PeerNotifySettings notify_settings;
        //public bool blocked;
        //public string real_first_name;
        //public string real_last_name;


		public GetFullUserRequest(InputUser id)
		{
			_id = id;
		}

		public override void OnSend(BinaryWriter writer)
		{
			writer.Write(0xca30a5b1);
			_id.Write(writer);
		}

		public override void OnResponse(BinaryReader reader)
		{
            var boolTrue = 0x997275b5;

			var code = reader.ReadUInt32(); // 0x771095da

            userfull = TL.Parse<UserFull>(reader);

            //user = TL.Parse<User>(reader);
            //link = TL.Parse<contacts_Link>(reader);
            //profile_photo = TL.Parse<Photo>(reader);
            //notify_settings = TL.Parse<PeerNotifySettings>(reader);
            //
            //blocked = reader.ReadUInt32() == boolTrue;
            //real_first_name = Serializers.String.read(reader);
            //real_last_name = Serializers.String.read(reader);
		}

		public override void OnException(Exception exception)
		{
			throw new NotImplementedException();
		}

		public override bool Confirmed => true;
		public override bool Responded { get; }
	}
}
