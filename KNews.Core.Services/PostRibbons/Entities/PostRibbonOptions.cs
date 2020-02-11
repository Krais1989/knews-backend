using System.Collections.Generic;

namespace KNews.Core.Services.PostRibbons.Entities
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

        public List<long> ExcludeCommunities { get; set; }
        public List<long> ExcludeAuthors { get; set; }
        public List<string> ExcludeHashtags { get; set; }

        public ESort Sort { get; set; }
    }
}
