using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using tibs.stem.Friendships.Dto;

namespace tibs.stem.Chat.Dto
{
    public class GetUserChatFriendsWithSettingsOutput
    {
        public DateTime ServerTime { get; set; }
        
        public List<FriendDto> Friends { get; set; }

        public GetUserChatFriendsWithSettingsOutput()
        {
            Friends = new EditableList<FriendDto>();
        }
    }
}