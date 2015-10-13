using System;
using System.Collections.Generic;
using System.IO;
using TLSharp.Core.MTProto;
using TLSharp.Core.Utils;

namespace TLSharp.Core.Requests
{
	public class ContactsSearchRequest : MTProtoRequest
	{
		private string _q;
        private int _limit;

        public List<ContactFound> results;
        public List<User> users;


		public ContactsSearchRequest(string q, int limit)
		{
			_q = q;
            _limit = limit;
		}

		public override void OnSend(BinaryWriter writer)
		{
			writer.Write(0x11f812d8); // Contacts_Search

            Serializers.String.write(writer, _q);
            writer.Write(_limit);
		}

		public override void OnResponse(BinaryReader reader)
		{
            var code = reader.ReadUInt32(); // 0x566000e

            //contactfound = TL.Parse<contacts_Found>(reader);

            var result = reader.ReadInt32(); // vector code
            int ContactFound_len = reader.ReadInt32();
            this.results = new List<ContactFound>(ContactFound_len);
            for (int ContactFound_index = 0; ContactFound_index < ContactFound_len; ContactFound_index++)
            {
                ContactFound ContactFound_element;
                ContactFound_element = TL.Parse<ContactFound>(reader);
                this.results.Add(ContactFound_element);
            }

            reader.ReadInt32(); // vector code
            int users_len = reader.ReadInt32();
            this.users = new List<User>(users_len);
            for (int users_index = 0; users_index < users_len; users_index++)
            {
                User users_element;
                users_element = TL.Parse<User>(reader);
                this.users.Add(users_element);
            }
		}

		public override void OnException(Exception exception)
		{
			throw new NotImplementedException();
		}

		public override bool Confirmed => true;
		public override bool Responded { get; }
	}
}
