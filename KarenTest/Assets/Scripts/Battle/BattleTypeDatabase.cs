using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class BattleTypeDatabase
{
    public static BattleType FlowerType = new BattleType("Flower", FruitType, VeggieType);
    public static BattleType FruitType = new BattleType("Fruit", VeggieType, FlowerType);
    public static BattleType VeggieType = new BattleType("Veggie", FlowerType, FruitType);
}
