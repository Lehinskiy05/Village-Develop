using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Develop.Model
{
    public enum Resources
    {
        Coin,
        Wood,
        Board,
        Water,
        Chair,
        Wheat,
        Flour,
        Bread,
        Grape,
        Wine,
        Nothing,
    }

    public static class ResourcesExpression
    {
        public static string ToFrendlyString(this Resources resource)
        {
            Dictionary<Resources, string> ToFrendly = new()
            {
                { Resources.Coin, "Монеты" },
                { Resources.Wood, "Дерево" },
                { Resources.Board, "Доски" },
                { Resources.Water, "Вода" },
                { Resources.Chair, "Стулья" },
                { Resources.Wheat, "Пшеница" },
                { Resources.Flour, "Мука" },
                { Resources.Bread, "Хлеб" },
                { Resources.Grape, "Виноград" },
                { Resources.Wine, "Вино" },
                { Resources.Nothing, "Ничего" },
            };
            return ToFrendly[resource];
        }
    }
}
