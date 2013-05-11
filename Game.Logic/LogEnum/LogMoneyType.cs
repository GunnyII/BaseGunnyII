using System;

namespace Game.Logic.LogEnum
{
  
    public enum LogMoneyType
    {
        Auction = 1,
        Auction_Update = 0x65,
        Award = 8,
        Award_Answer = 0x324,
        Award_BossDrop = 0x325,
        Award_Daily = 0x321,
        Award_Drop = 0x323,
        Award_Quest = 0x322,
        Award_TakeCard = 0x326,
        Box = 9,
        Box_Open = 0x385,
        Charge = 7,
        Charge_RMB = 0x2bd,
        Consortia = 5,
        Consortia_Rich = 0x19c,
        DismountGem = 0x25b,
        Game = 10,
        Game_Boos = 0x3e9,
        Game_Dispatches = 0x3ee,
        Game_Other = 0x3ec,
        Game_PaymentTakeCard = 0x3ea,
        Game_Shoot = 0x3ed,
        Game_TryAgain = 0x3eb,
        Item = 6,
        Item_Color = 0x25a,
        Item_Move = 0x259,
        Mail = 2,
        Mail_Money = 0xc9,
        Mail_Pay = 0xca,
        Mail_Send = 0xcb,
        Marry = 4,
        Marry_Flower = 0x198,
        Marry_Follow = 0x194,
        Marry_Gift = 0x193,
        Marry_Hymeneal = 410,
        Marry_Room = 0x196,
        Marry_RoomAdd = 0x197,
        Marry_Spark = 0x191,
        Marry_Stage = 0x192,
        Marry_Unmarry = 0x195,
        MoneyToGold = 0x132,
        Shop = 3,
        Shop_Buy = 0x12d,
        Shop_BuySale = 0x131,
        Shop_Card = 0x12f,
        Shop_Continue = 0x12e,
        Shop_Present = 0x130
    }
}

