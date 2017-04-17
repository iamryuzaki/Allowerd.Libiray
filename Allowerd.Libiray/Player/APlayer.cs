using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Allowerd.Libiray.Player
{
    public class APlayer
    {
        public static BasePlayer Create(string displayname, Vector3 position, Quaternion rotation, float min_hp = 100, float max_hp = 100, ulong steamID = 1L)
        {
            BasePlayer result = GameManager.server.CreatePrefab("assets/prefabs/player/player.prefab", position, rotation, true).GetComponent<BasePlayer>();
            result.displayName = displayname;
            result.userID = steamID;
            result.UserIDString = steamID.ToString();
            result.Spawn();
            result.health = UnityEngine.Random.Range(min_hp, max_hp);
            return result;
        }

        public static void GiveItem(BasePlayer player, int itemid, Inventory.AInventory.ETypeInventory type = Inventory.AInventory.ETypeInventory.Main, int min_condition = 100, int max_condition = 100, int min_count = 1, int max_count = 1, int min_ammo = 0, int max_ammo = 1)
        {
            Item item = ItemManager.CreateByItemID(itemid, UnityEngine.Random.Range(min_count, max_count));
            item.condition = UnityEngine.Random.Range((item.maxCondition / 100 * min_condition), (item.maxCondition / 100 * max_condition));
            BaseProjectile weapon = item.GetHeldEntity() as BaseProjectile;
            if (weapon != null)
                weapon.primaryMagazine.contents = UnityEngine.Random.Range(min_ammo, max_ammo);
            switch (type)
            {
                case Inventory.AInventory.ETypeInventory.Main:
                    player.inventory.GiveItem(item, player.inventory.containerMain);
                    break;
                case Inventory.AInventory.ETypeInventory.Belt:
                    player.inventory.GiveItem(item, player.inventory.containerBelt);
                    break;
                case Inventory.AInventory.ETypeInventory.Wear:
                    player.inventory.GiveItem(item, player.inventory.containerWear);
                    break;
            }
        }
    }
}
