using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace DungeonCrawler
{
    
    
    class Item
    {


        private int antalItems;    
        private string itemName;
        private static string[] itemNames = new string[] { "Tornado" ,"Potion" ,"Sten" };
        public static List<Item> itemList = new List<Item>();

        
        //Item counstructor
        public Item(int newAntalItems, string newItemName) 
        {
            antalItems = newAntalItems;
            itemName = newItemName;
        }

        public int AntalItems
        {
            get { return antalItems; }
            set { antalItems = value; }
        }
     
        //metode der genererer items for spilleren at starte med
        public static void GenerateItems()
        {
            foreach (string itemName in itemNames)
            {
                itemList.Add(new Item(5, itemName));

            }
        }

        //metode der bruger items og fjerner items fra spillerens inventar
        public void UseItem(string usedName, Player player)
        {
            
            foreach (Item i in itemList)
            {
                
                if (i.itemName == usedName && i.antalItems > 0 && !player.potionBuff)
                {

                    i.antalItems--;

                    if(usedName == "Potion")
                    {
                        player.potionBuff = true;
                    }
                    
                }
            }
        }
    }
}
