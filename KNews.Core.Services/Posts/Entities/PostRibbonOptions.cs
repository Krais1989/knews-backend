using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Posts.Entities
{
    /// <summary>
    /// Фильтр:
    ///     Автор
    ///     Сообщества
    ///     Хештеги
    /// Сортировка: 
    ///     Дата
    ///     Популярность
    /// </summary>
    public class PostRibbonOptions
    {
        public enum ESort
        {
            ByDate,
            ByPopularity
        }

        public List<long> Communities { get; set; }
        public List<long> Authors { get; set; }
        public List<string> Hashtags { get; set; }

        public ESort Sort { get; set; }
    }
}
