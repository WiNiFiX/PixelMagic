//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace ShadowMagic.Helpers
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Item
    {
        public Item(int itemId, string itemName, int internalItemNo)
        {
            InternalItemNo = internalItemNo;

            ItemId = itemId;
            ItemName = itemName.Replace("\r", "").Replace("\n", "");
        }

        public int InternalItemNo { get; }

        public int ItemId { get; }

        public string ItemName { get; }

        public string KeyBind { get; internal set; }

        public WoW.Keys Key => toKey(KeyBind);

        private static WoW.Keys toKey(string keystr)
        {
            return (WoW.Keys) Enum.Parse(typeof(WoW.Keys), keystr);
        }
    }
}